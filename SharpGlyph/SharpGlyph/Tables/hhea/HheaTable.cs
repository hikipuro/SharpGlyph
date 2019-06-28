using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Horizontal Header Table (hhea).
	/// <para>Required Table</para>
	/// </summary>
	//[RequiredTable]
	public class HheaTable : Table {
		public const string Tag = "hhea";

		/// <summary>
		/// Major version number of the horizontal header table — set to 1.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version number of the horizontal header table — set to 0.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// Typographic ascent (Distance from baseline of highest ascender).
		/// </summary>
		public short ascender;

		/// <summary>
		/// Typographic descent (Distance from baseline of lowest descender).
		/// </summary>
		public short descender;

		/// <summary>
		/// Typographic line gap.
		/// <para>
		/// Negative LineGap values are treated as zero
		/// in some legacy platform implementations.
		/// </para>
		/// </summary>
		public short lineGap;

		/// <summary>
		/// Maximum advance width value in 'hmtx' table.
		/// </summary>
		public ushort advanceWidthMax;

		/// <summary>
		/// Minimum left sidebearing value in 'hmtx' table.
		/// </summary>
		public short minLeftSideBearing;

		/// <summary>
		/// Minimum right sidebearing value; calculated as Min(aw - lsb - (xMax - xMin)).
		/// </summary>
		public short minRightSideBearing;

		/// <summary>
		/// Max(lsb + (xMax - xMin)).
		/// </summary>
		public short xMaxExtent;

		/// <summary>
		/// Used to calculate the slope of the cursor (rise/run); 1 for vertical.
		/// </summary>
		public short caretSlopeRise;

		/// <summary>
		/// 0 for vertical.
		/// </summary>
		public short caretSlopeRun;

		/// <summary>
		/// The amount by which a slanted highlight on a glyph needs
		/// to be shifted to produce the best appearance.
		/// Set to 0 for non-slanted fonts.
		/// </summary>
		public short caretOffset;

		/// <summary>
		/// set to 0.
		/// </summary>
		public short reserved0;

		/// <summary>
		/// set to 0.
		/// </summary>
		public short reserved1;

		/// <summary>
		/// set to 0.
		/// </summary>
		public short reserved2;

		/// <summary>
		/// set to 0.
		/// </summary>
		public short reserved3;

		/// <summary>
		/// 0 for current format.
		/// </summary>
		public short metricDataFormat;

		/// <summary>
		/// Number of hMetric entries in 'hmtx' table.
		/// </summary>
		public ushort numberOfHMetrics;

		public static HheaTable Read(BinaryReaderFont reader) {
			return new HheaTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				ascender = reader.ReadInt16(),
				descender = reader.ReadInt16(),
				lineGap = reader.ReadInt16(),
				advanceWidthMax = reader.ReadUInt16(),
				minLeftSideBearing = reader.ReadInt16(),
				minRightSideBearing = reader.ReadInt16(),
				xMaxExtent = reader.ReadInt16(),
				caretSlopeRise = reader.ReadInt16(),
				caretSlopeRun = reader.ReadInt16(),
				caretOffset = reader.ReadInt16(),
				reserved0 = reader.ReadInt16(),
				reserved1 = reader.ReadInt16(),
				reserved2 = reader.ReadInt16(),
				reserved3 = reader.ReadInt16(),
				metricDataFormat = reader.ReadInt16(),
				numberOfHMetrics = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"ascender\": {0},\n", ascender);
			builder.AppendFormat("\t\"descender\": {0},\n", descender);
			builder.AppendFormat("\t\"lineGap\": {0},\n", lineGap);
			builder.AppendFormat("\t\"advanceWidthMax\": {0},\n", advanceWidthMax);
			builder.AppendFormat("\t\"minLeftSideBearing\": {0},\n", minLeftSideBearing);
			builder.AppendFormat("\t\"minRightSideBearing\": {0},\n", minRightSideBearing);
			builder.AppendFormat("\t\"xMaxExtent\": {0},\n", xMaxExtent);
			builder.AppendFormat("\t\"caretSlopeRise\": {0},\n", caretSlopeRise);
			builder.AppendFormat("\t\"caretSlopeRun\": {0},\n", caretSlopeRun);
			builder.AppendFormat("\t\"caretOffset\": {0},\n", caretOffset);
			builder.AppendFormat("\t\"reserved0\": {0},\n", reserved0);
			builder.AppendFormat("\t\"reserved1\": {0},\n", reserved1);
			builder.AppendFormat("\t\"reserved2\": {0},\n", reserved2);
			builder.AppendFormat("\t\"reserved3\": {0},\n", reserved3);
			builder.AppendFormat("\t\"metricDataFormat\": {0},\n", metricDataFormat);
			builder.AppendFormat("\t\"numberOfHMetrics\": {0},\n", numberOfHMetrics);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
