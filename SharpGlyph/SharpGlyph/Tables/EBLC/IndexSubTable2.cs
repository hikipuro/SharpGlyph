using System;
using System.Text;

namespace SharpGlyph {
	public class IndexSubTable2 : IndexSubTable {
		/// <summary>
		/// All the glyphs are of the same size.
		/// </summary>
		public uint imageSize;

		/// <summary>
		/// All glyphs have the same metrics;
		/// glyph data may be compressed, byte-aligned, or bit-aligned.
		/// </summary>
		public BigGlyphMetrics bigMetrics;

		public static IndexSubTable2 Read(BinaryReaderFont reader) {
			return new IndexSubTable2 {
				header = IndexSubHeader.Read(reader),
				imageSize = reader.ReadUInt32(),
				bigMetrics = BigGlyphMetrics.Read(reader)
			};
		}

		public override GlyphBitmapData ReadBitmapData(BinaryReaderFont reader, int glyphId, int index) {
			reader.Position += imageSize * index;
			return GlyphBitmapData.Read(
				reader,
				header.imageFormat,
				(int)imageSize
			);
		}

		/*
		public override GlyphBitmapData[] ReadGlyphBitmapData(BinaryReaderFont reader, int count) {
			ushort imageFormat = header.imageFormat;
			return GlyphBitmapData.ReadArray(
				reader,
				imageFormat,
				(int)imageSize,
				count
			);
		}
		*/

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"header\": {0},\n", header.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"imageSize\": {0},\n", imageSize);
			builder.AppendFormat("\t\"bigMetrics\": {0},\n", bigMetrics.ToString().Replace("\n", "\n\t"));
			builder.Append("}");
			return builder.ToString();
		}
	}
}
