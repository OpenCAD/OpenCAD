using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.RenderContextProviders;
using SharpGL.Version;
using SharpGL.WPF;
using PixelFormat = System.Windows.Media.PixelFormat;
using Point = System.Drawing.Point;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer
{
    public class OpenGLStaticRenderer:IStaticRenderer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private OpenGL GL;
        public OpenGLStaticRenderer(int width, int height)
        {
            Width = width;
            Height = height;
            GL = new OpenGL();
            GL.Create(OpenGLVersion.OpenGL4_1, RenderContextType.FBO, Width, Height, 32, null);
            GL.MakeCurrent();

            GL.ShadeModel(OpenGL.GL_SMOOTH);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.ClearDepth(1.0f);
            GL.Enable(OpenGL.GL_DEPTH_TEST);
            GL.DepthFunc(OpenGL.GL_LEQUAL);
            GL.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);

            //GL.Enable(OpenGL.GL_TEXTURE_2D);
            //GL.Enable(OpenGL.GL_CULL_FACE);
            //GL.Enable(OpenGL.GL_DEPTH_TEST);
            //GL.Enable(OpenGL.GL_BLEND);
            //GL.Enable(OpenGL.GL_VERTEX_ARRAY);

            //GL.Hint(HintTarget.LineSmooth, HintMode.Nicest);
            //GL.Enable(OpenGL.GL_LINE_SMOOTH);
            //GL.Enable(OpenGL.GL_BLEND);
            //GL.BlendFunc(BlendingSourceFactor.SourceAlpha, BlendingDestinationFactor.OneMinusSourceAlpha);
            //GL.Enable(OpenGL.GL_MULTISAMPLE);
            //GL.MinSampleShading(4.0f);

            //GL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Filled);
            //GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);

        }

        public Image Render()
        {
            GL.MakeCurrent();
            GL.SetDimensions(Width, Height);
            GL.Viewport(0, 0, Width, Height);

            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            GL.ClearColor(1f, 0.0f, 0f, 0f);



            //GL.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f, string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS",4,4));
            GL.Flush();

            GL.Blit(IntPtr.Zero);


            var provider = GL.RenderContextProvider as FBORenderContextProvider;
            if (provider == null) return null;

            var newFormatedBitmapSource = new FormatConvertedBitmap();
            newFormatedBitmapSource.BeginInit();
            newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(provider.InternalDIBSection.HBitmap);
            newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
            newFormatedBitmapSource.EndInit();

            return BitmapFromSource(newFormatedBitmapSource);
        }

        private System.Drawing.Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            MemoryStream outStream = new MemoryStream();
         
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
  
            return bitmap;
        }

        public static System.Drawing.Bitmap BitmapSourceToBitmap2(BitmapSource srs)
        {
            int width = srs.PixelWidth;
            int height = srs.PixelHeight;
            int stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(height * stride);
                srs.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
                using (var btm = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppRgb, ptr))
                {
                    return new System.Drawing.Bitmap(btm);
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }

        Bitmap GetBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap(source.PixelWidth,source.PixelHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(
              new Rectangle(Point.Empty, bmp.Size),
              ImageLockMode.WriteOnly,
              System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            source.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
        public void Dispose()
        {
            
        }
    }
}
