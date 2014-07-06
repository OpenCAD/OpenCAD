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
        public List<IPoint> Points { get; protected set; }

        public PointCloud(List<IPoint> points)
        {
            Points = points;
        }

        protected PointCloud()
        {
            Points = new List<IPoint>();
        }
    }

    public interface IPointCloud
    {
        List<IPoint> Points { get; }
    }
}
