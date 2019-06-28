using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SharpGlyph {
	public class EBDTRenderer {
		public static void DrawGlyph(RendererContext context) {
			EBLCTable EBLC = context.Font.Tables.EBLC;
			EBDTTable EBDT = context.Font.Tables.EBDT;

			//Bitmap bitmap = context.Bitmap;

			BitmapSize size = EBLC.GetBitmapSize(context.FontSize, context.FontSize);
			//Console.WriteLine("bitDepth: {0}", size.bitDepth);
			//Console.WriteLine("subTables.Length: {0}", size.subTables.Length);

			//for (int i = 0; i < size.subTables.Length; i++) {
			//	Console.WriteLine("subTables {0}: {1}", i, size.subTables[i]);
			//}
			//Console.WriteLine("flags: {0}", size.flags);

			//GlyphBitmapData data = EBDT.bitmapData[0x53F8];
			GlyphBitmapData data = EBDT.GetGlyphBitmapData(size.index, context.GlyphId);
			if (data == null) {
				data = EBDT.GetGlyphBitmapData(size.index, 0);
			}
			if (data == null) {
				context.X += context.FontSize;
				return;
			}
			//GlyphBitmapData data = EBDT.bitmapData[0x835];
			switch (data.format) {
				case 1:
					GlyphBitmapData1 data1 = data as GlyphBitmapData1;
					//Console.WriteLine("data7: {0}", data7);
					DrawImageData(
						context,
						size.bitDepth,
						data1.smallMetrics,
						data1.imageData
					);
					break;
				case 2:
					GlyphBitmapData2 data2 = data as GlyphBitmapData2;
					//Console.WriteLine("data7: {0}", data7);
					DrawImageData(
						context,
						size.bitDepth,
						data2.smallMetrics,
						data2.imageData
					);
					break;
				case 5:
					DrawBitmapData5(context, size, data as GlyphBitmapData5);
					break;
				case 6:
					GlyphBitmapData6 data6 = data as GlyphBitmapData6;
					//Console.WriteLine("data7: {0}", data);
					DrawImageData(
						context,
						size.bitDepth,
						data6.bigMetrics,
						data6.imageData
					);
					break;
				case 7:
					GlyphBitmapData7 data7 = data as GlyphBitmapData7;
					//Console.WriteLine("data7: {0}", data7);
					DrawImageData(
						context,
						size.bitDepth,
						data7.bigMetrics,
						data7.imageData
					);
					break;
				case 8:
					break;
				case 9:
					break;
			}
			//context.X += context.FontSize;
		}

		protected static void DrawBitmapData5(RendererContext context, BitmapSize size, GlyphBitmapData5 data) {
			//Console.WriteLine("data5: {0}", data5);
			BigGlyphMetrics bigMetrics = null;
			IndexSubTableArray subTable = size.FindSubTableArray(context.GlyphId);
			if (subTable != null) {
				//Console.WriteLine("Size 2: {0}", context.GlyphId);
				//Console.WriteLine("Size 3: {0}", subTable);
				bigMetrics = subTable.GetBigGlyphMetrics();
				//Console.WriteLine("Size 4: {0}", metrics);
			}
			if (bigMetrics != null) {
				DrawImageData(
					context,
					size.bitDepth,
					bigMetrics,
					data.imageData
				);
			} else {
				DrawImageData(
					context,
					size.bitDepth,
					size.hori.widthMax,
					context.FontSize,
					data.imageData
				);
				context.X += context.FontSize;
			}
		}

		protected static void DrawImageData(RendererContext context, BitDepth bitDepth, SmallGlyphMetrics smallMetrics, byte[] data) {
			switch (bitDepth) {
				case BitDepth.BlackWhite:
					DrawImageData2(context, smallMetrics, data);
					break;
				case BitDepth.Gray4:
					DrawImageData4(context, smallMetrics, data);
					break;
				case BitDepth.Gray16:
					DrawImageData16(context, smallMetrics, data);
					break;
				case BitDepth.Gray256:
					DrawImageData256(context, smallMetrics, data);
					break;
			}
		}

		protected static void DrawImageData2(RendererContext context, SmallGlyphMetrics smallMetrics, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int width = smallMetrics.width;
			int height = smallMetrics.height;
			int bx = (int)context.DX + context.X + smallMetrics.bearingX;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				byte b = data[i];
				for (int n = 0; n < 8; n++) {
					if (dx >= width) {
						dx = 0;
						dy++;
						if (dy >= height) {
							i = length;
							break;
						}
					}
					if (bx + dx > bitmap.Width) {
						dx++;
						continue;
					}
					if (((b << n) & 0x80) == 0) {
						dx++;
						continue;
					}
					bitmap.SetPixel(bx + dx, by + dy, color);
					dx++;
				}
			}
			//context.NextGlyph();
			context.X += smallMetrics.advance;
		}

		protected static void DrawImageData4(RendererContext context, SmallGlyphMetrics smallMetrics, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int width = smallMetrics.width;
			int height = smallMetrics.height;
			int bx = (int)context.DX + context.X + smallMetrics.bearingX;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				byte b = data[i];
				for (int n = 6; n >= 0; n -= 2) {
					if (dx >= width) {
						dx = 0;
						dy++;
						if (dy >= height) {
							i = length;
							break;
						}
					}
					if (bx + dx > bitmap.Width) {
						dx++;
						continue;
					}
					byte alpha = (byte)((b >> n) & 0x03);
					if (alpha == 0) {
						dx++;
						continue;
					}
					alpha *= 85;
					bitmap.SetPixel(bx + dx, by + dy, Color.FromArgb(alpha, color));
					dx++;
				}
			}
			//context.NextGlyph();
			context.X += smallMetrics.advance;
		}

		protected static void DrawImageData16(RendererContext context, SmallGlyphMetrics smallMetrics, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int width = smallMetrics.width;
			int height = smallMetrics.height;
			int bx = (int)context.DX + context.X + smallMetrics.bearingX;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				byte b = data[i];
				for (int n = 4; n >= 0; n -= 4) {
					if (dx >= width) {
						dx = 0;
						dy++;
						if (dy >= height) {
							i = length;
							break;
						}
					}
					if (bx + dx > bitmap.Width) {
						dx++;
						continue;
					}
					byte alpha = (byte)((b >> n) & 0x0F);
					if (alpha == 0) {
						dx++;
						continue;
					}
					alpha *= 17;
					bitmap.SetPixel(bx + dx, by + dy, Color.FromArgb(alpha, color));
					dx++;
				}
			}
			//context.NextGlyph();
			context.X += smallMetrics.advance;
		}

		protected static void DrawImageData256(RendererContext context, SmallGlyphMetrics smallMetrics, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int width = smallMetrics.width;
			int height = smallMetrics.height;
			int bx = (int)context.DX + context.X + smallMetrics.bearingX;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				if (dx >= width) {
					dx = 0;
					dy++;
					if (dy >= height) {
						break;
					}
				}
				if (bx + dx > bitmap.Width) {
					dx++;
					continue;
				}
				byte alpha = data[i];
				bitmap.SetPixel(bx + dx, by + dy, Color.FromArgb(alpha, color));
				dx++;
			}
			//context.NextGlyph();
			context.X += smallMetrics.advance;
		}

		protected static void DrawImageData(RendererContext context, BitDepth bitDepth, BigGlyphMetrics bigMetrics, byte[] data) {
			switch (bitDepth) {
				case BitDepth.BlackWhite:
					DrawImageData2(context, bigMetrics, data);
					break;
				case BitDepth.Gray4:
					DrawImageData4(context, bigMetrics, data);
					break;
				case BitDepth.Gray16:
					DrawImageData16(context, bigMetrics, data);
					break;
				case BitDepth.Gray256:
					DrawImageData256(context, bigMetrics, data);
					break;
			}
		}

		protected static void DrawImageData2(RendererContext context, BigGlyphMetrics bigMetrics, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int width = bigMetrics.width;
			int height = bigMetrics.height;
			int bx = (int)context.DX + context.X + bigMetrics.horiBearingX;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				byte b = data[i];
				for (int n = 0; n < 8; n++) {
					if (dx >= width) {
						dx = 0;
						dy++;
						if (dy >= height) {
							i = length;
							break;
						}
					}
					if (bx + dx > bitmap.Width) {
						dx++;
						continue;
					}
					if (((b << n) & 0x80) == 0) {
						dx++;
						continue;
					}
					bitmap.SetPixel(bx + dx, by + dy, color);
					dx++;
				}
			}
			//context.NextGlyph();
			context.X += bigMetrics.horiAdvance;
		}

		protected static void DrawImageData4(RendererContext context, BigGlyphMetrics bigMetrics, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int width = bigMetrics.width;
			int height = bigMetrics.height;
			int bx = (int)context.DX + context.X + bigMetrics.horiBearingX;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				byte b = data[i];
				for (int n = 6; n >= 0; n -= 2) {
					if (dx >= width) {
						dx = 0;
						dy++;
						if (dy >= height) {
							i = length;
							break;
						}
					}
					if (bx + dx > bitmap.Width) {
						dx++;
						continue;
					}
					byte alpha = (byte)((b >> n) & 0x03);
					if (alpha == 0) {
						dx++;
						continue;
					}
					alpha *= 85;
					bitmap.SetPixel(bx + dx, by + dy, Color.FromArgb(alpha, color));
					dx++;
				}
			}
			//context.NextGlyph();
			context.X += bigMetrics.horiAdvance;
		}

		protected static void DrawImageData16(RendererContext context, BigGlyphMetrics bigMetrics, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int width = bigMetrics.width;
			int height = bigMetrics.height;
			int bx = (int)context.DX + context.X + bigMetrics.horiBearingX;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				byte b = data[i];
				for (int n = 4; n >= 0; n -= 4) {
					if (dx >= width) {
						dx = 0;
						dy++;
						if (dy >= height) {
							i = length;
							break;
						}
					}
					if (bx + dx > bitmap.Width) {
						dx++;
						continue;
					}
					byte alpha = (byte)((b >> n) & 0x0F);
					if (alpha == 0) {
						dx++;
						continue;
					}
					alpha *= 17;
					bitmap.SetPixel(bx + dx, by + dy, Color.FromArgb(alpha, color));
					dx++;
				}
			}
			//context.NextGlyph();
			context.X += bigMetrics.horiAdvance;
		}

		protected static void DrawImageData256(RendererContext context, BigGlyphMetrics bigMetrics, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int width = bigMetrics.width;
			int height = bigMetrics.height;
			int bx = (int)context.DX + context.X + bigMetrics.horiBearingX;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				if (dx >= width) {
					dx = 0;
					dy++;
					if (dy >= height) {
						break;
					}
				}
				if (bx + dx > bitmap.Width) {
					dx++;
					continue;
				}
				byte alpha = data[i];
				bitmap.SetPixel(bx + dx, by + dy, Color.FromArgb(alpha, color));
				dx++;
			}
			//context.NextGlyph();
			context.X += bigMetrics.horiAdvance;
		}

		protected static void DrawImageData(RendererContext context, BitDepth bitDepth, int width, int height, byte[] data) {
			switch (bitDepth) {
				case BitDepth.BlackWhite:
					DrawImageData2(context, width, height, data);
					break;
				case BitDepth.Gray4:
					DrawImageData4(context, width, height, data);
					break;
				case BitDepth.Gray16:
					DrawImageData16(context, width, height, data);
					break;
				case BitDepth.Gray256:
					DrawImageData256(context, width, height, data);
					break;
			}
		}

		protected static void DrawImageData2(RendererContext context, int width, int height, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int bx = (int)context.DX + context.X;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				byte b = data[i];
				for (int n = 0; n < 8; n++) {
					if (dx >= width) {
						dx = 0;
						dy++;
						if (dy >= height) {
							i = length;
							break;
						}
					}
					if (bx + dx > bitmap.Width) {
						dx++;
						continue;
					}
					if (((b << n) & 0x80) == 0) {
						dx++;
						continue;
					}
					bitmap.SetPixel(bx + dx, by + dy, color);
					dx++;
				}
			}
			//context.NextGlyph();
		}

		protected static void DrawImageData4(RendererContext context, int width, int height, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int bx = (int)context.DX + context.X;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				byte b = data[i];
				for (int n = 6; n >= 0; n -= 2) {
					if (dx >= width) {
						dx = 0;
						dy++;
						if (dy >= height) {
							i = length;
							break;
						}
					}
					if (bx + dx > bitmap.Width) {
						dx++;
						continue;
					}
					byte alpha = (byte)((b >> n) & 0x03);
					if (alpha == 0) {
						dx++;
						continue;
					}
					alpha *= 85;
					bitmap.SetPixel(bx + dx, by + dy, Color.FromArgb(alpha, color));
					dx++;
				}
			}
			//context.NextGlyph();
		}

		protected static void DrawImageData16(RendererContext context, int width, int height, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int bx = (int)context.DX + context.X;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				byte b = data[i];
				for (int n = 4; n >= 0; n -= 4) {
					if (dx >= width) {
						dx = 0;
						dy++;
						if (dy >= height) {
							i = length;
							break;
						}
					}
					if (bx + dx > bitmap.Width) {
						dx++;
						continue;
					}
					byte alpha = (byte)((b >> n) & 0x0F);
					if (alpha == 0) {
						dx++;
						continue;
					}
					alpha *= 17;
					bitmap.SetPixel(bx + dx, by + dy, Color.FromArgb(alpha, color));
					dx++;
				}
			}
			//context.NextGlyph();
		}

		protected static void DrawImageData256(RendererContext context, int width, int height, byte[] data) {
			Bitmap bitmap = context.Bitmap;
			Color color = Color.Black;
			int bx = (int)context.DX + context.X;
			int by = (int)context.DY;
			int dx = 0;
			int dy = 0;
			int length = data.Length;
			for (int i = 0; i < length; i++) {
				if (dx >= width) {
					dx = 0;
					dy++;
					if (dy >= height) {
						break;
					}
				}
				if (bx + dx > bitmap.Width) {
					dx++;
					continue;
				}
				byte alpha = data[i];
				bitmap.SetPixel(bx + dx, by + dy, Color.FromArgb(alpha, color));
				dx++;
			}
			//context.NextGlyph();
		}
	}
}
