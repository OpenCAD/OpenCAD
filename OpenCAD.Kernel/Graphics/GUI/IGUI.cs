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
        void Update();
        bool IsDirty { get; }
        byte[] Data { get; }
        void Resize(Size size);
        void MouseMove(Point point);
        void MouseButton(MouseButton button, bool down);


    }


    public class GUIManager:IGUIManager
    {
        public IGUI Create()
        {
            throw new NotImplementedException();
        }
    }

    public interface IGUIManager
    {
        IGUI Create();
    }
}