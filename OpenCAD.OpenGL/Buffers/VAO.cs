using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL.Buffers
{
    public class VAO : IBindable
    {

        private readonly uint _handle;

        public VAO()
        {
            GL.GenVertexArrays(1, out _handle);
        }

        public void Bind()
        {
            GL.BindVertexArray(_handle);
        }

        public void UnBind()
        {
            GL.BindVertexArray(0);
        }

    }
}