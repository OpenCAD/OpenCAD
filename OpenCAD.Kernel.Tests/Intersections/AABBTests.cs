using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Intersections;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Tests.Intersections
{
    [TestClass]
    public class AABBTests
    {
        [TestMethod]
        public void AABBandAABBOnOrigin()
        {
            var a = new AABB(Vect3.Zero, 5.0);
            var b = new AABB(Vect3.Zero, 5.0);
            Assert.IsTrue(a.Overlaps(b));
            Assert.IsTrue(b.Overlaps(a));
        }

        [TestMethod]
        public void AABBandAABBJustTouching()
        {
            var a = new AABB(Vect3.Zero, 5.0);
            var b = new AABB(new Vect3(5, 0, 0), 5.0);
            var c = new AABB(new Vect3(5, 5, 0), 5.0);
            Assert.IsFalse(a.Overlaps(b));
            Assert.IsFalse(a.Overlaps(c));
        }

        [TestMethod]
        public void AABBandAABBNotTouching()
        {
            var a = new AABB(Vect3.Zero, 5.0);
            var x = new AABB(new Vect3(20, 0, 0), 5.0);
            var y = new AABB(new Vect3(0, 20, 0), 5.0);
            var z = new AABB(new Vect3(0, 0, 20), 5.0);
            Assert.IsFalse(a.Overlaps(x));
            Assert.IsFalse(a.Overlaps(y));
            Assert.IsFalse(a.Overlaps(z));
        }

        [TestMethod]
        public void AABBandAABBPartial()
        {
            var a = new AABB(Vect3.Zero, 5.0);
            var x = new AABB(new Vect3(2.5, 0, 0), 5.0);
            var y = new AABB(new Vect3(0, 2.5, 0), 5.0);
            var z = new AABB(new Vect3(0, 0, 2.5), 5.0);
            Assert.IsTrue(a.Overlaps(x));
            Assert.IsTrue(a.Overlaps(y));
            Assert.IsTrue(a.Overlaps(z));
        }


        [TestMethod]
        public void AABBandAABBOnOriginDifferentSizes()
        {
            var a = new AABB(Vect3.Zero, 1.0);
            var b = new AABB(Vect3.Zero, 5.0);
            Assert.IsTrue(a.Overlaps(b));
            Assert.IsTrue(b.Overlaps(a));
        }
    }
}