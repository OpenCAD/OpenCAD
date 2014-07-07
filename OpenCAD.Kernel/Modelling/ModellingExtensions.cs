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
            //this is quite slow, need to not use linq...
            //var minX = pointCloud.Points.First().Position.X;
            //var minY = pointCloud.Points.First().Position.Y;
            //var minZ = pointCloud.Points.First().Position.Z;
            //var maxX = pointCloud.Points.First().Position.X;
            //var maxY = pointCloud.Points.First().Position.Y;
            //var maxZ = pointCloud.Points.First().Position.Z;

            //foreach (var point in pointCloud.Points.Skip(1))
            //{
            //    minX = Math.Min(minX,point.Position.X);
            //    minY = Math.Min(minY,point.Position.Y);
            //    minZ = Math.Min(minZ,point.Position.Z);
            //    maxX = Math.Min(maxX,point.Position.X);
            //    maxY = Math.Min(maxY,point.Position.Y);
            //    maxZ = Math.Min(maxZ,point.Position.Z);
            //}
            //var min = new Vect3(minX, minY, minZ);
            //var max = new Vect3(maxX, maxY, maxZ);
            //return new AABB((min + max) * 0.5f, (min - max) * 0.5f);

            var min = new Vect3(pointCloud.Points.Min(p => p.Position.X), pointCloud.Points.Min(p => p.Position.Y), pointCloud.Points.Min(p => p.Position.Z));
            var max = new Vect3(pointCloud.Points.Max(p => p.Position.X), pointCloud.Points.Max(p => p.Position.Y), pointCloud.Points.Max(p => p.Position.Z));
            return new AABB((max + min) * 0.5f, (max - min) * 0.5f);
        }

        private static IOctreeNode Intersect(this IOctreeNode node, Func<IOctreeNode,bool> testFunc,  int maxLevel)
        {
            if (testFunc(node))
            {
                if (node.Level == maxLevel)
                {
                    return new OctreeNode(node.Center, node.Size, node.Level, NodeType.Filled);
                }
                else
                {
                    switch (node.Type)
                    {
                        case NodeType.Interior:
                            return new OctreeNode(node.Center, node.Size, node.Level, NodeType.Interior, node.Children.Select(c => c.Intersect(testFunc, maxLevel)).ToArray());
                        case NodeType.Filled:
                            return node;
                        case NodeType.Empty:
                            return new OctreeNode(node.Center, node.Size, node.Level, NodeType.Interior, node.CreateChildren().Select(c => c.Intersect(testFunc, maxLevel)).ToArray());
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            else
            {
                return node;
            }
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
