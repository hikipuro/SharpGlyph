using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Horizontal Metrics Table (hmtx).
	/// <para>Required Table</para>
	/// </summary>
	//[RequiredTable]
	public class HmtxTable : Table {
		public const string Tag = "hmtx";

		/// <summary>
		/// Paired advance width and left side
		/// bearing values for each glyph.
		/// Records are indexed by glyph ID.
		/// </summary>
		//public List<LongHorMetric> hMetrics;

		/// <summary>
		/// Left side bearings for glyph IDs
		/// greater than or equal to numberOfHMetrics.
		/// </summary>
		//short[] leftSideBearings;

		long position;
		string filePath;
		ushort numGlyphs;
		ushort numberOfHMetrics;

		public static HmtxTable Read(BinaryReaderFont reader, HheaTable hhea, MaxpTable maxp) {
			HmtxTable value = new HmtxTable();
			value.filePath = reader.FilePath;
			value.numGlyphs = maxp.numGlyphs;
			value.numberOfHMetrics = hhea.numberOfHMetrics;
			value.position = reader.Position;

			/*
			value.hMetrics = LongHorMetric.ReadList(reader, numberOfHMetrics);
			value.leftSideBearings = reader.ReadInt16Array(maxp.numGlyphs - numberOfHMetrics);
			for (int i = 0; i < value.leftSideBearings.Length; i++) {
				LongHorMetric hMetric = new LongHorMetric();
				hMetric.lsb = value.leftSideBearings[i];
				value.hMetrics.Add(hMetric);
			}
			value.leftSideBearings = null;
			*/
			return value;
		}

		public LongHorMetric GetMetric(int glyphId) {
			if (glyphId < 0 || glyphId >= numGlyphs) {
				return null;
			}
			if (File.Exists(filePath) == false) {
				return null;
			}
			if (glyphId < numberOfHMetrics) {
				using (Stream stream = File.OpenRead(filePath))
				using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
					reader.Position = position + glyphId * LongHorMetric.ByteSize;
					return LongHorMetric.Read(reader);
				}
			}
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.Position = position
					+ numberOfHMetrics * LongHorMetric.ByteSize
					+ (glyphId - numberOfHMetrics) * 2;
				LongHorMetric hMetric = new LongHorMetric();
				hMetric.lsb = reader.ReadInt16();
				return hMetric;
			}
			/*
			if (glyphId >= hMetrics.Count) {
				return null;
			}
			return hMetrics[glyphId];
			*/
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			/*
			builder.AppendFormat("\t\"hMetrics.length\": {0},\n", hMetrics.Count);
			builder.AppendFormat("\t\"hMetrics\": [\n");
			for (int i = 0; i < hMetrics.Count; i++) {
				string item = hMetrics[i].ToString();
				builder.AppendFormat("\t\t\t{0},\n", item.Replace("\n", "\n\t\t\t"));
			}
			if (hMetrics.Count > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t],");
			builder.AppendFormat("\t\"leftSideBearings.length\": {0},\n", leftSideBearings.Length);
			builder.AppendFormat("\t\"leftSideBearings\": [\n");
			for (int i = 0; i < leftSideBearings.Length; i++) {
				builder.AppendFormat("\t\t{0},\n", leftSideBearings[i]);
			}
			if (leftSideBearings.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			*/
			builder.Append("}");
			return builder.ToString();
		}
	}
}
