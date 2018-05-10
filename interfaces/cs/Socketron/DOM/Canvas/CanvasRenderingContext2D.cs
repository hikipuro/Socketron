using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class CanvasRenderingContext2D : RenderingContext {
		public class LineCap {
			public const string butt = "butt";
			public const string round = "round";
			public const string square = "square";
		}

		public class LineJoin {
			public const string bevel = "bevel";
			public const string round = "round";
			public const string miter = "miter";
		}

		public class TextAlign {
			public const string left = "left";
			public const string right = "right";
			public const string center = "center";
			public const string start = "start";
			public const string end = "end";
		}

		public class TextBaseline {
			public const string top = "top";
			public const string hanging = "hanging";
			public const string middle = "middle";
			public const string alphabetic = "alphabetic";
			public const string ideographic = "ideographic";
			public const string bottom = "bottom";
		}

		public class Direction {
			public const string ltr = "ltr";
			public const string rtl = "rtl";
			public const string inherit = "inherit";
		}

		public class Repetition {
			public const string repeat = "repeat";
			public const string repeatX = "repeat-x";
			public const string repeatY = "repeat-y";
			public const string noRepeat = "no-repeat";
		}

		public class FillRule {
			public const string nonzero = "nonzero";
			public const string evenodd = "evenodd";
		}

		public class CompositeOperation {
			public const string sourceOver = "source-over";
			public const string sourceIn = "source-in";
			public const string sourceOut = "source-out";
			public const string sourceAtop = "source-atop";
			public const string destinationOver = "destination-over";
			public const string destinationIn = "destination-in";
			public const string destinationOut = "destination-out";
			public const string destinationAtop = "destination-atop";
			public const string lighter = "lighter";
			public const string copy = "copy";
			public const string xor = "xor";
			public const string multiply = "multiply";
			public const string screen = "screen";
			public const string overlay = "overlay";
			public const string darken = "darken";
			public const string lighten = "lighten";
			public const string colorDodge = "color-dodge";
			public const string colorBurn = "color-burn";
			public const string hardLight = "hard-light";
			public const string softLight = "soft-light";
			public const string difference = "difference";
			public const string exclusion = "exclusion";
			public const string hue = "hue";
			public const string saturation = "saturation";
			public const string color = "color";
			public const string luminosity = "luminosity";
		}

		public class SmoothingQuality {
			public const string low = "low";
			public const string medium = "medium";
			public const string high = "high";
		}

		public class Filter {
			public static string url(string url) {
				return string.Format("url({0}) ", url);
			}
			public static string blur(string length) {
				return string.Format("blur({0}) ", length);
			}
			public static string brightness(string percentage) {
				return string.Format("brightness({0}) ", percentage);
			}
			public static string contrast(string percentage) {
				return string.Format("contrast({0}) ", percentage);
			}
			public static string dropShadow(string offsetX, string offsetY, string blurRadius, string color) {
				return string.Format(
					"drop-shadow({0} {1} {2} {3}) ",
					offsetX, offsetY, blurRadius, color
				);
			}
			public static string grayscale(string percentage) {
				return string.Format("grayscale({0}) ", percentage);
			}
			public static string hueRotate(string degree) {
				return string.Format("hue-rotate({0}) ", degree);
			}
			public static string invert(string percentage) {
				return string.Format("invert({0}) ", percentage);
			}
			public static string opacity(string percentage) {
				return string.Format("opacity({0}) ", percentage);
			}
			public static string saturate(string percentage) {
				return string.Format("saturate({0}) ", percentage);
			}
			public static string sepia(string percentage) {
				return string.Format("sepia({0}) ", percentage);
			}
			public static string none() {
				return "none ";
			}
		}

		public CanvasRenderingContext2D() {
		}

		public double lineWidth {
			get { return API.GetProperty<double>("lineWidth"); }
			set { API.SetProperty("lineWidth", value); }
		}

		public string lineCap {
			get { return API.GetProperty<string>("lineCap"); }
			set { API.SetProperty("lineCap", value); }
		}

		public string lineJoin {
			get { return API.GetProperty<string>("lineJoin"); }
			set { API.SetProperty("lineJoin", value); }
		}

		public double miterLimit {
			get { return API.GetProperty<double>("miterLimit"); }
			set { API.SetProperty("miterLimit", value); }
		}

		public double lineDashOffset {
			get { return API.GetProperty<double>("lineDashOffset"); }
			set { API.SetProperty("lineDashOffset", value); }
		}

		public string font {
			get { return API.GetProperty<string>("font"); }
			set { API.SetProperty("font", value); }
		}

		public string textAlign {
			get { return API.GetProperty<string>("textAlign"); }
			set { API.SetProperty("textAlign", value); }
		}

		public string textBaseline {
			get { return API.GetProperty<string>("textBaseline"); }
			set { API.SetProperty("textBaseline", value); }
		}

		public string direction {
			get { return API.GetProperty<string>("direction"); }
			set { API.SetProperty("direction", value); }
		}

		public string fillStyle {
			get { return API.GetProperty<string>("fillStyle"); }
			set { API.SetProperty("fillStyle", value); }
		}

		public string strokeStyle {
			get { return API.GetProperty<string>("strokeStyle"); }
			set { API.SetProperty("strokeStyle", value); }
		}

		public double shadowBlur {
			get { return API.GetProperty<double>("shadowBlur"); }
			set { API.SetProperty("shadowBlur", value); }
		}

		public string shadowColor {
			get { return API.GetProperty<string>("shadowColor"); }
			set { API.SetProperty("shadowColor", value); }
		}

		public double shadowOffsetX {
			get { return API.GetProperty<double>("shadowOffsetX"); }
			set { API.SetProperty("shadowOffsetX", value); }
		}

		public double shadowOffsetY {
			get { return API.GetProperty<double>("shadowOffsetY"); }
			set { API.SetProperty("shadowOffsetY", value); }
		}

		/*
		[Experimental]
		public double currentTransform {
			get { return API.GetProperty<double>("currentTransform"); }
			set { API.SetProperty("currentTransform", value); }
		}
		//*/

		public double globalAlpha {
			get { return API.GetProperty<double>("globalAlpha"); }
			set { API.SetProperty("globalAlpha", value); }
		}

		public string globalCompositeOperation {
			get { return API.GetProperty<string>("globalCompositeOperation"); }
			set { API.SetProperty("globalCompositeOperation", value); }
		}

		[Experimental]
		public bool imageSmoothingEnabled {
			get { return API.GetProperty<bool>("imageSmoothingEnabled"); }
			set { API.SetProperty("imageSmoothingEnabled", value); }
		}

		[Experimental]
		public string imageSmoothingQuality {
			get { return API.GetProperty<string>("imageSmoothingQuality"); }
			set { API.SetProperty("imageSmoothingQuality", value); }
		}

		[Experimental]
		public string filter {
			get { return API.GetProperty<string>("filter"); }
			set { API.SetProperty("filter", value); }
		}

		public HTMLCanvasElement canvas {
			get { return API.GetObject<HTMLCanvasElement>("canvas"); }
		}

		public void clearRect(double x, double y, double width, double height) {
			string script = ScriptBuilder.Build(
				"{0}.clearRect({1},{2},{3},{4});",
				Script.GetObject(API.id),
				x, y, width, height
			);
			API.ExecuteJavaScript(script);
		}

		public void fillRect(double x, double y, double width, double height) {
			string script = ScriptBuilder.Build(
				"{0}.fillRect({1},{2},{3},{4});",
				Script.GetObject(API.id),
				x, y, width, height
			);
			API.ExecuteJavaScript(script);
		}

		public void strokeRect(double x, double y, double width, double height) {
			string script = ScriptBuilder.Build(
				"{0}.strokeRect({1},{2},{3},{4});",
				Script.GetObject(API.id),
				x, y, width, height
			);
			API.ExecuteJavaScript(script);
		}

		public void fillText(string text, double x, double y) {
			string script = ScriptBuilder.Build(
				"{0}.fillText({1},{2},{3});",
				Script.GetObject(API.id),
				text.Escape(), x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void fillText(string text, double x, double y, double maxWidth) {
			string script = ScriptBuilder.Build(
				"{0}.fillText({1},{2},{3},{4});",
				Script.GetObject(API.id),
				text.Escape(), x, y, maxWidth
			);
			API.ExecuteJavaScript(script);
		}

		public void strokeText(string text, double x, double y) {
			string script = ScriptBuilder.Build(
				"{0}.strokeText({1},{2},{3});",
				Script.GetObject(API.id),
				text.Escape(), x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void strokeText(string text, double x, double y, double maxWidth) {
			string script = ScriptBuilder.Build(
				"{0}.strokeText({1},{2},{3},{4});",
				Script.GetObject(API.id),
				text.Escape(), x, y, maxWidth
			);
			API.ExecuteJavaScript(script);
		}

		public TextMetrics measureText(string text) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var metrics = {0}.measureText({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				text.Escape(),
				Script.AddObject("metrics")
			);
			int id = API._ExecuteBlocking<int>(script);
			return API.CreateObject<TextMetrics>(id);
		}

		public double[] getLineDash() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var array = {0}.getLineDash();",
					"return {1};"
				),
				Script.GetObject(API.id),
				Script.AddObject("array")
			);
			object[] result = API._ExecuteBlocking<object[]>(script);
			return Array.ConvertAll(result, value => Convert.ToDouble(value));
		}

		public void setLineDash(params double[] segments) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.setLineDash({1});"
				),
				Script.GetObject(API.id),
				JSON.Stringify(segments)
			);
			API.ExecuteJavaScript(script);
		}

		/*
		public void createLinearGradient(double x0, double y0, double x1, double y1) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.createLinearGradient({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				x0, y0, x1, y1
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void createRadialGradient(double x0, double y0, double r0, double x1, double y1, double r1) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.createRadialGradient({1},{2},{3},{4},{5},{6});"
				),
				Script.GetObject(API.id),
				x0, y0, r0, x1, y1, r1
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void createPattern(HTMLImageElement image, string repetition) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.createRadialGradient({1},{2},{3},{4},{5},{6});"
				),
				Script.GetObject(API.id),
				x0, y0, r0, x1, y1, r1
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		public void beginPath() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.beginPath();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void closePath() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.closePath();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void moveTo(double x, double y) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.moveTo({1},{2});"
				),
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void lineTo(double x, double y) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.lineTo({1},{2});"
				),
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void bezierCurveTo(double cp1x, double cp1y, double cp2x, double cp2y, double x, double y) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.bezierCurveTo({1},{2},{3},{4},{5},{6});"
				),
				Script.GetObject(API.id),
				cp1x, cp1y, cp2x, cp2y, x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void quadraticCurveTo(double cpx, double cpy, double x, double y) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.quadraticCurveTo({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				cpx, cpy, x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void arc(double x, double y, double radius, double startAngle, double endAngle) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.arc({1},{2},{3},{4},{5});"
				),
				Script.GetObject(API.id),
				x, y, radius, startAngle, endAngle
			);
			API.ExecuteJavaScript(script);
		}

		public void arc(double x, double y, double radius, double startAngle, double endAngle, bool anticlockwise) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.arc({1},{2},{3},{4},{5},{6});"
				),
				Script.GetObject(API.id),
				x, y, radius, startAngle, endAngle,
				anticlockwise.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void arcTo(double x, double y, double x2, double y2, double radius) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.arcTo({1},{2},{3},{4},{5});"
				),
				Script.GetObject(API.id),
				x, y, x2, y2, radius
			);
			API.ExecuteJavaScript(script);
		}

		[Experimental]
		public void ellipse(double x, double y, double radiusX, double radiusY, double rotation, double startAngle, double endAngle) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.ellipse({1},{2},{3},{4},{5},{6},{7});"
				),
				Script.GetObject(API.id),
				x, y, radiusX, radiusY,
				rotation, startAngle, endAngle
			);
			API.ExecuteJavaScript(script);
		}

		public void ellipse(double x, double y, double radiusX, double radiusY, double rotation, double startAngle, double endAngle, bool anticlockwise) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.ellipse({1},{2},{3},{4},{5},{6},{7},{8});"
				),
				Script.GetObject(API.id),
				x, y, radiusX, radiusY,
				rotation, startAngle, endAngle,
				anticlockwise.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void rect(double x, double y, double width, double height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.rect({1},{2},{3},{4});"
				),
				Script.GetObject(API.id),
				x, y, width, height
			);
			API.ExecuteJavaScript(script);
		}

		public void fill() {
			// TODO: add parameter
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.fill();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void stroke() {
			// TODO: add parameter
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.stroke();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void drawFocusIfNeeded(Element element) {
			// TODO: add path parameter
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.drawFocusIfNeeded({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(element.API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void scrollPathIntoView() {
			// TODO: add path parameter
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.scrollPathIntoView();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void clip() {
			// TODO: add parameter
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.clip();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void isPointInPath(double x, double y) {
			// TODO: add parameter
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.isPointInPath({1},{2});"
				),
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void isPointInStroke(double x, double y) {
			// TODO: add parameter
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.isPointInStroke({1},{2});"
				),
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void rotate(double angle) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.rotate({1});"
				),
				Script.GetObject(API.id),
				angle
			);
			API.ExecuteJavaScript(script);
		}

		public void scale(double x, double y) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.scale({1},{2});"
				),
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void translate(double x, double y) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.translate({1},{2});"
				),
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void transform(double a, double b, double c, double d, double e, double f) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.transform({1},{2},{3},{4},{5},{6});"
				),
				Script.GetObject(API.id),
				a, b, c, d, e, f
			);
			API.ExecuteJavaScript(script);
		}

		public void setTransform(double a, double b, double c, double d, double e, double f) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.setTransform({1},{2},{3},{4},{5},{6});"
				),
				Script.GetObject(API.id),
				a, b, c, d, e, f
			);
			API.ExecuteJavaScript(script);
		}

		[Experimental]
		public void resetTransform() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.resetTransform();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		/*
		public void drawImage(HTMLImageElement image, double dx, double dy) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.drawImage({1},{2},{3});"
				),
				Script.GetObject(API.id),
				Script.GetObject(image.API.id),
				dx, dy
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void createImageData(double width, double height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var data = {0}.createImageData({1},{2});",
					"return {3}"
				),
				Script.GetObject(API.id),
				width, height,
				Script.AddObject("data")
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void getImageData(double sx, double sy, double sw, double sh) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var data = {0}.getImageData({1},{2});",
					"return {3}"
				),
				Script.GetObject(API.id),
				sx, sy, sw, sh
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void putImageData(ImageData imagedata, double dx, double dy) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var data = {0}.putImageData({1},{2});",
					"return {3}"
				),
				Script.GetObject(API.id),
				width, height,
				Script.AddObject("data")
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		public void save() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.save();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void restore() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.restore();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		[Experimental]
		public void addHitRegion(JsonObject options) {
			string script = ScriptBuilder.Build(
				"{0}.addHitRegion({1});",
				Script.GetObject(API.id),
				options.Stringify()
			);
			API.ExecuteJavaScript(script);
		}

		[Experimental]
		public void removeHitRegion(string id) {
			string script = ScriptBuilder.Build(
				"{0}.removeHitRegion({1});",
				Script.GetObject(API.id),
				id.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		[Experimental]
		public void clearHitRegions() {
			string script = ScriptBuilder.Build(
				"{0}.clearHitRegions();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}
	}
}
