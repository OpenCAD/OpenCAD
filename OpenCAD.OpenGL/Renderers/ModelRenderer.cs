using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel;
using OpenCAD.Kernel.Application;
using OpenCAD.Kernel.Maths;
using OpenCAD.Kernel.Modelling;
using OpenCAD.OpenGL.Buffers;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL.Renderers
{
    public class ModelRenderer
    {
        private bool _loaded = false;


        private Mat4 cam = Mat4.Scale(0.1);

        private IList<ILayerRenderer> _layerRenderers = new List<ILayerRenderer>();

        public ModelRenderer()
        {
            
        }

        public void Render(Scene scene)
        {

            if (!_loaded)
            {
                var groups = scene.Model.Layers.GroupBy(layer => layer.GetType());


                var polygons = scene.Model.Layers.OfType<PolygonLayer>();
                if (polygons.Any()) _layerRenderers.Add(new PolygonLayerRenderer(polygons));
                _loaded = true;
            }

            if (_loaded)
            {
                foreach (var renderer in _layerRenderers)
                {
                    renderer.Render(scene);
                }
            }

            //if (!_loaded)
            //{
            //    var polygonModel = scene.Model as PolygonModelOld;
            //    if (polygonModel != null)
            //    {
            //        _program = new ShaderProgram("Shaders/PolygonModel.vert", "Shaders/PolygonModel.frag");

            //        _vao = new VAO();
            //        var flatBuffer = new VBO();

            //        using (Bind.These(_vao, flatBuffer))
            //        {

            //            var data = new List<float>();

            //            foreach (var edge in polygonModel.Polygons.SelectMany(p=>p.Edges))
            //            {
            //                count++;
            //                data.AddRange(edge.Start.Position.ToArray().Select(d=>(float)d));
            //                data.AddRange(edge.Start.Color.ToFloatArray());
            //                data.AddRange(edge.End.Position.ToArray().Select(d=>(float)d));
            //                data.AddRange(Color.Aqua.ToFloatArray());
            //            }

            //            var flatData = data.ToArray();
            //            flatBuffer.Update(flatData, flatData.Length * sizeof(float));

            //            const int stride = sizeof(float) * 7;

            //            GL.EnableVertexAttribArray(0);
            //            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, new IntPtr(0));
            //            GL.EnableVertexAttribArray(1);
            //            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, stride, new IntPtr(sizeof(float) * 3));
            //        }
            //        using (new Bind(_program))

            //        _loaded = true;
            //    }
            //}

            //if (_loaded)
            //{
            //    using (new Bind(_program))
            //    using (new Bind(_vao))
            //    {
            //        _program.Uniforms.MVP = scene.Camera.MVP;
            //        GL.Disable(EnableCap.DepthTest);
            //        GL.DrawArrays(BeginMode.Lines, 0, count * 2);
            //        GL.Enable(EnableCap.DepthTest);
            //    }
            //}
        }
    }

    public interface ILayerRenderer
    {
        void Render(IScene scene);
    }

    class PolygonLayerRenderer:ILayerRenderer
    {
        private ShaderProgram _program;

        private VAO _vao;
        private int count = 0;
        public PolygonLayerRenderer(IEnumerable<PolygonLayer> polygons)
        {
            _program = new ShaderProgram("Shaders/PolygonModel.vert", "Shaders/PolygonModel.frag");
            var polygonModel = polygons.First();
            _vao = new VAO();
            var flatBuffer = new VBO();

            using (Bind.These(_vao, flatBuffer))
            {

                var data = new List<float>();

                foreach (var edge in polygonModel.Polygons.SelectMany(p => p.Edges))
                {
                    count++;
                    data.AddRange(edge.Start.Position.ToArray().Select(d => (float)d));
                    data.AddRange(edge.Start.Color.ToFloatArray());
                    data.AddRange(edge.End.Position.ToArray().Select(d => (float)d));
                    data.AddRange(Color.Aqua.ToFloatArray());
                }

                var flatData = data.ToArray();
                flatBuffer.Update(flatData, flatData.Length * sizeof(float));

                const int stride = sizeof(float) * 7;

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, new IntPtr(0));
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, stride, new IntPtr(sizeof(float) * 3));
            }
        }

        public void Render(IScene scene)
        {
            using (new Bind(_program))
            using (new Bind(_vao))
            {
                _program.Uniforms.MVP = scene.Camera.MVP;
                GL.Disable(EnableCap.DepthTest);
                GL.DrawArrays(BeginMode.Lines, 0, count * 2);
                GL.Enable(EnableCap.DepthTest);
            }
            }
        }
    }
