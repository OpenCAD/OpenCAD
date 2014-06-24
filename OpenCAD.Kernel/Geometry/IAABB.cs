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
        IEnumerable<Vect3> Points { get; }
    }

    public class AABB : IAABB
    {
        public Vect3 Min { get; private set; }
        public Vect3 Max { get; private set; }
        public Vect3 Center { get; private set; }
        public Vect3 Extent { get; private set; }

        public IEnumerable<Vect3> Points
        {
            get
            {
                yield return new Vect3(Min.X, Min.Y, Min.Z);
                yield return new Vect3(Min.X, Min.Y, Max.Z);
                yield return new Vect3(Min.X, Max.Y, Min.Z);
                yield return new Vect3(Min.X, Max.Y, Max.Z);
                yield return new Vect3(Max.X, Min.Y, Min.Z);
                yield return new Vect3(Max.X, Min.Y, Max.Z);
                yield return new Vect3(Max.X, Max.Y, Min.Z);
                yield return new Vect3(Max.X, Max.Y, Max.Z);
            }
        }
        public AABB(Vect3 min, Vect3 max)
        {
            Min = min;
            Max = max;
            Center = (Min + Max) * 0.5;
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
