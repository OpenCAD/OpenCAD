using SharpGL;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers
{
    public class VAO : IBuffer
    {
        private readonly OpenGL _gl;
        readonly uint[] _vao = new uint[1];

        public VAO(OpenGL gl)
        {
            _gl = gl;
            _gl.GenVertexArrays(2, _vao);
        }

        public void Bind()
        {
            _gl.BindVertexArray(_vao[0]);
        }

        public void UnBind()
        {
            _gl.BindVertexArray(0);
        }
    }
}