using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Embedded Bitmap Data Table (EBDT).
	/// <para>Bitmap Glyph</para>
	/// </summary>
	//[BitmapGlyph]
	public class EBDTTable : Table {
		public const string Tag = "EBDT";

		/// <summary>
		/// Major version of the EBDT table, = 2.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version of the EBDT table, = 0.
		/// </summary>
		public ushort minorVersion;

		//List<Dictionary<ushort, GlyphBitmapData>> bitmapData;

		protected long position;
		protected string filePath;
		protected EBLCTable EBLC;

		public static EBDTTable Read(BinaryReaderFont reader, EBLCTable EBLC) {
			EBDTTable value = new EBDTTable();
			value.position = reader.Position;
			value.filePath = reader.FilePath;
			value.EBLC = EBLC;
			value.majorVersion = reader.ReadUInt16();
			value.minorVersion = reader.ReadUInt16();
			//value.ReadBitmapData(reader, EBLC, position);
			return value;
		}

		public GlyphBitmapData GetGlyphBitmapData(int index, int glyphId) {
			if (index < 0 || index >= EBLC.bitmapSizes.Length) {
				return null;
			}
			if (File.Exists(filePath) == false) {
				return null;
			}
			BitmapSize size = EBLC.bitmapSizes[index];
			IndexSubTableArray array = size.FindSubTableArray(glyphId);
			if (array == null) {
				return null;
			}
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.Position = position + array.subTable.header.imageDataOffset;
				return array.subTable.ReadBitmapData(reader, glyphId, glyphId - array.firstGlyphIndex);
			}
			/*
			if (index < 0 || index >= bitmapData.Count) {
				return null;
			}
			if (bitmapData[index].ContainsKey((ushort)glyphId) == false) {
				return null;
			}
			return bitmapData[index][(ushort)glyphId];
			//*/
		}

		/*
		protected void ReadBitmapData(BinaryReaderFont reader, EBLCTable EBLC, long position) {
			BitmapSize[] bitmapSizes = EBLC.bitmapSizes;
			if (bitmapSizes == null) {
				return;
			}
			bitmapData = new List<Dictionary<ushort, GlyphBitmapData>>();
			for (int i = 0; i < bitmapSizes.Length; i++) {
				bitmapData.Add(new Dictionary<ushort, GlyphBitmapData>());
				BitmapSize size = bitmapSizes[i];
				int subTableCount = size.subTables.Length;
				for (int n = 0; n < subTableCount; n++) {
					IndexSubTableArray array = size.subTables[n];
					GlyphBitmapData[] data = array.ReadGlyphBitmapData(reader, position);
					if (data == null) {
						continue;
					}
					switch (array.subTable.header.indexFormat) {
						case 1:
						case 2:
						case 3:
							{
								ushort firstGlyphIndex = array.firstGlyphIndex;
								ushort lastGlyphIndex = array.lastGlyphIndex;
								for (ushort m = firstGlyphIndex; m <= lastGlyphIndex; m++) {
									if (bitmapData[i].ContainsKey(m)) {
										continue;
									}
									bitmapData[i].Add(m, data[m - firstGlyphIndex]);
								}
							}
							break;
						case 4:
							{
								IndexSubTable4 subTable4 = array.subTable as IndexSubTable4;
								GlyphIdOffsetPair[] glyphArray = subTable4.glyphArray;
								int length = data.Length;
								for (int m = 0; m < length; m++) {
									ushort glyphId = glyphArray[m].glyphID;
									if (bitmapData[i].ContainsKey(glyphId)) {
										continue;
									}
									bitmapData[i].Add(glyphId, data[m]);
								}
							}
							break;
						case 5:
							{
								IndexSubTable5 subTable5 = array.subTable as IndexSubTable5;
								ushort[] glyphIdArray = subTable5.glyphIdArray;
								int length = data.Length;
								for (int m = 0; m < length; m++) {
									ushort glyphId = glyphIdArray[m];
									if (bitmapData[i].ContainsKey(glyphId)) {
										continue;
									}
									bitmapData[i].Add(glyphId, data[m]);
								}
							}
							break;
					}
				}
			}
		}
		*/

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			//builder.AppendFormat("\t\"bitmapData.Length\": {0},\n", bitmapData.Length);
			//for (int i = 0; i < bitmapData.Length; i++) {
			//}
			builder.Append("}");
			return builder.ToString();
		}
	}
}
