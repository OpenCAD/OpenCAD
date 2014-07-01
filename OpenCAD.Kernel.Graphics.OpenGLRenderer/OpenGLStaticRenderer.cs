using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Shaders;
using SharpGL.Version;
using SharpGL.WPF;
using Color = System.Drawing.Color;


namespace OpenCAD.Kernel.Graphics.OpenGLRenderer
{
    public class OpenGLStaticRenderer:IStaticRenderer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private OpenGL gl = new OpenGL();

        public string Text { get; set; }
        //private PostProcesser _postProcesser;


        public OpenGLStaticRenderer(int width, int height)
        {
            Width = width;
            Height = height;
 
            Text = "";
            lock (gl)
            {
                //  Create OpenGL.
                gl.Create(OpenGLVersion.OpenGL2_1, RenderContextType.FBO, Width, Height, 32, null);
                gl.MakeCurrent();
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                gl.Enable(OpenGL.GL_CULL_FACE);
                gl.Enable(OpenGL.GL_DEPTH_TEST);
                gl.Enable(OpenGL.GL_BLEND);
                gl.Enable(OpenGL.GL_VERTEX_ARRAY);
                gl.Hint(HintTarget.LineSmooth, HintMode.Nicest);
                gl.Enable(OpenGL.GL_LINE_SMOOTH);
                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(BlendingSourceFactor.SourceAlpha, BlendingDestinationFactor.OneMinusSourceAlpha);
                gl.Enable(OpenGL.GL_MULTISAMPLE);
                gl.MinSampleShading(4.0f);
                gl.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Filled);

                //_postProcesser = new PostProcesser(gl, width, height);


                //var t = new ShaderBindable(gl, "Shaders/Background.vert", "Shaders/Background.frag");


            }
        }

        public Image Render(Action<IStaticScene> action)
        {
            gl.MakeCurrent();
            var scene = new StaticScene();
            action(scene);
            UpdateView(scene.Camera);
            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(1.0f, 0.0f, 0.0f, 0.0f);


            new BackgroundRenderer(gl,scene.Background).Render();

            new PointRenderer(gl,scene).Render();

            //new Teapot().Draw(gl, 14, 1, OpenGL.GL_FILL);

            if (!String.IsNullOrWhiteSpace(Text))
            {
                gl.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f, Text);
            }

            return Output();
        }

        private Image Output()
        {
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

            var outStream = new MemoryStream();
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(newFormatedBitmapSource));
            enc.Save(outStream);
            //Console.ReadLine();

            //gl.Flush();
            //Console.ReadLine();
            return new Bitmap(outStream);
        }

        private void UpdateView(ICamera camera)
        {

            lock (gl)
            {
                gl.SetDimensions(Width, Height);
                gl.Viewport(0, 0, Width, Height);
                //               gl.Perspective(45.0f, Width / (float)Height, 0.1f, 100.0f);
            }
        }

        public void Dispose()
        {
            
        }
    }


    public interface IBindable
    {
        void Bind();
        void UnBind();
    }

    public class PostProcesser
    {
        private readonly OpenGL _gl;
        private FBO _fbo;
        private ShaderBindable _flatProgram;

        private VAO _flat;
        private VBO _flatBuffer;

        private DateTime _start;
        public PostProcesser(OpenGL gl, int width, int height)
        {
            _gl = gl;
            _fbo = new FBO(gl, width, height);

            _flatProgram = new ShaderBindable(gl, "Shaders/Flat.vert", "Shaders/Flat.frag");





            _flat = new VAO(gl);
            _flatBuffer = new VBO(gl);
            using (new Bind(_flat))
            using (new Bind(_flatBuffer))
            {
                var flatData = new float[] { -1, -1, 1, -1, -1, 1, 1, 1, };
                _flatBuffer.Update(flatData, flatData.Length * sizeof(float));
                gl.EnableVertexAttribArray(0);
                gl.VertexAttribPointer(0, 2, OpenGL.GL_FLOAT, false, 0, new IntPtr(0));
                gl.BindVertexArray(0);
            }
            _start = DateTime.Now;
        }
        public void Resize(int width, int height)
        {
            _fbo.Resize(width, height);

        }
        public void Capture(Action func)
        {
            using (new Bind(_fbo))
            {
                func();
            }
        }

        public void Render()
        {
            using (new Bind(_flatProgram))
            using (new Bind(_flat))
            using (new Bind(_fbo.ColorTexture))
            {
                _flatProgram.Uniforms.offset = (float)((DateTime.Now.ToUniversalTime().Subtract(_start).TotalMilliseconds) / 1000.0 * 2 * Math.PI);
                _gl.DrawArrays(OpenGL.GL_TRIANGLE_STRIP, 0, 4);
            }
        }
    }
}
