using System;
using System.Text;

namespace SharpGlyph {
	public class TupleVariationHeader {
		/// <summary>
		/// The size in bytes of the serialized data for this tuple variation table.
		/// </summary>
		public ushort variationDataSize;

		/// <summary>
		/// A packed field.
		/// The high 4 bits are flags.
		/// The low 12 bits are an index into a shared tuple records array.
		/// </summary>
		public ushort tupleIndex;

		/// <summary>
		/// Peak tuple record for this tuple variation table — optional,
		/// determined by flags in the tupleIndex value.
		/// <para>Note that this must always be included in the 'cvar' table.</para>
		/// </summary>
		public Tuple peakTuple;

		/// <summary>
		/// Intermediate start tuple record for this tuple variation table
		/// — optional, determined by flags in the tupleIndex value.
		/// </summary>
		public Tuple intermediateStartTuple;

		/// <summary>
		/// Intermediate end tuple record for this tuple variation table
		/// — optional, determined by flags in the tupleIndex value.
		/// </summary>
		public Tuple intermediateEndTuple;

		public static TupleVariationHeader Read(BinaryReaderFont reader) {
			return new TupleVariationHeader {
				variationDataSize = reader.ReadUInt16(),
				tupleIndex = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"variationDataSize\": {0},\n", variationDataSize);
			builder.AppendFormat("\t\"tupleIndex\": {0},\n", tupleIndex);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
