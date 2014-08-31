using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Formats.STL
{
    public static class Extensions
    {
        public static IEnumerable<float> ReadSingles(this BinaryReader reader, uint length)
        {
            for (var i = 0; i < length; i++)
            {
                yield return reader.ReadSingle();
            }
        }
    }
}
