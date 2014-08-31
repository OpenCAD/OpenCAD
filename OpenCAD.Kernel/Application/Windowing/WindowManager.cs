using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using OpenCAD.Kernel.Application.Messaging;
using OpenCAD.Kernel.Application.Messaging.Messages;
using OpenCAD.Kernel.Graphics;
using OpenCAD.Kernel.Graphics.GUI;

namespace OpenCAD.Kernel.Application.Windowing
{
    public class WindowManager : IWindowManager, IHandle<ViewModelRequest>, IHandle<FocusChangedMessage>
    {
        private readonly Func<IViewModel, IWindow> _windowBuilder;

        public List<IWindow> Windows { get; protected set; }
        private IWindow _current = null;


        public IWindow Current
        {
            get { return _current; }
        }

        public WindowManager(Func<IViewModel,IWindow> windowBuilder)
        {
            _windowBuilder = windowBuilder;
            Windows = new List<IWindow>();
                
        }

        public IWindow Create(Size size, IViewModel viewModel)
        {
            var window = _windowBuilder(viewModel);
            Windows.Add(window);
            new Thread(() => window.Run(size)).Start();
            return window;
        }

        public IEnumerable<ILoadMessage> Load()
        {
            yield return new LoadMessage("Windowing Loaded");
        }

        public void Handle(ResizeRequestMessage message)
        {
            message.Window.Resize(new Size(message.Width, message.Height));
        }

        public void Dispose()
        {
            
        }

        public void Handle(ViewModelRequest message)
        {
            //Windows.First().Add( new Scene(message.ModelOld, new OrthographicCamera()));
        }

        public void Handle(FocusChangedMessage message)
        {
            _current = message.Window;
        }


    }
}