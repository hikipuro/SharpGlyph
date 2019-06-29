using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace SharpGlyph {
	public class Font {
		public static bool IsDebug = false;
		public static readonly int MaxCharCode = 0x10FFFF;

		public Tables Tables;
		public string Copyright = string.Empty;
		public string Family = string.Empty;
		public string Subfamily = string.Empty;
		public string Identifier = string.Empty;
		public string FullName = string.Empty;
		public string Version = string.Empty;
		//CharToGlyphTable charToGlyphTable;
		protected Renderer renderer;
		protected Interpreter interpreter;
		//protected bool Initialized;
		//protected byte[] Data;

		public int FontSize {
			get { return renderer.FontSize; }
			set { renderer.FontSize = value; }
		}

		public bool UseBitmapGlyph {
			get { return renderer.UseBitmapGlyph; }
			set { renderer.UseBitmapGlyph = value; }
		}

		public bool UseInterpreter {
			get { return renderer.UseInterpreter; }
			set { renderer.UseInterpreter = value; }
		}

		public Font() {
			Tables = new Tables();
			//charToGlyphTable = new CharToGlyphTable();
		}

		public static Font[] Load(string filePath) {
			return FontParser.Parse(filePath);
		}

		public static Font Read(BinaryReaderFont reader) {
			Font font = new Font();
			//Console.WriteLine(offsetTable);
			font.Tables.ReadTableRecords(reader);
			#if DEBUG
			if (IsDebug) {
				Console.WriteLine();
				font.DebugLogRecords();
			}
			#endif
			font.Tables.ReadTables(reader);
			//font.Tables.Records = null;

			font.SetFontName();
			//long memory = GC.GetTotalMemory(false);
			//font.CreateCharacterToGlyphTable();
			//memory = GC.GetTotalMemory(false) - memory;
			//Console.WriteLine("cmap memory: {0}", memory);

			font.interpreter = new Interpreter(font);
			font.InterpretFpgm();

			font.renderer = new Renderer(font, font.interpreter);
			//font.Tables.name = null;
			
			#if DEBUG
				//Console.WriteLine("GetFuncCount: " + font.interpreter.funcs.GetFuncCount());

				//foreach (Strike strike in fontData.sbix.strikes) {
				//	Console.WriteLine(strike);
				//}
				//Console.WriteLine(font.Tables.avar);
				//Console.WriteLine("\"BASE\": {0}", font.Tables.BASE);
				//Console.WriteLine(font.Tables.CBDT);
				//Console.WriteLine(font.Tables.CBLC);
				//Console.WriteLine(font.Tables.CFF);
				//Console.WriteLine(font.Tables.CFF2);
				//Console.WriteLine("\"cmap\": {0}", font.Tables.cmap);
				//Console.WriteLine(font.Tables.COLR);
				//Console.WriteLine(font.Tables.CPAL);
				//Console.WriteLine(font.Tables.cvar);
				//Console.WriteLine(font.Tables.cvt);
				//Console.WriteLine(font.Tables.DSIG);
				//Console.WriteLine("\"EBDT\": {0}", font.Tables.EBDT);
				//Console.WriteLine("\"EBLC\": {0}", font.Tables.EBLC);
				//Console.WriteLine(font.Tables.EBSC);
				//Console.WriteLine(font.Tables.fpgm);
				//Console.WriteLine(font.Tables.fvar);
				//Console.WriteLine(font.Tables.gasp);
				//Console.WriteLine(font.Tables.GDEF);
				//Console.WriteLine(font.Tables.glyf);
				//Console.WriteLine(font.Tables.GPOS);
				//Console.WriteLine(font.Tables.GSUB);
				//Console.WriteLine(font.Tables.gvar);
				//Console.WriteLine(font.Tables.hdmx);
				//Console.WriteLine("\"head\": {0}", font.Tables.head);
				//Console.WriteLine("\"hhea\": {0}", font.Tables.hhea);
				//Console.WriteLine(font.Tables.hmtx);
				//Console.WriteLine(font.Tables.HVAR);
				//Console.WriteLine(font.Tables.JSTF);
				//Console.WriteLine(font.Tables.kern);
				//Console.WriteLine(font.Tables.loca);
				//Console.WriteLine(font.Tables.LTSH);
				//Console.WriteLine(font.Tables.MATH);
				//Console.WriteLine("\"maxp\": {0}", font.Tables.maxp);
				//Console.WriteLine(font.Tables.MERG);
				//Console.WriteLine("\"meta\": {0}", font.Tables.meta);
				//Console.WriteLine(font.Tables.MVAR);
				//Console.WriteLine(font.Tables.name);
				//Console.WriteLine(font.Tables.OS2);
				//Console.WriteLine(font.Tables.pclt);
				//Console.WriteLine(font.Tables.post);
				//Console.WriteLine(font.Tables.prep);
				//Console.WriteLine(font.Tables.sbix);
				//Console.WriteLine(font.Tables.STAT);
				//Console.WriteLine(font.Tables.SVG);
				//Console.WriteLine(font.Tables.VDMX);
				//Console.WriteLine(font.Tables.vhea);
				//Console.WriteLine(font.Tables.vmtx);
				//Console.WriteLine(font.Tables.VORG);
				//Console.WriteLine(font.Tables.VVAR);
			#endif

			return font;
		}

		public int GetGlyphId(int charCode) {
			if (charCode < 0 || charCode > MaxCharCode) {
				return 0;
			}
			if (Tables.cmap == null) {
				return 0;
			}
			return Tables.cmap.GetGlyphId(charCode);
			/*
			if (charToGlyphTable.ContainsKey(charCode)) {
				return charToGlyphTable[charCode];
			}
			return 0;
			*/
		}

		public void DrawText(Bitmap bitmap, string text, float x, float y) {
			renderer.DrawText(bitmap, text, x, y);
		}

		protected void DebugLogRecords() {
			Regex regex = new Regex("\t|\n");
			for (int i = 0; i < Tables.offset.numTables; i++) {
				TableRecord record = Tables.Records[i];
				string text = record.ToString();
				text = regex.Replace(text, "");
				Console.WriteLine(text.Replace(",", ", "));
			}
		}

		protected void SetFontName() {
			if (Tables.name == null || Tables.name.nameRecord == null) {
				return;
			}
			foreach (NameRecord record in Tables.name.nameRecord) {
				switch (record.nameID) {
					case 0:
						Copyright = record.text;
						break;
					case 1:
						Family = record.text;
						break;
					case 2:
						Subfamily = record.text;
						break;
					case 3:
						Identifier = record.text;
						break;
					case 4:
						FullName = record.text;
						break;
					case 5:
						Version = record.text;
						break;
				}
			}
		}

		/*
		protected void CreateCharacterToGlyphTable() {
			CmapTable cmap = Tables.cmap;
			if (cmap == null || cmap.subtables == null) {
				return;
			}
			charToGlyphTable = cmap.CreateCharToGlyphTable();
		}
		*/

		protected void InterpretFpgm() {
			FpgmTable fpgm = Tables.fpgm;
			if (fpgm == null) {
				return;
			}
			byte[] data = fpgm.ReadData();
			if (data == null) {
				return;
			}
			#if DEBUG
			//Console.WriteLine("Fpgm Decode:\n{0}", Interpreter.Decode(data));
			#endif
			interpreter.Exec(data, null);
		}
	}
}
