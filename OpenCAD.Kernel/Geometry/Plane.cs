using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Geometry
{
    public interface IPlane:IGeometry
    {
        Vect3 Normal { get; }
        Vect3 A { get; }
        Vect3 B { get; }

        Vect3 Origin { get; }
        double Distance { get; }

    }

}
