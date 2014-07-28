using System;
using System.Collections.Generic;
using System.Drawing;
using OpenCAD.Kernel;
using OpenCAD.Kernel.Graphics.Backgrounds;
using OpenCAD.OpenGL.Buffers;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL.Renderers
{
    public class TestRenderer
    {
        private readonly VAO _vao;
        private readonly ShaderProgram _program;
        public Texture Texture;
        public TestRenderer()
        {
            using (var bmp = new Bitmap(@"C:\temp\test.jpg"))
            {
                Texture = new Texture(bmp);
            }
            _program = new ShaderProgram("Shaders/test.vert", "Shaders/test.frag");
            _vao = new VAO();
            var flatBuffer = new VBO();
            using (Bind.These(_vao, flatBuffer))
            {
                var flatData = new float[] { -1, -1, 0, 1, 1, -1, 1, 1, 1, 1, 1, 0, -1, 1, 0, 0 };
                flatBuffer.Update(flatData, flatData.Length * sizeof(float));
                const int stride = sizeof(float) * 4;
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, stride, new IntPtr(0));
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, stride, new IntPtr(sizeof(float) * 2));
            }

        }
        public void Render()
        {
            using (new Bind(Texture,_program, _vao))
            {
                GL.Disable(EnableCap.DepthTest);
                GL.DrawArrays(BeginMode.Quads, 0, 4);
                GL.Enable(EnableCap.DepthTest);
            }
        }
    }
}