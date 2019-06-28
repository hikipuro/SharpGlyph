using System;
using System.Drawing;

namespace SharpGlyph {
	public class SbixRenderer {
		public static void DrawGlyph(RendererContext context) {
			SbixTable sbix = context.Font.Tables.sbix;
			if (sbix == null) {
				//DrawGlyph(font, glyphId, g, size, 0);
				return;
			}
			Strike strike = sbix.FindStrike(context.FontSize);
			//Console.WriteLine(strike);
			GlyphData data = strike.GetGlyphData(context.GlyphId);
			if (data != null && data.data.Length > 0) {
				//MemoryStream stream = new MemoryStream(data2.data);
				ImageConverter converter = new ImageConverter();
				Image image = (Image)converter.ConvertFrom(data.data);

				float unitsPerEm = context.Font.Tables.head.unitsPerEm;
				float ascender = context.Ascender;
				float descender = context.Descender;
				float scale = context.Scale;
				float baseLine = scale * ascender;

				//Console.WriteLine("ascender " + ascender);
				//Console.WriteLine("baseLine " + baseLine);
				//Console.WriteLine("unitsPerEm * scale " + (unitsPerEm * scale));

				float width = (context.Glyph.xMax - context.Glyph.xMin) * scale;
				float height = (context.Glyph.yMax - context.Glyph.yMin) * scale;
				// unitsPerEm * scale

				float x = context.DX + context.X * scale + data.originOffsetX;
				float y = context.DY + (context.Glyph.yMin) * scale + data.originOffsetY;

				context.Graphics.DrawImage(
					image, x, y, width, height
				);
				//context.Graphics.DrawLine(Pens.Black, context.X * scale, baseLine, context.X * scale + 100, baseLine);
				image.Dispose();
				//if ((font.Tables.sbix.flags & 2) > 0) {
				//	DrawGlyph(font, glyphId, g, size, 0);
				//}
			} else {
				//DrawGlyph(font, glyphId, g, size, 0);
			}
		}
	}
}
