using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Remoting.Messaging;

namespace OpenCAD.Kernel.Application.Messaging
{
    public interface IMessageAggregator
    {
        IObservable<IMessage> Messages { get; }
        void Add(IMessage message);
        //void Subscribe(IHandle handle);
    }

    public class MessageAggregator : IMessageAggregator
    {
        private readonly Subject<IMessage> _subject;
        public IObservable<IMessage> Messages { get; private set; }

        public MessageAggregator()
        {
            _subject = new Subject<IMessage>();
            Messages = _subject.AsObservable();
        }

        public void Add(IMessage message)
        {
            _subject.OnNext(message);
        }

    }
}
