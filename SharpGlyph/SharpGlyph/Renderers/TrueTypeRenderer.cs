using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SharpGlyph {
	public class TrueTypeRenderer {
		public static void DrawGlyph(RendererContext context) {
			if (context.Glyph == null) {
				return;
			}
			SimpleGlyph simpleGlyph = context.Glyph.simpleGlyph;
			if (simpleGlyph == null) {
				return;
			}
			DrawSimpleGlyph(context, simpleGlyph);
			context.NextGlyph();
		}

		protected static void DrawSimpleGlyph(RendererContext context, SimpleGlyph simpleGlyph) {
			LongHorMetric hMetric = context.hMetric;
			//float unitsPerEm = font.Tables.head.unitsPerEm;
			float scale = context.Scale;
			//float scale = imageSize / unitsPerEm;
			//scale *= 0.01f;

			//float xMin = scale * (glyph.xMin + font.Tables.head.xMin);
			//float yMin = scale * (glyph.yMin + font.Tables.head.yMin);
			float xMin = context.X;// + glyph.xMin;
			float yMin = 0;

			if (hMetric != null) {
				//Console.WriteLine("hMetric.lsb: {0}", hMetric.lsb);
				//Console.WriteLine("hMetric.advanceWidth " + hMetric.advanceWidth);
				xMin += context.Glyph.xMin - hMetric.lsb;
				xMin = Math.Max(xMin, 0);
			} else {
				xMin += context.Glyph.xMin;
			}

			xMin *= scale;
			yMin = context.Ascender * scale;
			xMin += context.DX;
			yMin += context.DY;


			//xMin = (float)Math.Round(xMin);

			/*
			//Console.WriteLine("unitsPerEm " + unitsPerEm);
			Console.WriteLine("ascender " + ascender);
			Console.WriteLine("descender " + descender);
			Console.WriteLine("scale " + scale);
			Console.WriteLine("baseLine " + baseLine);
			Console.WriteLine("glyph.xMin " + glyph.xMin);
			Console.WriteLine("glyph.xMax " + glyph.xMax);
			Console.WriteLine("glyph.yMin " + glyph.yMin);
			Console.WriteLine("glyph.yMax " + glyph.yMax);
			Console.WriteLine("head.xMin " + font.Tables.head.xMin);
			Console.WriteLine("xMin " + xMin);
			Console.WriteLine("yMin " + yMin);
			Console.WriteLine();
			//*/


			ushort[] endPtsOfContours = simpleGlyph.endPtsOfContours;
			SimpleGlyphFlags[] flags = simpleGlyph.flags;
			short[] xCoordinates = simpleGlyph.xCoordinates;
			short[] yCoordinates = simpleGlyph.yCoordinates;

			GraphicsPath path = new GraphicsPath(FillMode.Alternate);
			int length = endPtsOfContours.Length;
			int start = 0;
			PointF[] on = new PointF[2];
			PointF[] off = new PointF[8];
			for (int n = 0; n < length; n++) {
				float fx = 0, fy = 0;
				int onCount = 0;
				int offCount = 0;
				int end = endPtsOfContours[n];

				// first point is not on-curve
				if ((flags[start] & SimpleGlyphFlags.ON_CURVE_POINT) == 0) {
					float x0 = xMin + xCoordinates[end] * scale;
					float y0 = yMin - (yCoordinates[end] * scale);
					on[0].X = x0;
					on[0].Y = y0;
					onCount++;
					fx = x0;
					fy = y0;
				}

				for (int i = start; i <= end; i++) {
					float x0 = xMin + xCoordinates[i] * scale;
					float y0 = yMin - (yCoordinates[i] * scale);
					if ((flags[i] & SimpleGlyphFlags.ON_CURVE_POINT) > 0) {
						//if (onCount == 0) {
						//	fx = x0;
						//	fy = y0;
						//}
						on[onCount].X = x0;
						on[onCount].Y = y0;
						onCount++;
						if (onCount == 2) {
							CreateCurvePoints(path, on, off, offCount);
							onCount = 1;
							offCount = 0;
							on[0].X = on[1].X;
							on[0].Y = on[1].Y;
						} else {
							fx = x0;
							fy = y0;
						}
					} else {
						off[offCount].X = x0;
						off[offCount].Y = y0;
						offCount++;
					}
				}
				if (onCount == 1) {
					on[1].X = fx;
					on[1].Y = fy;
					CreateCurvePoints(path, on, off, offCount);
				}
				path.CloseFigure();
				start = end + 1;
			}
			context.Graphics.FillPath(Brushes.Black, path);
			path.Dispose();
		}

		protected static void CreateCurvePoints(GraphicsPath path, PointF[] on, PointF[] off, int offCount) {
			//PointF[] points = null;
			switch (offCount) {
				case 0:
					path.AddLine(on[0], on[1]);
					return;
				case 1:
					DrawQuadraticCurve(
						path,
						on[0].X, on[0].Y,
						off[0].X, off[0].Y,
						on[1].X, on[1].Y
					);
					return;
			}
			int length = offCount - 1;
			float x0 = on[0].X;
			float y0 = on[0].Y;
			for (int i = 0; i < length; i++) {
				float x = off[i].X;
				float y = off[i].Y;
				float x1 = (off[i + 1].X + x) * 0.5f;
				float y1 = (off[i + 1].Y + y) * 0.5f;
				DrawQuadraticCurve(
					path,
					x0, y0,
					x, y,
					x1, y1
				);
				x0 = x1;
				y0 = y1;
			}
			DrawQuadraticCurve(
				path,
				x0, y0,
				off[length].X, off[length].Y,
				on[1].X, on[1].Y
			);
		}

		protected static void DrawQuadraticCurve(
			GraphicsPath path,
			float x0, float y0,
			float x1, float y1,
			float x2, float y2) {
			float d = Math.Max(Math.Abs(x2 - x0), Math.Abs(y2 - y0)) * 2f;
			float step = 1f / d;
			step = Math.Min(step, 0.1f);
			int length = (int)(1.0f / step) + 1;
			PointF[] points = new PointF[length];
			for (int i = 0; i < length; i++) {
				float t = (float)i / length;
				points[i].X = Quad(t, x0, x1, x2);
				points[i].Y = Quad(t, y0, y1, y2);
			}
			length--;
			points[length].X = Quad(1.0f, x0, x1, x2);
			points[length].Y = Quad(1.0f, y0, y1, y2);
			path.AddLines(points);
			//g.DrawLines(Pens.Black, points.ToArray());
			//GraphicsPath path = new GraphicsPath();
		}

		protected static float Quad(float t, float p0, float p1, float p2) {
			return (float)(
				p0 * Math.Pow(1f - t, 2f) +
				p1 * 2f * t * (1f - t) +
				p2 * Math.Pow(t, 2f)
			);
		}
	}
}
