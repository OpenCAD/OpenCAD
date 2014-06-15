using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Tests.Maths
{
    [TestClass]
    public class SwizzleTests
    {
        [TestMethod]
        public void SwizzleVect3()
        {
            var v2 = new Vect2(2.0, 3.0);
            var v3 = new Vect3(2.0, 3.0, 5.0);
            var v4 = new Vect4(2.0, 3.0, 5.0, 7.0);

            Assert.AreEqual(v2,v3.Swizzle.XY);
            Assert.AreEqual(v2,v4.Swizzle.XY);

            Assert.AreEqual(new Vect3(2.0, 2.0, 2.0), v3.Swizzle.XXX);
            Assert.AreEqual(new Vect3(2.0, 2.0, 2.0), v4.Swizzle.XXX);
        }
    }
}
