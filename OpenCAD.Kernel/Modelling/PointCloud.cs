using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Modelling
{
    public class PointCloud:IPointCloud
    {
        public IList<IPoint> Points { get; private set; }

        public PointCloud(IList<IPoint> points)
        {
            Points = points;
        }

        public void ConvexHull()
        {
            throw new Exception();
        }

    }

    public interface IPointCloud
    {
        IList<IPoint> Points { get; }
    }
}
