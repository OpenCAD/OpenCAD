using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.FileFormats;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var v = Vect3.UnitY;
            Vect2 t = v.Swizzle.YX;
            var y = t.Swizzle.X;


            var f = new PCLFile("bunny.pcd");

        }
    }
}
