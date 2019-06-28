using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Naming Table (name).
	/// <para>Required Table</para>
	/// </summary>
	//[RequiredTable]
	public class NameTable : Table {
		public const string Tag = "name";
		
		/// <summary>
		/// Format selector (0 or 1).
		/// </summary>
		public ushort format;

		/// <summary>
		/// [Format 0, 1]
		/// Number of name records.
		/// </summary>
		public ushort count;

		/// <summary>
		/// [Format 0, 1]
		/// Offset to start of string storage (from start of table).
		/// </summary>
		public ushort stringOffset;

		/// <summary>
		/// [Format 0, 1]
		/// The name records where count is the number of records.
		/// </summary>
		public NameRecord[] nameRecord;

		/// <summary>
		/// [Format 1]
		/// Number of language-tag records.
		/// </summary>
		public ushort langTagCount;

		/// <summary>
		/// [Format 1]
		/// The language-tag records where langTagCount is the number of records.
		/// </summary>
		public LangTagRecord[] langTagRecord;
		
		public static NameTable Read(BinaryReaderFont reader) {
			NameTable value = new NameTable {
				format = reader.ReadUInt16(),
				count = reader.ReadUInt16(),
				stringOffset = reader.ReadUInt16(),
			};
			value.nameRecord = NameRecord.ReadArray(reader, value.count);
			if (value.format >= 1) {
				value.langTagCount = reader.ReadUInt16();
				value.langTagRecord = LangTagRecord.ReadArray(reader, value.langTagCount);
			}
			long start = reader.Position;
			for (int i = 0; i < value.count; i++) {
				NameRecord record = value.nameRecord[i];
				reader.Position = start + record.offset;
				record.text = reader.ReadString(record.length, GetEncoding(record));
			}
			if (value.format >= 1) {
				for (int i = 0; i < value.langTagCount; i++) {
					LangTagRecord record = value.langTagRecord[i];
					reader.Position = start + record.offset;
					record.text = reader.ReadString(record.length, Encoding.BigEndianUnicode);
				}
			}
			return value;
		}
		
		protected static Encoding GetEncoding(NameRecord record) {
			try {
				switch (record.platformID) {
					case PlatformID.Unicode:
						return Encoding.BigEndianUnicode;
					case PlatformID.Macintosh:
						switch (record.encodingID) {
							case 0: return Encoding.ASCII;
							case 1: return Encoding.GetEncoding("x-mac-japanese");
							case 2: return Encoding.GetEncoding("x-mac-chinesetrad");
							case 3: return Encoding.GetEncoding("x-mac-korean");
							case 4: return Encoding.GetEncoding("x-mac-arabic");
							case 5: return Encoding.GetEncoding("x-mac-hebrew");
							case 6: return Encoding.GetEncoding("x-mac-greek");
							case 7: return Encoding.GetEncoding("x-mac-cyrillic");
							//case 8: return Encoding.GetEncoding("");
							case 9: return Encoding.GetEncoding("x-iscii-de");
							//case 10: return Encoding.GetEncoding("");
							case 11: return Encoding.GetEncoding("x-iscii-gu");
							case 12: return Encoding.GetEncoding("x-iscii-or");
							case 13: return Encoding.GetEncoding("x-iscii-be");
							case 14: return Encoding.GetEncoding("x-iscii-ta");
							case 15: return Encoding.GetEncoding("x-iscii-te");
							case 16: return Encoding.GetEncoding("x-iscii-ka");
							case 17: return Encoding.GetEncoding("x-iscii-ma");
							//case 18: return Encoding.GetEncoding("");
							case 19: return Encoding.GetEncoding("Windows-1252");
							//case 20: return Encoding.GetEncoding("");
							case 21: return Encoding.GetEncoding("x-mac-thai");
							//case 22: return Encoding.GetEncoding("");
							//case 23: return Encoding.GetEncoding("");
							//case 24: return Encoding.GetEncoding("");
							case 25: return Encoding.GetEncoding("x-mac-chinesesimp");
							//case 26: return Encoding.GetEncoding("");
							//case 27: return Encoding.GetEncoding("");
							//case 28: return Encoding.GetEncoding("");
							//case 29: return Encoding.GetEncoding("");
							case 30: return Encoding.GetEncoding("windows-1258");
							//case 31: return Encoding.GetEncoding("");
							//case 32: return Encoding.GetEncoding("");
						}
						break;
					case PlatformID.ISO:
						switch (record.encodingID) {
							case 0: return Encoding.ASCII;
							case 1: return Encoding.BigEndianUnicode;
							case 2: return Encoding.GetEncoding(1252);
						}
						break;
					case PlatformID.Windows:
						switch (record.encodingID) {
							case 1: return Encoding.BigEndianUnicode;
							case 2: return Encoding.GetEncoding("shift_jis");
							case 3: return Encoding.GetEncoding("gb2312");
							case 4: return Encoding.GetEncoding("big5");
							case 5: return Encoding.GetEncoding("x-cp20949");
							case 6: return Encoding.GetEncoding("Johab");
							case 10: return Encoding.BigEndianUnicode;
						}
						break;
				}
			} catch (System.Exception) {
				return Encoding.ASCII;
			}
			return Encoding.ASCII;
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"count\": {0},\n", count);
			builder.AppendFormat("\t\"stringOffset\": 0x{0:X4},\n", stringOffset);
			builder.AppendLine("\t\"nameRecord\": [");
			for (int i = 0; i < count; i++) {
				string item = nameRecord[i].ToString();
				builder.AppendFormat("\t\t{0},\n", item.Replace("\n", "\n\t\t"));
			}
			if (count > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			if (format == 1) {
				builder.AppendFormat("\t\"langTagCount\": {0},\n", langTagCount);
				for (int i = 0; i < count; i++) {
					builder.AppendLine(langTagRecord[i].text);
				}
			}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
