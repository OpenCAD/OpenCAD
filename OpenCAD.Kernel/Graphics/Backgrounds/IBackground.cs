using System.Drawing;

namespace OpenCAD.Kernel.Graphics.Backgrounds
{
    public interface IBackground
    {
        Color TopLeft { get; }
        Color TopRight { get; }
        Color BottomLeft { get; }
        Color BottomRight { get; }
    }
}

    