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

        /// <summary>
        /// A zero vector
        /// </summary>
        public static Vect3 Zero
        {
            get { return new Vect3(0.0, 0.0, 0.0); }
        }

        /// <summary>
        /// A unit vector in the X direction
        /// </summary>
        public static Vect3 UnitX
        {
            get { return new Vect3(1.0, 0.0, 0.0); }
        }

        /// <summary>
        /// A unit vector in the Y direction
        /// </summary>
        public static Vect3 UnitY
        {
            get { return new Vect3(0.0, 1.0, 0.0); }
        }

        /// <summary>
        /// A unit vector in the Z direction
        /// </summary>
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

        /// <summary>
        /// Returns a Vector perpendicular to both and therefore normal to the plane containing them.
        /// </summary>
        /// <param name="b">Other Vector</param>
        /// <returns>Vector perpendicular to both and therefore normal to the plane containing them</returns>
        public Vect3 CrossProduct(Vect3 b)
        {
            return new Vect3(Y * b.Z - Z * b.Y, Z * b.X - X * b.Z, X * b.Y - Y * b.X);
        }




        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Vect3) obj);
        }

        protected bool Equals(Vect3 other)
        {
            return X.NearlyEquals(other.X) && Y.NearlyEquals(other.Y) && Z.NearlyEquals(other.Z);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }


        //TODO Extensions methods XY YZ
    }
}
