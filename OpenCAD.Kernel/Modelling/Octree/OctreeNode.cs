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

        public OctreeNode(Vect3 center, Double size, int level, NodeType type)
            :base(center, size)
        {
            Size = size;
            Level = level;
            Type = type;
            Children = Enumerable.Empty<OctreeNode>();
        }

        public OctreeNode(Vect3 center, Double size, NodeType type)
            : this(center, size, 0, type)
        {

        }

        public OctreeNode(Vect3 center, Double size)
            : this(center, size, 0, NodeType.Empty)
        {

        }

        public OctreeNode(Double size)
            : this(Vect3.Zero, size, 0, NodeType.Empty)
        {

        }
    }
}