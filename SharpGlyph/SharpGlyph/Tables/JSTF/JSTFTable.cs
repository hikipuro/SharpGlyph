using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Justification Table (JSTF).
	/// <para>Advanced Typographic Table</para>
	/// </summary>
	//[AdvancedTypographicTable]
	public class JSTFTable : Table {
		public const string Tag = "JSTF";

		/// <summary>
		/// Major version of the JSTF table, = 1.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version of the JSTF table, = 0.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// Number of JstfScriptRecords in this table.
		/// </summary>
		public ushort jstfScriptCount;

		/// <summary>
		/// Array of JstfScriptRecords, in alphabetical order by jstfScriptTag.
		/// </summary>
		public JstfScriptRecord[] jstfScriptRecords;

		public static JSTFTable Read(BinaryReaderFont reader) {
			return new JSTFTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				jstfScriptCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"jstfScriptCount\": {0},\n", jstfScriptCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
