using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using OpenCAD.Kernel.Maths;
using SharpGL;
using SharpGL.SceneGraph.Shaders;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer
{
    public class ShaderBindable : IBindable
    {
        private readonly OpenGL _gl;
        public dynamic Uniforms { get; private set; }

        protected ShaderProgram _program;

        public ShaderBindable(OpenGL gl, params string[] files):
            this(gl, DefaultTypeDetector,files)
        {

        }

        public ShaderBindable(OpenGL gl, Func<string, Shader> typeDetector, params string[] files)
        {
            _gl = gl;
            if (typeDetector == null) throw new ArgumentNullException("typeDetector");

            _program = new ShaderProgram();
            _program.CreateInContext(gl);

            foreach (var file in files)
            {
                var shader = typeDetector(file);
                shader.CreateInContext(gl);
                shader.SetSource(File.ReadAllText(file));
                shader.Compile();
                _program.AttachShader(shader);
            }
            _program.Link();

            
            if (_program.LinkStatus != null && !(bool) _program.LinkStatus)
            {
                throw new Exception(_program.InfoLog);
            }

            foreach (var attachedShader in _program.AttachedShaders.Where(attachedShader => attachedShader.CompileStatus != null && !(bool)attachedShader.CompileStatus))
            {
                throw new Exception(attachedShader.InfoLog);
            }

            Uniforms = new DynamicUniforms(_gl, _program);
        }


        private static readonly Func<string, Shader> DefaultTypeDetector = s =>
        {
            var extension = Path.GetExtension(s);
            if (extension == null) throw new Exception("No extension found");
            switch (extension.TrimStart('.').ToLower())
            {
                case "vert":
                    return new VertexShader();
                case "frag":
                    return new FragmentShader();
                case "geo":
                    return new GeometryShader();
                default:
                    throw new Exception("Wrong Extension");
            }
        };

        public void Bind()
        {
            _program.Push(_gl, null);
        }

        public void UnBind()
        {
            _program.Pop(_gl, null);
        }
    }


    public class DynamicUniforms:DynamicObject
    {
        private readonly OpenGL _gl;
        private readonly ShaderProgram _program;

        private Dictionary<string, int> _locations = new Dictionary<string, int>(); 

        internal DynamicUniforms(OpenGL gl, ShaderProgram program)
        {
            _gl = gl;
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
                location = _gl.GetUniformLocation(_program.ProgramObject, binder.Name);
                if (location == -1) return false;
                _locations.Add(binder.Name, location);
            }

            var type = value.GetType();
            if (type == typeof(Mat4))
            {
                _gl.UniformMatrix4(location, 1, false, (value as Mat4).ToColumnMajorArrayFloat());
                return true;
            }
            if (type == typeof(float))
            {
                _gl.Uniform1(location, (float) value);
                return true;
            }
            return false;
        }
    }
}