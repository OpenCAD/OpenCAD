using System;
using System.Drawing;
using System.Threading;
using OpenCAD.Kernel.Application;
using OpenCAD.Kernel.Graphics.Window;

namespace OpenCAD.Kernel.Graphics.GUI
{
    public interface IGUI:IDisposable
    {
        Size Size { get; }
        //byte[] Render();
        void Update();
        bool IsDirty { get; }
        byte[] Data { get; }
        void Resize(Size size);
        void MouseMove(Point point);
        void MouseButton(MouseButton button, bool down);

        // event NewImageEventHandler NewImageData;
    }
    //public delegate void NewImageEventHandler(object sender, byte[] image);

    public interface IGUIManager
    {
        IWindow Create();
    }
}