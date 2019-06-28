using System;
using System.Text;

namespace SharpGlyph {
	public class InstructionsDecoder {
		public static string Decode(byte[] data) {
			if (data == null) {
				return string.Empty;
			}
			StringBuilder text = new StringBuilder();
			InterpreterStream stream = new InterpreterStream();
			stream.Push(data);
			int count = 0;

			while (stream.HasNext()) {
				int opcode = stream.Next();
				switch (opcode) {
					//-----------------------------------------
					// Pushing data onto the interpreter stack
					// 
					case 0x40: // NPUSHB[ ] (PUSH N Bytes)
						count = stream.Next();
						text.AppendFormat("NPUSHB[] {0}\n", count);
						for (int i = 0; i < count; i++) {
							text.AppendFormat(" {0}\n", stream.Next());
						}
						break;
					case 0x41: // NPUSHW[ ] (PUSH N Words)
						count = stream.Next();
						text.AppendFormat("NPUSHW[] {0}\n", count);
						for (int i = 0; i < count; i++) {
							text.AppendFormat(" {0}\n", stream.NextWord());
						}
						break;
					case 0xB0: // PUSHB[abc] (PUSH Bytes)
						text.AppendFormat("PUSHB[000]\n");
						text.AppendFormat(" {0}\n", stream.Next());
						break;
					case 0xB1: // PUSHB[abc]
						text.AppendFormat("PUSHB[001]\n");
						for (int i = 0; i < 2; i++) {
							text.AppendFormat(" {0}\n", stream.Next());
						}
						break;
					case 0xB2: // PUSHB[abc]
						text.AppendFormat("PUSHB[010]\n");
						for (int i = 0; i < 3; i++) {
							text.AppendFormat(" {0}\n", stream.Next());
						}
						break;
					case 0xB3: // PUSHB[abc]
						text.AppendFormat("PUSHB[011]\n");
						for (int i = 0; i < 4; i++) {
							text.AppendFormat(" {0}\n", stream.Next());
						}
						break;
					case 0xB4: // PUSHB[abc]
						text.AppendFormat("PUSHB[100]\n");
						for (int i = 0; i < 5; i++) {
							text.AppendFormat(" {0}\n", stream.Next());
						}
						break;
					case 0xB5: // PUSHB[abc]
						text.AppendFormat("PUSHB[101]\n");
						for (int i = 0; i < 6; i++) {
							text.AppendFormat(" {0}\n", stream.Next());
						}
						break;
					case 0xB6: // PUSHB[abc]
						text.AppendFormat("PUSHB[110]\n");
						for (int i = 0; i < 7; i++) {
							text.AppendFormat(" {0}\n", stream.Next());
						}
						break;
					case 0xB7: // PUSHB[abc]
						text.AppendFormat("PUSHB[111]\n");
						for (int i = 0; i < 8; i++) {
							text.AppendFormat(" {0}\n", stream.Next());
						}
						break;
					case 0xB8: // PUSHW[abc] (PUSH Words)
						text.AppendFormat("PUSHW[000]\n");
						text.AppendFormat(" {0}\n", stream.NextWord());
						break;
					case 0xB9: // PUSHW[abc]
						text.AppendFormat("PUSHW[001]\n");
						for (int i = 0; i < 2; i++) {
							text.AppendFormat(" {0}\n", stream.NextWord());
						}
						break;
					case 0xBA: // PUSHW[abc]
						text.AppendFormat("PUSHW[010]\n");
						for (int i = 0; i < 3; i++) {
							text.AppendFormat(" {0}\n", stream.NextWord());
						}
						break;
					case 0xBB: // PUSHW[abc]
						text.AppendFormat("PUSHW[011]\n");
						for (int i = 0; i < 4; i++) {
							text.AppendFormat(" {0}\n", stream.NextWord());
						}
						break;
					case 0xBC: // PUSHW[abc]
						text.AppendFormat("PUSHW[100]\n");
						for (int i = 0; i < 5; i++) {
							text.AppendFormat(" {0}\n", stream.NextWord());
						}
						break;
					case 0xBD: // PUSHW[abc]
						text.AppendFormat("PUSHW[101]\n");
						for (int i = 0; i < 6; i++) {
							text.AppendFormat(" {0}\n", stream.NextWord());
						}
						break;
					case 0xBE: // PUSHW[abc]
						text.AppendFormat("PUSHW[110]\n");
						for (int i = 0; i < 7; i++) {
							text.AppendFormat(" {0}\n", stream.NextWord());
						}
						break;
					case 0xBF: // PUSHW[abc]
						text.AppendFormat("PUSHW[111]\n");
						for (int i = 0; i < 8; i++) {
							text.AppendFormat(" {0}\n", stream.NextWord());
						}
						break;

					//-----------------------------------------
					// Managing the Storage Area
					//
					case 0x43: // RS[ ] (Read Store)
						text.AppendLine("RS[]");
						break;
					case 0x42: // WS[ ] (Write Store)
						text.AppendLine("WS[]");
						break;

					//-----------------------------------------
					// Managing the Control Value Table
					//
					case 0x44: // WCVTP[ ] (Write Control Value Table in Pixel units)
						text.AppendLine("WCVTP[]");
						break;
					case 0x70: // WCVTF[ ] (Write Control Value Table in FUnits)
						text.AppendLine("WCVTF[]");
						break;
					case 0x45: // RCVT[ ] (Read Control Value Table)
						text.AppendLine("RCVT[]");
						break;

					//-----------------------------------------
					// Managing the Graphics State
					//
					case 0x00: // SVTCA[a] (Set freedom and projection Vectors To Coordinate Axis)
						text.AppendLine("SVTCA[0]");
						break;
					case 0x01: // SVTCA[a]
						text.AppendLine("SVTCA[1]");
						break;
					case 0x02: // SPVTCA[a] (Set Projection_Vector To Coordinate Axis)
						text.AppendLine("SPVTCA[0]");
						break;
					case 0x03: // SPVTCA[a]
						text.AppendLine("SPVTCA[1]");
						break;
					case 0x04: // SFVTCA[a] (Set Freedom_Vector to Coordinate Axis)
						text.AppendLine("SFVTCA[0]");
						break;
					case 0x05: // SFVTCA[a]
						text.AppendLine("SFVTCA[1]");
						break;
					case 0x06: // SPVTL[a] (Set Projection_Vector To Line)
						text.AppendLine("SPVTL[0]");
						break;
					case 0x07: // SPVTL[a]
						text.AppendLine("SPVTL[1]");
						break;
					case 0x08: // SFVTL[a] (Set Freedom_Vector To Line)
						text.AppendLine("SFVTL[0]");
						break;
					case 0x09: // SFVTL[a]
						text.AppendLine("SFVTL[1]");
						break;
					case 0x0E: // SFVTPV[ ] (Set Freedom_Vector To Projection Vector)
						text.AppendLine("SFVTPV[]");
						break;
					case 0x86: // SDPVTL[a] (Set Dual Projection_Vector To Line)
						text.AppendLine("SDPVTL[0]");
						break;
					case 0x87: // SDPVTL[a]
						text.AppendLine("SDPVTL[1]");
						break;
					case 0x0A: // SPVFS[ ] (Set Projection_Vector From Stack)
						text.AppendLine("SPVFS[]");
						break;
					case 0x0B: // SFVFS[ ] (Set Freedom_Vector From Stack)
						text.AppendLine("SFVFS[]");
						break;
					case 0x0C: // GPV[ ] (Get Projection_Vector)
						text.AppendLine("GPV[]");
						break;
					case 0x0D: // GFV[ ] (Get Freedom_Vector)
						text.AppendLine("GFV[]");
						break;
					case 0x10: // SRP0[ ] (Set Reference Point 0)
						text.AppendLine("SRP0[]");
						break;
					case 0x11: // SRP1[ ] (Set Reference Point 1)
						text.AppendLine("SRP1[]");
						break;
					case 0x12: // SRP2[ ] (Set Reference Point 2)
						text.AppendLine("SRP2[]");
						break;
					case 0x13: // SZP0[ ] (Set Zone Pointer 0)
						text.AppendLine("SZP0[]");
						break;
					case 0x14: // SZP1[ ] (Set Zone Pointer 1)
						text.AppendLine("SZP1[]");
						break;
					case 0x15: // SZP2[ ] (Set Zone Pointer 2)
						text.AppendLine("SZP2[]");
						break;
					case 0x16: // SZPS[ ] (Set Zone PointerS)
						text.AppendLine("SZPS[]");
						break;
					case 0x19: // RTHG[ ] (Round To Half Grid)
						text.AppendLine("RTHG[]");
						break;
					case 0x18: // RTG[ ] (Round To Grid)
						text.AppendLine("RTG[]");
						break;
					case 0x3D: // RTDG[ ] (Round To Double Grid)
						text.AppendLine("RTDG[]");
						break;
					case 0x7D: // RDTG[ ] (Round Down To Grid)
						text.AppendLine("RDTG[]");
						break;
					case 0x7C: // RUTG[ ] (Round Up To Grid)
						text.AppendLine("RUTG[]");
						break;
					case 0x7A: // ROFF[ ] (Round OFF)
						text.AppendLine("ROFF[]");
						break;
					case 0x76: // SROUND[ ] (Super ROUND)
						text.AppendLine("SROUND[]");
						break;
					case 0x77: // S45ROUND[ ] (Super ROUND 45 degrees)
						text.AppendLine("S45ROUND[]");
						break;
					case 0x17: // SLOOP[ ] (Set LOOP variable)
						text.AppendLine("SLOOP[]");
						break;
					case 0x1A: // SMD[ ] (Set Minimum_Distance)
						text.AppendLine("SMD[]");
						break;
					case 0x8E: // INSTCTRL (INSTRuction execution ConTRoL)
						text.AppendLine("INSTCTRL[]");
						break;
					case 0x85: // SCANCTRL[ ] (SCAN conversion ConTRoL)
						text.AppendLine("SCANCTRL[]");
						break;
					case 0x8D: // SCANTYPE[ ] (SCANTYPE)
						text.AppendLine("SCANTYPE[]");
						break;
					case 0x1D: // SCVTCI[ ] (Set Control Value Table Cut In)
						text.AppendLine("SCVTCI[]");
						break;
					case 0x1E: // SSWCI[ ] (Set Single_Width_Cut_In)
						text.AppendLine("SSWCI[]");
						break;
					case 0x1F: // SSW[ ] (Set Single-width)
						text.AppendLine("SSW[]");
						break;
					case 0x4D: // FLIPON[ ] (Set the auto_flip Boolean to ON)
						text.AppendLine("FLIPON[]");
						break;
					case 0x4E: // FLIPOFF[ ] (Set the auto_flip Boolean to OFF)
						text.AppendLine("FLIPOFF[]");
						break;
					case 0x7E: // SANGW[ ] (Set Angle_Weight)
						text.AppendLine("SANGW[]");
						break;
					case 0x5E: // SDB[ ] (Set Delta_Base in the graphics state)
						text.AppendLine("SDB[]");
						break;
					case 0x5F: // SDS[ ] (Set Delta_Shift in the graphics state)
						text.AppendLine("SDS[]");
						break;
						
					//-----------------------------------------
					// Reading and writing data
					//
					case 0x46: // GC[a] (Get Coordinate projected onto the projection_vector)
						text.AppendLine("GC[0]");
						break;
					case 0x47: // GC[a]
						text.AppendLine("GC[1]");
						break;
					case 0x48: // SCFS[ ] (Sets Coordinate From the Stack using projection_vector and freedom_vector)
						text.AppendLine("SCFS[]");
						break;
					case 0x49: // MD[a] (Measure Distance)
						text.AppendLine("MD[0]");
						break;
					case 0x4A: // MD[a]
						text.AppendLine("MD[1]");
						break;
					case 0x4B: // MPPEM[ ] (Measure Pixels Per EM)
						text.AppendLine("MPPEM[]");
						break;
					case 0x4C: // MPS[ ] (Measure Point Size)
						text.AppendLine("MPS[]");
						break;

					//-----------------------------------------
					// Managing outlines
					//
					case 0x80: // FLIPPT[ ] (FLIP PoinT)
						text.AppendLine("FLIPPT[]");
						break;
					case 0x81: // FLIPRGON[ ] (FLIP RanGe ON)
						text.AppendLine("FLIPRGON[]");
						break;
					case 0x82: // FLIPRGOFF[ ] (FLIP RanGe OFF)
						text.AppendLine("FLIPRGOFF[]");
						break;
					case 0x32: // SHP[a] (SHift Point by the last point)
						text.AppendLine("SHP[0]");
						break;
					case 0x33: // SHP[a]
						text.AppendLine("SHP[1]");
						break;
					case 0x34: // SHC[a] (SHift Contour by the last point)
						text.AppendLine("SHC[0]");
						break;
					case 0x35: // SHC[a]
						text.AppendLine("SHC[1]");
						break;
					case 0x36: // SHZ[a] (SHift Zone by the last pt)
						text.AppendLine("SHZ[0]");
						break;
					case 0x37: // SHZ[a]
						text.AppendLine("SHZ[1]");
						break;
					case 0x38: // SHPIX[ ] (SHift point by a PIXel amount)
						text.AppendLine("SHPIX[]");
						break;
					case 0x3A: // MSIRP[a] (Move Stack Indirect Relative Point)
						text.AppendLine("MSIRP[0]");
						break;
					case 0x3B: // MSIRP[a]
						text.AppendLine("MSIRP[1]");
						break;
					case 0x2E: // MDAP[ a ] (Move Direct Absolute Point)
						text.AppendLine("MDAP[0]");
						break;
					case 0x2F: // MDAP[ a ]
						text.AppendLine("MDAP[1]");
						break;
					case 0x3E: // MIAP[a] (Move Indirect Absolute Point)
						text.AppendLine("MIAP[0]");
						break;
					case 0x3F: // MIAP[a]
						text.AppendLine("MIAP[1]");
						break;
					case 0xC0: // MDRP[abcde] (Move Direct Relative Point)
						text.AppendLine("MDRP[00000]");
						break;
					case 0xC1: // MDRP[abcde]
						text.AppendLine("MDRP[00001]");
						break;
					case 0xC2: // MDRP[abcde]
						text.AppendLine("MDRP[00010]");
						break;
					case 0xC3: // MDRP[abcde]
						text.AppendLine("MDRP[00011]");
						break;
					case 0xC4: // MDRP[abcde]
						text.AppendLine("MDRP[00100]");
						break;
					case 0xC5: // MDRP[abcde]
						text.AppendLine("MDRP[00101]");
						break;
					case 0xC6: // MDRP[abcde]
						text.AppendLine("MDRP[00110]");
						break;
					case 0xC7: // MDRP[abcde]
						text.AppendLine("MDRP[00111]");
						break;
					case 0xC8: // MDRP[abcde]
						text.AppendLine("MDRP[01000]");
						break;
					case 0xC9: // MDRP[abcde]
						text.AppendLine("MDRP[01001]");
						break;
					case 0xCA: // MDRP[abcde]
						text.AppendLine("MDRP[01010]");
						break;
					case 0xCB: // MDRP[abcde]
						text.AppendLine("MDRP[01011]");
						break;
					case 0xCC: // MDRP[abcde]
						text.AppendLine("MDRP[01100]");
						break;
					case 0xCD: // MDRP[abcde]
						text.AppendLine("MDRP[01101]");
						break;
					case 0xCE: // MDRP[abcde]
						text.AppendLine("MDRP[01110]");
						break;
					case 0xCF: // MDRP[abcde]
						text.AppendLine("MDRP[01111]");
						break;
					case 0xD0: // MDRP[abcde]
						text.AppendLine("MDRP[10000]");
						break;
					case 0xD1: // MDRP[abcde]
						text.AppendLine("MDRP[10001]");
						break;
					case 0xD2: // MDRP[abcde]
						text.AppendLine("MDRP[10010]");
						break;
					case 0xD3: // MDRP[abcde]
						text.AppendLine("MDRP[10011]");
						break;
					case 0xD4: // MDRP[abcde]
						text.AppendLine("MDRP[10100]");
						break;
					case 0xD5: // MDRP[abcde]
						text.AppendLine("MDRP[10101]");
						break;
					case 0xD6: // MDRP[abcde]
						text.AppendLine("MDRP[10110]");
						break;
					case 0xD7: // MDRP[abcde]
						text.AppendLine("MDRP[10111]");
						break;
					case 0xD8: // MDRP[abcde]
						text.AppendLine("MDRP[11000]");
						break;
					case 0xD9: // MDRP[abcde]
						text.AppendLine("MDRP[11001]");
						break;
					case 0xDA: // MDRP[abcde]
						text.AppendLine("MDRP[11010]");
						break;
					case 0xDB: // MDRP[abcde]
						text.AppendLine("MDRP[11011]");
						break;
					case 0xDC: // MDRP[abcde]
						text.AppendLine("MDRP[11100]");
						break;
					case 0xDD: // MDRP[abcde]
						text.AppendLine("MDRP[11101]");
						break;
					case 0xDE: // MDRP[abcde]
						text.AppendLine("MDRP[11110]");
						break;
					case 0xDF: // MDRP[abcde]
						text.AppendLine("MDRP[11111]");
						break;
					case 0xE0: // MIRP[abcde] (Move Indirect Relative Point)
						text.AppendLine("MIRP[00000]");
						break;
					case 0xE1: // MIRP[abcde]
						text.AppendLine("MIRP[00001]");
						break;
					case 0xE2: // MIRP[abcde]
						text.AppendLine("MIRP[00010]");
						break;
					case 0xE3: // MIRP[abcde]
						text.AppendLine("MIRP[00011]");
						break;
					case 0xE4: // MIRP[abcde]
						text.AppendLine("MIRP[00100]");
						break;
					case 0xE5: // MIRP[abcde]
						text.AppendLine("MIRP[00101]");
						break;
					case 0xE6: // MIRP[abcde]
						text.AppendLine("MIRP[00110]");
						break;
					case 0xE7: // MIRP[abcde]
						text.AppendLine("MIRP[00111]");
						break;
					case 0xE8: // MIRP[abcde]
						text.AppendLine("MIRP[01000]");
						break;
					case 0xE9: // MIRP[abcde]
						text.AppendLine("MIRP[01001]");
						break;
					case 0xEA: // MIRP[abcde]
						text.AppendLine("MIRP[01010]");
						break;
					case 0xEB: // MIRP[abcde]
						text.AppendLine("MIRP[01011]");
						break;
					case 0xEC: // MIRP[abcde]
						text.AppendLine("MIRP[01100]");
						break;
					case 0xED: // MIRP[abcde]
						text.AppendLine("MIRP[01101]");
						break;
					case 0xEE: // MIRP[abcde]
						text.AppendLine("MIRP[01110]");
						break;
					case 0xEF: // MIRP[abcde]
						text.AppendLine("MIRP[01111]");
						break;
					case 0xF0: // MIRP[abcde]
						text.AppendLine("MIRP[10000]");
						break;
					case 0xF1: // MIRP[abcde]
						text.AppendLine("MIRP[10001]");
						break;
					case 0xF2: // MIRP[abcde]
						text.AppendLine("MIRP[10010]");
						break;
					case 0xF3: // MIRP[abcde]
						text.AppendLine("MIRP[10011]");
						break;
					case 0xF4: // MIRP[abcde]
						text.AppendLine("MIRP[10100]");
						break;
					case 0xF5: // MIRP[abcde]
						text.AppendLine("MIRP[10101]");
						break;
					case 0xF6: // MIRP[abcde]
						text.AppendLine("MIRP[10110]");
						break;
					case 0xF7: // MIRP[abcde]
						text.AppendLine("MIRP[10111]");
						break;
					case 0xF8: // MIRP[abcde]
						text.AppendLine("MIRP[11000]");
						break;
					case 0xF9: // MIRP[abcde]
						text.AppendLine("MIRP[11001]");
						break;
					case 0xFA: // MIRP[abcde]
						text.AppendLine("MIRP[11010]");
						break;
					case 0xFB: // MIRP[abcde]
						text.AppendLine("MIRP[11011]");
						break;
					case 0xFC: // MIRP[abcde]
						text.AppendLine("MIRP[11100]");
						break;
					case 0xFD: // MIRP[abcde]
						text.AppendLine("MIRP[11101]");
						break;
					case 0xFE: // MIRP[abcde]
						text.AppendLine("MIRP[11110]");
						break;
					case 0xFF: // MIRP[abcde]
						text.AppendLine("MIRP[11111]");
						break;
					case 0x3C: // ALIGNRP[ ] (ALIGN Relative Point)
						text.AppendLine("ALIGNRP[]");
						break;
					case 0x0F: // ISECT[ ] (moves point p to the InterSECTion of two lines)
						text.AppendLine("ISECT[]");
						break;
					case 0x27: // ALIGNPTS (ALIGN Points)
						text.AppendLine("ALIGNPTS[]");
						break;
					case 0x39: // IP[ ] (Interpolate Point by the last relative stretch)
						text.AppendLine("IP[]");
						break;
					case 0x29: // UTP[ ] (UnTouch Point)
						text.AppendLine("UTP[]");
						break;
					case 0x30: // IUP[a] (Interpolate Untouched Points through the outline)
						text.AppendLine("IUP[0]");
						break;
					case 0x31: // IUP[a]
						text.AppendLine("IUP[1]");
						break;

					//-----------------------------------------
					// Managing exceptions
					//
					case 0x5D: // DELTAP1[ ] (DELTA exception P1)
						text.AppendLine("DELTAP1[]");
						break;
					case 0x71: // DELTAP2[ ] (DELTA exception P2)
						text.AppendLine("DELTAP2[]");
						break;
					case 0x72: // DELTAP3[ ] (DELTA exception P3)
						text.AppendLine("DELTAP3[]");
						break;
					case 0x73: // DELTAC1[ ] (DELTA exception C1)
						text.AppendLine("DELTAC1[]");
						break;
					case 0x74: // DELTAC2[ ] (DELTA exception C2)
						text.AppendLine("DELTAC2[]");
						break;
					case 0x75: // DELTAC3[ ] (DELTA exception C3)
						text.AppendLine("DELTAC3[]");
						break;

					//-----------------------------------------
					// Managing the stack
					//
					case 0x20: // DUP[ ] (Duplicate top stack element)
						text.AppendLine("DUP[]");
						break;
					case 0x21: // POP[ ] (POP top stack element)
						text.AppendLine("POP[]");
						break;
					case 0x22: // CLEAR[ ] (Clear the entire stack)
						text.AppendLine("CLEAR[]");
						break;
					case 0x23: // SWAP[ ] (SWAP the top two elements on the stack)
						text.AppendLine("SWAP[]");
						break;
					case 0x24: // DEPTH[ ] (Returns the DEPTH of the stack)
						text.AppendLine("DEPTH[]");
						break;
					case 0x25: // CINDEX[ ] (Copy the INDEXed element to the top of the stack)
						text.AppendLine("CINDEX[]");
						break;
					case 0x26: // MINDEX[ ] (Move the INDEXed element to the top of the stack)
						text.AppendLine("MINDEX[]");
						break;
					case 0x8A: // ROLL (ROLL the top three stack elements)
						text.AppendLine("ROLL[]");
						break;

					//-----------------------------------------
					// Managing the flow of control
					//
					case 0x58: // IF[ ] (IF test)
						text.AppendLine("IF[]");
						break;
					case 0x1B: // ELSE (ELSE)
						text.AppendLine("ELSE[]");
						break;
					case 0x59: // EIF[ ] (End IF)
						text.AppendLine("EIF[]");
						break;
					case 0x78: // JROT[ ] (Jump Relative On True)
						text.AppendLine("JROT[]");
						break;
					case 0x1C: // JMPR (JuMP)
						text.AppendLine("JMPR[]");
						break;
					case 0x79: // JROF[ ] (Jump Relative On False)
						text.AppendLine("JROF[]");
						break;

					//-----------------------------------------
					// Logical functions
					//
					case 0x50: // LT[ ] (Less Than)
						text.AppendLine("LT[]");
						break;
					case 0x51: // LTEQ[ ] (Less Than or Equal)
						text.AppendLine("LTEQ[]");
						break;
					case 0x52: // GT[ ] (Greater Than)
						text.AppendLine("GT[]");
						break;
					case 0x53: // GTEQ[ ] (Greater Than or Equal)
						text.AppendLine("GTEQ[]");
						break;
					case 0x54: // EQ[ ] (EQual)
						text.AppendLine("EQ[]");
						break;
					case 0x55: // NEQ[ ] (Not EQual)
						text.AppendLine("NEQ[]");
						break;
					case 0x56: // ODD[ ] (ODD)
						text.AppendLine("ODD[]");
						break;
					case 0x57: // EVEN[ ] (EVEN)
						text.AppendLine("EVEN[]");
						break;
					case 0x5A: // AND[ ] (logical AND)
						text.AppendLine("AND[]");
						break;
					case 0x5B: // OR[ ] (logical OR)
						text.AppendLine("OR[]");
						break;
					case 0x5C: // NOT[ ] (logical NOT)
						text.AppendLine("NOT[]");
						break;

					//-----------------------------------------
					// Arithmetic and math instructions
					//
					case 0x60: // ADD[ ] (ADD)
						text.AppendLine("ADD[]");
						break;
					case 0x61: // SUB[ ] (SUBtract)
						text.AppendLine("SUB[]");
						break;
					case 0x62: // DIV[ ] (DIVide)
						text.AppendLine("DIV[]");
						break;
					case 0x63: // MUL[ ] (MULtiply)
						text.AppendLine("MUL[]");
						break;
					case 0x64: // ABS[ ] (ABSolute value)
						text.AppendLine("ABS[]");
						break;
					case 0x65: // NEG[ ] (NEGate)
						text.AppendLine("NEG[]");
						break;
					case 0x66: // FLOOR[ ] (FLOOR)
						text.AppendLine("FLOOR[]");
						break;
					case 0x67: // CEILING[ ] (CEILING)
						text.AppendLine("CEILING[]");
						break;
					case 0X8B: // MAX[ ] (MAXimum of top two stack elements)
						text.AppendLine("MAX[]");
						break;
					case 0X8C: // MIN[ ] (MINimum of top two stack elements)
						text.AppendLine("MIN[]");
						break;

					//------------------------------------------------
					// Compensating for the engine characteristics
					//
					case 0x68: // ROUND[ab] (ROUND value)
						text.AppendLine("ROUND[00]");
						break;
					case 0x69: // ROUND[ab]
						text.AppendLine("ROUND[01]");
						break;
					case 0x6A: // ROUND[ab]
						text.AppendLine("ROUND[10]");
						break;
					case 0x6B: // ROUND[ab]
						text.AppendLine("ROUND[11]");
						break;
					case 0x6C: // NROUND[ab] (No ROUNDing of value)
						text.AppendLine("NROUND[00]");
						break;
					case 0x6D: // NROUND[ab]
						text.AppendLine("NROUND[01]");
						break;
					case 0x6E: // NROUND[ab]
						text.AppendLine("NROUND[10]");
						break;
					case 0x6F: // NROUND[ab]
						text.AppendLine("NROUND[11]");
						break;

					//------------------------------------------------
					// Defining and using functions and instructions
					//
					case 0x2C: // FDEF[ ] (Function DEFinition)
						text.AppendLine("FDEF[]");
						break;
					case 0x2D: // ENDF[ ] (END Function definition)
						text.AppendLine("ENDF[]");
						break;
					case 0x2B: // CALL[ ] (CALL function)
						text.AppendLine("CALL[]");
						break;
					case 0x2A: // LOOPCALL[ ] (LOOP and CALL function)
						text.AppendLine("LOOPCALL[]");
						break;
					case 0x89: // IDEF[ ] (Instruction DEFinition)
						text.AppendLine("IDEF[]");
						break;

					//------------------------------------------------
					// Debugging
					//
					case 0x4F: // DEBUG[ ] (DEBUG call)
						text.AppendLine("DEBUG[]");
						break;

					//------------------------------------------------
					// Miscellaneous instructions
					//
					case 0x88: // GETINFO[ ] (GET INFOrmation)
						text.AppendLine("GETINFO[]");
						break;
					case 0x91: // GETVARIATION[ ] (GET VARIATION)
						text.AppendLine("GETVARIATION[]");
						break;
					case 0x7F: // AA[ ]
						text.AppendLine("AA[]");
						break;

					default:
						text.AppendFormat("Unknown Opcode: 0x{0:X2}\n", opcode);
						break;
				}
			}
			return text.ToString();
		}
	}
}
