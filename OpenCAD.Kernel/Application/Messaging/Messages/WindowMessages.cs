using System;
using OpenCAD.Kernel.Application.Windowing;

namespace OpenCAD.Kernel.Application.Messaging.Messages
{
    public abstract class BaseWindowMessage:IMessage
    {
        public IWindow Window { get; protected set; }

        protected BaseWindowMessage(IWindow window)
        {
            Window = window;
        }

        public override string ToString()
        {
            return String.Format("Window<{0}>", Window.Guid);
        }
    }
    public class ResizeRequestMessage : BaseWindowMessage
    {
        public int Width { get; private set; } 
        public int Height { get; private set; }

        public ResizeRequestMessage(IWindow window, int width, int height)
            : base(window)
        {
            Width = width;
            Height = height;
        }
    }

    public class FocusChangedMessage : BaseWindowMessage
    {
        public bool Focused { get; protected set; }

        public FocusChangedMessage(IWindow window, bool focused) 
            : base(window)
        {
            Focused = focused;
        }

        public override string ToString()
        {
            return String.Format("{0} [{1}]", base.ToString(),Focused);
        }
    }
}