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
        //public static void ToOctreeNode(this IPointCloud pointCloud)
        //{
        //    var size = pointCloud.CalulateAABB().Extent.ToArray().Max();
        //    var node = new OctreeNode(size);
        //    foreach (var point in pointCloud.Points)
        //    {
                
        //    }
        //}

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
            //this is quite slow, need to not use linq...
            var minX = pointCloud.Points.First().Position.X;
            var minY = pointCloud.Points.First().Position.Y;
            var minZ = pointCloud.Points.First().Position.Z;
            var maxX = pointCloud.Points.First().Position.X;
            var maxY = pointCloud.Points.First().Position.Y;
            var maxZ = pointCloud.Points.First().Position.Z;

            foreach (var point in pointCloud.Points.Skip(1))
            {
                minX = Math.Min(minX,point.Position.X);
                minY = Math.Min(minY,point.Position.Y);
                minZ = Math.Min(minZ,point.Position.Z);
                maxX = Math.Min(maxX,point.Position.X);
                maxY = Math.Min(maxY,point.Position.Y);
                maxZ = Math.Min(maxZ,point.Position.Z);
            }
            var min = new Vect3(minX, minY, minZ);
            var max = new Vect3(maxX, maxY, maxZ);
            return new AABB((min + max) * 0.5f, (min - max) * 0.5f);

            //var min = new Vect3(pointCloud.Points.Min(p => p.Position.X), pointCloud.Points.Min(p => p.Position.Y), pointCloud.Points.Min(p => p.Position.Z));
            //var max = new Vect3(pointCloud.Points.Max(p => p.Position.X), pointCloud.Points.Max(p => p.Position.Y), pointCloud.Points.Max(p => p.Position.Z));
            //return new AABB((min + max) * 0.5f, (min - max) * 0.5f);
        }

        public static OctreeModel ToOctree(this IPointCloud pointCloud, int level)
        {
            var box = pointCloud.CalulateAABB();
            var size = box.HalfSize.ToArray().Max()*2;
            var node = new OctreeNode(box.Center, size, NodeType.Empty);
            return new OctreeModel(node);
        }
    }
}
