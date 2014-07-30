using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pencil.Gaming;
using CADButton = OpenCAD.Kernel.Application.MouseButton;
namespace OpenCAD.OpenGL
{
    public static class Extensions
    {
        public static MouseButton ToPencilButton(this CADButton button)
        {
            switch (button)
            {
                case CADButton.Left:
                    return MouseButton.LeftButton;
                case CADButton.Middle:
                    return MouseButton.MiddleButton;
                case CADButton.Right:
                    return MouseButton.RightButton;
                default:
                    return MouseButton.LeftButton;
            }
        }

        public static CADButton ToButton(this MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return CADButton.Left;
                case MouseButton.MiddleButton:
                    return CADButton.Middle;
                case MouseButton.RightButton:
                    return CADButton.Right;
                default:
                    return CADButton.Left;
            }
        }
    }
}
