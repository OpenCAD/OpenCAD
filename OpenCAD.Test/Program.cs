using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCAD.Kernel.FileFormats;
using OpenCAD.Kernel.Graphics;
using OpenCAD.Kernel.Graphics.Backgrounds;
using OpenCAD.Kernel.Graphics.OpenGLRenderer;
using OpenCAD.Kernel.Maths;
using Point = OpenCAD.Kernel.Geometry.Point;

namespace OpenCAD.Test
{
    class Program
    {
        public static void Main(string[] args)
        {
            //var t = new AsciiPoints("bunny.ascii");

            var points = new List<Point>();
            points.Add(new Point(Vect3.Zero));


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


        }
    }
}
