using System;
using System.Text;

namespace SharpGlyph {
	public class IndexSubTable4 : IndexSubTable {
		/// <summary>
		/// Array length.
		/// </summary>
		public uint numGlyphs;

		/// <summary>
		/// One per glyph.
		/// </summary>
		//public GlyphIdOffsetPair[] glyphArray;

		public static IndexSubTable4 Read(BinaryReaderFont reader) {
			IndexSubTable4 value = new IndexSubTable4();
			value.header = IndexSubHeader.Read(reader);
			value.numGlyphs = reader.ReadUInt32();
			value.position = reader.Position;
			//value.glyphArray = GlyphIdOffsetPair.ReadArray(reader, (int)value.numGlyphs + 1);
			return value;
		}

		public override GlyphBitmapData ReadBitmapData(BinaryReaderFont reader, int glyphId, int index) {
			if (index < 0 || index >= numGlyphs - 1) {
				return null;
			}
			long start = reader.Position;
			reader.Position = position + index;
			GlyphIdOffsetPair offset0 = GlyphIdOffsetPair.Read(reader);
			GlyphIdOffsetPair offset1 = GlyphIdOffsetPair.Read(reader);
			int byteSize = offset1.offset - offset0.offset;
			reader.Position = start + offset0.offset;
			return GlyphBitmapData.Read(
				reader,
				header.imageFormat,
				byteSize
			);
		}

		/*
		public override GlyphBitmapData[] ReadGlyphBitmapData(BinaryReaderFont reader, int count) {
			ushort imageFormat = header.imageFormat;
			GlyphBitmapData[] array = new GlyphBitmapData[numGlyphs];
			for (int i = 0; i < numGlyphs; i++) {
				ushort byteSize = (ushort)(glyphArray[i + 1].offset - glyphArray[i].offset);
				array[i] = GlyphBitmapData.Read(reader, imageFormat, byteSize);
			}
			return array;
		}
		*/

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"header\": {0},\n", header.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"numGlyphs\": {0},\n", numGlyphs);
			/*
			builder.AppendLine("\t\"glyphArray\": [");
			for (int i = 0; i < numGlyphs; i++) {
				builder.AppendFormat("\t\t{0},\n", glyphArray[i].ToString().Replace("\n", "\n\t"));
			}
			if (numGlyphs > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			*/
			builder.Append("}");
			return builder.ToString();
		}
	}
}
