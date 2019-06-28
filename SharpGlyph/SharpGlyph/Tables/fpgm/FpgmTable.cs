using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Font Program (fpgm).
	/// <para>TrueType Outline</para>
	/// </summary>
	//[TrueTypeOutline]
	public class FpgmTable : Table {
		public const string Tag = "fpgm";

		/// <summary>
		/// Instructions.
		/// <para>
		/// Type uint8[n]:
		/// n is the number of uint8 items that fit in the size of the table.
		/// </para>
		/// </summary>
		//public byte[] data;

		protected long position;
		protected string filePath;
		protected int length;

		public static FpgmTable Read(BinaryReaderFont reader, TableRecord record) {
			FpgmTable value = new FpgmTable();
			value.position = reader.Position;
			value.filePath = reader.FilePath;
			value.length = (int)record.length;
			//value.data = reader.ReadBytes((int)record.length);
			return value;
		}

		public byte[] ReadData() {
			if (File.Exists(filePath) == false) {
				return null;
			}
			using (Stream stream = File.OpenRead(filePath))
			using (BinaryReaderFont reader = new BinaryReaderFont(stream)) {
				reader.Position = position;
				return reader.ReadBytes(length);
			}
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			//builder.AppendFormat("\t\"data.Length\": {0}\n", data.Length);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
