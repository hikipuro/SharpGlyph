using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// CmapSubtable Format 12 Sequential Map Group.
	/// </summary>
	public class SequentialMapGroup {
		public static readonly int ByteSize = 12;
		
		/// <summary>
		/// First character code in this group.
		/// </summary>
		public uint startCharCode;
		
		/// <summary>
		/// Last character code in this group.
		/// </summary>
		public uint endCharCode;
		
		/// <summary>
		/// Glyph index corresponding to the starting character code.
		/// </summary>
		public uint startGlyphID;
		
		public static SequentialMapGroup[] ReadArray(BinaryReaderFont reader, uint count) {
			SequentialMapGroup[] array = new SequentialMapGroup[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}
		
		public static SequentialMapGroup Read(BinaryReaderFont reader) {
			return new SequentialMapGroup {
				startCharCode = reader.ReadUInt32(),
				endCharCode = reader.ReadUInt32(),
				startGlyphID = reader.ReadUInt32()
			};
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"startCharCode\": 0x{0:X8},\n", startCharCode);
			builder.AppendFormat("\t\"endCharCode\": 0x{0:X8},\n", endCharCode);
			builder.AppendFormat("\t\"startGlyphID\": 0x{0:X8}\n", startGlyphID);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
