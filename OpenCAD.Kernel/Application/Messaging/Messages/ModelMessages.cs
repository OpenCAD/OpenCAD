using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Modelling;

namespace OpenCAD.Kernel.Application.Messaging.Messages
{
    public abstract class BaseModelMessage : IMessage
    {
        public IModel Model { get; protected set; }

        protected BaseModelMessage(IModel modelOld)
        {
            Model = modelOld;
        }
    }

    public class ViewModelRequest:BaseModelMessage
    {
        public ViewModelRequest(IModel model) : base(model)
        {

        }
    }
}
