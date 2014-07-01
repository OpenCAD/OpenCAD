using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer
{
    public static class Extensions
    {
        public static float[] ToColumnMajorArrayFloat(this Mat4 m)
        {
            return new[]{
                (float) m[1, 1],(float) m[2, 1],(float) m[3, 1],(float) m[4, 1],
                (float) m[1, 2],(float) m[2, 2],(float) m[3, 2],(float) m[4, 2],
                (float) m[1, 3],(float) m[2, 3],(float) m[3, 3],(float) m[4, 3],
                (float) m[1, 4],(float) m[2, 4],(float) m[3, 4],(float) m[4, 4]};
        }
    }
}
