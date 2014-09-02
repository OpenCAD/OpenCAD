using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace OpenCAD.Formats.DXF
{
    class TaggedDataEnumerator : IEnumerator<IDXFTaggedData>
    {
        private readonly StreamReader _reader;
        public IDXFTaggedData Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public TaggedDataEnumerator(Stream stream)
        {
            _reader = new StreamReader(stream);
            Current = null;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public bool MoveNext()
        {
            int code;
            var intparse = int.TryParse(_reader.ReadLine(), out code);
            var data = _reader.ReadLine();
            Current = new DXFTaggedData(code, data);
            return intparse;
        }

        public void Reset()
        {
            _reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }
    }
}