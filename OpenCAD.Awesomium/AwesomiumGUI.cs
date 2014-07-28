using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Awesomium.Core;
using OpenCAD.Kernel.Graphics.GUI;

namespace OpenCAD.Awesomium
{
    public class AwesomiumGUI : IGUI
    {
        private Thread _thread;
        public int Width { get; private set; }
        public int Height { get; private set; }

        //public event NewImageEventHandler NewImageData;

        private WebView webView;

        private BitmapSurface _surface;
        private SynchronizationContext _awesomiumContext;
        private readonly ManualResetEvent _awesomiumReady = new ManualResetEvent(false);
        private byte[] _data;

        public AwesomiumGUI()
        {
            Width = 800;
            Height = 600;

            _data = new Byte[Width * 4 * Height];
            _thread = new Thread(() =>
            {
                WebCore.Started += (s, e) =>
                {
                    _awesomiumContext = SynchronizationContext.Current;
                    _awesomiumReady.Set();
                };
                WebCore.CreateWebSession(new WebPreferences() { CustomCSS = "::-webkit-scrollbar { visibility: hidden; }" });
                WebCore.Run();
            }){IsBackground = true};

            _thread.Start();

            WebCore.Initialize(new WebConfig()
            {
                LogPath = Environment.CurrentDirectory + "/awesomium.log",
                LogLevel = LogLevel.Verbose,
            });

            _awesomiumReady.WaitOne();

            _awesomiumContext.Post(state =>
            {
                webView = WebCore.CreateWebView(Width, Height, WebViewType.Offscreen);
                webView.IsTransparent = true;
                webView.CreateSurface += (s, e) =>
                {
                    _surface = new BitmapSurface(Width, Height);
                    e.Surface = _surface;
                };
                webView.Source = new Uri("http://www.google.co.uk/");
            }, null);

       
        }

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
            private set { _data = value; }
        }

        public void Resize(int width, int height)
        {
            
        }


        //public byte[] Render()
        //{
        //    if (_surface == null) return null;

        //    //_awesomiumContext.Post(state =>
        //    //{
        //    //    if (_surface == null) return null;

        //    //}, null);
        //    //if (exit)
        //    //{
        //    //    data = null;
        //    //    return false;
        //    //}
        //    Console.WriteLine("Dirty!");

            
        //    //_awesomiumContext.Send(state =>
        //    //{
        //    //    //if (_surface.IsDirty)
        //    //    {
        //    //        unsafe
        //    //        {
        //    //            // This part saves us from double copying everything.
        //    //            fixed (Byte* imagePtr = (byte[])state)
        //    //            {
        //    //                _surface.CopyTo((IntPtr)imagePtr, _surface.Width * 4, 4, true, false);
        //    //            }
        //    //        }
        //    //    }
        //    //}, imageBytes);
        //    //return imageBytes;
        //}


        public void Dispose()
        {

            WebCore.Shutdown();
        }
    }
}
