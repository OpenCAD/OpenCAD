using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCAD.Formats.DXF;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Graphics;
using OpenCAD.Kernel.Graphics.Backgrounds;
using OpenCAD.Kernel.Graphics.OpenGLRenderer;
using OpenCAD.Kernel.Maths;
using OpenCAD.Kernel.Modelling;
using OpenCAD.Kernel.Modelling.Octree;
using Point = OpenCAD.Kernel.Geometry.Point;

namespace OpenCAD.Test
{
    class Program
    {
        public static void Main(string[] args)
        {


            //test.dxf
   
            //var dxf = new DXFFile(@"C:\temp\test.dxf");
     
            


            //var f = new STLReader().Read(@"C:\Users\chris\Desktop\prt0001.stl");
            Console.ReadLine();
            return;
            
            //var t = new AsciiPoints("bunny.ascii");




            var size = 10.0;



            var points = new List<IPoint>
            {
                new Point(new Vect3(+size, +size, +size)),
                new Point(new Vect3(-size, +size, +size)),
                new Point(new Vect3(-size, -size, +size)),
                new Point(new Vect3(+size, -size, +size)),
                new Point(new Vect3(+size, +size, -size)),
                new Point(new Vect3(-size, +size, -size)),
                new Point(new Vect3(-size, -size, -size)),
                new Point(new Vect3(+size, -size, -size)),
            };


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //var oct = t.ToOctree(10);

            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedMilliseconds);
 


            //var points = new List<IPoint>
            //{
            //    new Point(Vect3.Zero),
            //    new ColorPoint(Vect3.UnitX, Color.SeaGreen),
            //    new ColorPoint(Vect3.UnitY, Color.RoyalBlue),
            //    new ColorPoint(Vect3.UnitZ, Color.Snow)
            //};


            using (var render = new OpenGLStaticRenderer(800, 600))
            {
                render.Text = "Testing";
                render.Render(scene =>
                {
                    scene.Background = new GradientBackground(Color.Red, Color.Blue, Color.Plum, Color.Aquamarine);

                    scene.Camera = new OrthographicCamera();

                    scene.Points.AddRange(points);


                }).Save("output.png");


                Process.Start(@"P:\OpenCAD\OpenCAD.Test\bin\Debug\output.png");
            }

            //var render = new OpenGLStaticRenderer(800, 600);

            //var t = render.Render();

            //t.Save("test2.png");

            Console.ReadLine();
        }
    }
}
