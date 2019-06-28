using System;
using System.Text;

namespace SharpGlyph {
	public class CFFFDSelect3 : CFFFDSelect {
		/// <summary>
		/// Number of ranges.
		/// </summary>
		public ushort nRanges;

		/// <summary>
		/// Range3 array.
		/// </summary>
		public CFFRange3[] Range3;

		/// <summary>
		/// Sentinel GID.
		/// </summary>
		public ushort sentinel;

		public static new CFFFDSelect3 Read(BinaryReaderFont reader) {
			CFFFDSelect3 value = new CFFFDSelect3 {
				format = reader.ReadByte(),
				nRanges = reader.ReadUInt16()
			};
			value.Range3 = CFFRange3.ReadArray(reader, value.nRanges);
			value.sentinel = reader.ReadUInt16();
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
