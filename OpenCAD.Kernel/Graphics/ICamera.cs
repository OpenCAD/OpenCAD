using System;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel.Graphics
{
    public interface ICamera
    {
        Mat4 Model { get; set; }
        Mat4 View { get; set; }
        Mat4 Projection { get; set; }
        Mat4 MVP { get; }
        void Resize(int width, int height);
    }

    public abstract class BaseCamera : ICamera
    {
        public Mat4 Model { get; set; }
        public Mat4 View { get; set; }
        public Mat4 Projection { get; set; }
        public Mat4 MVP
        {
            get { return Mat4.Scale(0.05); }
            //get { return Projection * View * Model; }
             //get { return Model * View * Projection; }
        }
        public abstract void Resize(int width, int height);

        public double Near { get; protected set; }
        public double Far { get; protected set; }
    }

    public class OrthographicCamera : BaseCamera
    {
        public double Scale { get; set; }
        public Vect3 Eye { get; set; }
        public Vect3 Target { get; set; }
        public Vect3 Up { get; set; }
        private float _dist = 5;
        public OrthographicCamera()
        {
            Near = 1;
            Far = 40.0;
            Target = Vect3.Zero;
            Up = Vect3.UnitY;
            Eye = new Vect3(0, 0, -_dist);
            Model = Mat4.Identity;

            View = Mat4.LookAt(Eye, Target, Up);

            Projection = Mat4.Identity;
            Scale = 1;
        }
        public override void Resize(int width, int height)
        {
            //Projection = Mat4.CreatePerspective(Math.PI / 4, width / (float)height, 1f, 2 * _dist);
            Projection = Mat4.CreatePerspective(Math.PI / 4, width / (float)height, 1f, 100.0f);
            //Projection = Mat4.CreateOrthographic(-width / 2.0, width / 2.0, -height / 2.0, height / 2.0, Near, Far) * Mat4.Scale(Scale);
        }
    }
}