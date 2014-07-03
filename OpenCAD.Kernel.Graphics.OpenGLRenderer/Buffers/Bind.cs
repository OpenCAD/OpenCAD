using System;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers
{
    public class Bind : IDisposable
    {
        private readonly IBindable[] _assets;
        public Bind(params IBindable[] assets)
        {
            _assets = assets;
            foreach (var asset in _assets)
            {
                asset.Bind();
            }
            
        }
        

        public void Dispose()
        {
            foreach (var asset in _assets)
            {
                asset.UnBind();
            }
            
        }
        public static Bind These(params IBindable[] assets)
        {
            return new Bind(assets);
        }

        public static Bind This(IBindable asset)
        {
            return new Bind(asset);
        }
    }
}