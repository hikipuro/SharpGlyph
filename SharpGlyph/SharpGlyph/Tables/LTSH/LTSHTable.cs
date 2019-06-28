using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Linear Threshold (LTSH).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class LTSHTable : Table {
		public const string Tag = "LTSH";

		/// <summary>
		/// Version number (starts at 0).
		/// </summary>
		public ushort version;

		/// <summary>
		/// Number of glyphs (from “numGlyphs” in 'maxp' table).
		/// </summary>
		public ushort numGlyphs;

		/// <summary>
		/// The vertical pel height at which the glyph
		/// can be assumed to scale linearly.
		/// On a per glyph basis.
		/// </summary>
		public byte[] yPels;

		public static LTSHTable Read(BinaryReaderFont reader) {
			return new LTSHTable {
				version = reader.ReadUInt16(),
				numGlyphs = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"numGlyphs\": {0},\n", numGlyphs);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
