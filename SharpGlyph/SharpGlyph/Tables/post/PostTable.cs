using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// PostScript Table (post).
	/// <para>Required Table</para>
	/// </summary>
	//[RequiredTable]
	public class PostTable : Table {
		public const string Tag = "post";

		/// <summary>
		/// [Version 1.0]
		/// <para>0x00010000 for version 1.0</para>
		/// <para>0x00020000 for version 2.0</para>
		/// <para>0x00025000 for version 2.5 (deprecated)</para>
		/// <para>0x00030000 for version 3.0</para>
		/// </summary>
		public uint version;

		/// <summary>
		/// [Version 1.0]
		/// Italic angle in counter-clockwise degrees from the vertical.
		/// Zero for upright text, negative for text that leans to the right (forward).
		/// </summary>
		public uint italicAngle;

		/// <summary>
		/// [Version 1.0]
		/// This is the suggested distance of the top of the underline
		/// from the baseline (negative values indicate below baseline).
		/// </summary>
		public short underlinePosition;

		/// <summary>
		/// [Version 1.0]
		/// Suggested values for the underline thickness. 
		/// </summary>
		public short underlineThickness;

		/// <summary>
		/// [Version 1.0]
		/// Set to 0 if the font is proportionally spaced,
		/// non-zero if the font is not proportionally spaced (i.e. monospaced).
		/// </summary>
		public uint isFixedPitch;

		/// <summary>
		/// [Version 1.0]
		/// Minimum memory usage when an OpenType font is downloaded.
		/// </summary>
		public uint minMemType42;

		/// <summary>
		/// [Version 1.0]
		/// Maximum memory usage when an OpenType font is downloaded.
		/// </summary>
		public uint maxMemType42;

		/// <summary>
		/// [Version 1.0]
		/// Minimum memory usage when an OpenType font
		/// is downloaded as a Type 1 font.
		/// </summary>
		public uint minMemType1;

		/// <summary>
		/// [Version 1.0]
		/// Maximum memory usage when an OpenType font
		/// is downloaded as a Type 1 font.
		/// </summary>
		public uint maxMemType1;

		/// <summary>
		/// [Version 2.0]
		/// Number of glyphs (this should be the same as numGlyphs in 'maxp' table).
		/// </summary>
		public ushort numGlyphs;

		/// <summary>
		/// [Version 2.0]
		/// This is not an offset, but is the ordinal number of the glyph in 'post' string tables.
		/// </summary>
		public ushort[] glyphNameIndex;

		/// <summary>
		/// [Version 2.0]
		/// Glyph names with length bytes [variable] (a Pascal string).
		/// </summary>
		public string[] names;

		/// <summary>
		/// [Version 2.5]
		/// Difference between graphic index and standard order of glyph.
		/// </summary>
		public sbyte[] offset;

		public static PostTable Read(BinaryReaderFont reader, TableRecord record) {
			PostTable value = new PostTable {
				version = reader.ReadFixed(),
				italicAngle = reader.ReadFixed(),
				underlinePosition = reader.ReadFWORD(),
				underlineThickness = reader.ReadFWORD(),
				isFixedPitch = reader.ReadUInt32(),
				minMemType42 = reader.ReadUInt32(),
				maxMemType42 = reader.ReadUInt32(),
				minMemType1 = reader.ReadUInt32(),
				maxMemType1 = reader.ReadUInt32(),
			};
			if (value.version == 0x20000) {
				value.numGlyphs = reader.ReadUInt16();
				value.glyphNameIndex = reader.ReadUInt16Array(value.numGlyphs);
				int remain = (int)record.length - 34;
				remain -= value.numGlyphs * 2;
				List<string> names = new List<string>();
				while (remain > 0) {
					byte nameLength = reader.ReadByte();
					if (nameLength == 0) {
						break;
					}
					string name = reader.ReadString(nameLength);
					remain -= nameLength + 1;
					names.Add(name);
				}
				value.names = names.ToArray();
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": 0x{0:X8},\n", version);
			builder.AppendFormat("\t\"italicAngle\": 0x{0:X8},\n", italicAngle);
			builder.AppendFormat("\t\"underlinePosition\": {0},\n", underlinePosition);
			builder.AppendFormat("\t\"underlineThickness\": {0},\n", underlineThickness);
			builder.AppendFormat("\t\"isFixedPitch\": {0},\n", isFixedPitch);
			builder.AppendFormat("\t\"minMemType42\": {0},\n", minMemType42);
			builder.AppendFormat("\t\"maxMemType42\": {0},\n", maxMemType42);
			builder.AppendFormat("\t\"minMemType1\": {0},\n", minMemType1);
			builder.AppendFormat("\t\"maxMemType1\": {0},\n", maxMemType1);
			if (version == 0x20000) {
				builder.AppendFormat("\t\"numGlyphs\": {0},\n", numGlyphs);
				builder.AppendLine("\t\"glyphNameIndex\": [");
				for (int i = 0; i < numGlyphs; i++) {
					builder.AppendFormat("\t\t{0},\n", glyphNameIndex[i]);
				}
				if (numGlyphs > 0) {
					builder.Remove(builder.Length - 2, 1);
				}
				builder.AppendLine("\t],");
				builder.AppendLine("\t\"names\": [");
				for (int i = 0; i < names.Length; i++) {
					builder.AppendFormat("\t\t\"{0}\",\n", names[i]);
				}
				if (numGlyphs > 0) {
					builder.Remove(builder.Length - 2, 1);
				}
				builder.AppendLine("\t]");
			}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
