using System.Runtime.InteropServices;
using Awesomium.Core.Data;

namespace OpenCAD.Awesomium
{
    public class LocalDataSource : DataSource {
        protected override void OnRequest(DataSourceRequest request)
        {
            var content = "<h1>Hello World</h1>";
            var ptr = Marshal.StringToHGlobalUni(content);
            SendResponse(request, new DataSourceResponse() { Buffer = ptr, MimeType = "text/html" , Size = (uint) content.Length});
            Marshal.FreeHGlobal(ptr);

        }
    }
}