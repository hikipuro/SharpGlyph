using System;
using System.Text;

namespace SharpGlyph {
	public class AxisValueMap {
		/// <summary>
		/// A normalized coordinate value obtained using default normalization.
		/// </summary>
		public float fromCoordinate;

		/// <summary>
		/// The modified, normalized coordinate value.
		/// </summary>
		public float toCoordinate;

		public static AxisValueMap[] ReadArray(BinaryReaderFont reader, int count) {
			AxisValueMap[] array = new AxisValueMap[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static AxisValueMap Read(BinaryReaderFont reader) {
			return new AxisValueMap {
				fromCoordinate = reader.ReadF2DOT14(),
				toCoordinate = reader.ReadF2DOT14()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"fromCoordinate\": {0},\n", fromCoordinate);
			builder.AppendFormat("\t\"toCoordinate\": {0},\n", toCoordinate);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
