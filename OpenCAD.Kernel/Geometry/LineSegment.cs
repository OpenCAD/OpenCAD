using System;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface ILineSegment:IGeometry
    {
        IPoint Start { get; }
        IPoint End { get; }
        Func<double, IPoint> Equation { get; }
    }
    public class LineSegment : ILineSegment
    {
        public IPoint Start { get; private set; }
        public IPoint End { get; private set; }

        public Func<double, IPoint> Equation {
            get
            {
                return d => new Point(Vect3.Zero);
            }
        }

        public LineSegment(IPoint start, IPoint end)
        {
            Start = start;
            End = end;
            

        }
    }
}
