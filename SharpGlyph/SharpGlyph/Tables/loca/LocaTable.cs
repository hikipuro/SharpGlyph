using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Index to Location (loca).
	/// <para>TrueType Outline</para>
	/// </summary>
	//[TrueTypeOutline]
	public class LocaTable : Table {
		public const string Tag = "loca";

		/// <summary>
		/// The actual local offset is stored.
		/// <para>
		/// The value of n is numGlyphs + 1.
		/// The value for numGlyphs is found in the 'maxp' table.
		/// </para>
		/// </summary>
		//public uint[] offsets;

		long position;
		string filePath;
		short indexToLocFormat;
		public ushort numGlyphs;

		public static LocaTable Read(BinaryReaderFont reader, HeadTable head, MaxpTable maxp) {
			LocaTable value = new LocaTable();
			value.position = reader.Position;
			value.filePath = reader.FilePath;
			value.indexToLocFormat = head.indexToLocFormat;
			value.numGlyphs = maxp.numGlyphs;
			/*
			short indexToLocFormat = head.indexToLocFormat;
			if (indexToLocFormat == 0) {
				int length = maxp.numGlyphs + 1;
				ushort[] offsets16 = reader.ReadUInt16Array(length);
				value.offsets = new uint[length];
				for (int i = 0; i < length; i++) {
					value.offsets[i] = (uint)offsets16[i] * 2;
				}
			} else if (indexToLocFormat == 1) {
				value.offsets = reader.ReadUInt32Array(maxp.numGlyphs + 1);
			}
			*/
			return value;
		}

		public void GetOffest(int glyphId, out uint offset0, out uint offset1) {
			if (glyphId < 0 || glyphId >= numGlyphs + 1) {
				offset0 = 0;
				offset1 = 0;
				return;
			}
			if (File.Exists(filePath) == false) {
				offset0 = 0;
				offset1 = 0;
				return;
			}
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				if (indexToLocFormat == 0) {
					reader.Position = position + glyphId * 2;
					offset0 = (uint)(reader.ReadUInt16() * 2);
					offset1 = (uint)(reader.ReadUInt16() * 2);
					return;
				}
				reader.Position = position + glyphId * 4;
				offset0 = reader.ReadUInt32();
				offset1 = reader.ReadUInt32();
			}
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendLine("\t\"offsets\": [");
			/*
			for (int i = 0; i < offsets.Length; i++) {
				builder.AppendFormat("\t\t0x{0:X8},\n", offsets[i]);
			}
			if (offsets.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			*/
			builder.AppendLine("\t]");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
