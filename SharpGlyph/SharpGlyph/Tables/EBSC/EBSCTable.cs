using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Embedded Bitmap Scaling Table (EBSC).
	/// <para>Bitmap Glyph</para>
	/// </summary>
	//[BitmapGlyph]
	public class EBSCTable : Table {
		public const string Tag = "EBSC";

		/// <summary>
		/// Major version of the EBSC table, = 2.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version of the EBSC table, = 0.
		/// </summary>
		public ushort minorVersion;
		public uint numSizes;

		public static EBSCTable Read(BinaryReaderFont reader) {
			return new EBSCTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				numSizes = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"numSizes\": {0},\n", numSizes);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
