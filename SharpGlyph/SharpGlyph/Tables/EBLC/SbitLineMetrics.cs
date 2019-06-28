using System;
using System.Text;

namespace SharpGlyph {
	public class SbitLineMetrics {
		public sbyte ascender;
		public sbyte descender;
		public byte widthMax;
		public sbyte caretSlopeNumerator;
		public sbyte caretSlopeDenominator;
		public sbyte caretOffset;
		public sbyte minOriginSB;
		public sbyte minAdvanceSB;
		public sbyte maxBeforeBL;
		public sbyte minAfterBL;
		public sbyte pad1;
		public sbyte pad2;

		public static SbitLineMetrics Read(BinaryReaderFont reader) {
			return new SbitLineMetrics {
				ascender = reader.ReadSByte(),
				descender = reader.ReadSByte(),
				widthMax = reader.ReadByte(),
				caretSlopeNumerator = reader.ReadSByte(),
				caretSlopeDenominator = reader.ReadSByte(),
				caretOffset = reader.ReadSByte(),
				minOriginSB = reader.ReadSByte(),
				minAdvanceSB = reader.ReadSByte(),
				maxBeforeBL = reader.ReadSByte(),
				minAfterBL = reader.ReadSByte(),
				pad1 = reader.ReadSByte(),
				pad2 = reader.ReadSByte()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"ascender\": {0},\n", ascender);
			builder.AppendFormat("\t\"descender\": {0},\n", descender);
			builder.AppendFormat("\t\"widthMax\": {0},\n", widthMax);
			builder.AppendFormat("\t\"caretSlopeNumerator\": {0},\n", caretSlopeNumerator);
			builder.AppendFormat("\t\"caretSlopeDenominator\": {0},\n", caretSlopeDenominator);
			builder.AppendFormat("\t\"caretOffset\": {0},\n", caretOffset);
			builder.AppendFormat("\t\"minOriginSB\": {0},\n", minOriginSB);
			builder.AppendFormat("\t\"minAdvanceSB\": {0},\n", minAdvanceSB);
			builder.AppendFormat("\t\"maxBeforeBL\": {0},\n", maxBeforeBL);
			builder.AppendFormat("\t\"minAfterBL\": {0},\n", minAfterBL);
			builder.AppendFormat("\t\"pad1\": {0},\n", pad1);
			builder.AppendFormat("\t\"pad2\": {0}\n", pad2);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
