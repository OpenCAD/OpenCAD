using System;
using System.Drawing;
using OpenCAD.Kernel.Application.Windowing;
using OpenCAD.Kernel.Graphics.GUI;

namespace OpenCAD.Kernel.Application
{
    public interface IApplication:IDisposable
    {
        void Run();
    }

    public abstract class BaseApplication:IApplication
    {
        public abstract void Dispose();
        public abstract void Run();
    }

}
