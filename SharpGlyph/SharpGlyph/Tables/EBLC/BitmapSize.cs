using System;
using System.Text;

namespace SharpGlyph {
	public class BitmapSize {
		/// <summary>
		/// Offset to index subtable from beginning of EBLC/CBLC.
		/// </summary>
		public uint indexSubTableArrayOffset;
		
		/// <summary>
		/// Number of bytes in corresponding index subtables and array.
		/// </summary>
		public uint indexTablesSize;
		
		/// <summary>
		/// There is an index subtable for each range or format change.
		/// </summary>
		public uint numberofIndexSubTables;
		
		/// <summary>
		/// Not used; set to 0.
		/// </summary>
		public uint colorRef;
		
		/// <summary>
		/// Line metrics for text rendered horizontally.
		/// </summary>
		public SbitLineMetrics hori;
		
		/// <summary>
		/// Line metrics for text rendered vertically.
		/// </summary>
		public SbitLineMetrics vert;
		
		/// <summary>
		/// Lowest glyph index for this size.
		/// </summary>
		public ushort startGlyphIndex;
		
		/// <summary>
		/// Highest glyph index for this size.
		/// </summary>
		public ushort endGlyphIndex;
		
		/// <summary>
		/// Horizontal pixels per em.
		/// </summary>
		public byte ppemX;
		
		/// <summary>
		/// Vertical pixels per em.
		/// </summary>
		public byte ppemY;
		
		/// <summary>
		/// In addtition to already defined bitDepth values 1, 2, 4, and 8
		/// supported by existing implementations, the value of 32 is used to
		/// identify color bitmaps with 8 bit per pixel RGBA channels.
		/// </summary>
		public BitDepth bitDepth;
		
		/// <summary>
		/// Vertical or horizontal.
		/// </summary>
		public BitmapFlags flags;

		public int index;

		//public IndexSubTableArray subTableArray;
		public IndexSubTableArray[] subTables;

		public static BitmapSize[] ReadArray(BinaryReaderFont reader, int count) {
			BitmapSize[] array = new BitmapSize[count];
			for (int i = 0; i < count; i++) {
				array[i] = Read(reader);
			}
			return array;
		}

		public static BitmapSize Read(BinaryReaderFont reader) {
			BitmapSize value = new BitmapSize {
				indexSubTableArrayOffset = reader.ReadUInt32(),
				indexTablesSize = reader.ReadUInt32(),
				numberofIndexSubTables = reader.ReadUInt32(),
				colorRef = reader.ReadUInt32(),
				hori = SbitLineMetrics.Read(reader),
				vert = SbitLineMetrics.Read(reader),
				startGlyphIndex = reader.ReadUInt16(),
				endGlyphIndex = reader.ReadUInt16(),
				ppemX = reader.ReadByte(),
				ppemY = reader.ReadByte(),
				bitDepth = (BitDepth)reader.ReadByte(),
				flags = (BitmapFlags)reader.ReadByte()
			};
			return value;
		}

		public void ReadSubTableArray(BinaryReaderFont reader, long start) {
			uint offset = indexSubTableArrayOffset;
			reader.Position = start + offset;
			subTables = new IndexSubTableArray[numberofIndexSubTables];
			for (int i = 0; i < numberofIndexSubTables; i++) {
				subTables[i] = IndexSubTableArray.Read(reader, start + offset);
			}

			/*
			subTableArray = IndexSubTableArray.Read(
				reader, start + offset, (int)numberofIndexSubTables
			);
			*/
		}

		public IndexSubTableArray FindSubTableArray(int glyphId) {
			for (int i = 0; i < numberofIndexSubTables; i++) {
				IndexSubTableArray subTable = subTables[i];
				if (subTable.firstGlyphIndex <= glyphId
				&& subTable.lastGlyphIndex >= glyphId) {
					return subTable;
				}
			}
			return null;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"index\": {0},\n", index);
			builder.AppendFormat("\t\"indexSubTableArrayOffset\": {0},\n", indexSubTableArrayOffset);
			builder.AppendFormat("\t\"indexTablesSize\": {0},\n", indexTablesSize);
			builder.AppendFormat("\t\"numberofIndexSubTables\": {0},\n", numberofIndexSubTables);
			builder.AppendFormat("\t\"colorRef\": {0},\n", colorRef);
			builder.AppendFormat("\t\"hori\": {0},\n", hori.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"vert\": {0},\n", vert.ToString().Replace("\n", "\n\t"));
			builder.AppendFormat("\t\"startGlyphIndex\": {0},\n", startGlyphIndex);
			builder.AppendFormat("\t\"endGlyphIndex\": {0},\n", endGlyphIndex);
			builder.AppendFormat("\t\"ppemX\": {0},\n", ppemX);
			builder.AppendFormat("\t\"ppemY\": {0},\n", ppemY);
			builder.AppendFormat("\t\"bitDepth\": \"{0}\",\n", bitDepth);
			builder.AppendFormat("\t\"flags\": \"{0}\"\n", flags);
			if (subTables != null) {
				builder.Remove(builder.Length - 1, 1);
				builder.AppendLine(",");
				builder.AppendLine("\t\"subTables\": [");
				for (int i = 0; i < subTables.Length; i++) {
					string subTable = subTables[i].ToString();
					builder.AppendFormat("\t\t{0},\n", subTable.Replace("\n", "\n\t\t"));
				}
				if (subTables.Length > 0) {
					builder.Remove(builder.Length - 2, 1);
				}
				builder.AppendLine("\t]");
			}
			/*
			if (subTableArray != null) {
				builder.Remove(builder.Length - 1, 1);
				builder.AppendLine(",");
				builder.AppendFormat("\t\"subTableArray\": {0}\n", subTableArray.ToString().Replace("\n", "\n\t"));
			}
			*/
			builder.Append("}");
			return builder.ToString();
		}
	}
}
