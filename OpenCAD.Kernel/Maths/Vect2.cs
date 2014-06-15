using System;
using System.Collections.Generic;

namespace OpenCAD.Kernel.Maths
{
    public class Vect2
    {
        /// <summary> X Component </summary>
        public double X { get; private set; }
        /// <summary> Y Component </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Returns Swizzle object
        /// </summary>
        public dynamic Swizzle { get { return new Swizzle(this); } }

        /// <summary>
        /// Create new Vect2 from doubles
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vect2(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Create new Vect2 from list of doubles
        /// </summary>
        /// <param name="a"></param>
        public Vect2(IList<double> a)
        {
            if (a.Count != 2) throw new ArgumentException("Array should be double[2]");
            X = a[0];
            Y = a[1];
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
            get { return X*X + Y*Y; }
        }

        /// <summary>
        /// A zero vector
        /// </summary>
        public static Vect2 Zero
        {
            get { return new Vect2(0.0, 0.0); }
        }

        /// <summary>
        /// A unit vector in the X direction
        /// </summary>
        public static Vect2 UnitX
        {
            get { return new Vect2(1.0, 0.0); }
        }

        /// <summary>
        /// A unit vector in the Y direction
        /// </summary>
        public static Vect2 UnitY
        {
            get { return new Vect2(0.0, 1.0); }
        }

        /// <summary>
        /// Returns a unit Vector
        /// </summary>
        /// <returns>Normalized Vector</returns>
        public Vect2 Normalized()
        {
            var num = 1f / Length;
            return new Vect2(X * num, Y * num);
        }

        /// <summary>
        /// Calculate the Dot Product
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double DotProduct(Vect2 v)
        {
            return X * v.X + Y * v.Y;
        }

        /// <summary>
        /// Add a Vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vect2 operator +(Vect2 v1, Vect2 v2)
        {
            return new Vect2(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// Subtract a Vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vect2 operator -(Vect2 v1, Vect2 v2)
        {
            return new Vect2(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// Invert Vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vect2 operator -(Vect2 v)
        {
            return new Vect2(-v.X, -v.Y);
        }

        /// <summary>
        /// Multiply by double
        /// </summary>
        /// <param name="v"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Vect2 operator *(Vect2 v, double d)
        {
            return new Vect2(v.X * d, v.Y * d);
        }

        /// <summary>
        /// Multiply by double
        /// </summary>
        /// <param name="d"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vect2 operator *(double d, Vect2 v)
        {
            return v * d;
        }

        /// <summary>
        /// Divide by double
        /// </summary>
        /// <param name="v"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Vect2 operator /(Vect2 v, double d)
        {
            return new Vect2(v.X / d, v.Y / d);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vect2 a, Vect2 b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Equals(b);
        }

        /// <summary>
        /// Test for not equality
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vect2 a, Vect2 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Vect2)obj);
        }

        protected bool Equals(Vect2 other)
        {
            return X.NearlyEquals(other.X) && Y.NearlyEquals(other.Y);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public double[] ToArray()
        {
            return new[] { X, Y};
        }

        public Vect2 Lerp(Vect2 end, double t)
        {
            return (1 - t) * this + t * end;
        }

        public override string ToString()
        {
            return String.Format("Vect2<{0},{1}>", X, Y);
        }
    }
}