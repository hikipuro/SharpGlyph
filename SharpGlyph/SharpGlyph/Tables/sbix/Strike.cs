using System.IO;
using System.Text;

namespace SharpGlyph {
	public class Strike {
		/// <summary>
		/// The PPEM size for which this strike was designed.
		/// </summary>
		public ushort ppem;

		/// <summary>
		/// The device pixel density (in PPI) for which this strike was designed.
		/// (E.g., 96 PPI, 192 PPI.)
		/// </summary>
		public ushort ppi;

		/// <summary>
		/// Offset from the beginning of the strike data header
		/// to bitmap data for an individual glyph ID.
		/// </summary>
		public uint[] glyphDataOffsets;

		//public GlyphData[] glyphData;

		protected long position;
		protected uint numGlyphs;
		protected string filePath;

		public static Strike Read(BinaryReaderFont reader, uint numGlyphs) {
			Strike value = new Strike();
			value.position = reader.Position;
			value.numGlyphs = numGlyphs;
			value.filePath = reader.FilePath;
			value.ppem = reader.ReadUInt16();
			value.ppi = reader.ReadUInt16();
			value.glyphDataOffsets = reader.ReadUInt32Array((int)numGlyphs + 1);
			/*
			value.glyphData = new GlyphData[numGlyphs];
			uint[] glyphDataOffsets = value.glyphDataOffsets;
			int length = value.glyphDataOffsets.Length - 1;
			for (int i = 0; i < length; i++) {
				uint offset = glyphDataOffsets[i];
				uint glyphLength = glyphDataOffsets[i + 1] - offset;
				reader.Position = position + offset;
				GlyphData glyphData = GlyphData.Read(reader, glyphLength);
				value.glyphData[i] = glyphData;
			}
			*/
			return value;
		}

		public GlyphData GetGlyphData(int index) {
			if (index < 0 || index >= numGlyphs) {
				return null;
			}
			if (File.Exists(filePath) == false) {
				return null;
			}
			GlyphData data = null;
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				uint offset = glyphDataOffsets[index];
				uint glyphLength = glyphDataOffsets[index + 1] - offset;
				reader.Position = position + offset;
				data = GlyphData.Read(reader, glyphLength);
			}
			return data;
			/*
			if (index >= glyphData.Length) {
				return null;
			}
			return glyphData[index];
			//*/
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"ppem\": {0},\n", ppem);
			builder.AppendFormat("\t\"ppi\": {0},\n", ppi);
			builder.AppendFormat("\t\"length\": {0},\n", glyphDataOffsets.Length);
			//for (int i = 0; i < glyphDataOffsets.Length; i++) {
				//builder.AppendFormat("\t\"glyphDataOffsets\": 0x{0:X8},\n", glyphDataOffsets[i]);
			//}
			//for (int i = 0; i < glyphData.Length; i++) {
				//builder.AppendFormat("\t\"glyphData {0}\": {1},\n", i, glyphData[i]);
			//}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
