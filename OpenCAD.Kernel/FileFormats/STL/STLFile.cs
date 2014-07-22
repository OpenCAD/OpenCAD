using System;
using System.Collections.Generic;
using System.Linq;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Modelling;

namespace OpenCAD.Kernel.FileFormats.STL
{
    public class STLFile:IFileFormat
    {
        public IEnumerable<Triangle> Triangles { get; private set; }

        public STLFile()
            : this(Enumerable.Empty<Triangle>())
        {

        }

        public STLFile(IEnumerable<Triangle> triangles)
        {
            Triangles = triangles;
        }

        //public PolygonModel ToPolygonModel()
        //{
        //    throw new Exception();
        //}
    }
}
