using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Graphics;
using OpenCAD.Kernel.Modelling;

namespace OpenCAD.Kernel.Application
{
    public interface IScene
    {
        ICamera Camera { get; }
        IModel Model { get; }
    }

    public class Scene : IScene
    {
        public ICamera Camera { get; private set; }
        public IModel Model { get; private set; }
        public Scene(IModel model, ICamera camera)
        {
            Model = model;
            Camera = camera;
        }
    }
}
