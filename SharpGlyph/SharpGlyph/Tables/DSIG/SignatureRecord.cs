using System;
using System.Text;

namespace SharpGlyph {
	public class SignatureRecord {
		/// <summary>
		/// Format of the signature.
		/// </summary>
		public uint format;

		/// <summary>
		/// Length of signature in bytes.
		/// </summary>
		public uint length;

		/// <summary>
		/// Offset to the signature block from the beginning of the table.
		/// </summary>
		public uint offset;

		public static SignatureRecord[] ReadArray(BinaryReaderFont reader, int count) {
			SignatureRecord[] array = new SignatureRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static SignatureRecord Read(BinaryReaderFont reader) {
			return new SignatureRecord {
				format = reader.ReadUInt32(),
				length = reader.ReadUInt32(),
				offset = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"length\": {0},\n", length);
			builder.AppendFormat("\t\"offset\": {0}\n", offset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
