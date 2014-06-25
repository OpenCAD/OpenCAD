using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Modelling.Octree
{
    public class OctreeModel
    {
        public OctreeNode Root { get; private set; }
        public OctreeModel(OctreeNode root)
        {
            Root = root;
        }
    }

    public enum NodeType { Interior, Filled, Empty }






    public interface IOctreeNode : IAABB
    {
        Double Size { get; }
        int Level { get; }
        NodeType Type { get; }
        IReadOnlyList<IOctreeNode> Children { get; }
    }



    public class OctreeNode : IOctreeNode
    {
        public Vect3 Min { get { return Center + new Vect3(-Size / 2.0, -Size / 2.0, -Size / 2.0); } }
        public Vect3 Max { get { return Center + new Vect3(Size / 2.0, Size / 2.0, Size / 2.0); } }
        public Vect3 Extent { get { return new Vect3(Size, Size, Size); } }

        public Vect3 Center { get; private set; }

        public Double Size { get; private set; }

        public int Level { get; private set; }
        public NodeType Type { get; private set; }
        public IReadOnlyList<IOctreeNode> Children { get; private set; }

        public OctreeNode(Vect3 center, Double size, int level, NodeType type)
        {
            Center = center;
            Size = size;
            Level = level;
            Type = type;
            Children = Enumerable.Empty<OctreeNode>().ToList();
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
