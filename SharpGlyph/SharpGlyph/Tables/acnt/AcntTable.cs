using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Accent attachment table (acnt).
	/// <para>Apple Table</para>
	/// </summary>
	//[AppleTable]
	public class AcntTable : Table {

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
