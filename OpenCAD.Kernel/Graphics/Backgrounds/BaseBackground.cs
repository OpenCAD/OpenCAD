using System.Drawing;

namespace OpenCAD.Kernel.Graphics.Backgrounds
{
    public abstract class BaseBackground : IBackground
    {
        public Color TopLeft { get; protected set; }
        public Color TopRight { get; protected set; }
        public Color BottomLeft { get; protected set; }
        public Color BottomRight { get; protected set; }
    }
}