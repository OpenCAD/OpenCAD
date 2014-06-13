using System;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface ILine:IGeometry
    {
        //Func<double, Vect3> Equation { get; }
    }

    public class Line : ILine
    {
        

        public Line(Point p1, Point p2)
        {
            throw new NotImplementedException();
        }
    }


}