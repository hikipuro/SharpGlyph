using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Glyph Data (glyf).
	/// <para>TrueType Outline</para>
	/// </summary>
	//[TrueTypeOutline]
	public class GlyfTable : Table {
		public const string Tag = "glyf";

		//Glyph[] glyphs;

		protected long position;
		protected string filePath;
		protected LocaTable loca;

		public static GlyfTable Read(BinaryReaderFont reader, LocaTable loca) {
			//long position = reader.Position;
			//uint[] offsets = loca.offsets;
			GlyfTable value = new GlyfTable();
			value.position = reader.Position;
			value.filePath = reader.FilePath;
			value.loca = loca;

			/*
			int length = loca.offsets.Length - 1;
			value.glyphs = new Glyph[length];
			for (int i = 0; i < length; i++) {
				uint offset = offsets[i];
				uint glyphLength = offsets[i + 1] - offset;
				if (glyphLength == 0) {
					value.glyphs[i] = null;
					continue;
				}
				reader.Position = position + offset;
				Glyph glyph = Glyph.Read(reader);
				value.glyphs[i] = glyph;
			}
			*/
			return value;
		}

		public Glyph GetGlyph(int glyphId) {
			if (glyphId < 0 || glyphId >= loca.numGlyphs) {
				return null;
			}
			if (File.Exists(filePath) == false) {
				return null;
			}
			uint offset0, offset1;
			loca.GetOffest(glyphId, out offset0, out offset1);
			uint glyphLength = offset1 - offset0;
			if (glyphLength == 0) {
				return null;
			}
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.Position = position + offset0;
				return Glyph.Read(reader);
			}
			/*
			if (index >= glyphs.Length) {
				return null;
			}
			return glyphs[index];
			//*/
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			/*
			builder.AppendFormat("\t\"glyphs.Count\": {0},\n", glyphs.Length);
			builder.AppendLine("\t\"glyphs\": [");
			for (int i = 0; i < glyphs.Length; i++) {
				Glyph glyph = glyphs[i];
				if (glyph == null) {
					continue;
				}
				builder.AppendFormat("\t\t{0},\n", glyph.ToString().Replace("\n", "\n\t\t"));
			}
			if (glyphs.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			*/
			builder.Append("}");
			return builder.ToString();
		}
	}
}
