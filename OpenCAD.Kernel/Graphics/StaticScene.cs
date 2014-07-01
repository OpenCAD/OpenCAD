using System.Collections.Generic;
using System.Drawing;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Graphics.Backgrounds;

namespace OpenCAD.Kernel.Graphics
{
    public class StaticScene : IStaticScene
    {
        public ICamera Camera { get; set; }
        public IBackground Background { get; set; }
        public List<IPoint> Points { get; private set; }

        public StaticScene()
        {
            Points = new List<IPoint>();
            Background = new SolidBackground(Color.Red);
        }
    }
}