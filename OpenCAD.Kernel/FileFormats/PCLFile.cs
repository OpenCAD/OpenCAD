using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.FileFormats
{
    public class PCLFile
    {
        public string Version { get; private set; }
        public IReadOnlyDictionary<string,Type> Fields { get; private set; }

        public int Count { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float[] Viewpoint { get; private set; }
        public int NumPoints { get; private set; }
        public PCLType Data { get; private set; }

        private readonly string[] _entries =
        {
            "VERSION",
            "FIELDS",
            "SIZE",
            "TYPE",
            "COUNT",
            "WIDTH",
            "HEIGHT",
            "VIEWPOINT",
            "POINTS",
            "DATA"
        };

  
        public PCLFile(string path)
        {
            throw new NotImplementedException();
            var lines = File.ReadLines(path);
            var headerLines = new Dictionary<string,string[]>();

            foreach (var line in lines.Select(l=>l.Trim()))
            {
                if(line[0] == '#')
                    continue;
                var split = line.Split(' ');
                if (_entries.Contains(split[0]))
                {
                    headerLines.Add(split[0], split);
                }
                else
                {
                    ParseHeader(headerLines);


                }
            }
        }

        private void ParseHeader(Dictionary<string, string[]> lines)
        {
            var types = new Dictionary<string, Dictionary<int, Type>>()
            {
                {
                    "I", new Dictionary<int, Type>
                    {
                        {1, typeof (byte)},
                        {2, typeof (short)},
                        {4, typeof (int)},
                    }
                },
                {
                    "U", new Dictionary<int, Type>
                    {
                        {1, typeof (byte)},
                        {2, typeof (ushort)},
                        {4, typeof (uint)},
                    }
                },
                {
                    "F", new Dictionary<int, Type>
                    {
                        {4, typeof (float)},
                        {8, typeof (double)},
                    }
                }
            };


            try
            {
                if (lines.ContainsKey("VERSION"))
                {
                    Version = lines["VERSION"][1];
                }
                if (lines.ContainsKey("FIELDS") && lines.ContainsKey("SIZE") && lines.ContainsKey("TYPE")) //TODO COUNT
                {
                    Fields = lines["FIELDS"].Skip(1).Zip(lines["SIZE"].Skip(1).Zip(lines["TYPE"].Skip(1), (b, c) => new {b, c}),(a, b) => new {Field = a, Type = types[b.c][int.Parse(b.b)]}).ToDictionary(x => x.Field, x => x.Type);
                }
                if (lines.ContainsKey("COUNT"))
                {
                    Count = int.Parse(lines["COUNT"][1]);
                }
                if (lines.ContainsKey("WIDTH"))
                {
                    Width = int.Parse(lines["WIDTH"][1]);
                }
                if (lines.ContainsKey("HEIGHT"))
                {
                    Height = int.Parse(lines["HEIGHT"][1]);
                }

                if (lines.ContainsKey("VIEWPOINT"))
                {
                    Viewpoint = lines["VIEWPOINT"].Skip(1).Select(float.Parse).ToArray();
                }
                if (lines.ContainsKey("POINTS"))
                {
                    NumPoints = int.Parse(lines["POINTS"][1]);
                }
                if (lines.ContainsKey("DATA"))
                {
                    
                }
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }




  
    }

    public enum PCLType
    {
        Ascii,
        Binary
    };

    public class PCLPoint : IPoint
    {
        public Vect3 Position { get; private set; }
        public Color Color { get; private set; }
        public Vect3 Normal { get; private set; }
    }
}
