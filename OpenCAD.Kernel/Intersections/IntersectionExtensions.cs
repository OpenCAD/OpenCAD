using System;
using System.Linq;
using OpenCAD.Kernel.Modelling.Octree;

namespace OpenCAD.Kernel.Intersections
{
    public static class IntersectionExtensions
    {
        public  static IOctreeNode Intersect(this IOctreeNode node, Func<IOctreeNode, bool> testFunc, int maxLevel)
        {
            if (!testFunc(node)) return node;
            if (node.Level == maxLevel)
            {
                return new OctreeNode(node.Center, node.Size, node.Level, NodeType.Filled);
            }
            switch (node.Type)
            {
                case NodeType.Interior:
                    return new OctreeNode(node.Center, node.Size, node.Level, NodeType.Interior, node.Children.Select(c => Intersect(c, testFunc, maxLevel)).ToArray());
                case NodeType.Filled:
                    return node;
                case NodeType.Empty:
                    return new OctreeNode(node.Center, node.Size, node.Level, NodeType.Interior, node.CreateChildren().Select(c => c.Intersect(testFunc, maxLevel)).ToArray());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}