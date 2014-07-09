using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Tests.Maths
{
    [TestClass]
    public class MathsExtensionsTest
    {
        [TestMethod]
        public void NearlyEquals()
        {
            Assert.IsTrue(0.0000001.NearlyEquals(0.0000001));
            Assert.IsTrue(1.0.NearlyEquals(1.0));
            Assert.IsFalse(0.0000001.NearlyEquals(0.000001));
        }

        [TestMethod]
        public void NearlyLessThanOrEquals()
        {
            Assert.IsTrue(0.0000001.NearlyLessThanOrEquals(0.0000001));
            Assert.IsTrue(0.0000001.NearlyLessThanOrEquals(0.001));
            Assert.IsFalse(0.1.NearlyLessThanOrEquals(0.001));
        }
        [TestMethod]
        public void NearlyGreaterThanOrEquals()
        {
            Assert.IsTrue(0.0000001.NearlyGreaterThanOrEquals(0.0000001));
            Assert.IsTrue(0.1.NearlyGreaterThanOrEquals(0.001));
            Assert.IsFalse(10.0.NearlyGreaterThanOrEquals(11.0));
        }
    }
}
