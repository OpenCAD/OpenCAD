using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Intersections
{
    public static class PointIntersections
    {
        public static bool In(this IPoint p, IAABB box)
        {
            var distance = box.Center - p.Position;
            return Math.Abs(distance.X).NearlyLessThanOrEquals(box.HalfSize.X) && Math.Abs(distance.Y).NearlyLessThanOrEquals(box.HalfSize.Y) && Math.Abs(distance.Z).NearlyLessThanOrEquals(box.HalfSize.Z);
        }
    }
}
