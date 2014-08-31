using System;
using System.Collections.Generic;
using System.Drawing;
using OpenCAD.Kernel.Application.Messaging;
using OpenCAD.Kernel.Application.Messaging.Messages;
using OpenCAD.Kernel.Graphics.GUI;

namespace OpenCAD.Kernel.Application.Windowing
{
    public interface IWindow:IDisposable
    {
        Guid Guid { get; }
        void Run(Size size);
        void Resize(Size size);
        //void Add(Scene scene);
        //IList<IScene> Scenes { get; }
        IViewModel ViewModel { get; }
    }


    public abstract class BaseWindow:IWindow
    {
        //public void Add(Scene scene)
        //{
        //    Scenes.Add(scene);
        //    CurrentScene = scene;
        //}

        //public IList<IScene> Scenes { get; private set; }
        //public Scene CurrentScene { get; private set; }
        public Guid Guid { get; protected set; }

        public IViewModel ViewModel { get; protected set; }

        protected BaseWindow(IViewModel viewModel)
        {
            ViewModel = viewModel;
            Guid = Guid.NewGuid();
            //Scenes = new List<IScene>();
        }


        public abstract void Dispose();

        public abstract void Run(Size size);
        public abstract void Resize(Size size);

    }


    public interface IWindowManager : IDisposable, ILoadable,IHandle<ResizeRequestMessage>
    {
        List<IWindow> Windows { get; }
        IWindow Create(Size size, IViewModel viewModel);
    }
}
