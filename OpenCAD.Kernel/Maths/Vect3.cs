using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Kernel.Maths
{
    /// <summary>
    /// A double precision vector 3 implementation
    /// </summary>
    public class Vect3
    {
        /// <summary> X Component </summary>
        public double X { get; private set; }
        /// <summary> Y Component </summary>
        public double Y { get; private set; }
        /// <summary> Z Component </summary>
        public double Z { get; private set; }

        /// <summary>
        /// Create new Vect3 from doubles
        /// </summary>
        /// <param name="x">X Component</param>
        /// <param name="y">Y Component</param>
        /// <param name="z">Z Component</param>
        public Vect3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Create new Vect3 from list of doubles
        /// </summary>
        /// <param name="a"></param>
        public Vect3(IList<double> a)
        {
            if (a.Count != 3) throw new ArgumentException("Array should be double[3]");
            X = a[0];
            Y = a[1];
            Z = a[2];
        }

        /// <summary>
        /// Calculate the length
        /// </summary>
        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }
        /// <summary>
        /// Calculate the square of the length
        /// </summary>
        public double LengthSquared
        {
            get { return X * X + Y * Y + Z * Z; }
        }

        public static Vect3 Zero
        {
            get { return new Vect3(0.0, 0.0, 0.0); }
        }

        public static Vect3 UnitX
        {
            get { return new Vect3(1.0, 0.0, 0.0); }
        }

        public static Vect3 UnitY
        {
            get { return new Vect3(0.0, 1.0, 0.0); }
        }

        public static Vect3 UnitZ
        {
            get { return new Vect3(0.0, 0.0, 1.0); }
        }

        /// <summary>
        /// Returns a normalized Vector
        /// </summary>
        /// <returns>Normalized Vector</returns>
        public Vect3 Normalized()
        {
            var num = 1f / Length;
            return new Vect3(X * num, Y * num, Z * num);
        }


        //TODO Extensions methods XY YZ
    }
}
