using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.FileFormats.STL
{
    public class STLReader : IFileReader<STLFile>
    {
        public STLFile Read(string path)
        {
            //TODO need a better checking method
            using(var stream = File.Open(path, FileMode.Open))
            using (var sr = new StreamReader(stream))
            {
                var line = sr.ReadLine();
                if (line != null && Regex.Match(line, "^solid \\S+$").Success)
                {
                    return new STLFile(ReadASCII(stream).ToList());
                }
                return new STLFile(ReadBinary(stream).ToList());
            }
        }

        private IEnumerable<Triangle> ReadBinary(Stream stream)
        {
            stream.Position = 0;
            using (var br = new BinaryReader(stream))
            {
                br.ReadBytes(80); //header
                var count = (int)br.ReadUInt32();
                for (var i = 0; i < count; i++)
                {
                    var normal = new Vect3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    var p1 = new Vect3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    var p2 = new Vect3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    var p3 = new Vect3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    br.ReadUInt16(); //attrib
                    yield return new Triangle(new Point(p1), new Point(p2), new Point(p3), normal);
                }
            }
        }

        private IEnumerable<Triangle> ReadASCII(Stream stream)
        {
            stream.Position = 0;
            Vect3 normal = null;
            var points = new Vect3[3];
            int i = 0;
            const NumberStyles style = NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.Number;
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Trim().ToLower().Split(' ');
                    switch (split[0])
                    {
                        case "solid":
                            break;
                        case "facet":
                            normal = new Vect3(Double.Parse(split[2], style), Double.Parse(split[3], style),
                                               Double.Parse(split[4], style));
                            break;
                        case "outer":
                            break;
                        case "vertex":
                            points[i++] = new Vect3(Double.Parse(split[1], style), Double.Parse(split[2], style), Double.Parse(split[3], style));
                            break;
                        case "endloop":
                            break;
                        case "endfacet":
                            yield return new Triangle(new Point( points[0]), new Point( points[1]), new Point( points[2]), normal );
                            i = 0;
                            break;
                        case "endsolid":
                            break;
                    }
                }
            }

        }
    }
}
