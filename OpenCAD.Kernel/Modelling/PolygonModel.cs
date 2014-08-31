using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;

namespace OpenCAD.Kernel.Modelling
{
    //public class PolygonModel<T>:IModel
    //    where T : IPolygon
    //{
    //    public IList<T> Polygons { get; private set; }

    //    public PolygonModel(IList<T> polygons)
    //    {
    //        Polygons = polygons;
    //    }
    //}
    //public class PolygonModelOld : IModelOld
    //{
    //    public IList<IPolygon> Polygons { get; private set; }

    //    public PolygonModelOld(IList<IPolygon> polygons)
    //    {
    //        Polygons = polygons;
    //    }

    //    public Guid State { get; private set; }
    //    public void Regenerate()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class PolygonLayer: ILayer
    {
        public IList<IPolygon> Polygons { get; private set; }
        public PolygonLayer(IList<IPolygon> polygons)
        {
            Polygons = polygons;
        }
    }
}
