using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenCAD.Formats.STL;
using OpenCAD.Kernel.Formats;
using OpenCAD.Kernel.Geometry;

namespace OpenCAD.Kernel.Modelling
{

    public interface IModel
    {
        String Name { get; }
        IList<ILayer> Layers { get; } 

    }

    public abstract class BaseModel:IModel
    {
        public string Name { get; protected set; }
        public IList<ILayer> Layers { get; protected set; }
    }

    public class JSONModel : BaseModel
    {
        public JSONModel(string path)
        {
            dynamic json = JsonConvert.DeserializeObject(File.ReadAllText(path));
            Name = json.Name;

            Layers = new List<ILayer>() { new PolygonLayer(new STLFile(@"C:\temp\testelephant.stl").Facets.ToTriangles().ToList<IPolygon>()) };
        }
    }




    public interface ILayer
    {
        //IList<ILayerOption> LayerOptions { get; }
        //IList<ILayerAction> LayerAction { get; }

        //IList<IPipe> InputPipes { get; }
        //IList<IPipe> OutputPipes { get; }
        //input
        //o
    }

    public interface IPipe
    {
        
    }



    public interface ILayerOption
    {
        
    }

    public interface ILayerAction
    {
        
    }

    //public interface IModelOld
    //{
    //    Guid State { get; }
    //    void Regenerate();
    //}

    //public abstract class BaseModelOld : IModelOld
    //{
    //    public Guid State { get; private set; }

    //    public abstract void Regenerate();
    //}
}
