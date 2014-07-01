using System.Drawing;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface IPoint:IGeometry
    {
        Vect3 Position { get; }
        Color Color { get; }
        Vect3 Normal { get; }
    }
    public class Point : IPoint
    {
        public Vect3 Position { get; private set; }
        public Color Color { get { return Color.Red; } }
        public Vect3 Normal {get { return Vect3.Zero; } }

        public Point(Vect3 position)
        {
            Position = position;
        }
    }
    public class ColorPoint : IPoint
    {
        public Vect3 Position { get; private set; }
        public Color Color { get; private set; }
        public Vect3 Normal { get { return Vect3.Zero; } }

        public ColorPoint(Vect3 position, Color color)
        {
            Position = position;
            Color = color;
        }
    }

}
