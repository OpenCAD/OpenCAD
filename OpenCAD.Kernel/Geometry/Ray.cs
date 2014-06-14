using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface IRay:IGeometry
    {
        Vect3 Start { get; }
        Vect3 Direction { get; }
    }

    public class Ray : IRay
    {
        public Vect3 Start { get; private set; }
        public Vect3 Direction { get; private set; }

        public Ray()
        {
            
        }
    }
}