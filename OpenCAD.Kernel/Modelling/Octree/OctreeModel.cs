using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Kernel.Modelling.Octree
{
    public class OctreeModel
    {
        public IOctreeNode Root { get; private set; }
        public OctreeModel(IOctreeNode root)
        {
            Root = root;
        }
    }
}
