using System;
using System.Text;

namespace SharpGlyph {
	public class BaseCoordFormat2 {
		/// <summary>
		/// Format identifier — format = 2.
		/// </summary>
		public ushort baseCoordFormat;

		/// <summary>
		/// X or Y value, in design units.
		/// </summary>
		public short coordinate;

		/// <summary>
		/// Glyph ID of control glyph.
		/// </summary>
		public ushort referenceGlyph;

		/// <summary>
		/// Index of contour point on the reference glyph.
		/// </summary>
		public ushort baseCoordPoint;

		public static BaseCoordFormat2 Read(BinaryReaderFont reader) {
			return new BaseCoordFormat2 {
				baseCoordFormat = reader.ReadUInt16(),
				coordinate = reader.ReadInt16(),
				referenceGlyph = reader.ReadUInt16(),
				baseCoordPoint = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"baseCoordFormat\": {0},\n", baseCoordFormat);
			builder.AppendFormat("\t\"coordinate\": {0},\n", coordinate);
			builder.AppendFormat("\t\"referenceGlyph\": {0},\n", referenceGlyph);
			builder.AppendFormat("\t\"baseCoordPoint\": {0},\n", baseCoordPoint);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
