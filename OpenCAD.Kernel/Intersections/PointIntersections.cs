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
        //public static bool In(this IPoint p, IAABB box)
        //{
        //    return p.Position.X > box.Min.X && p.Position.X < box.Max.X &&
        //           p.Position.Y > box.Min.Y && p.Position.Y < box.Max.Y &&
        //           p.Position.Z > box.Min.Z && p.Position.Z < box.Max.Z;
        //}
        public static bool In(this IPoint p, IAABB box)
        {
            var distance = box.Center - p.Position;
            return Math.Abs(distance.X).NearlyLessThanOrEquals(box.HalfSize.X) &&
                   Math.Abs(distance.Y).NearlyLessThanOrEquals(box.HalfSize.Y) &&
                   Math.Abs(distance.Z).NearlyLessThanOrEquals(box.HalfSize.Z);
            // return (Math.Abs(distance.X).NearlyEquals(box.HalfSize.X) && Math.Abs(distance.Y).NearlyEquals(box.HalfSize.Y) && Math.Abs(distance.Z).NearlyEquals(box.HalfSize.Z)) || (Math.Abs(distance.X) <= box.HalfSize.X) && (Math.Abs(distance.Y) <= box.HalfSize.Y) && (Math.Abs(distance.Z) <= box.HalfSize.Z);
        }
    }
}
