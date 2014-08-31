using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Maths;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL.Buffers
{
    public class ShaderProgram:IBindable
    {
        public readonly int Handle;

        public Func<string, ShaderType> TypeDetector { get; set; }
        public dynamic Uniforms { get; private set; }

        public ShaderProgram(params string[] files)
        {
            TypeDetector = DefaultTypeDetector;
            Handle = GL.CreateProgram();
            foreach (var file in files)
            {
                var shader = GL.CreateShader(TypeDetector(file));
                var text = File.ReadAllText(file);
                var length = text.Length;
                GL.ShaderSource(shader, 1, new[] { text }, ref length);
                GL.CompileShader(shader);
                GL.AttachShader(Handle, shader);
            }
            GL.LinkProgram(Handle);

            Uniforms = new DynamicUniforms(this);
        }

        private static readonly Func<string, ShaderType> DefaultTypeDetector = s =>
        {
            var extension = Path.GetExtension(s);
            if (extension == null) throw new Exception("No extension found");
            switch (extension.TrimStart('.').ToLower())
            {
                case "vert":
                    return ShaderType.VertexShader;
                case "frag":
                    return ShaderType.FragmentShader;
                case "geo":
                    return ShaderType.GeometryShader;
                default:
                    throw new Exception("Wrong Extension");
            }
        };

        public void Bind()
        {
            GL.UseProgram(Handle);
        }

        public void UnBind()
        {
            GL.UseProgram(0);
        }
    }


    public class DynamicUniforms : DynamicObject
    {
        private readonly ShaderProgram _program;

        private Dictionary<string, int> _locations = new Dictionary<string, int>();

        internal DynamicUniforms(ShaderProgram program)
        {
            _program = program;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            int location;
            if (_locations.ContainsKey(binder.Name))
            {
                location = _locations[binder.Name];
            }
            else
            {
                location = GL.GetUniformLocation(_program.Handle, binder.Name);
                if (location == -1) return false;
                _locations.Add(binder.Name, location);
            }

            var type = value.GetType();
            if (type == typeof(Mat4))
            {
                GL.UniformMatrix4(location, 1, false, (value as Mat4).ToColumnMajorArrayFloat());
                return true;
            }
            if (type == typeof(float))
            {
                GL.Uniform1(location, (float)value);
                return true;
            }
            return false;
        }
    }
}
