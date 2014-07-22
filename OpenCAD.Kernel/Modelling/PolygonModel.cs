using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;

namespace OpenCAD.Kernel.Modelling
{
    public class PolygonModel<T>:IModel
        where T : IPolygon
    {
        public IList<T> Polygons { get; private set; }

        public PolygonModel(IList<T> polygons)
        {
            Polygons = polygons;
        }
    }
}
