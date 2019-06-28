using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Metadata Table (meta).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class MetaTable : Table {
		public const string Tag = "meta";
		
		/// <summary>
		/// Version number of the metadata table — set to 1.
		/// </summary>
		public uint version;

		/// <summary>
		/// Flags — currently unused; set to 0.
		/// </summary>
		public uint flags;

		/// <summary>
		/// <para>[Microsoft] Not used; should be set to 0.</para>
		/// <para>[Apple] Offset from the beginning of the table to the data.</para>
		/// </summary>
		public uint dataOffset;

		/// <summary>
		/// The number of data maps in the table.
		/// </summary>
		public uint dataMapsCount;

		/// <summary>
		/// Array of data map records.
		/// </summary>
		public DataMap[] dataMaps;

		public static MetaTable Read(BinaryReaderFont reader) {
			long position = reader.Position;
			MetaTable value = new MetaTable {
				version = reader.ReadUInt32(),
				flags = reader.ReadUInt32(),
				dataOffset = reader.ReadUInt32(),
				dataMapsCount = reader.ReadUInt32(),
			};
			value.dataMaps = DataMap.ReadArray(reader, value.dataMapsCount);
			for (int i = 0; i < value.dataMapsCount; i++) {
				DataMap dataMap = value.dataMaps[i];
				reader.Position = position + dataMap.dataOffset;
				dataMap.data = reader.ReadBytes((int)dataMap.dataLength);
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"flags\": {0},\n", flags);
			builder.AppendFormat("\t\"dataOffset\": 0x{0:X8},\n", dataOffset);
			builder.AppendFormat("\t\"dataMapsCount\": {0},\n", dataMapsCount);
			builder.AppendLine("\t\"dataMap\": [");
			for (int i = 0; i < dataMapsCount; i++) {
				string dataMap = dataMaps[i].ToString();
				builder.AppendFormat("\t\t{0},\n", dataMap.Replace("\n", "\n\t\t"));
			}
			if (dataMapsCount > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
