using System;

namespace OpenCAD.Formats.DXF
{
    class DXFTaggedData : IDXFTaggedData
    {
        public int Code { get; private set; }
        public string Data { get; private set; }
        public DXFTaggedData(int code, string data)
        {
            Code = code;
            Data = data;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Code, Data);
        }
    }
}