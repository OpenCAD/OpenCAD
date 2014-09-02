using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace OpenCAD.Formats.DXF.Sections
{
    public partial class HeaderSection : ISection
    {
        Dictionary<string, object> _dict = new Dictionary<string, object>();
        public IList<IHeaderVariable> HeaderVariables { get; private set; }

        internal HeaderSection(TaggedDataReader reader)
        {
            LoadVariables(reader);
        }

        //public override bool TryGetMember(GetMemberBinder binder, out object result)
        //{
        //    // Converting the property name to lowercase 
        //    // so that property names become case-insensitive. 
        //    string name = binder.Name.ToLower();

        //    // If the property name is found in a dictionary, 
        //    // set the result parameter to the property value and return true. 
        //    // Otherwise, return false. 
        //    return _dict.TryGetValue(name, out result);
        //}


        private void LoadVariables(TaggedDataReader reader)
        {
            IDXFTaggedData data;
            while ((data = reader.GetNext()) != null)
            {
                if (data.Code == 0 && data.Data == "ENDSEC")
                {
                    Console.WriteLine("Exit Header");
                    break;
                }
                Console.WriteLine(data.Data);
                

            }
        }

        public string Name { get; private set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class DXFObject : Attribute
    {
        public string Name { get; private set; }
        public int[] Group { get; private set; }

        public DXFObject(string name, params int[] group)
        {
            Name = name;
            Group = @group;
        }
    }
}