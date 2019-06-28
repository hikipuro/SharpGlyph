using System;
using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Format 0: Byte encoding table.
	/// <para>
	/// This is the Apple standard character to glyph index mapping table.
	/// </para>
	/// </summary>
	public class CmapSubtable0 : CmapSubtable {
		/// <summary>
		/// This is the length in bytes of the subtable.
		/// </summary>
		public ushort length;
		
		/// <summary>
		/// For requirements on use of the language field.
		/// </summary>
		public ushort language;
		
		/// <summary>
		/// An array that maps character codes to glyph index values.
		/// </summary>
		public byte[] glyphIdArray;

		public static new CmapSubtable0 Read(BinaryReaderFont reader) {
			CmapSubtable0 value = new CmapSubtable0();
			value.filePath = reader.FilePath;
			value.format = reader.ReadUInt16();
			value.length = reader.ReadUInt16();
			value.language = reader.ReadUInt16();
			value.position = reader.Position;
			//value.glyphIdArray = reader.ReadBytes(256);
			return value;
		}

		public override int GetGlyphId(int charCode) {
			if (charCode >= length - 6) {
				return 0;
			}
			if (File.Exists(filePath) == false) {
				return 0;
			}
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.Position = position + charCode;
				return reader.ReadByte();
			}
		}

		public override CharToGlyphTable CreateCharToGlyphTable() {
			CharToGlyphTable table = new CharToGlyphTable();
			for (int i = 0; i < glyphIdArray.Length; i++) {
				table.Add(i, glyphIdArray[i]);
			}
			return table;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"format\": {0},\n", format);
			builder.AppendFormat("\t\"length\": {0},\n", length);
			builder.AppendFormat("\t\"language\": \"{0}\",\n", language);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
