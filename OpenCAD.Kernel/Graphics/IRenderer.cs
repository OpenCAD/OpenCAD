using System;
using System.Drawing;

namespace OpenCAD.Kernel.Graphics
{
    public interface IRenderer
    {
        //string Name { get; }
        //void Load(IModel model, ICamera camera, int width, int height);
        //void Update(ICamera camera);
        //ImageSource Render();
        //void Resize(int width, int height);
    }

    public interface IStaticRenderer:IDisposable
    {
        int Width { get; }
        int Height { get; }
        
        Image Render();
    }
}