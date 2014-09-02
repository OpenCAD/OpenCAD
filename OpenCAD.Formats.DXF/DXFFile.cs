using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenCAD.Formats.DXF.Properties;
using OpenCAD.Formats.DXF.Sections;

namespace OpenCAD.Formats.DXF
{
    public class DXFFile
    {
        
        public HeaderSection Header { get; private set; }

        public DXFFile()
        {

        }

        public DXFFile(string path)
            : this(File.Open(path, FileMode.Open))
        {

        }

        public DXFFile(Stream stream)
        {
            //using (var t = Resources.Schema)
            using (stream)
            using (var reader = new TaggedDataReader(stream))
            {



                //var test = JsonConvert.DeserializeObject();

                LoadSections(reader);
            }
        }
        void LoadSections(TaggedDataReader reader)
        {
            IDXFTaggedData data;
            while ((data = reader.GetNext()) != null)
            {
                if (data.Code == 0 && data.Data == "SECTION")
                {
                    var section = reader.GetNext();

                    if (section.Code == 2)
                    {
                        switch (section.Data)
                        {
                            case "HEADER":
                                Header = new HeaderSection(reader);
                                //Header = new DXFHeader(_reader);
                                break;
                            case "CLASSES":
                                Console.WriteLine(section.Data);
                                break;
                            case "TABLES":
                                Console.WriteLine(section.Data);
                                break;
                            case "BLOCKS":
                                Console.WriteLine(section.Data);
                                break;
                            case "ENTITIES":
                                Console.WriteLine(section.Data);
                                break;
                            case "OBJECTS":
                                Console.WriteLine(section.Data);
                                break;
                        }
                    }
                }
            }
        }
    }
}
