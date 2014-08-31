using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Formats.STL;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Formats
{
    public static class Extensions
    {


        public static Triangle ToTriangle(this STLFacet facet)
        {
            return new Triangle(new Point(new Vect3(facet.Vertex1)),new Point(new Vect3(facet.Vertex2)),new Point(new Vect3(facet.Vertex3)),new Vect3(facet.Normal));
        }

        public static IEnumerable<Triangle> ToTriangles(this IEnumerable<STLFacet> facets)
        {
            return facets.Select(f => f.ToTriangle());
        }
    }
}
