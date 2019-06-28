using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SharpGlyph {
	public class Renderer {
		public readonly int DefaultFontSize = 24;

		public int FontSize;
		public bool UseInterpreter;
		public bool UseBitmapGlyph = false;

		Font font;
		Interpreter interpreter;
		RendererContext context;

		public Renderer(Font font, Interpreter interpreter) {
			this.font = font;
			this.interpreter = interpreter;
			FontSize = DefaultFontSize;
			context = new RendererContext(font);
		}

		public void DrawText(Bitmap bitmap, string text, float x, float y) {
			if (UseInterpreter) {
				interpreter.ppem = FontSize;
				InterpretPrep();
			}

			context.UseInterpreter = UseInterpreter;
			context.UseBitmapGlyph = UseBitmapGlyph;
			context.DX = x;
			context.DY = y;
			context.FontSize = FontSize;
			context.Bitmap = bitmap;
			context.Graphics = Graphics.FromImage(bitmap);
			context.Graphics.Clear(Color.White);
			context.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			HmtxTable hmtx = font.Tables.hmtx;
			GlyfTable glyf = font.Tables.glyf;
			char highSurrogate = '\0';
			int length = text.Length;
			for (int i = 0; i < length; i++) {
				char charCode = text[i];
				if (char.IsHighSurrogate(charCode)) {
					highSurrogate = charCode;
					continue;
				}
				int codePoint;
				if (char.IsLowSurrogate(charCode)) {
					codePoint = char.ConvertToUtf32(highSurrogate, charCode);
				} else {
					codePoint = charCode;
				}
				context.GlyphId = font.GetGlyphId(codePoint);
				if (hmtx != null) {
					context.hMetric = hmtx.GetMetric(context.GlyphId);
				}
				if (glyf != null) {
					context.Glyph = glyf.GetGlyph(context.GlyphId);
				}
				DrawGlyph(context);
			}
			context.Reset();
		}

		protected void InterpretPrep() {
			PrepTable prep = font.Tables.prep;
			if (prep == null) {
				return;
			}
			byte[] data = prep.ReadData();
			if (data == null) {
				return;
			}
			//Console.WriteLine("Prep Decode {0}", Interpreter.Decode(prep.data));
			interpreter.Exec(data, null);
		}

		protected Glyph InterpretGlyph(Glyph glyph) {
			if (glyph == null) {
				return null;
			}
			if (glyph.simpleGlyph != null) {
				return interpreter.Exec(glyph.simpleGlyph.instructions, glyph);
			}
			return null;
		}

		protected void DrawGlyph(RendererContext context) {
			if (font.Tables.CFF != null) {
				CFFRenderer.DrawGlyph(context);
				return;
			}
			if (UseBitmapGlyph && font.Tables.EBLC != null) {
				if (font.Tables.EBLC.HasSize(FontSize, FontSize)) {
					EBDTRenderer.DrawGlyph(context);
					return;
				}
			}

			if (context.Glyph == null) {
				//DrawSbix(font, glyphId, size, g, x);
				//Console.WriteLine("##### empty glyph: {0} ", context.GlyphId);
				if (context.hMetric != null) {
					if (context.hMetric.advanceWidth == 0) {
						context.X += font.Tables.hhea.xMaxExtent;
						return;
					}
					context.X += context.hMetric.advanceWidth;
					return;
				}
				context.X += font.Tables.hhea.xMaxExtent;
				return;
			}

			//Console.WriteLine(glyph);
			//Console.WriteLine("Decode:\n{0}", Interpreter.Decode(glyph.simpleGlyph.instructions));
			if (context.UseInterpreter) {
				context.Glyph = InterpretGlyph(context.Glyph);
			}

			if (font.Tables.sbix != null) {
				SbixRenderer.DrawGlyph(context);
			}
			TrueTypeRenderer.DrawGlyph(context);
			//Console.WriteLine("hMetric " + hMetric);

		}
	}
}
