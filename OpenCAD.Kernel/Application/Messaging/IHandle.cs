namespace OpenCAD.Kernel.Application.Messaging
{
    public interface IHandle
    {
         
    }

    public interface IHandle<in TMessage> : IHandle where TMessage : class,IMessage
    {
        void Handle(TMessage message);
    }

    public interface IMessage
    {
        
    }
}