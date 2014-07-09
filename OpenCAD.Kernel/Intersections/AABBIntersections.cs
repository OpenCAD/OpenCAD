using System;
using OpenCAD.Kernel.Geometry;

namespace OpenCAD.Kernel.Intersections
{
    public static class AABBIntersections
    {
        public static bool Overlaps(this IAABB a, IAABB b)
        {
            return Math.Abs(a.Center.X - b.Center.X) < (a.HalfSize.X + b.HalfSize.X) && Math.Abs(a.Center.Y - b.Center.Y) < (a.HalfSize.Y + b.HalfSize.Y) && Math.Abs(a.Center.Z - b.Center.Z) < (a.HalfSize.Z + b.HalfSize.Z);
        }
    }
}