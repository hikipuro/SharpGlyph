using System;
using System.Drawing;

namespace SharpGlyph {
	public class RendererContext {
		public bool UseInterpreter;
		public bool UseBitmapGlyph;
		public Font Font;
		public LongHorMetric hMetric;
		public int CodePoint;
		public int GlyphId;
		public Glyph Glyph;
		public int X;
		public float DX;
		public float DY;
		public Bitmap Bitmap;
		public Graphics Graphics;
		public float Scale;

		public int FontSize {
			get { return fontSize; }
			set {
				fontSize = value;
				//Scale = (float)fontSize / (Ascender + Descender);
				//if (Font.Tables.head.unitsPerEm)
				//Console.WriteLine("Font.Tables.head.unitsPerEm " + Font.Tables.head.unitsPerEm);
				//Console.WriteLine("(Ascender + Descender) " + (Ascender + Descender));
				Scale = (float)fontSize / Font.Tables.head.unitsPerEm;
			}
		}

		public int Ascender;
		public int Descender;

		protected int fontSize;

		public RendererContext(Font font) {
			Font = font;
			Init();
		}

		public void Init() {
			HheaTable hhea = Font.Tables.hhea;
			if (hhea != null) {
				Ascender = hhea.ascender;
				Descender = hhea.descender;
			}
		}

		public void Reset() {
			UseInterpreter = false;
			UseBitmapGlyph = false;
			hMetric = null;
			GlyphId = 0;
			Glyph = null;
			X = 0;
			Bitmap = null;
			if (Graphics != null) {
				Graphics.Dispose();
			}
			Graphics = null;
		}

		public void NextGlyph() {
			if (hMetric != null) {
				if (hMetric.advanceWidth == 0) {
					X += Glyph.xMax - Glyph.xMin;
					return;
				}
				X += hMetric.advanceWidth;
				return;
			}
			X += Glyph.xMax - Glyph.xMin;
		}
	}
}
