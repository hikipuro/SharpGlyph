using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Vertical Header Table (vhea).
	/// <para>OpenType Table</para>
	/// </summary>
	//[OpenTypeTable]
	public class VheaTable : Table {
		public const string Tag = "vhea";

		/// <summary>
		/// Version number of the vertical header table (1.0 or 1.1);
		/// <para>0x00010000 for version 1.0</para>
		/// <para>Note the representation of a non-zero fractional part, in Fixed numbers.</para>
		/// </summary>
		public uint version;
		
		public static VheaTable Read(BinaryReaderFont reader) {
			uint version = reader.ReadFixed();
			VheaTable value = null;
			if (version == 0x10000) {
				value = VerticalHeader1.Read(reader);
				value.version = version;
			} else if (version == 0x18000) {
				value = VerticalHeader1_1.Read(reader);
				value.version = version;
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": 0x{0:X8},\n", version);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
