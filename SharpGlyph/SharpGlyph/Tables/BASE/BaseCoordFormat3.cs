using System;
using System.Text;

namespace SharpGlyph {
	public class BaseCoordFormat3 {
		/// <summary>
		/// Format identifier — format = 3.
		/// </summary>
		public ushort baseCoordFormat;

		/// <summary>
		/// X or Y value, in design units.
		/// </summary>
		public short coordinate;

		/// <summary>
		/// Offset to Device table (non-variable font)
		/// / Variation Index table (variable font)
		/// for X or Y value, from beginning of
		/// BaseCoord table (may be NULL).
		/// </summary>
		public ushort deviceTable;

		public static BaseCoordFormat3 Read(BinaryReaderFont reader) {
			return new BaseCoordFormat3 {
				baseCoordFormat = reader.ReadUInt16(),
				coordinate = reader.ReadInt16(),
				deviceTable = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"baseCoordFormat\": {0},\n", baseCoordFormat);
			builder.AppendFormat("\t\"coordinate\": {0},\n", coordinate);
			builder.AppendFormat("\t\"deviceTable\": {0},\n", deviceTable);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
