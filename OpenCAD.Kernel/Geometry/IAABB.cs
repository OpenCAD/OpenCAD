using System.Collections.Generic;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface IAABB
    {
        Vect3 Min { get; }
        Vect3 Max { get; }
        Vect3 Center { get; }
        Vect3 Extent { get; }

    }

    public class AABB : IAABB
    {
        public Vect3 Min { get; private set; }
        public Vect3 Max { get; private set; }
        public Vect3 Center { get; private set; }
        public Vect3 Extent { get; private set; }

        public AABB(Vect3 min, Vect3 max)
        {
            Center = (min + max) * 0.5;
            Min = min;
            Max = max;
            Extent = Max - Min;
        }

        public AABB(Vect3 center, double size)
        {
            Center = center;
            Min = center - new Vect3(size / 2.0, size / 2.0, size / 2.0);
            Max = center + new Vect3(size / 2.0, size / 2.0, size / 2.0);
            Extent = new Vect3(size, size, size);
        }

        //public AABB(Vect3 center, Vect3 extent)
        //{
        //    Center = center;
        //    Min = center - new Vect3(extent.X / 2.0, extent.Y / 2.0, extent.Z / 2.0);
        //    Max = center + new Vect3(extent.X / 2.0, extent.Y / 2.0, extent.Z / 2.0);
        //    Extent = extent;
        //}
    }
}
