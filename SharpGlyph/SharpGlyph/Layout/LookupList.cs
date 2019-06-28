using System;
using System.Text;

namespace SharpGlyph {
	public class LookupList {
		/// <summary>
		/// Number of lookups in this table.
		/// </summary>
		public ushort lookupCount;

		/// <summary>
		/// Array of offsets to Lookup tables,
		/// from beginning of LookupList — zero based
		/// (first lookup is Lookup index = 0).
		/// </summary>
		public ushort[] lookups;

		public static LookupList Read(BinaryReaderFont reader) {
			return new LookupList {
				lookupCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"lookupCount\": {0},\n", lookupCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
