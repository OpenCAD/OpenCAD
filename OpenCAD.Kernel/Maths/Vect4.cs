using System;
using System.Collections.Generic;

namespace OpenCAD.Kernel.Maths
{
    public class Vect4
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double W { get; private set; }

        /// <summary>
        /// Returns Swizzle object
        /// </summary>
        public dynamic Swizzle { get { return new Swizzle(this); } }

        public Vect4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vect4(IList<double> a)
        {
            if (a.Count != 2) throw new ArgumentException("Array should be double[2]");
            X = a[0];
            Y = a[1];
            Z = a[2];
            W = a[3];
        }
    }
}