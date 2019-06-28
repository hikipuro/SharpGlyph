using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGlyph {
	public class SimpleGlyph : GlyphDescription {
		/// <summary>
		/// Array of point indices for the last point
		/// of each contour, in increasing numeric order.
		/// </summary>
		public ushort[] endPtsOfContours;

		/// <summary>
		/// Total number of bytes for instructions.
		/// <para>
		/// If instructionLength is zero, no instructions are present for this glyph,
		/// and this field is followed directly by the flags field.
		/// </para>
		/// </summary>
		public ushort instructionLength;
		
		/// <summary>
		/// Array of instruction byte code for the glyph.
		/// </summary>
		public byte[] instructions;

		/// <summary>
		/// Array of flag elements.
		/// </summary>
		public SimpleGlyphFlags[] flags;

		/// <summary>
		/// Contour point x-coordinates.
		/// <para>
		/// Coordinate for the first point is relative to (0,0);
		/// others are relative to previous point.
		/// </para>
		/// </summary>
		public short[] xCoordinates;

		/// <summary>
		/// Contour point y-coordinates.
		/// <para>
		/// Coordinate for the first point is relative to (0,0);
		/// others are relative to previous point.
		/// </para>
		/// </summary>
		public short[] yCoordinates;

		public static new SimpleGlyph Read(BinaryReaderFont reader, Glyph glyph) {
			SimpleGlyph value = new SimpleGlyph {
				endPtsOfContours = reader.ReadUInt16Array(glyph.numberOfContours),
				instructionLength = reader.ReadUInt16()
			};
			if (value.instructionLength > 0) {
				value.instructions = reader.ReadBytes(value.instructionLength);
			}
			int length = value.endPtsOfContours.Length;
			int count = value.endPtsOfContours[length - 1] + 1;
			value.ReadFlags(reader, count);
			value.ReadXCoordinates(reader, count);
			value.ReadYCoordinates(reader, count);
			//Console.WriteLine("Glyph count: " + count);
			//Console.WriteLine("Glyph count2: " + value.flags.Length);
			return value;
		}

		protected void ReadFlags(BinaryReaderFont reader, int count) {
			flags = new SimpleGlyphFlags[count];
			for (int i = 0; i < count; i++) {
				SimpleGlyphFlags flag = (SimpleGlyphFlags)reader.ReadByte();
				flags[i] = flag;
				if (flag.HasFlag(SimpleGlyphFlags.REPEAT_FLAG)) {
					int repeat = reader.ReadByte();
					for (int n = 0; n <= repeat; n++) {
						flags[i + n] = flag;
					}
					i += repeat;
				}
			}
		}

		protected void ReadXCoordinates(BinaryReaderFont reader, int count) {
			SimpleGlyphFlags bit1 = SimpleGlyphFlags.X_SHORT_VECTOR;
			SimpleGlyphFlags bit4 = SimpleGlyphFlags.X_IS_SAME_OR_POSITIVE_X_SHORT_VECTOR;
			xCoordinates = new short[count];
			int prevX = 0;
			for (int i = 0; i < count; i++) {
				SimpleGlyphFlags flag = flags[i];
				int x = 0;
				if ((flag & bit1) > 0) {
					x = reader.ReadByte();
					if ((flag & bit4) == 0) {
						x = -x;
					}
				} else {
					if ((flag & bit4) == 0) {
						x = reader.ReadInt16();
					}
				}
				xCoordinates[i] = (short)(x + prevX);
				prevX = xCoordinates[i];
			}
		}

		protected void ReadYCoordinates(BinaryReaderFont reader, int count) {
			SimpleGlyphFlags bit2 = SimpleGlyphFlags.Y_SHORT_VECTOR;
			SimpleGlyphFlags bit5 = SimpleGlyphFlags.Y_IS_SAME_OR_POSITIVE_Y_SHORT_VECTOR;
			yCoordinates = new short[count];
			int prevY = 0;
			for (int i = 0; i < count; i++) {
				SimpleGlyphFlags flag = flags[i];
				int y = 0;
				if ((flag & bit2) > 0) {
					y = reader.ReadByte();
					if ((flag & bit5) == 0) {
						y = -y;
					}
				} else {
					if ((flag & bit5) == 0) {
						y = reader.ReadInt16();
					}
				}
				yCoordinates[i] = (short)(y + prevY);
				prevY = yCoordinates[i];
			}
		}

		public SimpleGlyph Clone() {
			SimpleGlyph value = new SimpleGlyph();
			value.endPtsOfContours = new ushort[endPtsOfContours.Length];
			for (int i = 0; i < endPtsOfContours.Length; i++) {
				value.endPtsOfContours[i] = endPtsOfContours[i];
			}
			value.instructionLength = instructionLength;
			value.instructions = new byte[instructionLength];
			for (int i = 0; i < instructionLength; i++) {
				value.instructions[i] = instructions[i];
			}
			value.flags = new SimpleGlyphFlags[flags.Length];
			for (int i = 0; i < flags.Length; i++) {
				value.flags[i] = flags[i];
			}
			value.xCoordinates = new short[xCoordinates.Length];
			for (int i = 0; i < xCoordinates.Length; i++) {
				value.xCoordinates[i] = xCoordinates[i];
			}
			value.yCoordinates = new short[yCoordinates.Length];
			for (int i = 0; i < yCoordinates.Length; i++) {
				value.yCoordinates[i] = yCoordinates[i];
			}
			return value;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("{");
			builder.AppendLine("\t\"endPtsOfContours\": [");
			for (int i = 0; i < endPtsOfContours.Length; i++) {
				builder.AppendFormat("\t\t{0},\n", endPtsOfContours[i]);
			}
			if (endPtsOfContours.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t],");
			builder.AppendFormat("\t\"instructionLength\": {0},\n", instructionLength);
			builder.AppendFormat("\t\"flags.Length\": {0},\n", flags.Length);
			builder.AppendLine("\t\"flags\": [");
			for (int i = 0; i < flags.Length; i++) {
				builder.AppendFormat("\t\t\"{0}\",\n", flags[i]);
			}
			if (flags.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t],");
			builder.AppendFormat("\t\"xCoordinates.Length\": {0},\n", xCoordinates.Length);
			builder.AppendLine("\t\"xCoordinates\": [");
			for (int i = 0; i < xCoordinates.Length; i++) {
				builder.AppendFormat("\t\t{0},\n", xCoordinates[i]);
			}
			if (xCoordinates.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t],");
			builder.AppendFormat("\t\"yCoordinates.Length\": {0},\n", yCoordinates.Length);
			builder.AppendLine("\t\"yCoordinates\": [");
			for (int i = 0; i < yCoordinates.Length; i++) {
				builder.AppendFormat("\t\t{0},\n", yCoordinates[i]);
			}
			if (yCoordinates.Length > 0) {
				builder.Remove(builder.Length - 2, 1);
			}
			builder.AppendLine("\t]");
			builder.Append("}");
			return builder.ToString();
		}
	}
}
