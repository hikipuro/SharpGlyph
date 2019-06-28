using System;
using System.Text;

namespace SharpGlyph {
	public class GlyphIdOffsetPair {
		/// <summary>
		/// Glyph ID of glyph present.
		/// </summary>
		public ushort glyphID;

		/// <summary>
		/// Location in EBDT.
		/// </summary>
		public ushort offset;

		public static GlyphIdOffsetPair[] ReadArray(BinaryReaderFont reader, int count) {
			GlyphIdOffsetPair[] array = new GlyphIdOffsetPair[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static GlyphIdOffsetPair Read(BinaryReaderFont reader) {
			return new GlyphIdOffsetPair {
				glyphID = reader.ReadUInt16(),
				offset = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"glyphID\": {0},\n", glyphID);
			builder.AppendFormat("\t\"offset\": {0},\n", offset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
