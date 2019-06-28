using System;
using System.Text;

namespace SharpGlyph {
	public class ScriptList {
		/// <summary>
		/// Number of ScriptRecords.
		/// </summary>
		public ushort scriptCount;
		
		/// <summary>
		/// Array of ScriptRecords, listed alphabetically by script tag.
		/// </summary>
		public ScriptRecord[] scriptRecords;

		public static ScriptList Read(BinaryReaderFont reader) {
			return new ScriptList {
				scriptCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"scriptCount\": {0},\n", scriptCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
