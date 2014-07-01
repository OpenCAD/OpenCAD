using System.Drawing;

namespace OpenCAD.Kernel.Graphics.Backgrounds
{
    public class SolidBackground : BaseBackground
    {
        public SolidBackground(Color color)
        {
            TopLeft = color;
            TopRight = color;
            BottomLeft = color;
            BottomRight = color;
        }
    }
}