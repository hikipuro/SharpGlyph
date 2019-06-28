using System;
using System.Text;

namespace SharpGlyph {
	public class ConstantMapGroup {
		/// <summary>
		/// First character code in this group.
		/// </summary>
		public uint startCharCode;
		
		/// <summary>
		/// Last character code in this group.
		/// </summary>
		public uint endCharCode;
		
		/// <summary>
		/// Glyph index to be used for all the characters in the group’s range.
		/// </summary>
		public uint glyphID;

		public static ConstantMapGroup[] ReadArray(BinaryReaderFont reader, uint count) {
			ConstantMapGroup[] array = new ConstantMapGroup[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static ConstantMapGroup Read(BinaryReaderFont reader) {
			return new ConstantMapGroup {
				startCharCode = reader.ReadUInt32(),
				endCharCode = reader.ReadUInt32(),
				glyphID = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"startCharCode\": 0x{0:X8},\n", startCharCode);
			builder.AppendFormat("\t\"endCharCode\": 0x{0:X8},\n", endCharCode);
			builder.AppendFormat("\t\"glyphID\": 0x{0:X8}\n", glyphID);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
