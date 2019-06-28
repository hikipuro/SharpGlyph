using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Kerning (kern).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class KernTable : Table {
		public const string Tag = "kern";

		/// <summary>
		/// Table version number (0).
		/// </summary>
		public ushort version;

		/// <summary>
		/// Number of subtables in the kerning table.
		/// </summary>
		public ushort nTables;

		public static KernTable Read(BinaryReaderFont reader) {
			return new KernTable {
				version = reader.ReadUInt16(),
				nTables = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"nTables\": {0},\n", nTables);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
