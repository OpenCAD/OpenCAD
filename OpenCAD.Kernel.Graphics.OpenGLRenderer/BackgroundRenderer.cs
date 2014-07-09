using System;
using System.Collections.Generic;
using OpenCAD.Kernel.Graphics.Backgrounds;
using OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers;
using OpenCAD.Kernel.Maths;
using SharpGL;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer
{
    public class BackgroundRenderer
    {
        private readonly OpenGL _gl;
        private readonly VAO _vao;

        private readonly ShaderBindable _program;

        public BackgroundRenderer(OpenGL gl, IBackground background)
        {
            _gl = gl;
            _program = new ShaderBindable(gl, "Shaders/Background.vert", "Shaders/Background.frag");

            _vao = new VAO(gl);
            var flatBuffer = new VBO(gl);
            using (new Bind(_vao))
            using (new Bind(flatBuffer))
            {
                var data = new List<float>();
                data.AddRange(new float[] { -1, -1 });
                data.AddRange(background.BottomLeft.ToFloatArray());
                data.AddRange(new float[] {  1, -1 });
                data.AddRange(background.BottomRight.ToFloatArray());
                data.AddRange(new float[] { 1,  1 });
                data.AddRange(background.TopRight.ToFloatArray());

                data.AddRange(new float[] { -1, 1 });
                data.AddRange(background.TopLeft.ToFloatArray());

                var flatData = data.ToArray();

                flatBuffer.Update(flatData, flatData.Length * sizeof(float));

                const int stride = sizeof(float) * 6;

                gl.EnableVertexAttribArray(0);
                gl.VertexAttribPointer(0, 2, OpenGL.GL_FLOAT, false, stride, new IntPtr(0));

                gl.EnableVertexAttribArray(1);
                gl.VertexAttribPointer(1, 4, OpenGL.GL_FLOAT, false, stride, new IntPtr(sizeof(float) * 2));

                gl.BindVertexArray(0);
            }
        }


        public void Render()
        {
            using (new Bind(_program))
            using (new Bind(_vao))
            {
                _gl.Disable(OpenGL.GL_DEPTH_TEST);
                _gl.DrawArrays(OpenGL.GL_QUADS, 0, 4);
                _gl.Enable(OpenGL.GL_DEPTH_TEST);
            }
        }
    }
}