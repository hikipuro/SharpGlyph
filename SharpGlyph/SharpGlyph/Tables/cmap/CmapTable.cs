using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Character to Glyph Index Mapping Table (cmap).
	/// <para>Required Table</para>
	/// </summary>
	//[RequiredTable]
	public class CmapTable : Table {
		public const string Tag = "cmap";
		
		/// <summary>
		/// Table version number (0).
		/// </summary>
		public ushort version;
		
		/// <summary>
		/// Number of encoding tables that follow.
		/// </summary>
		public ushort numTables;

		/// <summary>
		/// Encoding records.
		/// </summary>
		public EncodingRecord[] encodingRecords;

		public CmapSubtable[] subtables;
		
		public static CmapTable Read(BinaryReaderFont reader) {
			long start = reader.Position;
			CmapTable value = new CmapTable {
				version = reader.ReadUInt16(),
				numTables = reader.ReadUInt16()
			};
			ushort numTables = value.numTables;
			//value.encodingRecords = EncodingRecord.ReadArray(reader, numTables);
			value.encodingRecords = new EncodingRecord[numTables];
			value.subtables = new CmapSubtable[numTables];
			long position = reader.Position;
			for (int i = 0; i < numTables; i++) {
				reader.Position = position;
				EncodingRecord record = EncodingRecord.Read(reader);
				value.encodingRecords[i] = record;
				position = reader.Position;
				reader.Position = start + record.offset;
				//reader.Position = start + value.encodingRecords[i].offset;
				value.subtables[i] = CmapSubtable.Read(reader);
			}
			//Console.WriteLine("position: {0:X}", (reader.Position - start));
			return value;
		}

		public int GetGlyphId(int charCode) {
			int index = FindIndex(PlatformID.Unicode, EncodingID.Unicode.Unicode_2_Full);
			//if (index < 0) {
			//	index = FindIndex(PlatformID.Unicode, EncodingID.Unicode.UnicodeVariation);
			//}
			if (index < 0) {
				index = FindIndex(PlatformID.Unicode, EncodingID.Unicode.Unicode_2_Full);
			}
			if (index < 0) {
				index = FindIndex(PlatformID.Unicode, EncodingID.Unicode.Unicode_2_BMP);
			}
			if (index < 0) {
				index = FindIndex(PlatformID.Windows, EncodingID.Windows.UnicodeFullRepertoire);
			}
			if (index < 0) {
				index = FindIndex(PlatformID.Windows, EncodingID.Windows.UnicodeBMP);
			}
			if (index < 0) {
				index = 0;
			}
			//int index = FindIndex(platformID, encodingID);
			if (index < 0) {
				return -1;
			}
			//System.Console.WriteLine("subtables[index]: {0}", subtables[index]);
			return subtables[index].GetGlyphId(charCode);
		}

		public CharToGlyphTable CreateCharToGlyphTable() {
			int index = FindIndex(PlatformID.Unicode, EncodingID.Unicode.Unicode_2_Full);
			//if (index < 0) {
			//	index = FindIndex(PlatformID.Unicode, EncodingID.Unicode.UnicodeVariation);
			//}
			if (index < 0) {
				index = FindIndex(PlatformID.Unicode, EncodingID.Unicode.Unicode_2_Full);
			}
			if (index < 0) {
				index = FindIndex(PlatformID.Unicode, EncodingID.Unicode.Unicode_2_BMP);
			}
			if (index < 0) {
				index = FindIndex(PlatformID.Windows, EncodingID.Windows.UnicodeFullRepertoire);
			}
			if (index < 0) {
				index = FindIndex(PlatformID.Windows, EncodingID.Windows.UnicodeBMP);
			}
			if (index < 0) {
				index = 0;
			}
			return subtables[index].CreateCharToGlyphTable();
		}

		protected int FindIndex(PlatformID platformID, ushort encodingID) {
			for (int i = 0; i < numTables; i++) {
				EncodingRecord record = encodingRecords[i];
				if (record.platformID == platformID
				&& record.encodingID == encodingID) {
					return i;
				}
			}
			return -1;
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"numTables\": {0},\n", numTables);
			builder.AppendLine("\t\"encodingRecords\": [");
			for (int i = 0; i < numTables; i++) {
				string encodingRecord = encodingRecords[i].ToString();
				builder.AppendFormat("\t\t{0},\n", encodingRecord.Replace("\n", "\n\t\t"));
			}
			if (numTables > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t],");
			builder.AppendLine("\t\"subtables\": [");
			for (int i = 0; i < numTables; i++) {
				string subtable = subtables[i].ToString();
				builder.AppendFormat("\t\t{0},\n", subtable.Replace("\n", "\n\t\t"));
			}
			if (numTables > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
