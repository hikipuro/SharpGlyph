using System;
using System.Text;

namespace SharpGlyph {
	public class ConditionSet {
		/// <summary>
		/// Number of conditions for this condition set.
		/// </summary>
		public ushort conditionCount;
		
		/// <summary>
		/// Array of offsets to condition tables,
		/// from beginning of the ConditionSet table.
		/// </summary>
		public uint[] conditions;

		public static ConditionSet Read(BinaryReaderFont reader) {
			return new ConditionSet {
				conditionCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"conditionCount\": {0},\n", conditionCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
