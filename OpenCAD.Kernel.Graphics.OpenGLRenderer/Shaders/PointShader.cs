using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Maths;
using SharpGL;
using SharpGL.SceneGraph.Shaders;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Shaders
{
    public class PointShader : ShaderBindable
    {
        private readonly int _mvpLocation;

        public Mat4 MVP
        {
            set
            {
                _gl.UniformMatrix4(_mvpLocation, 1, false, value.ToColumnMajorArrayFloat());
            }
        }

        public PointShader(OpenGL gl)
            : base(gl)
        {

            _program = new ShaderProgram();
            var vertex = new VertexShader();
            var fragment = new FragmentShader();

            vertex.CreateInContext(gl);
            vertex.SetSource(File.ReadAllText("Shaders/Point.vert"));
            vertex.Compile();

            fragment.CreateInContext(gl);
            fragment.SetSource(File.ReadAllText("Shaders/Point.frag"));
            fragment.Compile();

            _program.CreateInContext(gl);
            _program.AttachShader(vertex);
            _program.AttachShader(fragment);
            _program.Link();

            Debug.WriteLine(_program.InfoLog);
            foreach (var attachedShader in _program.AttachedShaders)
            {
                Debug.WriteLine(attachedShader.InfoLog);
            }

            _mvpLocation = gl.GetUniformLocation(_program.ProgramObject, "MVP");
        }
    }
}
