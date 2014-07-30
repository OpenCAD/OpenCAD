using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Awesomium.Core;
using Awesomium.Core.Data;
using OpenCAD.Kernel.Graphics.GUI;
using MouseButton = OpenCAD.Kernel.Application.MouseButton;

namespace OpenCAD.Awesomium
{
    public class AwesomiumGUI : IGUI
    {
        private Thread _thread;
        //public event NewImageEventHandler NewImageData;

        private WebView webView;

        private BitmapSurface _surface;
        private SynchronizationContext _awesomiumContext;
        private readonly ManualResetEvent _awesomiumReady = new ManualResetEvent(false);
        private byte[] _data;

        public AwesomiumGUI()
        {
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
                    EnableGPUAcceleration = true
                });

                
                //var path = ;Path.Combine(Environment.CurrentDirectory, "GUI")
                

                WebCore.Run();
            }){IsBackground = true};

            _thread.Start();

            WebCore.Initialize(new WebConfig()
            {
                LogPath = Environment.CurrentDirectory + "/awesomium.log",
                LogLevel = LogLevel.Verbose,
                AssetProtocol = "cad"
            });
            

            _awesomiumReady.WaitOne();

            _awesomiumContext.Post(state =>
            {
                webView = WebCore.CreateWebView(Size.Width, Size.Height, WebViewType.Offscreen);
                webView.LoadingFrameFailed += webView_LoadingFrameFailed;

                webView.IsTransparent = true;
                webView.CreateSurface += (s, e) =>
                {
                    _surface = new BitmapSurface(Size.Width, Size.Height);
                    e.Surface = _surface;
                };
                webView.WebSession.AddDataSource("gui", new DirectoryDataSource("GUI"));
                webView.Source = new Uri("cad://gui/index.html");
                webView.FocusView();
            }, null);

       
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
