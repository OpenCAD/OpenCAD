using System;
using System.Drawing;
using System.IO;
using OpenCAD.Kernel.Application.Messaging.Messages;
using OpenCAD.Kernel.Graphics.Backgrounds;
using OpenCAD.Kernel.Graphics.GUI;
using OpenCAD.Kernel.Graphics.Window;
using OpenCAD.OpenGL.Renderers;
using Pencil.Gaming;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL
{
    public class OpenGLWindow : BaseWindow
    {
        private readonly IGUI _gui;
        private GlfwWindowPtr _window;

        public OpenGLWindow(IGUI gui)
        {
            _gui = gui;

        }

        public override void Dispose()
        {

            //var vertices = new float[] { 0.75f, 0.75f, 0.0f };
            //uint vbo;

            //GL.GenBuffers(1, out vbo);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            //GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(vertices.Length * sizeof(float)), vertices, BufferUsageHint.StaticDraw);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //         GL.BindTexture(TextureTarget.Texture2D, image);
            //       GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(vertices.Length * sizeof(float)), vertices, BufferUsageHint.StaticDraw);

            //int tex;
            //GL.GenTextures(1,out tex);
            //GL.BindTexture(TextureTarget.Texture2D, tex);


            // Create and compile the vertex shader
            //var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            //int length = 0;
            //GL.ShaderSource(vertexShader,1,File.ReadAllLines("Shaders/test.vert"),ref length);
            //GL.CompileShader(vertexShader);

            //var geoShader = GL.CreateShader(ShaderType.GeometryShader);
            //int length2 = 0;
            //GL.ShaderSource(geoShader, 1, File.ReadAllLines("Shaders/test.geom"), ref length2);
            //GL.CompileShader(geoShader);

            //var shaderProgram = GL.CreateProgram();
            //GL.AttachShader(shaderProgram, vertexShader);
            //GL.AttachShader(shaderProgram, geoShader);

            //GL.LinkProgram(shaderProgram);
            //GL.UseProgram(shaderProgram);


            //GL.DrawElements(BeginMode.Triangles, 1, DrawElementsType.UnsignedByte, IntPtr.Zero);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            //GL.DrawArrays(BeginMode.Points, 0, 1);
        }

        public override void Run(Size size)
        {
            Glfw.Init();

            // Create GLFW window
            _window = Glfw.CreateWindow(size.Width, size.Height, "OpenCAD", GlfwMonitorPtr.Null, GlfwWindowPtr.Null);
            Glfw.SetWindowSizeCallback(_window, (wnd, newwidth, newheight) =>
            {
                GL.Viewport(0, 0, newwidth, newheight);
                _gui.Resize(new Size(newwidth, newheight));
            });

            Glfw.SetCursorPosCallback(_window, (wnd, x, y) => _gui.MouseMove(new Point((int) x,(int) y)));
            Glfw.SetMouseButtonCallback(_window, OnCbfun);
            // Enable the OpenGL context for the current window
            Glfw.MakeContextCurrent(_window);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.VertexArray);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);

            var back = new BackgroundRenderer(new GradientBackground(Color.YellowGreen, Color.Blue, Color.Plum, Color.Aquamarine));
            var test = new TestRenderer();

            var vertexPositions = new[] { 0.75f, 0.75f, 0.0f, 1.0f, 0.75f, -0.75f, 0.0f, 1.0f, -0.75f, -0.75f, 0.0f, 1.0f, };

            uint positionBufferObject;
            GL.GenBuffers(1, out positionBufferObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(vertexPositions.Length * sizeof(float)), vertexPositions, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            Console.WriteLine(GL.GetError());


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
                    test.Texture.Update(_gui);
                }
                // Set OpenGL clear colour to red
                GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);

                // Clear the screen
                GL.Clear(ClearBufferMask.ColorBufferBit);


                back.Render();
                test.Render();
                GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferObject);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
                GL.DrawArrays(BeginMode.Triangles, 0, 3);
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
