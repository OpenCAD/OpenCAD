using System.Diagnostics;
using System.IO;
using SharpGL;
using SharpGL.SceneGraph.Shaders;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Shaders
{
    public class FlatShader : ShaderBindable
    {
        private readonly int _offsetLocation;

        public float Offset
        {
            set
            {
                _gl.Uniform1(_offsetLocation, value);
            }
        }

        public FlatShader(OpenGL gl)
            : base(gl)
        {
            _program = new ShaderProgram();
            var vertex = new VertexShader();
            var fragment = new FragmentShader();

            vertex.CreateInContext(gl);
            vertex.SetSource(File.ReadAllText("Shaders/Flat.vert"));
            vertex.Compile();

            fragment.CreateInContext(gl);
            fragment.SetSource(File.ReadAllText("Shaders/Flat.frag"));
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
            _offsetLocation = gl.GetUniformLocation(_program.ProgramObject, "offset");
            gl.Uniform1(_offsetLocation, 0);
        }
    
    }
}