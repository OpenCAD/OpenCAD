using System.Collections.Generic;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Graphics.Backgrounds;

namespace OpenCAD.Kernel.Graphics
{
    public interface IStaticScene
    {
        ICamera Camera { get; set; }
        IBackground Background { get; set; }
        List<IPoint> Points { get; }

    }
}