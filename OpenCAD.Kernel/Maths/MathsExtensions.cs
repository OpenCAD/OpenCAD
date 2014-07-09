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
    }
}
