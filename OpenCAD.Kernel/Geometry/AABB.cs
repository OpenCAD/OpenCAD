using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public class AABB : IAABB
    {
        public Vect3 Min
        {
            get
            {
                return Center - HalfSize;
            }
        }

        public Vect3 Max
        {
            get
            {
                return Center + HalfSize;
            }
        }

        public Vect3 Center { get; private set; }
        public Vect3 HalfWidth { get; set; }
        public Vect3 HalfSize { get; private set; }

        public AABB(Vect3 center, double size)
        {
            Center = center;
            HalfSize = new Vect3(size / 2.0, size / 2.0, size / 2.0);
        }

        public AABB(Vect3 center, Vect3 halfWidth)
        {
            Center = center;
            HalfWidth = halfWidth;
        }
    }
}