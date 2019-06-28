using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Compact Font Format Version 2 (CFF2).
	/// <para>CFF Outline</para>
	/// </summary>
	//[CFFOutline]
	public class CFF2Table : Table {
		public const string Tag = "CFF2";

		public static CFF2Table Read(BinaryReaderFont reader) {
			return null;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
