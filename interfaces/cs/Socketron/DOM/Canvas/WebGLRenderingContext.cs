using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	using GLenum = System.UInt32;
	using GLboolean = System.Boolean;
	using GLbitfield = System.UInt32;
	using GLbyte = System.SByte;
	using GLshort = System.Int16;
	using GLint = System.Int32;
	using GLsizei = System.Int32;
	using GLintptr = System.Int64;
	using GLsizeiptr = System.Int64;
	using GLubyte = System.Byte;
	using GLushort = System.UInt16;
	using GLuint = System.UInt32;
	using GLfloat = System.Single;
	using GLclampf = System.Single;

	[type: SuppressMessage("Style", "IDE1006")]
	public class WebGLRenderingContext : RenderingContext {
		public WebGLRenderingContext() {
		}

		public HTMLCanvasElement canvas {
			get { return API.GetObject<HTMLCanvasElement>("canvas"); }
		}

		public double drawingBufferWidth {
			get { return API.GetProperty<double>("drawingBufferWidth"); }
		}

		public double drawingBufferHeight {
			get { return API.GetProperty<double>("drawingBufferHeight"); }
		}

		[Experimental]
		public void commit() {
			string script = ScriptBuilder.Build(
				"{0}.commit();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public JsonObject getContextAttributes() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var attr = {0}.getContextAttributes();",
					"return attr;"
				),
				Script.GetObject(API.id)
			);
			object result = API._ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}

		public GLboolean isContextLost() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.isContextLost();"
				),
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<GLboolean>(script);
		}

		public void scissor(GLint x, GLint y, GLsizei width, GLsizei height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.scissor({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				x, y, width, height
			);
			API.ExecuteJavaScript(script);
		}

		public void viewport(GLint x, GLint y, GLsizei width, GLsizei height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.viewport({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				x, y, width, height
			);
			API.ExecuteJavaScript(script);
		}

		/*
		public void activeTexture(GLint x) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.activeTexture({1});"
				),
				Script.GetObject(API.id),
				x
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		public void blendColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.blendColor({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				red, green, blue, alpha
			);
			API.ExecuteJavaScript(script);
		}

		public void blendEquation(GLenum mode) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.blendEquation({1});"
				),
				Script.GetObject(API.id),
				mode
			);
			API.ExecuteJavaScript(script);
		}

		public void blendEquationSeparate(GLenum modeRGB, GLenum modeAlpha) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.blendEquationSeparate({1},{2});"
				),
				Script.GetObject(API.id),
				modeRGB, modeAlpha
			);
			API.ExecuteJavaScript(script);
		}

		public void blendFunc(GLenum sfactor, GLenum dfactor) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.blendFunc({1},{2});"
				),
				Script.GetObject(API.id),
				sfactor, dfactor
			);
			API.ExecuteJavaScript(script);
		}

		public void blendFuncSeparate(GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.blendFuncSeparate({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				srcRGB, dstRGB, srcAlpha, dstAlpha
			);
			API.ExecuteJavaScript(script);
		}

		public void clearColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.clearColor({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				red, green, blue, alpha
			);
			API.ExecuteJavaScript(script);
		}

		public void clearDepth(GLclampf depth) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.clearDepth({1});"
				),
				Script.GetObject(API.id),
				depth
			);
			API.ExecuteJavaScript(script);
		}

		public void clearStencil(GLint s) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.clearStencil({1});"
				),
				Script.GetObject(API.id), s
			);
			API.ExecuteJavaScript(script);
		}

		public void colorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.colorMask({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				red.Escape(), green.Escape(), blue.Escape(), alpha.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void cullFace(GLenum mode) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.cullFace({1});"
				),
				Script.GetObject(API.id), mode
			);
			API.ExecuteJavaScript(script);
		}

		public void depthFunc(GLenum func) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.depthFunc({1});"
				),
				Script.GetObject(API.id), func
			);
			API.ExecuteJavaScript(script);
		}

		public void depthMask(GLboolean flag) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.depthMask({1});"
				),
				Script.GetObject(API.id),
				flag.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void depthRange(GLclampf zNear, GLclampf zFar) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.depthRange({1});"
				),
				Script.GetObject(API.id),
				zNear, zFar
			);
			API.ExecuteJavaScript(script);
		}

		public void disable(GLenum cap) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.disable({1});"
				),
				Script.GetObject(API.id), cap
			);
			API.ExecuteJavaScript(script);
		}

		public void enable(GLenum cap) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.enable({1});"
				),
				Script.GetObject(API.id), cap
			);
			API.ExecuteJavaScript(script);
		}

		public void frontFace(GLenum mode) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.frontFace({1});"
				),
				Script.GetObject(API.id), mode
			);
			API.ExecuteJavaScript(script);
		}

		public JsonObject getParameter(GLenum pname) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var param = {0}.getParameter({1});",
					"return param;"
				),
				Script.GetObject(API.id),
				pname
			);
			object result = API._ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}

		public GLenum getError() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.getError();"
				),
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<GLenum>(script);
		}

		public void hint(GLenum target, GLenum mode) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.hint({1},{2});"
				),
				Script.GetObject(API.id),
				target, mode
			);
			API.ExecuteJavaScript(script);
		}

		public GLboolean isEnabled(GLenum cap) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.isEnabled({1});"
				),
				Script.GetObject(API.id), cap
			);
			return API._ExecuteBlocking<GLboolean>(script);
		}

		public void lineWidth(GLfloat width) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.lineWidth({1});"
				),
				Script.GetObject(API.id), width
			);
			API.ExecuteJavaScript(script);
		}

		public void pixelStorei(GLenum pname, GLint param) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.pixelStorei({1},{2});"
				),
				Script.GetObject(API.id),
				pname, param
			);
			API.ExecuteJavaScript(script);
		}

		public void polygonOffset(GLfloat factor, GLfloat units) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.polygonOffset({1},{2});"
				),
				Script.GetObject(API.id),
				factor, units
			);
			API.ExecuteJavaScript(script);
		}

		public void sampleCoverage(GLclampf value, GLboolean invert) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.sampleCoverage({1},{2});"
				),
				Script.GetObject(API.id),
				value, invert.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void stencilFunc(GLenum func, GLint @ref, GLuint mask) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.stencilFunc({1},{2},{3});"
				),
				Script.GetObject(API.id),
				func, @ref, mask
			);
			API.ExecuteJavaScript(script);
		}

		public void stencilFuncSeparate(GLenum face, GLenum func, GLint @ref, GLuint mask) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.stencilFuncSeparate({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				face, func, @ref, mask
			);
			API.ExecuteJavaScript(script);
		}

		public void stencilMask(GLuint mask) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.stencilMask({1});"
				),
				Script.GetObject(API.id),
				mask
			);
			API.ExecuteJavaScript(script);
		}

		public void stencilMaskSeparate(GLenum face, GLuint mask) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.stencilMaskSeparate({1},{2});"
				),
				Script.GetObject(API.id),
				face, mask
			);
			API.ExecuteJavaScript(script);
		}

		public void stencilOp(GLenum fail, GLenum zfail, GLenum zpass) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.stencilOp({1},{2},{3});"
				),
				Script.GetObject(API.id),
				fail, zfail, zpass
			);
			API.ExecuteJavaScript(script);
		}

		public void stencilOpSeparate(GLenum face, GLenum fail, GLenum zfail, GLenum zpass) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.stencilOpSeparate({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				face, fail, zfail, zpass
			);
			API.ExecuteJavaScript(script);
		}

		/*
		public void bindBuffer(GLenum target, WebGLBuffer buffer) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.bindBuffer({1},{2});"
				),
				Script.GetObject(API.id),
				target,
				Script.GetObject(buffer.API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		public void bufferData(GLenum target, GLsizeiptr size, GLenum usage) {
			// TODO: add params
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.bufferData({1},{2},{3});"
				),
				Script.GetObject(API.id),
				target, size, usage
			);
			API.ExecuteJavaScript(script);
		}

		/*
		public void bufferSubData(GLenum target, GLuint offset, ArrayBuffer srcData) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.bufferSubData({1},{2},{3});"
				),
				Script.GetObject(API.id),
				target, size, usage
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public WebGLBuffer createBuffer() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = {0}.createBuffer();",
					"return {1};"
				),
				Script.GetObject(API.id),
				Script.AddObject("buf")
			);
			int id = API._ExecuteBlocking<int>(script);
			return new WebGLBuffer();
		}
		//*/

		/*
		public void deleteBuffer(WebGLBuffer buffer) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.deleteBuffer({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(buffer.API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		public JsonObject getBufferParameter(GLenum target, GLenum pname) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.getBufferParameter({1},{2});"
				),
				Script.GetObject(API.id),
				target, pname
			);
			object result = API._ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}

		/*
		public GLboolean isBuffer(WebGLBuffer buffer) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.isBuffer({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(buffer.API.id)
			);
			return API._ExecuteBlocking<GLboolean>(script);
		}
		//*/

		/*
		public void bindFramebuffer(GLenum target, WebGLFramebuffer framebuffer) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.bindFramebuffer({1},{2});"
				),
				Script.GetObject(API.id),
				target,
				Script.GetObject(framebuffer.API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		public GLenum checkFramebufferStatus(GLenum target) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.checkFramebufferStatus({1});"
				),
				Script.GetObject(API.id),
				target
			);
			return API._ExecuteBlocking<GLenum>(script);
		}

		/*
		public WebGLFramebuffer createFramebuffer() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = {0}.createFramebuffer();",
					"return {1}"
				),
				Script.GetObject(API.id),
				Script.AddObject("buf")
			);
			int id = API._ExecuteBlocking<int>(script);
		}
		//*/

		/*
		public void deleteFramebuffer(WebGLFramebuffer framebuffer) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.deleteFramebuffer({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(framebuffer.API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void framebufferRenderbuffer(GLenum target, GLenum attachment, GLenum renderbuffertarget, WebGLRenderbuffer renderbuffer) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.framebufferRenderbuffer({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(framebuffer.API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void framebufferTexture2D(GLenum target, GLenum attachment, GLenum textarget, WebGLTexture texture, GLint level) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.deleteFramebuffer({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(framebuffer.API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/
	}
}
