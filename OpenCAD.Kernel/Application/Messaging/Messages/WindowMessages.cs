using OpenCAD.Kernel.Graphics.Window;

namespace OpenCAD.Kernel.Application.Messaging.Messages
{
    public abstract class BaseWindowMessage:IMessage
    {
        public IWindow Window { get; protected set; }

        protected BaseWindowMessage(IWindow window)
        {
            Window = window;
        }
    }
    public class ResizeRequestMessage : BaseWindowMessage
    {
        public int Width { get; private set; } 
        public int Height { get; private set; }

        public ResizeRequestMessage(IWindow window, int width, int height)
            :base(window)
        {
            Width = width;
            Height = height;
        }
    }


}