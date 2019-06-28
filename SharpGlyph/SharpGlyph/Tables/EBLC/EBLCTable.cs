using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Embedded Bitmap Location Table (EBLC).
	/// <para>Bitmap Glyph</para>
	/// </summary>
	//[BitmapGlyph]
	public class EBLCTable : Table {
		public const string Tag = "EBLC";

		/// <summary>
		/// Major version of the EBLC table, = 2.
		/// </summary>
		public ushort majorVersion;

		/// <summary>
		/// Minor version of the EBLC table, = 0.
		/// </summary>
		public ushort minorVersion;

		/// <summary>
		/// Number of BitmapSize tables.
		/// </summary>
		public uint numSizes;

		public BitmapSize[] bitmapSizes;

		public static EBLCTable Read(BinaryReaderFont reader) {
			long position = reader.Position;
			EBLCTable value = new EBLCTable {
				majorVersion = reader.ReadUInt16(),
				minorVersion = reader.ReadUInt16(),
				numSizes = reader.ReadUInt32()
			};
			value.bitmapSizes = BitmapSize.ReadArray(reader, (int)value.numSizes);
			for (int i = 0; i < value.numSizes; i++) {
				value.bitmapSizes[i].ReadSubTableArray(reader, position);
				value.bitmapSizes[i].index = i;
			}
			return value;
		}

		public bool HasSize(int ppemX, int ppemY) {
			if (bitmapSizes == null) {
				return false;
			}
			int length = bitmapSizes.Length;
			for (int i = 0; i < length; i++) {
				BitmapSize size = bitmapSizes[i];
				if (size.ppemX == ppemX && size.ppemY == ppemY) {
					return true;
				}
			}
			return false;
		}

		public BitmapSize GetBitmapSize(int ppemX, int ppemY) {
			if (bitmapSizes == null) {
				return null;
			}
			int length = bitmapSizes.Length;
			for (int i = 0; i < length; i++) {
				BitmapSize size = bitmapSizes[i];
				if (size.ppemX == ppemX && size.ppemY == ppemY) {
					return size;
				}
			}
			return null;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendFormat("\t\"majorVersion\": {0},\n", majorVersion);
			builder.AppendFormat("\t\"minorVersion\": {0},\n", minorVersion);
			builder.AppendFormat("\t\"numSizes\": {0},\n", numSizes);
			builder.AppendLine("\t\"bitmapSizes\": [");
			for (int i = 0; i < numSizes; i++) {
				string sizes = bitmapSizes[i].ToString();
				builder.AppendFormat("\t\t{0},\n", sizes.Replace("\n", "\n\t\t"));
			}
			if (numSizes > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
