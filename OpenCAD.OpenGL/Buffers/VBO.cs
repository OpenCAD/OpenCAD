using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OpenCAD.OpenGL.Vertices;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL.Buffers
{
    public class VBO : IBindable
    {
        readonly uint _handle;

        public VBO()
        {
            GL.GenBuffers(1, out _handle);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
        }

        public void Update(object data, int size)
        {
            var pointer = GCHandle.Alloc(data, GCHandleType.Pinned).AddrOfPinnedObject();
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size), pointer, BufferUsageHint.StaticDraw);
        }

        public void Update(byte[] data)
        {
            var pointer = GCHandle.Alloc(data, GCHandleType.Pinned).AddrOfPinnedObject();
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(data.Length), pointer, BufferUsageHint.StaticDraw);
        }

        public void Update(IEnumerable<Vertex> vertices)
        {
            Update(vertices.SelectMany(v => v.Data).ToArray());
        }


        //public void Update<T>(IEnumerable<T> vertices)where T:IVertex
        //{

        //    var data = vertices.Select(v => v.Data).ToArray();

        //    var pointer = GCHandle.Alloc(data, GCHandleType.Pinned).AddrOfPinnedObject();
        //    _gl.BufferData(OpenGL.GL_ARRAY_BUFFER, Marshal.SizeOf(data), pointer, OpenGL.GL_STATIC_DRAW);
        //}

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}