using System;
using System.Text;

namespace SharpGlyph {
	public class Lookup {
		/// <summary>
		/// Different enumerations for GSUB and GPOS.
		/// </summary>
		public ushort lookupType;
		
		/// <summary>
		/// Lookup qualifiers.
		/// </summary>
		public ushort lookupFlag;
		
		/// <summary>
		/// Number of subtables for this lookup.
		/// </summary>
		public ushort subTableCount;
		
		/// <summary>
		/// Array of offsets to lookup subtables, from beginning of Lookup table.
		/// </summary>
		public ushort[] subtableOffsets;
		
		/// <summary>
		/// Index (base 0) into GDEF mark glyph sets structure.
		/// This field is only present if bit useMarkFilteringSet of lookup flags is set.
		/// </summary>
		public ushort markFilteringSet;

		public static Lookup Read(BinaryReaderFont reader) {
			return new Lookup {
				lookupType = reader.ReadUInt16(),
				lookupFlag = reader.ReadUInt16(),
				subTableCount = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"lookupType\": {0},\n", lookupType);
			builder.AppendFormat("\t\"lookupFlag\": {0},\n", lookupFlag);
			builder.AppendFormat("\t\"subTableCount\": {0},\n", subTableCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
