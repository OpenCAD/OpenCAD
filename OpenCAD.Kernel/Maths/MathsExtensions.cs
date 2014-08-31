using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Kernel.Maths
{
    public static class MathsExtensions
    {
        public static bool NearlyEquals(this Double x, Double y, Double epsilon = 0.0000001)
        {
            return Math.Abs(x - y) <= Math.Abs(x * .00001);
        }

        public static bool NearlyLessThanOrEquals(this Double x, Double y, Double epsilon = 0.0000001)
        {
            return x <= y || x.NearlyEquals(y, epsilon);
        }

        public static bool NearlyGreaterThanOrEquals(this Double x, Double y, Double epsilon = 0.0000001)
        {
            return x >= y || x.NearlyEquals(y, epsilon);
        }

        public static double Map(this double x, double inMin, double inMax, double outMin, double outMax)
        {
            return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
        public static float Map(this float x, float inMin, float inMax, float outMin, float outMax)
        {
            return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
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
