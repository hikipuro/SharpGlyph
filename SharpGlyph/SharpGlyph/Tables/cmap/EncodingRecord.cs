using System.Text;

namespace SharpGlyph {
	public class EncodingRecord {
		/// <summary>
		/// Platform ID.
		/// </summary>
		public PlatformID platformID;

		/// <summary>
		/// Platform-specific encoding ID.
		/// </summary>
		public ushort encodingID;

		/// <summary>
		/// Byte offset from beginning of table to the subtable for this encoding.
		/// </summary>
		public uint offset;

		public static EncodingRecord[] ReadArray(BinaryReaderFont reader, int count) {
			EncodingRecord[] array = new EncodingRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}
		
		public static EncodingRecord Read(BinaryReaderFont reader) {
			EncodingRecord record = new EncodingRecord {
				platformID = (PlatformID)reader.ReadUInt16(),
				encodingID = reader.ReadUInt16(),
				offset = reader.ReadUInt32()
			};
			return record;
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"platformID\": \"{0}\",\n", platformID);
			builder.AppendFormat("\t\"encodingID\": \"{0}\",\n", EncodingID.ToName(platformID, encodingID));
			builder.AppendFormat("\t\"offset\": 0x{0:X8}\n", offset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
