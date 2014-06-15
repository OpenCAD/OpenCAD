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
            if (a.Count != 4) throw new ArgumentException("Array should be double[2]");
            X = a[0];
            Y = a[1];
            Z = a[2];
            W = a[3];
        }
        
        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public double LengthSquared
        {
            get { return X * X + Y * Y + Z * Z + W * W; }
        }

        public Vect4 Normalized()
        {
            var num = 1f / Length;
            return new Vect4(X * num, Y * num, Z * num, W * num);
        }

        public static Vect4 Zero
        {
            get { return new Vect4(0.0, 0.0, 0.0, 0.0); }
        }
        public static Vect4 UnitX
        {
            get { return new Vect4(1.0, 0.0, 0.0, 0.0); }
        }

        public static Vect4 UnitY
        {
            get { return new Vect4(0.0, 1.0, 0.0, 0.0); }
        }

        public static Vect4 UnitZ
        {
            get { return new Vect4(0.0, 0.0, 1.0, 0.0); }
        }

        public static Vect4 UnitW
        {
            get { return new Vect4(0.0, 0.0, 0.0, 1.0); }
        }

        public double DotProduct(Vect4 v)
        {
            return X * v.X + Y * v.Y + Z * v.Z + W * v.W;
        }

        public Vect4 ExteriorProduct(Vect4 v)
        {
            throw new NotImplementedException();
        }

        public static Vect4 operator +(Vect4 v1, Vect4 v2)
        {
            return new Vect4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Vect4 operator -(Vect4 v1, Vect4 v2)
        {
            return new Vect4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }
        public static Vect4 operator -(Vect4 v)
        {
            return new Vect4(-v.X, -v.Y, -v.Z, -v.W);
        }

        public static Vect4 operator *(Vect4 v, double d)
        {
            return new Vect4(v.X * d, v.Y * d, v.Z * d, v.W * d);
        }

        public static Vect4 operator *(double d, Vect4 v)
        {
            return v * d;
        }

        public static Vect4 operator /(Vect4 v, double d)
        {
            return new Vect4(v.X / d, v.Y / d, v.Z / d, v.W / d);
        }
        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vect4 a, Vect4 b)
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
        public static bool operator !=(Vect4 a, Vect4 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Vect4)obj);
        }

        protected bool Equals(Vect4 other)
        {
            return X.NearlyEquals(other.X) && Y.NearlyEquals(other.Y) && Z.NearlyEquals(other.Z) && W.NearlyEquals(other.W);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode();
        }

 
        public Vect4 Lerp(Vect4 end, double t)
        {
            return (1 - t) * this + t * end;
        }

        public override string ToString()
        {
            return String.Format("Vector4({0},{1},{2},{3})", X, Y, Z, W);
        }
        
        public double[] ToArray()
        {
            return new[] { X, Y, Z, W};
        }

    }
}