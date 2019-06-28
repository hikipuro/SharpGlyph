using System;
using System.Text;

namespace SharpGlyph {
	public class ColorRecord {
		/// <summary>
		/// Blue value (B0).
		/// </summary>
		public byte blue;
		
		/// <summary>
		/// Green value (B1).
		/// </summary>
		public byte green;
		
		/// <summary>
		/// Red value (B2).
		/// </summary>
		public byte red;
		
		/// <summary>
		/// Alpha value (B3).
		/// </summary>
		public byte alpha;

		public static ColorRecord[] ReadArray(BinaryReaderFont reader, int count) {
			ColorRecord[] array = new ColorRecord[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static ColorRecord Read(BinaryReaderFont reader) {
			return new ColorRecord {
				blue = reader.ReadByte(),
				green = reader.ReadByte(),
				red = reader.ReadByte(),
				alpha = reader.ReadByte()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"blue\": {0},\n", blue);
			builder.AppendFormat("\t\"green\": {0},\n", green);
			builder.AppendFormat("\t\"red\": {0},\n", red);
			builder.AppendFormat("\t\"alpha\": {0},\n", alpha);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
