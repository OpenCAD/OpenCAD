using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Intersections;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Tests.Intersections
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void PointInAABBOnOrigin()
        {
            var aabb = new AABB(Vect3.Zero, 10.0);

            //Inside
            Assert.IsTrue(new Point(new Vect3(0, 0, 0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(2, 0, 0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(0, 2, 0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(0, 0, 2)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-2, 0, 0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(0, -2, 0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(0, 0, -2)).In(aabb));

            //On Surface
            Assert.IsTrue(new Point(new Vect3(5, 0, 0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(0, 5, 0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(0, 0, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-5, 0, 0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(0, -5, 0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(0, 0, -5)).In(aabb));

            //On Corners
            Assert.IsTrue(new Point(new Vect3(+5.0, +5.0, +5.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-5.0, +5.0, +5.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-5.0, -5.0, +5.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(+5.0, -5.0, +5.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(+5.0, +5.0, -5.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-5.0, +5.0, -5.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-5.0, -5.0, -5.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(+5.0, -5.0, -5.0)).In(aabb));

            // Out Of Surface
            Assert.IsFalse(new Point(new Vect3(6, 0, 0)).In(aabb));
            Assert.IsFalse(new Point(new Vect3(0, 6, 0)).In(aabb));
            Assert.IsFalse(new Point(new Vect3(0, 0, 6)).In(aabb));
            Assert.IsFalse(new Point(new Vect3(-6, 0, 0)).In(aabb));
            Assert.IsFalse(new Point(new Vect3(0, -6, 0)).In(aabb));
            Assert.IsFalse(new Point(new Vect3(0, 0, -6)).In(aabb));
        }
        [TestMethod]
        public void PointInAABBWithOffset()
        {
            var aabb = new AABB(new Vect3(5, 5, 5), 10.0);

            //Inside
            Assert.IsTrue(new Point(new Vect3(5, 5, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(7, 5, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(5, 7, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(5, 5, 7)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(3, 5, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(5, 3, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(5, 5, 3)).In(aabb));

            //On Surface
            Assert.IsTrue(new Point(new Vect3(10, 5, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(5, 10, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(5, 5, 10)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(0, 5, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(5, 0, 5)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(5, 5, 0)).In(aabb));

            //On Corners
            Assert.IsTrue(new Point(new Vect3(+10.0, +10.0, +10.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-0.0, +10.0, +10.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-0.0, -0.0, +10.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(+10.0, -0.0, +10.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(+10.0, +10.0, -0.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-0.0, +10.0, -0.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(-0.0, -0.0, -0.0)).In(aabb));
            Assert.IsTrue(new Point(new Vect3(+10.0, -0.0, -0.0)).In(aabb));
        }
    }
}
