using System;
using System.IO;
using OpenCAD.Kernel.Application.Messaging.Messages;
using OpenCAD.Kernel.Graphics.GUI;
using OpenCAD.Kernel.Graphics.Window;
using Pencil.Gaming;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL
{
    public class OpenGLWindow:IWindow
    {
        private readonly IGUI _gui;
        private GlfwWindowPtr _window;
        public Guid Guid { get; protected set; }

        public OpenGLWindow(IGUI gui)
        {
            _gui = gui;
            Guid = Guid.NewGuid();
        }

        public void Dispose()
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

        public void Run()
        {
            Glfw.Init();

            // Create GLFW window
            _window = Glfw.CreateWindow(800, 600, "OpenCAD", GlfwMonitorPtr.Null, GlfwWindowPtr.Null);
            Glfw.SetWindowSizeCallback(_window, (wnd, width, height) => GL.Viewport(0, 0, width, height));
            // Enable the OpenGL context for the current window
            Glfw.MakeContextCurrent(_window);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.VertexArray);





            var vertexPositions = new[] { 0.75f, 0.75f, 0.0f, 1.0f, 0.75f, -0.75f, 0.0f, 1.0f, -0.75f, -0.75f, 0.0f, 1.0f, };

            uint positionBufferObject;
            GL.GenBuffers(1, out positionBufferObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(vertexPositions.Length * sizeof(float)), vertexPositions, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);


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
                    Console.WriteLine("Dirt");
                    var t = _gui.Data;
                    // GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 2, 2, 0, PixelFormat.Rgb, PixelType.Byte, gui.Data);
                }
                // Set OpenGL clear colour to red
                GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);

                // Clear the screen
                GL.Clear(ClearBufferMask.ColorBufferBit);

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

        public void Handle(ResizeRequestMessage message)
        {
            if(message.Window.Guid != Guid) return;
            Glfw.SetWindowSize(_window, message.Width, message.Height);
        }
    }
}
