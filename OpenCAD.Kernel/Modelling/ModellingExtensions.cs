using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;
using OpenCAD.Kernel.Modelling.Octree;

namespace OpenCAD.Kernel.Modelling
{
    public static class ModellingExtensions
    {
        public static void ToOctreeNode(this IPointCloud pointCloud)
        {
            var size = pointCloud.CalulateAABB().Extent.ToArray().Max();
            var node = new OctreeNode(size);
            foreach (var point in pointCloud.Points)
            {
                
            }
        }

        public static IEnumerable<Vect3> ToPoints(this IAABB aabb)
        {
            yield return new Vect3(aabb.Min.X, aabb.Min.Y, aabb.Min.Z);
            yield return new Vect3(aabb.Min.X, aabb.Min.Y, aabb.Max.Z);
            yield return new Vect3(aabb.Min.X, aabb.Max.Y, aabb.Min.Z);
            yield return new Vect3(aabb.Min.X, aabb.Max.Y, aabb.Max.Z);
            yield return new Vect3(aabb.Max.X, aabb.Min.Y, aabb.Min.Z);
            yield return new Vect3(aabb.Max.X, aabb.Min.Y, aabb.Max.Z);
            yield return new Vect3(aabb.Max.X, aabb.Max.Y, aabb.Min.Z);
            yield return new Vect3(aabb.Max.X, aabb.Max.Y, aabb.Max.Z);
        }

        public static IAABB CalulateAABB(this IPointCloud pointCloud)
        {
            var maxX = pointCloud.Points.Max(p => p.Position.X);
            var maxY = pointCloud.Points.Max(p => p.Position.Y);
            var maxZ = pointCloud.Points.Max(p => p.Position.Z);
            var minX = pointCloud.Points.Min(p => p.Position.X);
            var minY = pointCloud.Points.Min(p => p.Position.Y);
            var minZ = pointCloud.Points.Min(p => p.Position.Z);
            return new AABB(new Vect3(minX, minY, minZ), new Vect3(maxX, maxY, maxZ));
        }
    }
}
