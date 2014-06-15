using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Tests.Maths
{
    [TestClass]
    public class VectorTests
    {
        private static double Delta = 0.0000001;

        [TestMethod]
        public void CreationFromDoubles()
        {
            var x = 2.0;
            var y = 3.0;
            var z = 5.0;
            var w = 7.0;

            var v2 = new Vect2(x, y);
            Assert.AreEqual(x, v2.X, Delta);
            Assert.AreEqual(y, v2.Y, Delta);

            var v3 = new Vect3(x, y, z);
            Assert.AreEqual(x, v3.X, Delta);
            Assert.AreEqual(y, v3.Y, Delta);
            Assert.AreEqual(z, v3.Z, Delta);

            var v4 = new Vect4(x, y, z,w);
            Assert.AreEqual(x, v4.X, Delta);
            Assert.AreEqual(y, v4.Y, Delta);
            Assert.AreEqual(z, v4.Z, Delta);
            Assert.AreEqual(w, v4.W, Delta);
        }

        [TestMethod]
        public void CreationFromListTooLong()
        {
            try
            {
                var v = new Vect2(new[] {2.0, 3.0, 5.0});
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }

            try
            {
                var v = new Vect3(new[] { 2.0, 3.0, 5.0, 7.0 });
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }

            try
            {
                var v = new Vect4(new[] { 2.0, 3.0, 5.0, 7.0, 7.0 });
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CreationFromList()
        {
            var x = 2.0;
            var y = 3.0;
            var z = 5.0;
            var w = 7.0;

            var v2 = new Vect2(new[] { x, y});
            Assert.AreEqual(x, v2.X, Delta);
            Assert.AreEqual(y, v2.Y, Delta);

            var v3 = new Vect3(new[] {x, y, z});
            Assert.AreEqual(x, v3.X, Delta);
            Assert.AreEqual(y, v3.Y, Delta);
            Assert.AreEqual(z, v3.Z, Delta);

            var v4 = new Vect4(new[] { x, y, z, w });
            Assert.AreEqual(x, v4.X, Delta);
            Assert.AreEqual(y, v4.Y, Delta);
            Assert.AreEqual(z, v4.Z, Delta);
            Assert.AreEqual(w, v4.W, Delta);
        }

        [TestMethod]
        public void LengthResult()
        {
            var v2 = new Vect2(2.0, 3.0);
            Assert.AreEqual(13.0, v2.LengthSquared, Delta);
            Assert.AreEqual(3.60555127546, v2.Length, Delta);

            var v3 = new Vect3(2.0, 3.0, 5.0);
            Assert.AreEqual(38, v3.LengthSquared, Delta);
            Assert.AreEqual(6.16441400297, v3.Length, Delta);

            var v4 = new Vect4(2.0, 3.0, 5.0, 7.0);
            Assert.AreEqual(87, v4.LengthSquared, Delta);
            Assert.AreEqual(9.32737905308, v4.Length, Delta);
        }

        [TestMethod]
        public void NormalisedResult()
        {
            Assert.AreEqual(new Vect2(0.5547, 0.83205), new Vect2(2.0, 3.0).Normalized());
            Assert.AreEqual(new Vect3(0.324443, 0.486664, 0.811107), new Vect3(2.0, 3.0, 5.0).Normalized());
            Assert.AreEqual(new Vect4(0.214423, 0.321634, 0.536056, 0.750479), new Vect4(2.0, 3.0, 5.0, 7.0).Normalized());
        }

        [TestMethod]
        public void StaticDefaults()
        {
            Assert.AreEqual(new Vect2(0.0, 0.0), Vect2.Zero);
            Assert.AreEqual(new Vect2(1.0, 0.0), Vect2.UnitX);
            Assert.AreEqual(new Vect2(0.0, 1.0), Vect2.UnitY);

            Assert.AreEqual(new Vect3(0.0, 0.0, 0.0), Vect3.Zero);
            Assert.AreEqual(new Vect3(1.0, 0.0, 0.0), Vect3.UnitX);
            Assert.AreEqual(new Vect3(0.0, 1.0, 0.0), Vect3.UnitY);
            Assert.AreEqual(new Vect3(0.0, 0.0, 1.0), Vect3.UnitZ);

            Assert.AreEqual(new Vect4(0.0, 0.0, 0.0, 0.0), Vect4.Zero);
            Assert.AreEqual(new Vect4(1.0, 0.0, 0.0, 0.0), Vect4.UnitX);
            Assert.AreEqual(new Vect4(0.0, 1.0, 0.0, 0.0), Vect4.UnitY);
            Assert.AreEqual(new Vect4(0.0, 0.0, 1.0, 0.0), Vect4.UnitZ);
            Assert.AreEqual(new Vect4(0.0, 0.0, 0.0, 1.0), Vect4.UnitW);
        }

        [TestMethod]
        public void DotProduct()
        {
            Assert.AreEqual(14, new Vect2(1.0, 2.0).DotProduct(new Vect2(4.0, 5.0)), Delta);
            Assert.AreEqual(32, new Vect3(1.0, 2.0, 3.0).DotProduct(new Vect3(4.0, 5.0, 6.0)), Delta);
            Assert.AreEqual(60, new Vect4(1.0, 2.0, 3.0, 4.0).DotProduct(new Vect4(4.0, 5.0, 6.0, 7.0)), Delta);

            Assert.AreEqual(0, Vect2.UnitX.DotProduct(Vect2.UnitY));
            Assert.AreEqual(0, Vect3.UnitX.DotProduct(Vect3.UnitY));
            Assert.AreEqual(0, Vect4.UnitX.DotProduct(Vect4.UnitY));
        }

        [TestMethod]
        public void CrossProduct()
        {
            Assert.AreEqual(new Vect3(-15.0, -2.0, 39.0), new Vect3(3.0, -3.0, 1.0).CrossProduct(new Vect3(4.0, 9.0, 2.0)));
            //same vector should equal zero
            Assert.AreEqual(new Vect3(0, 0, 0), new Vect3(3.0, -3.0, 1.0).CrossProduct(new Vect3(3.0, -3.0, 1.0)));
            //parallel shuold also equal zero
            Assert.AreEqual(new Vect3(0, 0, 0), new Vect3(0, 0, 10.0).CrossProduct(new Vect3(0, 0, -10.0)));
        }



        [TestMethod]
        public void Addition()
        {
            Assert.AreEqual(new Vect2(7.0, 15.0), new Vect2(3.0, 6.0) + new Vect2(4.0, 9.0));
            Assert.AreEqual(new Vect2(1.0, 15.0), new Vect2(-3.0, 6.0) + new Vect2(4.0, 9.0));

            Assert.AreEqual(new Vect3(7.0, 15.0, 11.0), new Vect3(3.0, 6.0, 9.0) + new Vect3(4.0, 9.0, 2.0));
            Assert.AreEqual(new Vect3(1.0, 15.0, 0.0), new Vect3(-3.0, 6.0, 2.0) + new Vect3(4.0, 9.0, -2.0));

            Assert.AreEqual(new Vect4(7.0, 15.0, 11.0, 10.0), new Vect4(3.0, 6.0, 9.0, 7.0) + new Vect4(4.0, 9.0, 2.0, 3.0));
            Assert.AreEqual(new Vect4(1.0, 15.0, 0.0, 7.0), new Vect4(-3.0, 6.0, 2.0, 5.0) + new Vect4(4.0, 9.0, -2.0, 2.0));

        }
        [TestMethod]
        public void Subtract()
        {
            Assert.AreEqual(new Vect2(-1.0, -3.0), new Vect2(3.0, 6.0) - new Vect2(4.0, 9.0));
            Assert.AreEqual(new Vect2(-7.0, -3.0), new Vect2(-3.0, 6.0) - new Vect2(4.0, 9.0));

            Assert.AreEqual(new Vect3(-1.0, -3.0, 7.0), new Vect3(3.0, 6.0, 9.0) - new Vect3(4.0, 9.0, 2.0));
            Assert.AreEqual(new Vect3(-7.0, -3.0, 4.0), new Vect3(-3.0, 6.0, 2.0) - new Vect3(4.0, 9.0, -2.0));

            Assert.AreEqual(new Vect4(-1.0, -3.0, 7.0, 6.0), new Vect4(3.0, 6.0, 9.0, 12.0) - new Vect4(4.0, 9.0, 2.0, 6.0));
            Assert.AreEqual(new Vect4(-7.0, -3.0, 4.0, 7.0), new Vect4(-3.0, 6.0, 2.0, 0) - new Vect4(4.0, 9.0, -2.0, -7.0));


            Assert.AreEqual(new Vect2(-4.0, -9.0), - new Vect2(4.0, 9.0));
            Assert.AreEqual(new Vect3(-4.0, -9.0, 2.0), - new Vect3(4.0, 9.0, -2.0));
            Assert.AreEqual(new Vect4(-4.0, -9.0, 2.0, 6.0), - new Vect4(4.0, 9.0, -2.0, -6.0));
        }

        [TestMethod]
        public void Multiply()
        {
            Assert.AreEqual(new Vect2(16.0, 36.0), 4.0 * new Vect2(4.0, 9.0));
            Assert.AreEqual(new Vect2(-8.0, -18.0), -2.0 * new Vect2(4.0, 9.0));
            Assert.AreEqual(new Vect2(16.0, 36.0), new Vect2(4.0, 9.0) * 4.0);
            Assert.AreEqual(new Vect2(-8.0, -18.0), new Vect2(4.0, 9.0) * -2.0);

            Assert.AreEqual(new Vect3(16.0, 36.0, 8.0), 4.0 * new Vect3(4.0, 9.0, 2.0));
            Assert.AreEqual(new Vect3(-8.0, -18.0, 4.0), -2.0 * new Vect3(4.0, 9.0, -2.0));
            Assert.AreEqual(new Vect3(16.0, 36.0, 8.0), new Vect3(4.0, 9.0, 2.0) * 4.0);
            Assert.AreEqual(new Vect3(-8.0, -18.0, 4.0), new Vect3(4.0, 9.0, -2.0) * -2.0);

            Assert.AreEqual(new Vect4(16.0, 36.0, 8.0 , 8.0), 4.0 * new Vect4(4.0, 9.0, 2.0, 2.0));
            Assert.AreEqual(new Vect4(-8.0, -18.0, 4.0, 8.0), -2.0 * new Vect4(4.0, 9.0, -2.0, -4.0));
            Assert.AreEqual(new Vect4(16.0, 36.0, 8.0, 8.0), new Vect4(4.0, 9.0, 2.0, 2.0) * 4.0);
            Assert.AreEqual(new Vect4(-8.0, -18.0, 4.0, 8.0), new Vect4(4.0, 9.0, -2.0, -4.0) * -2.0);
        }

        [TestMethod]
        public void Divide()
        {
            Assert.AreEqual(new Vect2(2.0, 4.5), new Vect2(4.0, 9.0) /2);
            Assert.AreEqual(new Vect3(2.0, 4.5, 1.0), new Vect3(4.0, 9.0, 2.0) /2);
            Assert.AreEqual(new Vect4(2.0, 4.5, 1.0,32.0), new Vect4(4.0, 9.0, 2.0, 64.0) /2);
        }

        [TestMethod]
        public void Vect2Equality()
        {
            var a = new Vect2(2.0, 3.0);
            var b = new Vect2(2.0, 3.0);
            var c = new Vect2(3.0, 4.0);
            var d = a;

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsTrue(a.Equals(d));

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreEqual(a, d);

            Assert.IsTrue(a == b);
            Assert.IsTrue(a != c);

            Assert.IsTrue(a != null);
            Assert.IsTrue(a == a);
        }

        [TestMethod]
        public void Vect3Equality()
        {
            var a = new Vect3(2.0, 3.0, 5.0);
            var b = new Vect3(2.0, 3.0, 5.0);
            var c = new Vect3(3.0, 3.0, 6.0);
            var d = a;

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsTrue(a.Equals(d));

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a,c);
            Assert.AreEqual(a, d);

            Assert.IsTrue(a == b);
            Assert.IsTrue(a != c);

            Assert.IsTrue(a != null);
            Assert.IsTrue(a == a);
        }

        [TestMethod]
        public void Vect4Equality()
        {
            var a = new Vect4(2.0, 3.0, 5.0, 6.0);
            var b = new Vect4(2.0, 3.0, 5.0, 6.0);
            var c = new Vect4(3.0, 3.0, 6.0, 7.0);
            var d = a;

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsTrue(a.Equals(d));

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreEqual(a, d);

            Assert.IsTrue(a == b);
            Assert.IsTrue(a != c);

            Assert.IsTrue(a != null);
            Assert.IsTrue(a == a);
        }



        [TestMethod]
        public void Lerp()
        {
            Assert.AreEqual(new Vect3(2.0, 4.5, 1.0), new Vect3(2.0, 4.5, 1.0).Lerp(new Vect3(4.0, 9.0, 2.0), 0.0));
            Assert.AreEqual(new Vect3(4.0, 9.0, 2.0), new Vect3(2.0, 4.5, 1.0).Lerp(new Vect3(4.0, 9.0, 2.0), 1.0));
            Assert.AreEqual(new Vect3(0.0, 5.0, 0.0), new Vect3(0.0, 0.0, 0.0).Lerp(new Vect3(0.0, 10.0, 0.0), 0.5));
            Assert.AreEqual(new Vect3(0.0, 0.0, 0.0), new Vect3(0.0, -10.0, 0.0).Lerp(new Vect3(0.0, 10.0, 0.0), 0.5));
        }

    }
}

