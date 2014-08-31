using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Awesomium.Core;
using Awesomium.Core.Data;
using Newtonsoft.Json;
using OpenCAD.Kernel.Application;
using OpenCAD.Kernel.Graphics.GUI;
using MouseButton = OpenCAD.Kernel.Application.MouseButton;

namespace OpenCAD.Awesomium
{
    public class AwesomiumGUIManager : BaseGUIManager
    {
        private readonly Thread _coreThread;
        private readonly ManualResetEvent _awesomiumReady = new ManualResetEvent(false);
        private WebSession _session;
        private SynchronizationContext _awesomiumContext;
        private DataSource _dataSource;
        public AwesomiumGUIManager()
        {

            _dataSource = new DirectoryDataSource("GUI");

            _coreThread = new Thread(() =>
            {
                WebCore.Started += (s, e) =>
                {
                    _awesomiumContext = SynchronizationContext.Current;
                    _awesomiumReady.Set();
                };
                _session = WebCore.CreateWebSession(new WebPreferences
                {
                    EnableGPUAcceleration = true,
                });
                WebCore.Run();
            }) { IsBackground = true };


        }


        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ILoadMessage> Load()
        {
            yield return new LoadMessage("GUI Starting");
            _coreThread.Start();
            WebCore.Initialize(new WebConfig
            {
                LogPath = Environment.CurrentDirectory + "/awesomium.log",
                LogLevel = LogLevel.Verbose,
                AssetProtocol = "cad",
                RemoteDebuggingHost = "192.168.1.201",
                RemoteDebuggingPort = 8001,
            });
            _awesomiumReady.WaitOne();
            yield return new LoadMessage("GUI Started");
        }


        public override IGUI Create(Size size, IViewModel viewModel)
        {

            return new GUI(size, viewModel, _awesomiumContext, _dataSource);
        }


    }

    class GUI:IGUI
    {
        private WebView webView;
        private BitmapSurface _surface;
        public Size Size { get; private set; }
        private byte[] _data;
        private IViewModel _viewModel;
        public GUI(Size size, IViewModel viewModel, SynchronizationContext awesomiumContext, DataSource dataSource)
        {
            _viewModel = viewModel;
            Size = size;
            _data = new Byte[Size.Width * 4 * Size.Height];
            awesomiumContext.Post(state =>
            {
                webView = WebCore.CreateWebView(Size.Width, Size.Height, WebViewType.Offscreen);

                webView.LoadingFrameFailed += webView_LoadingFrameFailed;
                webView.DocumentReady += WebViewOnDocumentReady;

                webView.IsTransparent = true;
                webView.CreateSurface += (s, e) =>
                {
                    _surface = new BitmapSurface(Size.Width, Size.Height);
                    e.Surface = _surface;
                };

                webView.WebSession.AddDataSource("gui", dataSource);
                webView.Source = viewModel.CreateUri();
                webView.FocusView();
            }, null);
        }

        private void WebViewOnDocumentReady(object s, UrlEventArgs urlEventArgs)
        {
            webView.ExecuteJavascript("var VM = {}");
            JSObject remote = webView.ExecuteJavascriptWithResult("VM");
            var sb = new StringBuilder();
            foreach (var propertyInfo in _viewModel.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(BindAttribute))))
            {
                var value = propertyInfo.GetValue(_viewModel);
                if (value is IList)
                {
                    sb.AppendLine(String.Format("VM.{0} = ko.observableArray({1});", propertyInfo.Name, JsonConvert.SerializeObject(value)));
                }
                else
                {
                    sb.AppendLine(String.Format("VM.{0} = ko.observable({1});", propertyInfo.Name, JsonConvert.SerializeObject(value)));
                }
            }

            foreach (var methodInfo in _viewModel.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => !m.IsSpecialName).Where(m => Attribute.IsDefined(m, typeof(BindAttribute))))
            {
                var info = methodInfo;
                remote.Bind(methodInfo.Name, true, (sender, args) =>
                {
                    try
                    {
                        var p = info.GetParameters();
                        if (p.Length == 0)
                        {
                            var ret = info.Invoke(_viewModel, new Object[0]);
                            args.Result = new JSValue(Map(ret));
                        }
                        else
                        {
                            if (p.Length != args.Arguments.Length) throw new TargetParameterCountException();
                            var arguments = p.Zip(args.Arguments, (parameterInfo, value) => Cast(value));
                            var ret = info.Invoke(_viewModel, arguments.ToArray());
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
            _viewModel.PropertyChanged += (sender, args) => webView.Invoke(new Action(() =>
            {
                var value = sender.GetType().GetProperty(args.PropertyName).GetValue(sender);
                webView.ExecuteJavascript(String.Format("VM.{0}({1});", args.PropertyName, value));
                //webView.ExecuteJavascript(String.Format("ko.mapping.fromJS({0}, VM);", CreateJSON(args.PropertyName, value)));




            }), null);

        }

        private string CreateJSON(string key, object value)
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName(key);
                writer.WriteValue(value);
                writer.WriteEndObject();
            }
            return sb.ToString();
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

        private void webView_LoadingFrameFailed(object sender, LoadingFrameFailedEventArgs e)
        {
            
        }

        public void Dispose()
        {

        }


        public void Update()
        {
            webView.Invoke(new Action(() =>
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

            }), null);
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
            Size = size;
            _data = new Byte[Size.Width * 4 * Size.Height];
            webView.Invoke(new Action(() =>
            {
                webView.Resize(size.Width, size.Height);

                _surface.IsDirty = true;
            }), null);
        }

        public void MouseMove(Point point)
        {
            webView.Invoke(new Action(() => webView.InjectMouseMove(point.X, point.Y)), null);
        }

        public void MouseButton(MouseButton button, bool down)
        {
            webView.Invoke(new Action(() =>
            {
                if (down)
                {
                    webView.InjectMouseDown(button.ToAwesomiumButton());
                }
                else
                {
                    webView.InjectMouseUp(button.ToAwesomiumButton());
                }
            }), null);
        }
    }
}