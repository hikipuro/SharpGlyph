using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Vertical Origin Table (VORG).
	/// <para>CFF Outline</para>
	/// </summary>
	//[CFFOutline]
	public class VORGTable : Table {
		public const string Tag = "VORG";
		
		/// <summary>
		/// Major version (starting at 1). Set to 1.
		/// </summary>
		public ushort majorVersion;
		
		/// <summary>
		/// Minor version (starting at 0). Set to 0.
		/// </summary>
		public ushort minorVersion;
		
		/// <summary>
		/// The y coordinate of a glyph’s vertical origin,
		/// in the font’s design coordinate system,
		/// to be used if no entry is present for the glyph
		/// in the vertOriginYMetrics array.
		/// </summary>
		public short defaultVertOriginY;
		
		/// <summary>
		/// Number of elements in the vertOriginYMetrics array.
		/// </summary>
		public ushort numVertOriginYMetrics;

		public static VORGTable Read(BinaryReaderFont reader) {
			return new VORGTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				defaultVertOriginY = reader.ReadInt16(),
				numVertOriginYMetrics = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"defaultVertOriginY\": {0},\n", defaultVertOriginY);
			builder.AppendFormat("\t\"numVertOriginYMetrics\": {0},\n", numVertOriginYMetrics);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
