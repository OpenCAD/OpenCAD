using System;

namespace OpenCAD.OpenGL
{

    public interface IBindable
    {
        void Bind();
        void UnBind();
    }

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
    }
}