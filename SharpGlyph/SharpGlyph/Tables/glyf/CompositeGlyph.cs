using System;
using System.Text;

namespace SharpGlyph {
	public class CompositeGlyph : GlyphDescription {
		/// <summary>
		/// component flag.
		/// </summary>
		public ushort flags;

		/// <summary>
		/// glyph index of component.
		/// </summary>
		public ushort glyphIndex;

		/// <summary>
		/// x-offset for component or point number;
		/// type depends on bits 0 and 1 in component flags.
		/// </summary>
		public ushort argument1;

		/// <summary>
		/// y-offset for component or point number;
		/// type depends on bits 0 and 1 in component flags.
		/// </summary>
		public ushort argument2;

		public static new CompositeGlyph Read(BinaryReaderFont reader, Glyph glyph) {
			CompositeGlyph value = new CompositeGlyph {
				flags = reader.ReadUInt16(),
				glyphIndex = reader.ReadUInt16()
			};
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"flags\": 0x{0:X4},\n", flags);
			builder.AppendFormat("\t\"glyphIndex\": {0},\n", glyphIndex);
			builder.AppendFormat("\t\"argument1\": {0},\n", argument1);
			builder.AppendFormat("\t\"argument2\": {0},\n", argument2);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
