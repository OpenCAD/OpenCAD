using System;
using System.Collections.Generic;
using System.Linq;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Modelling.Octree
{
    public class OctreeNode : AABB,IOctreeNode
    {
        public Double Size { get; private set; }
        public int Level { get; private set; }
        public NodeType Type { get; private set; }
        public IEnumerable<IOctreeNode> Children { get; private set; }

        public IEnumerable<IOctreeNode> CreateChildren()
        {
            var newSize = Size / 2.0;
            var half = Size / 4.0;
            //top-front-right
            yield return new OctreeNode(Center + new Vect3(+half, +half, +half), newSize, Level + 1, NodeType.Empty);
            //top-back-right
            yield return new OctreeNode(Center + new Vect3(-half, +half, +half), newSize, Level + 1, NodeType.Empty);
            //top-back-left
            yield return new OctreeNode(Center + new Vect3(-half, -half, +half), newSize, Level + 1, NodeType.Empty);
            //top-front-left
            yield return new OctreeNode(Center + new Vect3(+half, -half, +half), newSize, Level + 1, NodeType.Empty);
            //bottom-front-right
            yield return new OctreeNode(Center + new Vect3(+half, +half, -half), newSize, Level + 1, NodeType.Empty);
            //bottom-back-right
            yield return new OctreeNode(Center + new Vect3(-half, +half, -half), newSize, Level + 1, NodeType.Empty);
            //bottom-back-left
            yield return new OctreeNode(Center + new Vect3(-half, -half, -half), newSize, Level + 1, NodeType.Empty);
            //bottom-front-left
            yield return new OctreeNode(Center + new Vect3(+half, -half, -half), newSize, Level + 1, NodeType.Empty);
        }

        public OctreeNode(Vect3 center, Double size, int level, NodeType type, IEnumerable<IOctreeNode> children)
            :base(center, size)
        {
            Size = size;
            Level = level;
            Type = type;
            Children = children;
        }

        public OctreeNode(Vect3 center, Double size, int level, NodeType type)
            : this(center, size, level, type, Enumerable.Empty<OctreeNode>())
        {

        }

        //public OctreeNode(Vect3 center, Double size, NodeType type)
        //    : this(center, size, 0, type)
        //{

        //}

        //public OctreeNode(Vect3 center, Double size)
        //    : this(center, size, 0, NodeType.Empty)
        //{

        //}

        //public OctreeNode(Double size)
        //    : this(Vect3.Zero, size, 0, NodeType.Empty)
        //{

        //}

        public override string ToString()
        {
            return String.Format("Node<{0}>[{1}]",Type,Center);
        }
    }
}