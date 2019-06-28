using System;
using System.Text;

namespace SharpGlyph {
	public class Glyph {
		/// <summary>
		/// If the number of contours is greater than or equal to zero, this is a simple glyph.
		/// If negative, this is a composite glyph — the value -1 should be used for composite glyphs.
		/// </summary>
		public short numberOfContours;

		/// <summary>
		/// Minimum x for coordinate data.
		/// </summary>
		public short xMin;

		/// <summary>
		/// Minimum y for coordinate data.
		/// </summary>
		public short yMin;

		/// <summary>
		/// Maximum x for coordinate data.
		/// </summary>
		public short xMax;

		/// <summary>
		/// Maximum y for coordinate data.
		/// </summary>
		public short yMax;

		public SimpleGlyph simpleGlyph;
		public CompositeGlyph compositeGlyph;

		public static Glyph Read(BinaryReaderFont reader) {
			Glyph value = new Glyph {
				numberOfContours = reader.ReadInt16(),
				xMin = reader.ReadInt16(),
				yMin = reader.ReadInt16(),
				xMax = reader.ReadInt16(),
				yMax = reader.ReadInt16()
			};
			if (value.numberOfContours >= 0) {
				value.simpleGlyph = SimpleGlyph.Read(reader, value);
			} else {
				value.compositeGlyph = CompositeGlyph.Read(reader, value);
			}
			return value;
		}

		public Glyph Clone() {
			Glyph value = new Glyph();
			value.numberOfContours = numberOfContours;
			value.xMin = xMin;
			value.yMin = yMin;
			value.xMax = xMax;
			value.yMax = yMax;
			value.simpleGlyph = simpleGlyph.Clone();
			//value.compositeGlyph = compositeGlyph.Clone();
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"numberOfContours\": {0},\n", numberOfContours);
			builder.AppendFormat("\t\"xMin\": {0},\n", xMin);
			builder.AppendFormat("\t\"yMin\": {0},\n", yMin);
			builder.AppendFormat("\t\"xMax\": {0},\n", xMax);
			builder.AppendFormat("\t\"yMax\": {0},\n", yMax);
			if (simpleGlyph != null) {
				builder.AppendFormat("\t\"simpleGlyph\": {0}\n", simpleGlyph.ToString().Replace("\n", "\n\t"));
			}
			if (compositeGlyph != null) {
				builder.AppendFormat("\t\"compositeGlyph\": {0}\n", compositeGlyph.ToString().Replace("\n", "\n\t"));
			}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
