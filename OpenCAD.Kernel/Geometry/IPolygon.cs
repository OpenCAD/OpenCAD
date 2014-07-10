using System.Collections;
using System.Collections.Generic;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface IPolygon : IPolytope
    {
        //IPlane Plane { get; }
        IList<IPoint> Points { get; }
        IList<ILineSegment> Edges { get; }
        //bool IsConvex();
        //bool IsSimple();
        //bool IsConcave();
    }

    public abstract class BasePolygon:IPolygon
    {
        // IPlane Plane { get; private set; }

        public IList<IPoint> Points { get; protected set; }
        public IList<ILineSegment> Edges { get; protected set; }

        protected BasePolygon(IList<IPoint> points, IList<ILineSegment> edges)
        {
            Points = points;
            Edges = edges;
        }
    }

    public interface ITriangle : IPolygon
    {
        IPoint P1 { get; }
        IPoint P2 { get; }
        IPoint P3 { get; }
        Vect3 Normal { get; }
    }

    public class Triangle : BasePolygon, ITriangle
    {
        public IPoint P1 { get; private set; }
        public IPoint P2 { get; private set; }
        public IPoint P3 { get; private set; }
        public Vect3 Normal { get; private set; }

        public Triangle(IPoint p1, IPoint p2, IPoint p3, Vect3 normal)
            : base(new[] { p1, p2, p3 }, new ILineSegment[] { new LineSegment(p1, p2), new LineSegment(p2, p3), new LineSegment(p3, p1) })
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            Normal = normal;
        }
    }
}