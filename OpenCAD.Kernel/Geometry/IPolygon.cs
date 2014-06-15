using System.Collections;
using System.Collections.Generic;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface IPolygon:IGeometry
    {
         IList<ILineSegment> Edges { get; }
         IList<IPoint> Points { get; }
    }

    public interface ITriangle:IPolygon
    {
        Vect3 P1 { get; }
        Vect3 P2 { get; }
        Vect3 P3 { get; }
        Vect3 Normal { get; }
    }

    public class Triangle:ITriangle
    {
        public IList<ILineSegment> Edges { get; private set; }
        public IList<IPoint> Points { get; private set; }

        public Vect3 P1 { get; private set; }
        public Vect3 P2 { get; private set; }
        public Vect3 P3 { get; private set; }
        public Vect3 Normal { get; private set; }
    }
}