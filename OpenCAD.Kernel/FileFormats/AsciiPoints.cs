using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;
using OpenCAD.Kernel.Modelling;

namespace OpenCAD.Kernel.FileFormats
{
    public class AsciiPoints:PointCloud
    {
        public AsciiPoints(string path)
        {
            try
            {
                Points.AddRange(File.ReadLines(path).Select(line => new Point(new Vect3(line.Split(' ').Select(Double.Parse).ToList()))).ToList());
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
