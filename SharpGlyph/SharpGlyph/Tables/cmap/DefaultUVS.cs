using System;
using System.Text;

namespace SharpGlyph {
	public class DefaultUVS {
		/// <summary>
		/// Number of Unicode character ranges.
		/// </summary>
		public uint numUnicodeValueRanges;
		
		/// <summary>
		/// Array of UnicodeRange records.
		/// </summary>
		public UnicodeRange[] ranges;

		public static DefaultUVS Read(BinaryReaderFont reader) {
			DefaultUVS value = new DefaultUVS {
				numUnicodeValueRanges = reader.ReadUInt32()
			};
			value.ranges = UnicodeRange.ReadArray(reader, value.numUnicodeValueRanges);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"numUnicodeValueRanges\": {0},\n", numUnicodeValueRanges);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
