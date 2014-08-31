using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace OpenCAD.Formats.STL
{
    public class STLFile
    {
        public IList<STLFacet> Facets { get; private set; }

        public STLFile()
        {
            Facets = new List<STLFacet>();
        }

        public STLFile(string path)
            : this(File.Open(path, FileMode.Open))
        {

        }

        public STLFile(Stream stream)
        {
            using(stream)
            using (var sr = new StreamReader(stream))
            {
                var line = sr.ReadLine();
                if (line != null && Regex.Match(line, "^solid \\S+$").Success)
                {
                    Facets = ReadASCII(stream).ToList();
                }
                Facets = ReadBinary(stream).ToList();
            }
        }

        private IEnumerable<STLFacet> ReadBinary(Stream stream)
        {
            stream.Position = 0;
            using (var br = new BinaryReader(stream))
            {
                br.ReadBytes(80); //header
                var count = (int)br.ReadUInt32();
                for (var i = 0; i < count; i++)
                {
                    yield return new STLFacet(br);
                }
            }
        }

        private IEnumerable<STLFacet> ReadASCII(Stream stream)
        {
            stream.Position = 0;
            var normal = new float[3];
            var vertices = new float[3,3];
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
                            normal[0] = float.Parse(split[2], style);
                            normal[1] = float.Parse(split[3], style);
                            normal[2] = float.Parse(split[4], style);
                            break;
                        case "outer":
                            break;
                        case "vertex":
                            vertices[i++,0] = float.Parse(split[1], style);
                            vertices[i++,1] = float.Parse(split[2], style);
                            vertices[i++,2] = float.Parse(split[3], style);
                            break;
                        case "endloop":
                            break;
                        case "endfacet":
                            yield return new STLFacet(normal, vertices);
                            i = 0;
                            break;
                        case "endsolid":
                            break;
                    }
                }
            }
        }

        public void Save()
        {
            
        }

        public void SaveToBinary()
        {
            
        }

        public void SaveToASCII()
        {
            
        }
    }
}