using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL.Buffers
{
    public class Texture:IBindable
    {
        private readonly uint _handle;
        private TextureTarget _target = TextureTarget.Texture2D;

        public Texture(Bitmap bmp)
        {
            GL.GenTextures(1, out _handle);
            Update(bmp);
            using (new Bind(this))
            {
                GL.TexParameter(_target, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(_target, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
        }
        
        public void Update(Bitmap bitmap)
        {
            using (new Bind(this))
            {
                var data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(_target, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, Pencil.Gaming.Graphics.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            }
        }

        public void Update(byte[] data, int width, int height)
        {
            using (new Bind(this))
            {
                GL.TexImage2D(_target, 0, PixelInternalFormat.Rgba, width, height, 0, Pencil.Gaming.Graphics.PixelFormat.Bgra, PixelType.UnsignedByte, data);
            }
        }

        public void Bind()
        {
            GL.BindTexture(_target, _handle);
        }

        public void UnBind()
        {
            GL.BindTexture(_target, 0);
        }

    }
}
