using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Kernel.Maths
{
    public class Vect3
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public Vect3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vect3(IList<double> a)
        {
            if (a.Count != 3) throw new ArgumentException("Array should be double[3]");
            X = a[0];
            Y = a[1];
            Z = a[2];
        }

    }
}
