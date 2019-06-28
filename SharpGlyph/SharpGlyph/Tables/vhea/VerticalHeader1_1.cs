using System;
namespace SharpGlyph {
	public class VerticalHeader1_1 : VheaTable {
		/// <summary>
		/// The vertical typographic ascender for this font.
		/// It is the distance in FUnits from the ideographic
		/// em-box center baseline for the vertical axis
		/// to the right edge of the ideographic em-box.
		/// 
		/// It is usually set to(head.unitsPerEm)/2.
		/// For example, a font with an em of 1000 fUnits
		/// will set this field to 500.
		/// See the Baseline tags section of the OpenType
		/// Layout Tag Registry for a description of the
		/// ideographic em-box.
		/// </summary>
		public short vertTypoAscender;

		/// <summary>
		/// The vertical typographic descender for this font.
		/// It is the distance in FUnits from the ideographic
		/// em-box center baseline for the vertical axis
		/// to the left edge of the ideographic em-box.
		/// 
		/// It is usually set to(head.unitsPerEm)/2.
		/// For example, a font with an em of 1000 fUnits
		/// will set this field to -500.
		/// </summary>
		public short vertTypoDescender;

		/// <summary>
		/// The vertical typographic gap for this font.
		/// An application can determine the recommended
		/// line spacing for single spaced vertical text
		/// for an OpenType font by the following expression:
		/// ideo embox width + vhea.vertTypoLineGap
		/// </summary>
		public short vertTypoLineGap;

		/// <summary>
		/// The maximum advance height measurement
		/// -in FUnits found in the font.
		/// This value must be consistent with the entries
		/// in the vertical metrics table.
		/// </summary>
		public short advanceHeightMax;

		/// <summary>
		/// The minimum top sidebearing measurement found
		/// in the font, in FUnits.
		/// This value must be consistent with the entries
		/// in the vertical metrics table.
		/// </summary>
		public short minTopSideBearing;

		/// <summary>
		/// The minimum bottom sidebearing measurement found
		/// in the font, in FUnits.
		/// This value must be consistent with the entries
		/// in the vertical metrics table.
		/// </summary>
		public short minBottomSideBearing;

		/// <summary>
		/// Defined as yMaxExtent = max(tsb + (yMax - yMin)).
		/// </summary>
		public short yMaxExtent;

		/// <summary>
		/// The value of the caretSlopeRise field divided
		/// by the value of the caretSlopeRun Field determines
		/// the slope of the caret.
		/// A value of 0 for the rise and a value of 1 for
		/// the run specifies a horizontal caret. A value of 1
		/// for the rise and a value of 0 for the run specifies
		/// a vertical caret. Intermediate values are desirable
		/// for fonts whose glyphs are oblique or italic.
		/// For a vertical font, a horizontal caret is best.
		/// </summary>
		public short caretSlopeRise;

		/// <summary>
		/// See the caretSlopeRise field.
		/// Value=1 for nonslanted vertical fonts.
		/// </summary>
		public short caretSlopeRun;

		/// <summary>
		/// The amount by which the highlight on a slanted glyph
		/// needs to be shifted away from the glyph in order
		/// to produce the best appearance.
		/// Set value equal to 0 for nonslanted fonts.
		/// </summary>
		public short caretOffset;

		/// <summary>
		/// Set to 0.
		/// </summary>
		public short reserved0;

		/// <summary>
		/// Set to 0.
		/// </summary>
		public short reserved1;

		/// <summary>
		/// Set to 0.
		/// </summary>
		public short reserved2;

		/// <summary>
		/// Set to 0.
		/// </summary>
		public short reserved3;

		/// <summary>
		/// Set to 0.
		/// </summary>
		public short metricDataFormat;

		/// <summary>
		/// Number of advance heights in the vertical metrics table.
		/// </summary>
		public ushort numOfLongVerMetrics;

		public static new VerticalHeader1_1 Read(BinaryReaderFont reader) {
			return new VerticalHeader1_1 {
				vertTypoAscender = reader.ReadInt16(),
				vertTypoDescender = reader.ReadInt16(),
				vertTypoLineGap = reader.ReadInt16(),
				advanceHeightMax = reader.ReadInt16(),
				minTopSideBearing = reader.ReadInt16(),
				minBottomSideBearing = reader.ReadInt16(),
				yMaxExtent = reader.ReadInt16(),
				caretSlopeRise = reader.ReadInt16(),
				caretSlopeRun = reader.ReadInt16(),
				caretOffset = reader.ReadInt16(),
				reserved0 = reader.ReadInt16(),
				reserved1 = reader.ReadInt16(),
				reserved2 = reader.ReadInt16(),
				reserved3 = reader.ReadInt16(),
				metricDataFormat = reader.ReadInt16(),
				numOfLongVerMetrics = reader.ReadUInt16()
			};
		}
	}
}
