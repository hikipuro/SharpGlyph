using System;
using System.Text;

namespace SharpGlyph {
	public class SubHeader {
		/// <summary>
		/// First valid low byte for this SubHeader.
		/// </summary>
		public ushort firstCode;
		
		/// <summary>
		/// Number of valid low bytes for this SubHeader.
		/// </summary>
		public ushort entryCount;
		
		/// <summary>
		/// if the value obtained from the subarray is not 0
		/// (which indicates the missing glyph), you should add
		/// idDelta to it in order to get the glyphIndex.
		/// The value idDelta permits the same subarray to be used
		/// for several different subheaders.
		/// The idDelta arithmetic is modulo 65536.
		/// </summary>
		public short idDelta;
		
		/// <summary>
		/// Number of bytes past the actual location of the
		/// idRangeOffset word where the glyphIndexArray element
		/// corresponding to firstCode appears.
		/// </summary>
		public ushort idRangeOffset;

		public static SubHeader Read(BinaryReaderFont reader) {
			return new SubHeader {
				firstCode = reader.ReadUInt16(),
				entryCount = reader.ReadUInt16(),
				idDelta = reader.ReadInt16(),
				idRangeOffset = reader.ReadUInt16()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"firstCode\": {0},\n", firstCode);
			builder.AppendFormat("\t\"entryCount\": {0},\n", entryCount);
			builder.AppendFormat("\t\"idDelta\": {0},\n", idDelta);
			builder.AppendFormat("\t\"idRangeOffset\": {0}\n", idRangeOffset);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
