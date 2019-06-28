using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Color Bitmap Data Table (CBDT).
	/// <para>Bitmap Glyph, Color Font</para>
	/// </summary>
	//[BitmapGlyph]
	//[ColorFont]
	public class CBDTTable : Table {
		public const string Tag = "CBDT";
		
		/// <summary>
		/// Major version of the CBDT table, = 3.
		/// </summary>
		public ushort majorVersion;
		
		/// <summary>
		/// Minor version of the CBDT table, = 0.
		/// </summary>
		public ushort minorVersion;

		public List<Dictionary<ushort, GlyphBitmapData>> bitmapData;

		public static CBDTTable Read(BinaryReaderFont reader, CBLCTable CBLC) {
			long position = reader.Position;
			CBDTTable value = new CBDTTable();
			value.majorVersion = reader.ReadUInt16();
			value.minorVersion = reader.ReadUInt16();
			//value.ReadBitmapData(reader, CBLC, position);
			return value;
		}

		/*
		protected void ReadBitmapData(BinaryReaderFont reader, CBLCTable CBLC, long position) {
			BitmapSize[] bitmapSizes = CBLC.bitmapSizes;
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
						case 3: {
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
						case 4: {
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
						case 5: {
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
			builder.Append("}");
			return builder.ToString();
		}
	}
}
