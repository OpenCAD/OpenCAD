using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.FileFormats;
using OpenCAD.Kernel.Graphics.OpenGLRenderer;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Test
{
    class Program
    {
        public static void Main(string[] args)
        {
            var render = new OpenGLStaticRenderer(800, 600);

            var t = render.Render();
            t.Save("test2.png");
        }
    }
}
