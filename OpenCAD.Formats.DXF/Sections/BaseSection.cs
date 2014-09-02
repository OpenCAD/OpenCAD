using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Formats.DXF.Sections
{
    public interface ISection
    {
        string Name { get; }
    }

    public abstract class BaseSection : ISection
    {
        public string Name { get; protected set; }

        protected BaseSection(string name)
        {
            Name = name;
        }
    }

    public interface IHeaderVariable
    {
        string Name { get; }
    }
}
