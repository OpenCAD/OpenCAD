using System;
using System.Runtime.InteropServices;
using SharpGL;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers
{
    public interface IBuffer : IBindable
    {

    }
    public class Bind : IDisposable
    {
        private readonly IBindable _asset;
        public Bind(IBindable asset)
        {
            _asset = asset;
            _asset.Bind();
        }

        public void Dispose()
        {
            _asset.UnBind();
        }
        public static Bind Asset(IBindable asset)
        {
            return new Bind(asset);
        }
    }
    public class Texture : IBindable
    {
        private readonly OpenGL _gl;


        public uint Handle { get { return _texture[0]; } }
        readonly uint[] _texture = new uint[1];
        public Texture(OpenGL gl)
        {
            _gl = gl;
            _gl.GenTextures(1, _texture);
        }

        public void Bind()
        {
            _gl.BindTexture(OpenGL.GL_TEXTURE_2D, Handle);
        }

        public void UnBind()
        {
            _gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }
    }
    public class FBO : IBuffer
    {
        private readonly OpenGL _gl;
        readonly uint[] _fbo = new uint[1];
        public Texture ColorTexture;
        public Texture DepthTexture;
        public FBO(OpenGL gl, int width, int height)
        {
            _gl = gl;
            if (width < 1) width = 16;
            if (height < 1) height = 16;
            gl.GenFramebuffersEXT(1, _fbo);

            ColorTexture = new Texture(gl);
            using (new Bind(ColorTexture))
            {
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
                gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGBA, width, height, 0, OpenGL.GL_RGBA, OpenGL.GL_UNSIGNED_BYTE, null);
            }

            DepthTexture = new Texture(gl);
            using (new Bind(DepthTexture))
            {
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_DEPTH_TEXTURE_MODE_ARB, OpenGL.GL_INTENSITY);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_COMPARE_MODE, OpenGL.GL_COMPARE_R_TO_TEXTURE_ARB);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_COMPARE_FUNC, OpenGL.GL_LEQUAL);
                gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_DEPTH_COMPONENT24, width, height, 0, OpenGL.GL_DEPTH_COMPONENT, OpenGL.GL_UNSIGNED_BYTE, null);
            }

            using (new Bind(this))
            {
                gl.FramebufferTexture2DEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_TEXTURE_2D, ColorTexture.Handle, 0);
                gl.FramebufferTexture2DEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_TEXTURE_2D, DepthTexture.Handle, 0);
                var status = _gl.CheckFramebufferStatusEXT(OpenGL.GL_FRAMEBUFFER_EXT);
                if (status != OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT) throw new Exception();
            }
        }

        public void Resize(int width, int height)
        {
            using (new Bind(this))
            {
                _gl.BindTexture(OpenGL.GL_TEXTURE_2D, ColorTexture.Handle);
                _gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGBA, width, height, 0, OpenGL.GL_BGR, OpenGL.GL_UNSIGNED_BYTE, null);
                _gl.BindTexture(OpenGL.GL_TEXTURE_2D, DepthTexture.Handle);
                _gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_DEPTH_COMPONENT24, width, height, 0, OpenGL.GL_DEPTH_COMPONENT, OpenGL.GL_UNSIGNED_BYTE, null);
            }
        }

        public void Bind()
        {
            _gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _fbo[0]);
        }

        public void UnBind()
        {
            _gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, 0);
        }
    }
    public class VAO : IBuffer
    {
        private readonly OpenGL _gl;
        readonly uint[] _vao = new uint[1];

        public VAO(OpenGL gl)
        {
            _gl = gl;
            _gl.GenVertexArrays(2, _vao);
        }

        public void Bind()
        {
            _gl.BindVertexArray(_vao[0]);
        }

        public void UnBind()
        {
            _gl.BindVertexArray(0);
        }
    }
    public class VBO : IBuffer
    {
        private readonly OpenGL _gl;

        readonly uint[] _handle = new uint[1];

        public VBO(OpenGL gl)
        {
            _gl = gl;
            _gl.GenBuffers(1, _handle);
        }

        public void Bind()
        {
            _gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, _handle[0]);
        }

        public void Update(object data, int size)
        {
            var pointer = GCHandle.Alloc(data, GCHandleType.Pinned).AddrOfPinnedObject();
            _gl.BufferData(OpenGL.GL_ARRAY_BUFFER, size, pointer, OpenGL.GL_STATIC_DRAW);
        }

        public void UnBind()
        {
            _gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}