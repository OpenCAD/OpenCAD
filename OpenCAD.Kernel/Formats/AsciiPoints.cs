using System;
using System.IO;
using System.Linq;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;
using OpenCAD.Kernel.Modelling;

namespace OpenCAD.Kernel.Formats
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
