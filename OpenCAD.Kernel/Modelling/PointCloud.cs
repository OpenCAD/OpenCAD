using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Modelling
{
    public class PointCloud<T>
        where T:IPoint
    {
        public IList<T> Points { get; private set; }

        public PointCloud(IList<T> points)
        {
            Points = points;
        }

        public void ConvexHull()
        {
            throw new Exception();
        }

        public IAABB CalulateAABB()
        {
            var maxX = Points.Max(p => p.Position.X);
            var maxY = Points.Max(p => p.Position.Y);
            var maxZ = Points.Max(p => p.Position.Z);
            var minX = Points.Min(p => p.Position.X);
            var minY = Points.Min(p => p.Position.Y);
            var minZ = Points.Min(p => p.Position.Z);
            return new AABB(new Vect3(minX, minY, minZ), new Vect3(maxX, maxY, maxZ));
        }
    }
}
