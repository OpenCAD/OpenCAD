using System;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Vertices
{
    public interface IVertex
    {
        byte[] Data { get; }
    }
}
