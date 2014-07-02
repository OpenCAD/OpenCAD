using SharpGL;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers
{
    public class Texture : IBindable
    {
        private readonly OpenGL _gl;


        public uint Handle { get { return _texture[0]; } }
        readonly uint[] _texture = new uint[1];
        public Texture(OpenGL gl)
        {
            _gl = gl;
            _gl.GenTextures(1, _texture);
        }

        public void Bind()
        {
            _gl.BindTexture(OpenGL.GL_TEXTURE_2D, Handle);
        }

        public void UnBind()
        {
            _gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }
    }
}