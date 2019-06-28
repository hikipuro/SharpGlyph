using System;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Control Value Table (cvt).
	/// <para>TrueType Outline</para>
	/// </summary>
	//[TrueTypeOutline]
	public class CvtTable : Table {
		public const string Tag = "cvt ";

		/// <summary>
		/// List of n values referenceable by instructions.
		/// <para>
		/// n is the number of FWORD items that fit in the size of the table.
		/// </para>
		/// </summary>
		public int[] data;

		public static CvtTable Read(BinaryReaderFont reader, TableRecord record) {
			CvtTable value = new CvtTable();
			value.data = Array.ConvertAll(
				reader.ReadInt16Array((int)(record.length / 2)),
				new Converter<short, int>((a) => {
					return a;
				})
			);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"data.Length\": {0}\n", data.Length);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
