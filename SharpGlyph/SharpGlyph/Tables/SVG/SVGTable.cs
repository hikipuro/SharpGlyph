using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// The SVG (Scalable Vector Graphics) table.
	/// <para>SVG Outline, Color Font</para>
	/// </summary>
	//[SVGOutline]
	//[ColorFont]
	public class SVGTable : Table {
		public const string Tag = "SVG ";
		public ushort version;
		public uint offsetToSVGDocumentList;
		public uint reserved;

		public static SVGTable Read(BinaryReaderFont reader) {
			return new SVGTable {
				version = reader.ReadUInt16(),
				offsetToSVGDocumentList = reader.ReadUInt32(),
				reserved = reader.ReadUInt32()
			};
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"version\": {0},\n", version);
			builder.AppendFormat("\t\"offsetToSVGDocumentList\": {0},\n", offsetToSVGDocumentList);
			builder.AppendFormat("\t\"reserved\": {0},\n", reserved);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
