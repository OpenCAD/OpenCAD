using System;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface ILineSegment : IGeometry, ICurve
    {
        IPoint Start { get; }
        IPoint End { get; }
    }

    public class LineSegment : ILineSegment
    {
        public IPoint Start { get; private set; }
        public IPoint End { get; private set; }

        public Func<double, IPoint> Equation {
            get
            {
                return d => new Point(Start.Position.Lerp(End.Position, d));
            }
        }

        public LineSegment(IPoint start, IPoint end)
        {
            Start = start;
            End = end;
        }
    }
}
