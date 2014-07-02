using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Vertices
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vertex : IVertex
    {
        public readonly Vect3 Position;
        public readonly Color Color;

        public Vertex(Vect3 position, Color color)
        {
            Position = position;
            Color = color;
        }

        public static int Stride { get { return Marshal.SizeOf(typeof(Vertex)); } }

        public float[] Data
        {
            get
            {
                var data = new List<float>(7);
                data.AddRange(Position.ToArray().Select(d => (float)d));
                data.AddRange(Color.ToFloatArray());
                return data.ToArray();
            }
        }
    }
}