using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Tests.Maths
{
    [TestClass]
    public class Vect3Tests
    {
        private static double Delta = 0.0000001;

        [TestMethod]
        public void CreationFromDoubles()
        {
            var x = 2.0;
            var y = 3.0;
            var z = 5.0;
            var v = new Vect3(x, y, z);
            Assert.AreEqual(x, v.X, Delta);
            Assert.AreEqual(y, v.Y, Delta);
            Assert.AreEqual(z, v.Z, Delta);
        }

        [TestMethod]
        public void CreationFromListTooLong()
        {
            try
            {
                var v = new Vect3(new[] {2.0, 3.0, 5.0, 7.0});
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
            var v = new Vect3(new[] { x, y, z });
            Assert.AreEqual(x, v.X, Delta);
            Assert.AreEqual(y, v.Y, Delta);
            Assert.AreEqual(z, v.Z, Delta);
        }

        [TestMethod]
        public void LengthResult()
        {
            var v = new Vect3(2.0, 3.0, 5.0);
            Assert.AreEqual(38, v.LengthSquared, Delta);
            Assert.AreEqual(6.16441400297, v.Length, Delta);
        }

        [TestMethod]
        public void NormalisedResult()
        {
            var v = new Vect3(3.0, 1.0, 2.0).Normalized();
            Assert.AreEqual(0.8017837257, v.X, Delta);
            Assert.AreEqual(0.2672612419, v.Y, Delta);
            Assert.AreEqual(0.5345224838, v.Z, Delta);
        }

        [TestMethod]
        public void StaticDefaults()
        {
            Assert.AreEqual(0.0, Vect3.Zero.X, Delta);
            Assert.AreEqual(0.0, Vect3.Zero.Y, Delta);
            Assert.AreEqual(0.0, Vect3.Zero.Z, Delta);

            Assert.AreEqual(1.0, Vect3.UnitX.X, Delta);
            Assert.AreEqual(0.0, Vect3.UnitX.Y, Delta);
            Assert.AreEqual(0.0, Vect3.UnitX.Z, Delta);

            Assert.AreEqual(0.0, Vect3.UnitY.X, Delta);
            Assert.AreEqual(1.0, Vect3.UnitY.Y, Delta);
            Assert.AreEqual(0.0, Vect3.UnitY.Z, Delta);

            Assert.AreEqual(0.0, Vect3.UnitZ.X, Delta);
            Assert.AreEqual(0.0, Vect3.UnitZ.Y, Delta);
            Assert.AreEqual(1.0, Vect3.UnitZ.Z, Delta);
        }

    }
}

