using System;
using System.Drawing;
using System.IO;
using OpenCAD.Kernel.Application;
using OpenCAD.Kernel.Application.Messaging;
using OpenCAD.Kernel.Application.Messaging.Messages;
using OpenCAD.Kernel.Application.Windowing;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Graphics.Backgrounds;
using OpenCAD.Kernel.Graphics.GUI;
using OpenCAD.Kernel.Modelling;
using OpenCAD.OpenGL.Renderers;
using Pencil.Gaming;
using Pencil.Gaming.Graphics;
using MouseButton = Pencil.Gaming.MouseButton;
using Point = System.Drawing.Point;

namespace OpenCAD.OpenGL
{
    public class OpenGLWindow : BaseWindow
    {
        private readonly IGUIManager _guiManager;
        private readonly IMessageAggregator _messageAggregator;
        private IGUI _gui;
        private GlfwWindowPtr _window;
        private ModelRenderer _modelRenderer;

        public OpenGLWindow(IViewModel viewModel, IGUIManager guiManager, IMessageAggregator messageAggregator)
            :base(viewModel)
        {
            _guiManager = guiManager;
            _messageAggregator = messageAggregator;
        }

        public override void Dispose()
        {

        }

        public override void Run(Size size)
        {
            _gui = _guiManager.Create(size,ViewModel);
            Glfw.Init();
            // Create GLFW window
            _window = Glfw.CreateWindow(size.Width, size.Height, "OpenCAD", GlfwMonitorPtr.Null, GlfwWindowPtr.Null);
            Glfw.SetWindowSizeCallback(_window, (wnd, newwidth, newheight) =>
            {
                GL.Viewport(0, 0, newwidth, newheight);
                _gui.Resize(new Size(newwidth, newheight));
            });

            Glfw.SetCursorPosCallback(_window, (wnd, x, y) => _gui.MouseMove(new Point((int) x,(int) y)));
            Glfw.SetWindowFocusCallback(_window, (wnd, focus) => _messageAggregator.Add(new FocusChangedMessage(this, focus)));
            Glfw.SetMouseButtonCallback(_window, OnCbfun);
            // Enable the OpenGL context for the current window
            Glfw.MakeContextCurrent(_window);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.VertexArray);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //var back = new BackgroundRenderer(new GradientBackground(Color.YellowGreen, Color.Blue, Color.Plum, Color.Aquamarine));
            var back = new BackgroundRenderer(new SolidBackground(Color.FromArgb(35, 30, 32)));
            var gui = new GUIRenderer();
            _modelRenderer = new ModelRenderer();
            _gui.Resize(size);

            while (!Glfw.WindowShouldClose(_window))
            {
                // Poll GLFW window events
                Glfw.PollEvents();

                // If you press escape the window will close
                if (Glfw.GetKey(_window, Key.Escape))
                {
                    Glfw.SetWindowShouldClose(_window, true);
                }

                _gui.Update();
                if (_gui.IsDirty)
                {
                    Console.WriteLine("Dirty");
                    gui.Texture.Update(_gui);
                }
                // Set OpenGL clear colour to red
                GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);

                // Clear the screen
                GL.Clear(ClearBufferMask.ColorBufferBit);
                back.Render();

                var model = ViewModel as IHasScenes;
                if (model != null)
                {
                    _modelRenderer.Render(model.CurrentScene);
                }

                //if (CurrentScene != null)
                //{
                //    _modelRenderer.Render(CurrentScene);
                //}

                gui.Render();

                // Swap the front and back buffer, displaying the scene
                Glfw.SwapBuffers(_window);
            }

            // Finally, clean up Glfw, and close the window
            Glfw.Terminate();

        }

        private void OnCbfun(GlfwWindowPtr wnd, MouseButton btn, KeyAction action)
        {
            _gui.MouseButton(btn.ToButton(),action == KeyAction.Press);
        }

        public override void Resize(Size size)
        {
            Glfw.SetWindowSize(_window, size.Width, size.Height);
        }
    }
}
