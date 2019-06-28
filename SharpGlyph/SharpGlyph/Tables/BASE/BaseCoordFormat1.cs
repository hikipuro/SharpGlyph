using System;
using System.Text;

namespace SharpGlyph {
	public class BaseCoordFormat1 {
		/// <summary>
		/// Format identifier — format = 1.
		/// </summary>
		public ushort baseCoordFormat;

		/// <summary>
		/// X or Y value, in design units.
		/// </summary>
		public short coordinate;

		public static BaseCoordFormat1 Read(BinaryReaderFont reader) {
			return new BaseCoordFormat1 {
				baseCoordFormat = reader.ReadUInt16(),
				coordinate = reader.ReadInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"baseCoordFormat\": {0},\n", baseCoordFormat);
			builder.AppendFormat("\t\"coordinate\": {0},\n", coordinate);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
