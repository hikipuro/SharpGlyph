using System;
using System.Text;

namespace SharpGlyph {
	public class UnicodeRange {
		/// <summary>
		/// First value in this range.
		/// </summary>
		public int startUnicodeValue;
		
		/// <summary>
		/// Number of additional values in this range.
		/// </summary>
		public byte additionalCount;

		public static UnicodeRange[] ReadArray(BinaryReaderFont reader, uint count) {
			UnicodeRange[] array = new UnicodeRange[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static UnicodeRange Read(BinaryReaderFont reader) {
			return new UnicodeRange {
				startUnicodeValue = reader.ReadUInt24(),
				additionalCount = reader.ReadByte()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"startUnicodeValue\": {0},\n", startUnicodeValue);
			builder.AppendFormat("\t\"additionalCount\": {0}\n", additionalCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
