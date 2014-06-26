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
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Shaders;
using SharpGL.Version;
using SharpGL.WPF;
using Point = System.Drawing.Point;


namespace OpenCAD.Kernel.Graphics.OpenGLRenderer
{
    public class OpenGLStaticRenderer:IStaticRenderer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private OpenGL gl = new OpenGL();
        ShaderProgram program = new ShaderProgram();
        public OpenGLStaticRenderer(int width, int height)
        {
            Width = width;
            Height = height;


            lock (gl)
            {
                //  Create OpenGL.
                gl.Create(OpenGLVersion.OpenGL2_1, RenderContextType.FBO, Width, Height, 32, null);
            }

            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.ClearDepth(1.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);


            gl.Enable(OpenGL.GL_DEPTH_TEST);

            float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };
            float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);

            gl.ShadeModel(OpenGL.GL_SMOOTH);

            //  Create a vertex shader.
            VertexShader vertexShader = new VertexShader();
            vertexShader.CreateInContext(gl);
            vertexShader.SetSource(
                "void main()" + Environment.NewLine +
                "{" + Environment.NewLine +
                "gl_Position = ftransform();" + Environment.NewLine +
                "}" + Environment.NewLine);

            //  Create a fragment shader.
            FragmentShader fragmentShader = new FragmentShader();
            fragmentShader.CreateInContext(gl);
            fragmentShader.SetSource(
                "void main()" + Environment.NewLine +
                "{" + Environment.NewLine +
                "gl_FragColor = vec4(0.4,0.4,0.8,1.0);" + Environment.NewLine +
                "}" + Environment.NewLine);

            //  Compile them both.
            vertexShader.Compile();
            fragmentShader.Compile();

            //  Build a program.
            program.CreateInContext(gl);

            //  Attach the shaders.
            program.AttachShader(vertexShader);
            program.AttachShader(fragmentShader);
            program.Link();
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
        float rotation = 0;
        public Image Render()
        {
            gl.MakeCurrent();
            lock (gl)
            {
                gl.SetDimensions(Width, Height);

                //	Set the viewport.
                gl.Viewport(0, 0, Width, Height);

                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();

                // Calculate The Aspect Ratio Of The Window
                gl.Perspective(45.0f, Width / (float)Height, 0.1f, 100.0f);

                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
            }


            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(1.0f, 0.0f, 0.0f, 0.0f);
            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -6.0f);

            program.Push(gl, null);
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            Teapot tp = new Teapot();
            tp.Draw(gl, 14, 1, OpenGL.GL_FILL);

            rotation += 3.0f;
            program.Pop(gl, null);

            gl.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f,  string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", 444, 1000.0 / 444));
            gl.Flush();

            gl.Blit(IntPtr.Zero);
            var provider = gl.RenderContextProvider as FBORenderContextProvider;
            //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
            //  meaning the drawing comes out transparent.
            var newFormatedBitmapSource = new FormatConvertedBitmap();
            if (provider == null) return null;

            newFormatedBitmapSource.BeginInit();
            newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(provider.InternalDIBSection.HBitmap);
            newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
            newFormatedBitmapSource.EndInit();

            return BitmapFromSource(newFormatedBitmapSource);
        }

        private Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            MemoryStream outStream = new MemoryStream();
         
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
  
            return bitmap;
        }

        public void Dispose()
        {
            
        }
    }
}
