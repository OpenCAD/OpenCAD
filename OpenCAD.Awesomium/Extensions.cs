using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Application;
using Awesomium.Core;
using CADButton = OpenCAD.Kernel.Application.MouseButton;
using MouseButton = Awesomium.Core.MouseButton;

namespace OpenCAD.Awesomium
{
    public static class Extensions
    {
        public static MouseButton ToAwesomiumButton(this CADButton button)
        {
            switch (button)
            {
                case CADButton.Left:
                    return MouseButton.Left;
                case CADButton.Middle:
                    return MouseButton.Middle;
                case CADButton.Right:
                    return MouseButton.Right;
                default:
                    return MouseButton.Left;
            }
        }

        public static Uri CreateUri(this IViewModel vm)
        {
            return new Uri(String.Format("cad://gui/{0}.html", vm.ViewName));
        }
    }
}
