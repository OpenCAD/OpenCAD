using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Awesomium.Core;
using Pencil.Gaming;
using Pencil.Gaming.Graphics;
namespace OpenCAD.GraphicsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WebCore.Initialize(new WebConfig()
            {
                LogPath = Environment.CurrentDirectory + "/awesomium.log",
                LogLevel = LogLevel.Verbose,

            });
            WebCore.CreateWebSession(new WebPreferences() { CustomCSS = "::-webkit-scrollbar { visibility: hidden; }" });
            var webView = WebCore.CreateWebView(800, 600);
            webView.IsTransparent = true;
            webView.Source = new Uri("http://google.com");
            webView.LoadingFrameComplete += (s, e) =>
            {
                Console.WriteLine(String.Format("Frame Loaded: {0}", e.FrameId));

                // The main frame usually finishes loading last for a given page load.
                if (!e.IsMainFrame)
                    return;

                // Print some more information.
                Console.WriteLine(String.Format("Page Title: {0}", webView.Title));
                Console.WriteLine(String.Format("Loaded URL: {0}", webView.Source));

                // Take snapshots of the page.
                //TakeSnapshots((WebView)s);
            };

            WebCore.Run();
            // Initialize GLFW system
            Glfw.Init();

            // Create GLFW window
            var window = Glfw.CreateWindow(800, 600, "OpenCAD", GlfwMonitorPtr.Null, GlfwWindowPtr.Null);

            // Enable the OpenGL context for the current window
            Glfw.MakeContextCurrent(window);

            while (!Glfw.WindowShouldClose(window))
            {
                // Poll GLFW window events
                Glfw.PollEvents();

                // If you press escape the window will close
                if (Glfw.GetKey(window, Key.Escape))
                {
                    Glfw.SetWindowShouldClose(window, true);
                }

                // Set OpenGL clear colour to red
                GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);

                // Clear the screen
                GL.Clear(ClearBufferMask.ColorBufferBit);


                // Swap the front and back buffer, displaying the scene
                Glfw.SwapBuffers(window);
            }

            // Finally, clean up Glfw, and close the window
            Glfw.Terminate();
        }
    }
}
