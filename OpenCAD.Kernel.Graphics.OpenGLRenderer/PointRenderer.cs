using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Graphics.Backgrounds;
using OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers;
using OpenCAD.Kernel.Maths;
using SharpGL;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer
{
    public class PointRenderer
    {
        private readonly OpenGL _gl;
        private readonly IStaticScene _scene;
        private readonly VAO _vao;
        private VBO _vbo;
        private ShaderBindable _program;

        private int count = 0;
        public PointRenderer(OpenGL gl, IStaticScene scene)
        {
            _gl = gl;
            _scene = scene;

            _program = new ShaderBindable(gl, "Shaders/Point.vert", "Shaders/Point.frag");
            _vao = new VAO(gl);
            _vbo = new VBO(gl);

            using (new Bind(_vao))
            using (new Bind(_vbo))
            {

                var data = new List<float>();
                count = scene.Points.Count;
                

                foreach (var point in scene.Points)
                {
                    data.AddRange(point.Position.ToArray().Select(d=>(float)d));
                    data.AddRange(point.Color.ToFloatArray());
                }

                var flatData = data.ToArray();

                _vbo.Update(flatData, flatData.Length * sizeof(float));

                const int stride = sizeof(float) * 7;

                gl.EnableVertexAttribArray(0);
                gl.VertexAttribPointer(0, 3, OpenGL.GL_FLOAT, false, stride, new IntPtr(0));

                gl.EnableVertexAttribArray(1);
                gl.VertexAttribPointer(1, 4, OpenGL.GL_FLOAT, false, stride, new IntPtr(sizeof(float) * 3));

                gl.BindVertexArray(0);
            }
        }


        public void Update(ICamera camera)
        {

        }

        public void Render()
        {
            using (new Bind(_program))
            using (new Bind(_vao))
            {

                //_program.MVP = _scene.Camera.MVP;
                _program.Uniforms.MVP = Mat4.Scale(0.05);// * Mat4.Translate(-0.2, -0.2, 0);
                //_program.MVP = _scene.Camera.MVP * Mat4.Scale(5);

                _gl.PointSize(3f);
                _gl.DrawArrays(OpenGL.GL_POINTS, 0, count);
            }
        }
    }
}