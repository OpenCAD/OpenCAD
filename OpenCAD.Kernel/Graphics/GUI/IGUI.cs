using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using OpenCAD.Kernel.Application;

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


    public abstract class BaseGUIManager:IGUIManager
    {
        public abstract void Dispose();
        //public abstract void Run();
        public abstract IGUI Create(Size size, IViewModel viewModel);
        public abstract IEnumerable<IResult> Load();

    }

    public interface IGUIManager:ILoadable,IDisposable
    {
        //void Run();
        IGUI Create(Size size, IViewModel viewModel);
    }

    public interface ILoadable
    {
        IEnumerable<IResult> Load();
    }

    //public interface ILoadMessage
    //{
    //    string Message { get; }
    //}

    //public class LoadMessage : ILoadMessage
    //{
    //    public string Message { get; private set; }
    //    public LoadMessage(string message)
    //    {
    //        Message = message;
    //    }
    //}
}