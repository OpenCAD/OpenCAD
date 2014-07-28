using System;
using System.Drawing;
using System.Threading;
using OpenCAD.Kernel.Graphics.Window;

namespace OpenCAD.Kernel.Graphics.GUI
{
    public interface IGUI:IDisposable
    {
        int Width { get; }
        int Height { get; }
        //byte[] Render();
        void Update();
        bool IsDirty { get; }
        byte[] Data { get; }
        void Resize(int width, int height);
        // event NewImageEventHandler NewImageData;
    }
    //public delegate void NewImageEventHandler(object sender, byte[] image);

    public interface IGUIManager
    {
        IWindow Create();
    }
}