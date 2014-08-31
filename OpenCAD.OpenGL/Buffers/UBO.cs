using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Graphics;
using OpenCAD.Kernel.Maths;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL.Buffers
{
    public class UBO<T> : IBindable
    {

        private readonly string _blockName;
        private readonly int _location;
        private readonly int _size;
        readonly uint _handle;

        public UBO(string blockName, int location, int size)
        {
            _blockName = blockName;
            _location = location;
            _size = size;
            GL.GenBuffers(1, out _handle);
            using (new Bind(this))
            {
                GL.BufferData(BufferTarget.UniformBuffer, (IntPtr)(_size), (IntPtr)(null), BufferUsageHint.StreamDraw);
                GL.BindBufferRange(BufferTarget.UniformBuffer, (uint) _location, _handle, (IntPtr)0, (IntPtr)_size);
            }

        }

        protected UBO(string blockName, int location)
            : this(blockName, location, Marshal.SizeOf(default(T)))
        {

        }

        public void BindTo(ShaderProgram program)
        {
            GL.UniformBlockBinding(program.Handle, GL.GetUniformBlockIndex(program.Handle, _blockName), _location);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, _handle);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.UniformBuffer, 0);
        }
    }

    //public class CameraUBO : UBO<CameraUBO.CameraData>
    //{

    //    public struct CameraData
    //    {
    //        public Mat4 MVP;
    //        public Mat4 Model;
    //        public Mat4 View;
    //        public Mat4 Projection;
    //        public Mat4 NormalMatrix;
    //    }

    //    public CameraUBO()
    //        : base("Camera", 0)
    //    {

    //    }

    //    public void Update(ICamera camera)
    //    {

    //        var normal = (camera.Model * camera.View).ToMatrix4();
    //        normal.Invert();
    //        normal.Transpose();
    //        Data = new CameraData
    //        {
    //            MVP = camera.MVP.ToMatrix4(),
    //            Model = camera.Model.ToMatrix4(),
    //            View = camera.View.ToMatrix4(),
    //            Projection = camera.Projection.ToMatrix4(),
    //            NormalMatrix = normal
    //        };
    //        Update();
    //    }
    //}
}
