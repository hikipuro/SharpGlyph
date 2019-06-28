using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class IndexSubTableArray {
		/// <summary>
		/// First glyph ID of this range.
		/// </summary>
		public ushort firstGlyphIndex;

		/// <summary>
		/// Last glyph ID of this range (inclusive).
		/// </summary>
		public ushort lastGlyphIndex;

		/// <summary>
		/// Add to indexSubTableArrayOffset to get offset from beginning of EBLC.
		/// </summary>
		public uint additionalOffsetToIndexSubtable;

		public IndexSubTable subTable;

		public static IndexSubTableArray Read(BinaryReaderFont reader, long start) {
			IndexSubTableArray value = new IndexSubTableArray {
				firstGlyphIndex = reader.ReadUInt16(),
				lastGlyphIndex = reader.ReadUInt16(),
				additionalOffsetToIndexSubtable = reader.ReadUInt32()
			};
			long position = reader.Position;
			uint offset = value.additionalOffsetToIndexSubtable;
			reader.Position = start + offset;
			value.subTable = IndexSubTable.Read(
				reader, value.firstGlyphIndex, value.lastGlyphIndex
			);
			reader.Position = position;
			//value.subTables = new IndexSubTable[count];
			//uint offset = value.additionalOffsetToIndexSubtable;
			//for (int i = 0; i < count; i++) {
				//reader.Position = start + offset * (i + 1);
				//value.subTables[i] = IndexSubTable.Read(reader);
			//}
			return value;
		}

		/*
		public GlyphBitmapData[] ReadGlyphBitmapData(BinaryReaderFont reader, long position) {
			if (subTable == null || subTable.header == null) {
				return null;
			}
			position += subTable.header.imageDataOffset;
			reader.Position = position;
			return subTable.ReadGlyphBitmapData(
				reader, lastGlyphIndex - firstGlyphIndex + 1
			);
		}
		*/

		public BigGlyphMetrics GetBigGlyphMetrics() {
			return subTable.GetBigGlyphMetrics();
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"firstGlyphIndex\": 0x{0:X4},\n", firstGlyphIndex);
			builder.AppendFormat("\t\"lastGlyphIndex\": 0x{0:X4},\n", lastGlyphIndex);
			builder.AppendFormat("\t\"additionalOffsetToIndexSubtable\": {0}\n", additionalOffsetToIndexSubtable);
			if (subTable != null) {
				builder.Remove(builder.Length - 1, 1);
				builder.AppendLine(",");
				builder.AppendFormat("\t\"subTable\" {0}\n", subTable.ToString().Replace("\n", "\n\t"));
				/*
				builder.AppendLine("\t\"subTables\": [");
				for (int i = 0; i < subTables.Length; i++) {
					string subTable = subTables[i].ToString();
					builder.AppendFormat("\t\t{0},\n", subTable.Replace("\n", "\n\t\t"));
				}
				if (subTables.Length > 0) {
					builder.Remove(builder.Length - 2, 1);
				}
				builder.AppendLine("\t]");
				*/
			}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
