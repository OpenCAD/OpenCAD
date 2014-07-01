using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel
{
    public static class Extensions
    {
        public static IEnumerable<float> ToFloatArray(this Color c)
        {
            yield return ((float) c.R).Map(0, 255, 0, 1);
            yield return ((float) c.G).Map(0, 255, 0, 1);
            yield return ((float) c.B).Map(0, 255, 0, 1);
            yield return ((float) c.A).Map(0, 255, 0, 1);
        }
    }
}
