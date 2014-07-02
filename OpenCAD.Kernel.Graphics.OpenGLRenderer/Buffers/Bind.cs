using System;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers
{
    public class Bind : IDisposable
    {
        private readonly IBindable _asset;
        public Bind(IBindable asset)
        {
            _asset = asset;
            _asset.Bind();
        }

        public void Dispose()
        {
            _asset.UnBind();
        }
        public static Bind Asset(IBindable asset)
        {
            return new Bind(asset);
        }
    }
}