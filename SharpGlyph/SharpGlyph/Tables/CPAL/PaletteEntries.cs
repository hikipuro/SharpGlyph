using System;
using System.Text;

namespace SharpGlyph {
	public class PaletteEntries {
		/// <summary>
		/// Color records for all palettes.
		/// </summary>
		public ColorRecord[] colorRecords;

		public static PaletteEntries Read(BinaryReaderFont reader) {
			PaletteEntries value = new PaletteEntries();
			//value.colorRecords = ColorRecord.ReadArray(reader);
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
