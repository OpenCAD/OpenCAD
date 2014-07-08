using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Intersections;
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
            var agg = pointCloud.Points.Aggregate(new
            {
                MinX = Double.PositiveInfinity,
                MinY = Double.PositiveInfinity,
                MinZ = Double.PositiveInfinity,
                MaxX = Double.NegativeInfinity,
                MaxY = Double.NegativeInfinity,
                MaxZ = Double.NegativeInfinity,
            }, (a, p) => new
            {
                MinX = Math.Min(a.MinX,p.Position.X),
                MinY = Math.Min(a.MinY,p.Position.Y),
                MinZ = Math.Min(a.MinZ,p.Position.Z),
                MaxX = Math.Max(a.MaxX,p.Position.X),
                MaxY = Math.Max(a.MaxY,p.Position.Y),
                MaxZ = Math.Max(a.MaxZ,p.Position.Z),
            });
            var min = new Vect3(agg.MinX, agg.MinY, agg.MinZ);
            var max = new Vect3(agg.MaxX, agg.MaxY, agg.MaxZ);
            return new AABB((max + min) * 0.5f, (max - min) * 0.5f);
        }


        public static OctreeModel ToOctree(this IPointCloud pointCloud, int maxLevel)
        {
            var box = pointCloud.CalulateAABB();
            var size = Math.Abs(box.HalfSize.ToArray().Max() * 2.0) ;
            IOctreeNode node = new OctreeNode(box.Center, size, 0, NodeType.Empty);
            return new OctreeModel(pointCloud.Points.Aggregate(node, (current, point) => current.Intersect(point.In, maxLevel)));
        }

        public static IEnumerable<IOctreeNode> Flatten(this IOctreeNode node)
        {
            yield return node;
            foreach (var c in node.Children.SelectMany(child => child.Flatten()))
            {
                yield return c;
            }
        }
    }
}
