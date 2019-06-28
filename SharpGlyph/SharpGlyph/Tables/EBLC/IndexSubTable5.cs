using System;
using System.Text;

namespace SharpGlyph {
	public class IndexSubTable5 : IndexSubTable {
		/// <summary>
		/// All glyphs have the same data size.
		/// </summary>
		public uint imageSize;

		/// <summary>
		/// All glyphs have the same metrics.
		/// </summary>
		public BigGlyphMetrics bigMetrics;

		/// <summary>
		/// Array length.
		/// </summary>
		public uint numGlyphs;

		/// <summary>
		/// One per glyph, sorted by glyph ID.
		/// </summary>
		//public ushort[] glyphIdArray;

		public static IndexSubTable5 Read(BinaryReaderFont reader) {
			IndexSubTable5 value = new IndexSubTable5();
			value.header = IndexSubHeader.Read(reader);
			value.imageSize = reader.ReadUInt32();
			value.bigMetrics = BigGlyphMetrics.Read(reader);
			value.numGlyphs = reader.ReadUInt32();
			value.position = reader.Position;
			//value.glyphIdArray = reader.ReadUInt16Array((int)value.numGlyphs);
			return value;
		}

		public override GlyphBitmapData ReadBitmapData(BinaryReaderFont reader, int glyphId, int index) {
			reader.Position += imageSize * FindGlyphIndex(reader, glyphId);
			return GlyphBitmapData.Read(
				reader,
				header.imageFormat,
				(int)imageSize
			);
		}

		protected int FindGlyphIndex(BinaryReaderFont reader, int glyphId) {
			int range = (int)(numGlyphs / 2);
			int index = range;
			//int count = (int)Math.Log(index, 2);
			//Console.WriteLine();
			//Console.WriteLine("charCode: {0:X}", charCode);
			while (range >= 1) {
				if (range > 1) {
					range /= 2;
				}
				reader.Position = position + index * 2;
				ushort id = reader.ReadUInt16();
				if (id == glyphId) {
					return index;
				}
				if (id < glyphId) {
					index += range;
					if (index >= numGlyphs) {
						break;
					}
					if (range == 1) {
						id = reader.ReadUInt16();
						if (id > glyphId) {
							break;
						}
					}
					continue;
				}
				if (id > glyphId) {
					index -= range;
					if (index < 0) {
						break;
					}
					continue;
				}
			}
			/*
			for (int i = 0; i < glyphIdArray.Length; i++) {
				if (glyphIdArray[i] == glyphId) {
					return i;
				}
			}
			*/
			return -1;
		}

		/*
		public override GlyphBitmapData[] ReadGlyphBitmapData(BinaryReaderFont reader, int count) {
			ushort imageFormat = header.imageFormat;
			return GlyphBitmapData.ReadArray(
				reader,
				imageFormat,
				(int)imageSize,
				(int)numGlyphs
			);
		}
		*/

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"header\": {0},\n", header.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"imageSize\": {0},\n", imageSize);
			builder.AppendFormat("\t\"bigMetrics\": {0},\n", bigMetrics.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"numGlyphs\": {0},\n", numGlyphs);
			/*
			builder.AppendLine("\t\"glyphIdArray\": [");
			for (int i = 0; i < numGlyphs; i++) {
				builder.AppendFormat("\t\t{0},\n", glyphIdArray[i]);
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
