using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface IPoint:IGeometry
    {
        Vect3 Position { get; }
    }

    public class Point : IPoint
    {
        public Vect3 Position { get; private set; }
        public Point(Vect3 position)
        {
            Position = position;
        }
    }
}
