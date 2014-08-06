using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCAD.Kernel.Application.Messaging;
using OpenCAD.Kernel.Application.Messaging.Messages;

namespace OpenCAD.Kernel.Graphics.Window
{
    public interface IWindow:IDisposable
    {
        Guid Guid { get; }
        void Run(Size size);
        void Resize(Size size);

    }

    public abstract class BaseWindow:IWindow
    {
        public Guid Guid { get; protected set; }
        protected BaseWindow()
        {
            Guid = Guid.NewGuid();
        }


        public abstract void Dispose();

        public abstract void Run(Size size);
        public abstract void Resize(Size size);
    }


    public interface IWindowManager:IHandle<ResizeRequestMessage>
    {
        List<IWindow> Windows { get; }
        IWindow Create();

    }

    public abstract class BaseWindowManager:IWindowManager
    {
        public List<IWindow> Windows { get; protected set; }

        protected BaseWindowManager()
        {
            Windows = new List<IWindow>();

        }

        public abstract IWindow Create();

        public void Handle(ResizeRequestMessage message)
        {
            message.Window.Resize(new Size(message.Width, message.Height));
        }
    }

    

    public class WindowManager:BaseWindowManager
    {
        private readonly Func<IWindow> _windowBuilder;

        public WindowManager(Func<IWindow> windowBuilder)
        {
            _windowBuilder = windowBuilder;
        }

        public override IWindow Create()
        {
            var window = _windowBuilder();
            Windows.Add(window);
            new Thread(() => window.Run(new Size(800, 600))).Start();
            return window;
        }
    }
}
