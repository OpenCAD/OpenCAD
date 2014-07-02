using System;
using SharpGL;

namespace OpenCAD.Kernel.Graphics.OpenGLRenderer.Buffers
{
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
}