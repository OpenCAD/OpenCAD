using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pencil.Gaming.Graphics;

namespace OpenCAD.OpenGL.Buffers
{
    public class ShaderProgram:IBindable
    {
        private readonly int _shaderProgram;

        public Func<string, ShaderType> TypeDetector { get; set; }

        public ShaderProgram(params string[] files)
        {
            TypeDetector = DefaultTypeDetector;
            _shaderProgram = GL.CreateProgram();
            foreach (var file in files)
            {
                var shader = GL.CreateShader(TypeDetector(file));
                var text = File.ReadAllText(file);
                var length = text.Length;
                GL.ShaderSource(shader, 1, new[] { text }, ref length);
                GL.CompileShader(shader);
                GL.AttachShader(_shaderProgram, shader);
            }
            GL.LinkProgram(_shaderProgram);
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
            GL.UseProgram(_shaderProgram);
        }

        public void UnBind()
        {
            GL.UseProgram(0);
        }
    }
}
