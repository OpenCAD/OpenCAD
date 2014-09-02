using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace OpenCAD.Formats.DXF
{
    public static class Helper
    {
        private static readonly IList<int> StringCodes;
        private static IList<int> DoubleCodes;
        private static IList<int> Int16Codes;
        private static IList<int> Int32Codes;
        private static IList<int> Int64Codes;

        static Helper()
        {
            StringCodes = Seq(S(0, 9), S(100), S(102), S(105)).ToList();
            DoubleCodes = Seq(S(10, 39), S(40, 59), S(110, 149),S(210,239)).ToList();
            Int16Codes = Seq(S(60, 79), S(40, 59), S(170, 179), S(270, 279)).ToList();
            Int32Codes = Seq(S(90, 99)).ToList();
            Int64Codes = Seq(S(160, 169)).ToList();


        }

        private static Type SingleCode(int code)
        {
            if (StringCodes.Contains(code)) return typeof(string);
            if (DoubleCodes.Contains(code)) return typeof(double);
            if (Int16Codes.Contains(code)) return typeof(Int16);
            if (Int32Codes.Contains(code)) return typeof(Int32);
            if (Int64Codes.Contains(code)) return typeof(Int64);

            return typeof(IntPtr);
        }

        public static Type TypeStringFromJArray(JArray array)
        {
            return GroupCodeToType(array.Select(jv => (int) jv).ToArray());
        }
        public static Type GroupCodeToType(int[] codes)
        {
            if (codes.Length == 1) return SingleCode(codes.First());
            return typeof(IntPtr);
        }

        public static string TypeStringFromGroupCode(int[] codes)
        {

            return GroupCodeToType(codes).Name;
        }


        public static IEnumerable<int> Seq(params IEnumerable<int>[] p)
        {
            return p.SelectMany(n=>n);
        }

        public static IEnumerable<int> S(int n1, int n2)
        {
            while (n1 <= n2)
            {
                yield return n1++;
            }
        }
        public static IEnumerable<int> S(int n)
        {
            yield return n;
        }
    }

}
