using System;
using System.Text;

namespace SharpGlyph {
	public class NameRecord {
		/// <summary>
		/// Platform ID.
		/// </summary>
		public PlatformID platformID;
		
		/// <summary>
		/// Platform-specific encoding ID.
		/// </summary>
		public ushort encodingID;
		
		/// <summary>
		/// Language ID.
		/// </summary>
		public ushort languageID;
		
		/// <summary>
		/// Name ID.
		/// </summary>
		public ushort nameID;
		
		/// <summary>
		/// String length (in bytes).
		/// </summary>
		public ushort length;
		
		/// <summary>
		/// String offset from start of storage area (in bytes).
		/// </summary>
		public ushort offset;

		public string text;

		public static NameRecord[] ReadArray(BinaryReaderFont reader, int count) {
			NameRecord[] array = new NameRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}
		
		public static NameRecord Read(BinaryReaderFont reader) {
			NameRecord value = new NameRecord {
				platformID = (PlatformID)reader.ReadUInt16(),
				encodingID = reader.ReadUInt16(),
				languageID = reader.ReadUInt16(),
				nameID = reader.ReadUInt16(),
				length = reader.ReadUInt16(),
				offset = reader.ReadUInt16()
			};
			return value;
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"platformID\": \"{0}\",\n", platformID);
			builder.AppendFormat("\t\"encodingID\": \"{0}\",\n", EncodingID.ToName(platformID, encodingID));
			builder.AppendFormat("\t\"languageID\": \"{0}\",\n", LanguageID.ToName(platformID, languageID));
			builder.AppendFormat("\t\"nameID\": \"{0}\",\n", NameID.ToName(nameID));
			builder.AppendFormat("\t\"length\": 0x{0:X4},\n", length);
			builder.AppendFormat("\t\"offset\": 0x{0:X4},\n", offset);
			builder.AppendFormat("\t\"text\": \"{0}\"\n", text);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
