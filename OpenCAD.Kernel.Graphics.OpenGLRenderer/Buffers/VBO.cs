using System.Runtime.InteropServices;
using SharpGL;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers
{
    public class VBO : IBuffer
    {
        private readonly OpenGL _gl;

        readonly uint[] _handle = new uint[1];

        public VBO(OpenGL gl)
        {
            _gl = gl;
            _gl.GenBuffers(1, _handle);
        }

        public void Bind()
        {
            _gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, _handle[0]);
        }

        public void Update(object data, int size)
        {
            var pointer = GCHandle.Alloc(data, GCHandleType.Pinned).AddrOfPinnedObject();
            _gl.BufferData(OpenGL.GL_ARRAY_BUFFER, size, pointer, OpenGL.GL_STATIC_DRAW);
        }

        //public void Update<T>(IEnumerable<T> vertices)where T:IVertex
        //{

        //    var data = vertices.Select(v => v.Data).ToArray();

        //    var pointer = GCHandle.Alloc(data, GCHandleType.Pinned).AddrOfPinnedObject();
        //    _gl.BufferData(OpenGL.GL_ARRAY_BUFFER, Marshal.SizeOf(data), pointer, OpenGL.GL_STATIC_DRAW);
        //}

        public void UnBind()
        {
            _gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}