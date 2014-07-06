using System.Collections.Generic;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface IAABB
    {
        Vect3 Min { get; }
        Vect3 Max { get; }

        Vect3 Center { get; }
        Vect3 HalfSize{ get; }

    }
}
