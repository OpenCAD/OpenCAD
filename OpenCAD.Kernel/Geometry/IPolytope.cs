using System.Collections.Generic;

namespace OpenCAD.Kernel.Geometry
{
    public interface IPolytope : IGeometry
    {
        IList<IPoint> Points { get; }
        IList<ILineSegment> Edges { get; }

    }
}