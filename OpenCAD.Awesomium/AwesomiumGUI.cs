using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Linq.ObservableImpl;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Awesomium.Core;
using Awesomium.Core.Data;
using Newtonsoft.Json;
using OpenCAD.Kernel.Application;
using OpenCAD.Kernel.Graphics.GUI;
using MouseButton = OpenCAD.Kernel.Application.MouseButton;

namespace OpenCAD.Awesomium
{
    public class AwesomiumGUI<THome> : IGUI where THome:IViewModel
    {
        private Thread _thread;
        //public event NewImageEventHandler NewImageData;

        private WebView webView;

        private BitmapSurface _surface;
        private SynchronizationContext _awesomiumContext;
        private readonly ManualResetEvent _awesomiumReady = new ManualResetEvent(false);
        private byte[] _data;

        private IViewModel _viewModel;

        public AwesomiumGUI(Func<THome> homeBuilder)
        {
            _viewModel = homeBuilder();

            Size = new Size(100,100);
            _data = new Byte[Size.Width * 4 * Size.Height];
            _thread = new Thread(() =>
            {
                WebCore.Started += (s, e) =>
                {
                    _awesomiumContext = SynchronizationContext.Current;
                    _awesomiumReady.Set();
                };
                
                var session = WebCore.CreateWebSession(new WebPreferences
                {
                    //CustomCSS = "body {background:transparent}",
                    EnableGPUAcceleration = true,
                });

                //var path = ;Path.Combine(Environment.CurrentDirectory, "GUI")

                WebCore.Run();
            }){IsBackground = true};

            _thread.Start();


            
            WebCore.Initialize(new WebConfig()
            {
                LogPath = Environment.CurrentDirectory + "/awesomium.log",
                LogLevel = LogLevel.Verbose,
                AssetProtocol = "cad",
                RemoteDebuggingHost = "192.168.1.201",
                RemoteDebuggingPort = 8001,
            });
            

            _awesomiumReady.WaitOne();

            _awesomiumContext.Post(state =>
            {
                webView = WebCore.CreateWebView(Size.Width, Size.Height, WebViewType.Offscreen);

                webView.LoadingFrameFailed += webView_LoadingFrameFailed;
                
                webView.DocumentReady += (sender, args) => Bind(_viewModel);

                webView.IsTransparent = true;
                webView.CreateSurface += (s, e) =>
                {
                    _surface = new BitmapSurface(Size.Width, Size.Height);
                    e.Surface = _surface;
                };

                webView.WebSession.AddDataSource("gui", new DirectoryDataSource("GUI"));

                Load(_viewModel);
            }, null);

       
        }

        void Load(IViewModel model)
        {


            model.PropertyChanged += (sender, args) =>
            {
                
            };
            _awesomiumContext.Post(state =>
            {
                webView.Source = model.CreateUri();
                webView.FocusView();
            }, null);
        }


        void Bind(IViewModel viewModel)
        {
            if (!webView.IsLive)
                return;
            //webView.ExecuteJavascript(File.ReadAllText("GUI/js/knockout-3.1.0.js"));
            //webView.ExecuteJavascript(File.ReadAllText("GUI/js/jquery-1.11.1.min.js"));
            //webView.ExecuteJavascript(File.ReadAllText("GUI/js/bootstrap.min.js")); 
            var sb = new StringBuilder();
            webView.ExecuteJavascript("var VM = {}");
            JSObject remote = webView.ExecuteJavascriptWithResult("VM");
            foreach (var propertyInfo in viewModel.GetType().GetProperties())
            {
                var value = propertyInfo.GetValue(viewModel);
                if (value is IList)
                {
                    sb.AppendLine(String.Format("VM.{0} = ko.observableArray({1});", propertyInfo.Name, JsonConvert.SerializeObject(value)));
                }
                else
                {
                    sb.AppendLine(String.Format("VM.{0} = ko.observable({1});", propertyInfo.Name, JsonConvert.SerializeObject(value)));
                }

            }
            foreach (var methodInfo in viewModel.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => !m.IsSpecialName))
            {
                var info = methodInfo;
                remote.Bind(methodInfo.Name, true, (sender, args) =>
                {
                    try
                    {
                        var p = info.GetParameters();
                        if (p.Length == 0)
                        {
                            var ret = info.Invoke(viewModel, new Object[0]);
                            args.Result = new JSValue(Map(ret));
                        }
                        else
                        {
                            if (p.Length != args.Arguments.Length) throw new TargetParameterCountException();
                            var arguments = p.Zip(args.Arguments, (parameterInfo, value) => Cast(value));
                            var ret = info.Invoke(viewModel, arguments.ToArray());
                            args.Result = new JSValue(Map(ret));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }
            sb.AppendLine("ko.applyBindings(VM);");
            webView.ExecuteJavascript(sb.ToString());

            viewModel.PropertyChanged += (sender, args) => webView.Invoke(new Action(() =>
            {
                //if (webView.IsLive) return;
                var value = sender.GetType().GetProperty(args.PropertyName).GetValue(sender);
                webView.ExecuteJavascript(String.Format("VM.{0}({1});", args.PropertyName, value));
            }), null);

        }

        private object Cast(JSValue value)
        {
            if (value.IsBoolean) return (Boolean)value;
            if (value.IsInteger) return (int)value;
            if (value.IsDouble) return (Double)value;
            if (value.IsString) return (String)value;
            if (value.IsNull) return null;
            if (value.IsUndefined) return null;
            return null;
        }

        private JSValue Map(object obj)
        {
            if (obj is Boolean) return new JSValue((Boolean)obj);
            if (obj is int) return new JSValue((int)obj);
            if (obj is Double) return new JSValue((Double)obj);
            if (obj is String) return new JSValue((String)obj);
            return JSValue.Undefined;
        }

        private T Cast<T>(object input)
        {
            return ((T)input);
        }
        void webView_LoadingFrameFailed(object sender, LoadingFrameFailedEventArgs e)
        {
            Console.WriteLine(e.ErrorCode);
        }

        public Size Size { get; private set; }

        public void Update()
        {
            _awesomiumContext.Post(state =>
            {
                if (_surface == null || !_surface.IsDirty) return;
                unsafe
                {
                    // This part saves us from double copying everything.
                    fixed (Byte* imagePtr = _data)
                    {
                        _surface.CopyTo((IntPtr)imagePtr, _surface.Width * 4, 4, false, false);
                    }
                }
                IsDirty = true;
            }, null);
        }

        public bool IsDirty { get; private set; }

        public byte[] Data
        {
            get
            {
                IsDirty = false;
                return _data;
            }
        }

        public void Resize(Size size)
        {
            Console.WriteLine(size);
            Size = size;
            _data = new Byte[Size.Width * 4 * Size.Height];
            _awesomiumContext.Send(state =>
            {
                webView.Resize(size.Width, size.Height);
                
                _surface.IsDirty = true;
            }, null);
        }

        public void MouseMove(Point point)
        {
            _awesomiumContext.Post(state => webView.InjectMouseMove(point.X,point.Y), null);

        }

        public void MouseButton(MouseButton button, bool down)
        {
    
            _awesomiumContext.Send(state =>
            {
                if (down)
                {
                    webView.InjectMouseDown(button.ToAwesomiumButton());
                }
                else
                {
                    webView.InjectMouseUp(button.ToAwesomiumButton());
                }
            }, null);
        }

        public void Dispose()
        {

            WebCore.Shutdown();
        }
    }

    public class LocalDataSource : DataSource {
        protected override void OnRequest(DataSourceRequest request)
        {
            var content = "<h1>Hello World</h1>";
            var ptr = Marshal.StringToHGlobalUni(content);
            SendResponse(request, new DataSourceResponse() { Buffer = ptr, MimeType = "text/html" , Size = (uint) content.Length});
            Marshal.FreeHGlobal(ptr);

        }
    }
}
