using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Tests.Maths
{
    [TestClass]
    public class Vect3Tests
    {
        [TestMethod]
        public void CreationFromDoubles()
        {
            var x = 2.0;
            var y = 3.0;
            var z = 5.0;
            var v = new Vect3(x, y, z);
            Assert.AreEqual(v.X, x, 0.0000001);
            Assert.AreEqual(v.Y, y, 0.0000001);
            Assert.AreEqual(v.Z, z, 0.0000001);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void CreationFromListTooLong()
        {
            var v = new Vect3(new[] {2.0, 3.0, 5.0, 7.0});
        }

        [TestMethod]
        public void CreationFromList()
        {
            var x = 2.0;
            var y = 3.0;
            var z = 5.0;
            var v = new Vect3(new[] { x, y, z });
            Assert.AreEqual(v.X, x, 0.0000001);
            Assert.AreEqual(v.Y, y, 0.0000001);
            Assert.AreEqual(v.Z, z, 0.0000001);
        }
    }
}

