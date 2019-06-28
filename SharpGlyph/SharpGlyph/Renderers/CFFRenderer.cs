using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SharpGlyph {
	public class CFFRenderer {
		public static void DrawGlyph(RendererContext context) {
			CFFTable cff = context.Font.Tables.CFF;
			HmtxTable hmtx = context.Font.Tables.hmtx;
			LongHorMetric hMetric = context.hMetric;

			//Console.WriteLine("glyphId: " + glyphId);

			bool hasWidth = false;
			if (hmtx != null) {
				hMetric = hmtx.GetMetric(context.GlyphId);
				if (cff.privateDict != null && hMetric != null) {
					//Console.WriteLine("advanceWidth: {0}", hMetric.advanceWidth);
					//Console.WriteLine("cff.privateDict.defaultWidthX: {0}", cff.privateDict.defaultWidthX);
					hasWidth = hMetric.advanceWidth != (ushort)cff.privateDict.defaultWidthX;
				}
			}

			//Console.WriteLine();
			//Console.WriteLine("glyphId: {0}", context.GlyphId);
			//Console.WriteLine("Docode:\n{0}", CFFCharString.Decode(cff.charStrings[context.GlyphId]));
			GraphicsPath path = cff.GetGlyph(context.GlyphId, hasWidth);

			if (path == null) {
				if (hMetric != null) {
					context.X += hMetric.advanceWidth;
					return;
				}
				context.X += 100;
				return;
			}

			float imageSize = context.FontSize;
			float unitsPerEm = context.Font.Tables.head.unitsPerEm;
			float ascender = context.Font.Tables.hhea.ascender;
			float descender = context.Font.Tables.hhea.descender;
			float scale = imageSize / (ascender - descender);
			//float scale = imageSize / unitsPerEm;
			//scale *= 0.01f;

			float baseLine = scale * ascender;
			/*
			GraphicsPath path = new GraphicsPath(FillMode.Alternate);
			for (int i = 0; i < points.Count; i++) {
				PointF p = points[i];
				Console.WriteLine("Point: {0}", points[i]);
				p.X /= 10;
				p.Y /= 10;
				//p.X = -p.X;
				//p.Y = -p.Y;
				//p.X += 100;
				//p.Y += 100;
				points[i] = p;
			}
			Console.WriteLine();
			*/

			//path.AddLines(points.ToArray());
			//path.Transform(new Matrix(scale, 0, 0, -scale, (float)Math.Round(x * scale), baseLine));

			float x = context.DX + context.X * scale;
			float y = context.DY + baseLine;

			path.Transform(new Matrix(
				scale, 0,
				0, -scale,
				x, y)
			);
			path.CloseFigure();
			context.Graphics.FillPath(Brushes.Black, path);

			//Console.WriteLine("hMetric.advanceWidth: {0}", hMetric.advanceWidth);
			if (hMetric != null) {
				context.X += hMetric.advanceWidth;
				return;
			}
			context.X += 100;
		}
	}
}
