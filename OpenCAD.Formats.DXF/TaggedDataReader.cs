using System;
using System.IO;

namespace OpenCAD.Formats.DXF
{
    class TaggedDataReader:IDisposable
    {

        private readonly TaggedDataEnumerator _enumerator;

        public TaggedDataReader(Stream stream)
        {

            _enumerator = new TaggedDataEnumerator(stream);
        }

        public IDXFTaggedData GetNext()
        {
            return _enumerator.MoveNext() ? _enumerator.Current : null;
        }

        public void Dispose()
        {
            _enumerator.Dispose();
        }
    }
}