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

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
    //public class VBO<T> : IBindable 
    //    where T : struct
    //{

    //    readonly uint _handle;

    //    public VBO()
    //    {
    //        GL.GenBuffers(1, out _handle);
    //    }

    //    public void Bind()
    //    {
    //        GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
    //    }

    //    public void Buffer(IEnumerable<T> data)
    //    {

    //        var array = data.ToArray();
    //        var pointer = GCHandle.Alloc(data, GCHandleType.Pinned).AddrOfPinnedObject();
    //        GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(data.Length), pointer, BufferUsageHint.StaticDraw);
    //    }

    //    public void UnBind()
    //    {
    //        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    //    }
    //}
}