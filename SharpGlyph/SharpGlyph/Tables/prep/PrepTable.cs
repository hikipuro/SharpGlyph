using System.IO;
using System.Text;

namespace SharpGlyph {
	/// <summary>
	/// Control Value Program (prep).
	/// <para>TrueType Outline</para>
	/// </summary>
	//[TrueTypeOutline]
	public class PrepTable : Table {
		public const string Tag = "prep";

		/// <summary>
		/// Set of instructions executed whenever point size
		/// or font or transformation change.
		/// <para>
		/// Type uint8[n]:
		/// n is the number of uint8 items that fit in the size of the table.
		/// </para>
		/// </summary>
		//public byte[] data;

		protected long position;
		protected string filePath;
		protected int length;

		public static PrepTable Read(BinaryReaderFont reader, TableRecord record) {
			PrepTable value = new PrepTable();
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
