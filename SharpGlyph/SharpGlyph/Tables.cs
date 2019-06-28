using System;

namespace SharpGlyph {
	public class Tables {
		public OffsetTable offset;
		public TableRecord[] Records;

		/// <summary>
		/// Axis Variations Table (avar).
		/// </summary>
		public AvarTable avar;

		/// <summary>
		/// Baseline table (BASE).
		/// </summary>
		public BASETable BASE;

		/// <summary>
		/// Color Bitmap Data Table (CBDT).
		/// </summary>
		public CBDTTable CBDT;

		/// <summary>
		/// Color Bitmap Location Table (CBLC).
		/// </summary>
		public CBLCTable CBLC;

		/// <summary>
		/// Compact Font Format table (CFF).
		/// </summary>
		public CFFTable  CFF;

		/// <summary>
		/// Compact Font Format Version 2 (CFF2).
		/// </summary>
		public CFF2Table CFF2;

		/// <summary>
		/// Character to Glyph Index Mapping Table (cmap).
		/// </summary>
		public CmapTable cmap;

		/// <summary>
		/// Color Table (COLR).
		/// </summary>
		public COLRTable COLR;

		/// <summary>
		/// Color Palette Table (CPAL).
		/// </summary>
		public CPALTable CPAL;

		/// <summary>
		/// CVT Variations Table (cvar).
		/// </summary>
		public CvarTable cvar;

		/// <summary>
		/// Control Value Table (cvt).
		/// </summary>
		public CvtTable  cvt;

		/// <summary>
		/// Digital Signature Table (DSIG).
		/// </summary>
		public DSIGTable DSIG;

		/// <summary>
		/// Embedded Bitmap Data Table (EBDT).
		/// </summary>
		public EBDTTable EBDT;

		/// <summary>
		/// Embedded Bitmap Location Table (EBLC).
		/// </summary>
		public EBLCTable EBLC;

		/// <summary>
		/// Embedded Bitmap Scaling Table (EBSC).
		/// </summary>
		public EBSCTable EBSC;

		/// <summary>
		/// Font Program (fpgm).
		/// </summary>
		public FpgmTable fpgm;

		/// <summary>
		/// Font Variations Table (fvar).
		/// </summary>
		public FvarTable fvar;

		/// <summary>
		/// Grid-fitting and Scan-conversion Procedure Table (gasp).
		/// </summary>
		public GaspTable gasp;

		/// <summary>
		/// Glyph Definition Table (GDEF).
		/// </summary>
		public GDEFTable GDEF;

		/// <summary>
		/// Glyph Data (glyf).
		/// </summary>
		public GlyfTable glyf;

		/// <summary>
		/// Glyph Positioning Table (GPOS).
		/// </summary>
		public GPOSTable GPOS;

		/// <summary>
		/// Glyph Substitution Table (GSUB).
		/// </summary>
		public GSUBTable GSUB;

		/// <summary>
		/// Glyph Variations Table (gvar).
		/// </summary>
		public GvarTable gvar;

		/// <summary>
		/// Horizontal Device Metrics (hdmx).
		/// </summary>
		public HdmxTable hdmx;

		/// <summary>
		/// Font Header Table (head).
		/// </summary>
		public HeadTable head;

		/// <summary>
		/// Horizontal Header Table (hhea).
		/// </summary>
		public HheaTable hhea;

		/// <summary>
		/// Horizontal Metrics Table (hmtx).
		/// </summary>
		public HmtxTable hmtx;

		/// <summary>
		/// Horizontal Metrics Variations Table (HVAR).
		/// </summary>
		public HVARTable HVAR;

		/// <summary>
		/// Justification Table (JSTF).
		/// </summary>
		public JSTFTable JSTF;

		/// <summary>
		/// Kerning (kern).
		/// </summary>
		public KernTable kern;

		/// <summary>
		/// Index to Location (loca).
		/// </summary>
		public LocaTable loca;

		/// <summary>
		/// Linear Threshold (LTSH).
		/// </summary>
		public LTSHTable LTSH;

		/// <summary>
		/// The mathematical typesetting table (MATH).
		/// </summary>
		public MATHTable MATH;

		/// <summary>
		/// Maximum Profile (maxp).
		/// </summary>
		public MaxpTable maxp;

		/// <summary>
		/// Merge Table (MERG).
		/// </summary>
		public MERGTable MERG;

		/// <summary>
		/// Metadata Table (meta).
		/// </summary>
		public MetaTable meta;

		/// <summary>
		/// Metrics Variations Table (MVAR).
		/// </summary>
		public MVARTable MVAR;

		/// <summary>
		/// Naming Table (name).
		/// </summary>
		public NameTable name;

		/// <summary>
		/// OS/2 table (OS/2).
		/// </summary>
		public OS2Table  OS2;

		/// <summary>
		/// PCL 5 Table (pclt).
		/// </summary>
		public PcltTable pclt;

		/// <summary>
		/// PostScript Table (post).
		/// </summary>
		public PostTable post;

		/// <summary>
		/// Control Value Program (prep).
		/// </summary>
		public PrepTable prep;

		/// <summary>
		/// Standard Bitmap Graphics Table (sbix).
		/// </summary>
		public SbixTable sbix;

		/// <summary>
		/// Style Attributes Table (STAT).
		/// </summary>
		public STATTable STAT;

		/// <summary>
		/// The SVG (Scalable Vector Graphics) table.
		/// </summary>
		public SVGTable  SVG;

		/// <summary>
		/// Vertical Device Metrics (VDMX).
		/// </summary>
		public VDMXTable VDMX;

		/// <summary>
		/// Vertical Header Table (vhea).
		/// </summary>
		public VheaTable vhea;

		/// <summary>
		/// Vertical Metrics Table (vmtx).
		/// </summary>
		public VmtxTable vmtx;

		/// <summary>
		/// Vertical Origin Table (VORG).
		/// </summary>
		public VORGTable VORG;

		/// <summary>
		/// Vertical Metrics Variations Table (VVAR).
		/// </summary>
		public VVARTable VVAR;

		public void ReadTableRecords(BinaryReaderFont reader) {
			offset = OffsetTable.Read(reader);
			Records = TableRecord.ReadArray(reader, offset.numTables);
			Array.Sort(Records, (a, b) => {
				if (a.tableTag == HeadTable.Tag) { return -1; }
				if (b.tableTag == HeadTable.Tag) { return 1; }
				if (a.tableTag == MaxpTable.Tag) { return -1; }
				if (b.tableTag == MaxpTable.Tag) { return 1; }
				if (a.tableTag == HheaTable.Tag) { return -1; }
				if (b.tableTag == HheaTable.Tag) { return 1; }
				if (a.tableTag == LocaTable.Tag) { return -1; }
				if (b.tableTag == LocaTable.Tag) { return 1; }
				if (a.tableTag == CBLCTable.Tag) { return -1; }
				if (b.tableTag == CBLCTable.Tag) { return 1; }
				if (a.tableTag == EBLCTable.Tag) { return -1; }
				if (b.tableTag == EBLCTable.Tag) { return 1; }
				if (a.tableTag == CBLCTable.Tag) { return -1; }
				if (b.tableTag == CBLCTable.Tag) { return 1; }
				if (a.offset < b.offset) { return -1; }
				if (a.offset > b.offset) { return 1; }
				return 0;
			});
		}

		public void ReadTables(BinaryReaderFont reader) {
			if (offset == null) {
				return;
			}
			int length = offset.numTables;
			for (int i = 0; i < length; i++) {
				TableRecord record = Records[i];
				//Console.WriteLine(record);
				//bool checkSum = reader.TableChecksum(record);
				//Console.WriteLine("checkSum {0}: {1}", record.tableTag, checkSum);
				reader.Position = record.offset;
				long memory = 0;
				if (Font.IsDebug) {
					memory = GC.GetTotalMemory(false);
				}

				switch (record.tableTag) {
					case AvarTable.Tag: // OpenType Font Variations
						avar = AvarTable.Read(reader);
						break;
					case BASETable.Tag: // Advanced Typographic Table
						//BASE = BASETable.Read(reader);
						break;
					case CBDTTable.Tag: // Related to Bitmap Glyphs, Related to Color Fonts
						CBDT = CBDTTable.Read(reader, CBLC);
						break;
					case CBLCTable.Tag: // Related to Bitmap Glyphs, Related to Color Fonts
						CBLC = CBLCTable.Read(reader);
						break;
					case CFFTable.Tag:  // Related to CFF Outlines
						CFF = CFFTable.Read(reader);
						break;
					case CFF2Table.Tag: // Related to CFF Outlines
						//CFF2 = CFF2Table.Read(reader);
						break;
					case CmapTable.Tag: // Required Table
						cmap = CmapTable.Read(reader);
						break;
					case COLRTable.Tag: // Related to Color Fonts
						//COLR = COLRTable.Read(reader);
						break;
					case CPALTable.Tag: // Related to Color Fonts
						//CPAL = CPALTable.Read(reader);
						break;
					case CvarTable.Tag: // OpenType Font Variations
						cvar = CvarTable.Read(reader);
						break;
					case CvtTable.Tag:  // Related to TrueType Outlines
						cvt = CvtTable.Read(reader, record);
						break;
					case DSIGTable.Tag: // Other OpenType Table
						//DSIG = DSIGTable.Read(reader);
						break;
					case EBDTTable.Tag: // Related to Bitmap Glyphs
						EBDT = EBDTTable.Read(reader, EBLC);
						break;
					case EBLCTable.Tag: // Related to Bitmap Glyphs
						EBLC = EBLCTable.Read(reader);
						break;
					case EBSCTable.Tag: // Related to Bitmap Glyphs
						//EBSC = EBSCTable.Read(reader);
						break;
					case FpgmTable.Tag: // Related to TrueType Outlines
						fpgm = FpgmTable.Read(reader, record);
						break;
					case FvarTable.Tag: // OpenType Font Variations
						fvar = FvarTable.Read(reader);
						break;
					case GaspTable.Tag: // Related to TrueType Outlines
						//gasp = GaspTable.Read(reader);
						break;
					case GDEFTable.Tag: // Advanced Typographic Table
						//GDEF = GDEFTable.Read(reader);
						break;
					case GlyfTable.Tag: // Related to TrueType Outlines
						glyf = GlyfTable.Read(reader, loca);
						break;
					case GPOSTable.Tag: // Advanced Typographic Table
						//GPOS = GPOSTable.Read(reader);
						break;
					case GSUBTable.Tag: // Advanced Typographic Table
						//GSUB = GSUBTable.Read(reader);
						break;
					case GvarTable.Tag: // OpenType Font Variations
						//gvar = GvarTable.Read(reader);
						break;
					case HdmxTable.Tag: // Other OpenType Table
						hdmx = HdmxTable.Read(reader);
						break;
					case HeadTable.Tag: // Required Table
						head = HeadTable.Read(reader);
						break;
					case HheaTable.Tag: // Required Table
						hhea = HheaTable.Read(reader);
						break;
					case HmtxTable.Tag: // Required Table
						hmtx = HmtxTable.Read(reader, hhea, maxp);
						break;
					case HVARTable.Tag: // OpenType Font Variations
						//HVAR = HVARTable.Read(reader);
						break;
					case JSTFTable.Tag: // Advanced Typographic Table
						//JSTF = JSTFTable.Read(reader);
						break;
					case KernTable.Tag: // Other OpenType Table
						//kern = KernTable.Read(reader);
						break;
					case LocaTable.Tag: // Related to TrueType Outlines
						loca = LocaTable.Read(reader, head, maxp);
						break;
					case LTSHTable.Tag: // Other OpenType Table
						//LTSH = LTSHTable.Read(reader);
						break;
					case MATHTable.Tag: // Advanced Typographic Table
						//MATH = MATHTable.Read(reader);
						break;
					case MaxpTable.Tag: // Required Table
						maxp = MaxpTable.Read(reader);
						break;
					case MERGTable.Tag: // Other OpenType Table
						//MERG = MERGTable.Read(reader);
						break;
					case MetaTable.Tag: // Other OpenType Table
						//meta = MetaTable.Read(reader);
						break;
					case MVARTable.Tag: // OpenType Font Variations
						//MVAR = MVARTable.Read(reader);
						break;
					case NameTable.Tag: // Required Table
						name = NameTable.Read(reader);
						break;
					case OS2Table.Tag:  // Required Table
						//OS2 = OS2Table.Read(reader);
						break;
					case PcltTable.Tag: // Other OpenType Table
						pclt = PcltTable.Read(reader);
						break;
					case PostTable.Tag: // Required Table
						//post = PostTable.Read(reader, record);
						break;
					case PrepTable.Tag: // Related to TrueType Outlines
						prep = PrepTable.Read(reader, record);
						break;
					case SbixTable.Tag: // Related to Bitmap Glyphs, Related to Color Fonts
						sbix = SbixTable.Read(reader, maxp);
						break;
					case STATTable.Tag: // OpenType Font Variations, Other OpenType Table
						//STAT = STATTable.Read(reader);
						break;
					case SVGTable.Tag:  // Related to SVG Outlines, Related to Color Fonts
						//SVG = SVGTable.Read(reader);
						break;
					case VDMXTable.Tag: // Other OpenType Table
						//VDMX = VDMXTable.Read(reader);
						break;
					case VheaTable.Tag: // Other OpenType Table
						//vhea = VheaTable.Read(reader);
						break;
					case VmtxTable.Tag: // Other OpenType Table
						//vmtx = VmtxTable.Read(reader);
						break;
					case VORGTable.Tag: // Related to CFF Outlines
						//VORG = VORGTable.Read(reader);
						break;
					case VVARTable.Tag: // OpenType Font Variations
						//VVAR = VVARTable.Read(reader);
						break;
				}
				if (Font.IsDebug) {
					memory = GC.GetTotalMemory(false) - memory;
					Console.WriteLine("{0} memory: {1}", record.tableTag, memory);
				}
			}
		}
	}
}
