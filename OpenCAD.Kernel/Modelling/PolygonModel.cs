using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;

namespace OpenCAD.Kernel.Modelling
{
    public class PolygonModel:IModel
    {
        public IList<IPolygon> Polygons { get; private set; }

        public PolygonModel(IList<IPolygon> polygons)
        {
            Polygons = polygons;
        }
    }
}
