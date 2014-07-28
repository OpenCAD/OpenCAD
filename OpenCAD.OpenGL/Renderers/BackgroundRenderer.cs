using System;
using System.Collections.Generic;
using OpenCAD.Kernel;
using OpenCAD.Kernel.Graphics.Backgrounds;
using OpenCAD.OpenGL.Buffers;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL.Renderers
{
    public class BackgroundRenderer
    {
        private readonly VAO _vao;
        private readonly ShaderProgram _program;

        public BackgroundRenderer(IBackground background)
        {
            _program = new ShaderProgram("Shaders/Background.vert", "Shaders/Background.frag");
            _vao = new VAO();
            var flatBuffer = new VBO();
            using (Bind.These(_vao, flatBuffer))
            {
                var data = new List<float>();
                data.AddRange(new float[] { -1, -1 });
                data.AddRange(background.BottomLeft.ToFloatArray());
                data.AddRange(new float[] { 1, -1 });
                data.AddRange(background.BottomRight.ToFloatArray());
                data.AddRange(new float[] { 1, 1 });
                data.AddRange(background.TopRight.ToFloatArray());
                data.AddRange(new float[] { -1, 1 });
                data.AddRange(background.TopLeft.ToFloatArray());
                var flatData = data.ToArray();
                flatBuffer.Update(flatData, flatData.Length * sizeof(float));
                const int stride = sizeof(float) * 6;

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, stride, new IntPtr(0));
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, stride, new IntPtr(sizeof(float) * 2));
            }

        }
        public void Render()
        {
            using (new Bind(_program))
            using (new Bind(_vao))
            {
                GL.Disable(EnableCap.DepthTest);
                GL.DrawArrays(BeginMode.Quads, 0, 4);
                GL.Enable(EnableCap.DepthTest);
            }
        }

    }
}
