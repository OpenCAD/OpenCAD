using SharpGL;
using SharpGL.SceneGraph.Shaders;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer
{
    public class GeometryShader : Shader
    {
        public GeometryShader()
        {
            Name = "Geometry Shader";
        }
        public override void CreateInContext(OpenGL gl)
        {
            ShaderObject = gl.CreateShader(0x8DD9);
            CurrentOpenGLContext = gl;
        }
    }
}