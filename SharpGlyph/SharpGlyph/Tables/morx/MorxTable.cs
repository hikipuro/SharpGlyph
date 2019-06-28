using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Extended glyph metamorphosis table (morx).
	/// <para>Apple Table</para>
	/// </summary>
	//[AppleTable]
	public class MorxTable : Table {
		/// <summary>
		/// Version number of the extended glyph metamorphosis table (either 2 or 3).
		/// </summary>
		public ushort version;

		/// <summary>
		/// Set to 0.
		/// </summary>
		public ushort unused;

		/// <summary>
		/// Number of metamorphosis chains contained in this table.
		/// </summary>
		public uint nChains;

		public static MorxTable Read(BinaryReaderFont reader, TableRecord record) {
			MorxTable value = new MorxTable {
				version = reader.ReadUInt16(),
				unused = reader.ReadUInt16(),
				nChains = reader.ReadUInt32(),
			};
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"unused\": {0},\n", unused);
			builder.AppendFormat("\t\"nChains\": {0},\n", nChains);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
