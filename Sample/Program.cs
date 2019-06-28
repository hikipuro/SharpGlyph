using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using SharpGlyph;

namespace Sample {
	class MainClass {
		public static void Main(string[] args) {
			long memory = System.GC.GetTotalMemory(false);
			//string path = "/System/Library/Fonts/Apple Color Emoji.ttc";
			//string path = "/System/Library/Fonts/Times.ttc";
			//string path = "/System/Library/Fonts/Helvetica.ttc";
			string path = "/System/Library/Fonts/Menlo.ttc";
			//string path = "/System/Library/Fonts/PingFang.ttc";
			//string path = "/System/Library/Fonts/Hiragino Sans GB.ttc";
			//string path = "/System/Library/Fonts/Apple Symbols.ttf";
			//string path = "/System/Library/Fonts/SFNSTextCondensed-Semibold.otf";
			//string path = "/Library/Fonts/Arial.ttf";
			//string path = "/Library/Fonts/Verdana.ttf";
			//string path = "/Library/Fonts/Tahoma.ttf";
			//string path = "/Library/Fonts/Courier New.ttf";
			//string path = "C:/Windows/Fonts/Arvo-Regular.ttf";
			//string path = "C:/Windows/Fonts/msgothic.ttc";
			SharpGlyph.Font[] fonts = SharpGlyph.Font.Load(path);

			if (fonts == null) {
				Console.WriteLine("Font file not found.");
				return;
			}

			for (int i = 0; i < fonts.Length; i++) {
				SharpGlyph.Font f = fonts[i];
				Console.WriteLine("Font name: " + f.FullName);
				Console.WriteLine("Font Family: " + f.Family);
				Console.WriteLine("Font Subfamily: " + f.Subfamily);
				Console.WriteLine("Font Identifier: " + f.Identifier);
				Console.WriteLine();
			}

			SharpGlyph.Font font = fonts[0];
			Bitmap bitmap = new Bitmap(256, 256);
			//Graphics g = Graphics.FromImage(bitmap);
			//g.Clear(Color.White);
			//g.SmoothingMode = SmoothingMode.HighQuality;


			Stopwatch stopwatch = Stopwatch.StartNew();
			font.FontSize = 24;
			//font.UseBitmapGlyph = true;
			font.DrawText(bitmap, font.FullName, 0, 0);
			stopwatch.Stop();
			Console.WriteLine(stopwatch.Elapsed);

			bitmap.Save("Test.png", ImageFormat.Png);
			bitmap.Dispose();

			memory = System.GC.GetTotalMemory(false) - memory;
			Console.WriteLine("memory: {0:N0}", memory);
			Console.ReadKey();
		}
	}
}
