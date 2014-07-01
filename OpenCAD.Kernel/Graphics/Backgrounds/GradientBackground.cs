using System.Drawing;

namespace OpenCAD.Kernel.Graphics.Backgrounds
{
    public class GradientBackground : BaseBackground
    {
        public GradientBackground(Color topLeft, Color topRight, Color bottomLeft, Color bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
    }
}