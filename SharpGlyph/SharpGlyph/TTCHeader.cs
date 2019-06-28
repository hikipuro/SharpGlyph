using System.Text;

namespace SharpGlyph {
	public class TTCHeader {
		public const string Tag = "ttcf";

		/// <summary>
		/// [Version 1]
		/// Font Collection ID string: 'ttcf'.
		/// (used for fonts with CFF or CFF2
		/// outlines as well as TrueType outlines).
		/// </summary>
		public string ttcTag;
		
		/// <summary>
		/// [Version 1]
		/// Major version of the TTC Header (1 or 2).
		/// </summary>
		public ushort majorVersion;
		
		/// <summary>
		/// [Version 1]
		/// Minor version of the TTC Header.
		/// </summary>
		public ushort minorVersion;
		
		/// <summary>
		/// [Version 1]
		/// Number of fonts in TTC.
		/// </summary>
		public uint numFonts;
		
		/// <summary>
		/// [Version 1]
		/// Array of offsets to the OffsetTable for
		/// each font from the beginning of the file.
		/// </summary>
		public uint[] offsetTable;
		
		/// <summary>
		/// [Version 2]
		/// Tag indicating that a DSIG table exists,
		/// 0x44534947 ('DSIG') (null if no signature).
		/// </summary>
		public uint dsigTag;
		
		/// <summary>
		/// [Version 2]
		/// The length (in bytes) of the DSIG table
		/// (null if no signature).
		/// </summary>
		public uint dsigLength;
		
		/// <summary>
		/// [Version 2]
		/// The offset (in bytes) of the DSIG table from
		/// the beginning of the TTC file (null if no signature).
		/// </summary>
		public uint dsigOffset;
		
		public static TTCHeader Read(BinaryReaderFont reader) {
			TTCHeader value = new TTCHeader();
			value.ttcTag = reader.ReadTag();
			if (value.ttcTag != Tag) {
				throw new System.Exception(
					string.Format("Parse error: ttcTag is not valid. {0}",  value.ttcTag)
				);
			}
			value.majorVersion = reader.ReadUInt16();
			value.minorVersion = reader.ReadUInt16();
			value.numFonts = reader.ReadUInt32();
			value.offsetTable = reader.ReadUInt32Array((int)value.numFonts);
			if (value.majorVersion >= 2) {
				value.dsigTag = reader.ReadUInt32();
				value.dsigLength = reader.ReadUInt32();
				value.dsigOffset = reader.ReadUInt32();
			}
			return value;
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"ttcTag\": \"{0}\",\n", ttcTag);
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"numFonts\": {0},\n", numFonts);
			builder.AppendLine("\t\"offsetTable\": [");
			for (int i = 0; i < numFonts; i++) {
				builder.AppendFormat("\t\t0x{0:X8},\n", offsetTable[i]);
			}
			if (numFonts > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t],");
			if (majorVersion >= 2) {
				builder.AppendFormat("\t\"dsigTag\": 0x{0:X8},\n", dsigTag);
				builder.AppendFormat("\t\"dsigLength\": 0x{0:X8},\n", dsigLength);
				builder.AppendFormat("\t\"dsigOffset\": 0x{0:X8}\n", dsigOffset);
			} else {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
