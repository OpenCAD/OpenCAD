using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Kernel.Modelling.Octree
{
    public class OctreeModel
    {
        public OctreeNode Root { get; private set; }
        public OctreeModel(OctreeNode root)
        {
            Root = root;
        }
    }
}
