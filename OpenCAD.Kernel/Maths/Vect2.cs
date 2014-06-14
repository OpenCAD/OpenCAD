using System;
using System.Collections.Generic;

namespace OpenCAD.Kernel.Maths
{
    public class Vect2
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        /// <summary>
        /// Returns Swizzle object
        /// </summary>
        public dynamic Swizzle { get { return new Swizzle(this); } }

        

        public Vect2(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Vect2(IList<double> a)
        {
            if (a.Count != 2) throw new ArgumentException("Array should be double[2]");
            X = a[0];
            Y = a[1];

        }


    }
}