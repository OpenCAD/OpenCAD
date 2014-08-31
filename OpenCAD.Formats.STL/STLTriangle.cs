using System;
using System.IO;
using System.Linq;

namespace OpenCAD.Formats.STL
{
    public class STLFacet 
    {
        public float[] Normal { get; private set; }
        public float[] Vertex1 { get; private set; }
        public float[] Vertex2 { get; private set; }
        public float[] Vertex3 { get; private set; }
        public UInt16 AttributeByteCount { get; private set; }
        internal STLFacet(BinaryReader reader)
        {
            Normal = reader.ReadSingles(3).ToArray();
            Vertex1 = reader.ReadSingles(3).ToArray();
            Vertex2 = reader.ReadSingles(3).ToArray();
            Vertex3 = reader.ReadSingles(3).ToArray();
            AttributeByteCount = reader.ReadUInt16();
        }
        public STLFacet(float[] normal, float[,] vertices)
        {
            Normal = normal;
            Vertex1 = new[] { vertices[0, 0], vertices[0, 1], vertices[0, 2] };
            Vertex2 = new[] { vertices[1, 0], vertices[1, 1], vertices[1, 2] };
            Vertex3 = new[] { vertices[2, 0], vertices[2, 1], vertices[2, 2] };
        }
    }
}
