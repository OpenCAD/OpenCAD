using System;
using System.Collections.Generic;
using OpenCAD.Kernel.Geometry;

namespace OpenCAD.Kernel.Modelling.Octree
{
    public interface IOctreeNode : IAABB
    {
        Double Size { get; }
        int Level { get; }
        NodeType Type { get; }
        IReadOnlyList<IOctreeNode> Children { get; }
    }

    public enum NodeType { Interior, Filled, Empty }
}