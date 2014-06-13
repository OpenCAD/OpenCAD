using System;

namespace OpenCAD.Kernel.Geometry
{
    public interface ICurve
    {
        Func<double, IPoint> Equation { get; }
    }
}