using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Standard Bitmap Graphics Table (sbix).
	/// <para>Bitmap Glyph, Color Font</para>
	/// </summary>
	//[BitmapGlyph]
	//[ColorFont]
	public class SbixTable : Table {
		public const string Tag = "sbix";
		
		/// <summary>
		/// Table version number — set to 1.
		/// </summary>
		public ushort version;

		/// <summary>
		/// <para>Bit 0: Set to 1.</para>
		/// <para>Bit 1: Draw outlines.</para>
		/// <para>Bits 2 to 15: reserved (set to 0).</para>
		/// </summary>
		public ushort flags;

		/// <summary>
		/// Number of bitmap strikes.
		/// </summary>
		public uint numStrikes;

		/// <summary>
		/// Offsets from the beginning of the 'sbix' table
		/// to data for each individual bitmap strike.
		/// </summary>
		public uint[] strikeOffsets;

		public Strike[] strikes;

		public static SbixTable Read(BinaryReaderFont reader, MaxpTable maxp) {
			long position = reader.Position;
			SbixTable value = new SbixTable {
				version = reader.ReadUInt16(),
				flags = reader.ReadUInt16(),
				numStrikes = reader.ReadUInt32(),
			};
			uint numGlyphs = maxp.numGlyphs;
			uint numStrikes = value.numStrikes;
			value.strikeOffsets = reader.ReadUInt32Array((int)numStrikes);
			value.strikes = new Strike[numStrikes];
			for (int i = 0; i < numStrikes; i++) {
				uint strikeOffset = value.strikeOffsets[i];
				reader.Position = position + strikeOffset;
				value.strikes[i] = Strike.Read(reader, numGlyphs);
			}
			return value;
		}

		public Strike FindStrike(int pixelSize) {
			for (int i = 0; i < strikes.Length; i++) {
				if (strikes[i].ppem > pixelSize) {
					return strikes[i];
				}
			}
			return strikes[strikes.Length - 1];
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"flags\": 0x{0:X4},\n", flags);
			builder.AppendFormat("\t\"numStrikes\": {0},\n", numStrikes);
			for (int i = 0; i < numStrikes; i++) {
				builder.AppendFormat("\t\"strikeOffsets\": 0x{0:X8},\n", strikeOffsets[i]);
				//builder.AppendFormat("\t\"strikes\": {0},\n", strikes[i]);
			}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
