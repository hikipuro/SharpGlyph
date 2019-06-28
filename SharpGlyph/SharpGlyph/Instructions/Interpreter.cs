using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SharpGlyph {
	public class Interpreter {
		public event Action<int> Debug;

		public InterpreterFuncs funcs = new InterpreterFuncs();
		public int ppem = 24;
		protected MaxpTable maxp;
		protected CvtTable cvt;
		//InterpreterStack stack;
		//public int[] storage;

		public static string Decode(byte[] data) {
			return InstructionsDecoder.Decode(data);
		}

		public void Init(Font font) {
			maxp = font.Tables.maxp;
			cvt = font.Tables.cvt;
			//stack = new InterpreterStack(maxp.maxStackElements);
			//storage = new int[maxp.maxStorage];
		}

		public Glyph Exec(byte[] data, Glyph glyph) {
			if (data == null || data.Length <= 0) {
				return glyph;
			}
			InterpreterStack stack = new InterpreterStack(maxp.maxStackElements);
			InterpreterStream stream = new InterpreterStream();
			GraphicsState state = new GraphicsState();
			GetInfoResult getInfoResult = new GetInfoResult();
			int[] storage = new int[maxp.maxStorage];
			PointF[] origin = null;
			PointF[][] points = new PointF[2][];

			stream.Push(data);

			if (glyph != null) {
				glyph = glyph.Clone();
				SimpleGlyph simpleGlyph = glyph.simpleGlyph;
				if (simpleGlyph != null) {
					short[] xCoordinates = simpleGlyph.xCoordinates;
					short[] yCoordinates = simpleGlyph.yCoordinates;
					int length = xCoordinates.Length;
					origin = new PointF[length + 4];
					points[0] = new PointF[maxp.maxTwilightPoints];
					points[1] = new PointF[length + 4];
					for (int i = 0; i < length; i++) {
						int x = xCoordinates[i];
						int y = yCoordinates[i];
						origin[i].X = x;
						origin[i].Y = y;
						if (i < maxp.maxTwilightPoints) {
							points[0][i].X = x;
							points[0][i].Y = y;
						}
						points[1][i].X = x;
						points[1][i].Y = y;
					}
				}
			}


			while (true) {
				if (stream.HasNext() == false) {
					stream.Pop();
					if (stream.Depth <= 1) {
						break;
					}
					continue;
				}

				ushort opcode = stream.Next();
				//if (glyph != null) {
				//	Console.WriteLine("{0:X}", opcode);
				//}
				switch (opcode) {
					//-----------------------------------------
					// Pushing data onto the interpreter stack
					// 
					case 0x40: // NPUSHB[ ] (PUSH N Bytes)
						stack.PushBytes(stream, stream.Next());
						break;
					case 0x41: // NPUSHW[ ] (PUSH N Words)
						stack.PushWords(stream, stream.Next());
						break;
					case 0xB0: // PUSHB[abc] (PUSH Bytes)
						stack.Push(stream.Next());
						break;
					case 0xB1: case 0xB2: case 0xB3:
					case 0xB4: case 0xB5: case 0xB6: case 0xB7:
						stack.PushBytes(stream, (opcode & 7) + 1);
						break;
					case 0xB8: // PUSHW[abc] (PUSH Words)
						stack.Push(stream.NextWord());
						break;
					case 0xB9: case 0xBA: case 0xBB:
					case 0xBC: case 0xBD: case 0xBE: case 0xBF:
						stack.PushWords(stream, (opcode & 7) + 1);
						break;

					//-----------------------------------------
					// Managing the Storage Area
					//
					case 0x43: // RS[ ] (Read Store)
						stack.Push(
							storage[stack.Pop()]
						);
						break;
					case 0x42: // WS[ ] (Write Store)
						{
							int d = stack.Pop();
							int i = stack.Pop();
							storage[i] = d;
						}
						break;

					//-----------------------------------------
					// Managing the Control Value Table
					//
					case 0x44: // WCVTP[ ] (Write Control Value Table in Pixel units)
						{
							int d = stack.Pop();
							int i = stack.Pop();
							cvt.data[i] = d;
						}
						break;
					case 0x70: // WCVTF[ ] (Write Control Value Table in FUnits)
						{
							int d = stack.Pop();
							int i = stack.Pop();
							cvt.data[i] = d * ppem;
						}
						break;
					case 0x45: // RCVT[ ] (Read Control Value Table)
						{
							int n = stack.Pop();
							stack.Push(
								cvt.data[n]
							);
						}
						break;

					//-----------------------------------------
					// Managing the Graphics State
					//
					case 0x00: // SVTCA[a] (Set freedom and projection Vectors To Coordinate Axis)
						state.projection_vector = GraphicsState.YAxis;
						state.freedom_vector = GraphicsState.YAxis;
						break;
					case 0x01: // SVTCA[a]
						state.projection_vector = 0;
						state.freedom_vector = 0;
						break;
					case 0x02: // SPVTCA[a] (Set Projection_Vector To Coordinate Axis)
						state.projection_vector = GraphicsState.YAxis;
						break;
					case 0x03: // SPVTCA[a]
						state.projection_vector = 0;
						break;
					case 0x04: // SFVTCA[a] (Set Freedom_Vector to Coordinate Axis)
						state.freedom_vector = GraphicsState.YAxis;
						break;
					case 0x05: // SFVTCA[a]
						state.freedom_vector = 0;
						break;
					case 0x06: // SPVTL[a] (Set Projection_Vector To Line)
						state.SetProjectionVector(
							points[state.zp2][stack.Pop()],
							points[state.zp1][stack.Pop()]
						);
						break;
					case 0x07: // SPVTL[a]
						state.SetProjectionVectorY(
							points[state.zp2][stack.Pop()],
							points[state.zp1][stack.Pop()]
						);
						break;
					case 0x08: // SFVTL[a] (Set Freedom_Vector To Line)
						{
							int p1 = stack.Pop();
							int p2 = stack.Pop();
							state.SetFreedomVector(
								points[state.zp2][p1],
								points[state.zp1][p2]
							);
						}
						break;
					case 0x09: // SFVTL[a]
						{
							int p1 = stack.Pop();
							int p2 = stack.Pop();
							state.SetFreedomVectorY(
								points[state.zp2][p1],
								points[state.zp1][p2]
							);
						}
						break;
					case 0x0E: // SFVTPV[ ] (Set Freedom_Vector To Projection Vector)
						state.freedom_vector = state.projection_vector;
						break;
					case 0x86: // SDPVTL[a] (Set Dual Projection_Vector To Line)
						{
							int p1 = stack.Pop();
							int p2 = stack.Pop();
							state.SetProjectionVector(
								points[state.zp2][p1],
								points[state.zp1][p2]
							);
							state.SetDualProjectionVectors(
								origin[p1],
								origin[p2]
							);
						}
						break;
					case 0x87: // SDPVTL[a]
						{
							int p1 = stack.Pop();
							int p2 = stack.Pop();
							state.SetProjectionVectorY(
								points[state.zp2][p1],
								points[state.zp1][p2]
							);
							state.SetDualProjectionVectorsY(
								origin[p1],
								origin[p2]
							);
						}
						break;
					case 0x0A: // SPVFS[ ] (Set Projection_Vector From Stack)
						state.projection_vector = stack.PopVector();
						break;
					case 0x0B: // SFVFS[ ] (Set Freedom_Vector From Stack)
						state.freedom_vector = stack.PopVector();
						break;
					case 0x0C: // GPV[ ] (Get Projection_Vector)
						stack.PushVector(state.projection_vector);
						break;
					case 0x0D: // GFV[ ] (Get Freedom_Vector)
						stack.PushVector(state.freedom_vector);
						break;
					case 0x10: // SRP0[ ] (Set Reference Point 0)
						state.rp0 = stack.Pop();
						break;
					case 0x11: // SRP1[ ] (Set Reference Point 1)
						state.rp1 = stack.Pop();
						break;
					case 0x12: // SRP2[ ] (Set Reference Point 2)
						state.rp2 = stack.Pop();
						break;
					case 0x13: // SZP0[ ] (Set Zone Pointer 0)
						state.zp0 = stack.Pop();
						break;
					case 0x14: // SZP1[ ] (Set Zone Pointer 1)
						state.zp1 = stack.Pop();
						break;
					case 0x15: // SZP2[ ] (Set Zone Pointer 2)
						state.zp2 = stack.Pop();
						break;
					case 0x16: // SZPS[ ] (Set Zone PointerS)
						state.zp0 = state.zp1 = state.zp2 = stack.Pop();
						break;
					case 0x19: // RTHG[ ] (Round To Half Grid)
						state.round_state = 0;
						break;
					case 0x18: // RTG[ ] (Round To Grid)
						state.round_state = 1;
						break;
					case 0x3D: // RTDG[ ] (Round To Double Grid)
						state.round_state = 2;
						break;
					case 0x7D: // RDTG[ ] (Round Down To Grid)
						state.round_state = 3;
						break;
					case 0x7C: // RUTG[ ] (Round Up To Grid)
						state.round_state = 4;
						break;
					case 0x7A: // ROFF[ ] (Round OFF)
						state.round_state = 5;
						break;
					case 0x76: // SROUND[ ] (Super ROUND)
						stack.Pop();
						break;
					case 0x77: // S45ROUND[ ] (Super ROUND 45 degrees)
						stack.Pop();
						break;
					case 0x17: // SLOOP[ ] (Set LOOP variable)
						state.loop = stack.Pop();
						break;
					case 0x1A: // SMD[ ] (Set Minimum_Distance)
						state.minimum_distance = (uint)stack.Pop();
						break;
					case 0x8E: // INSTCTRL (INSTRuction execution ConTRoL)
						stack.Pop();
						stack.Pop();
						break;
					case 0x85: // SCANCTRL[ ] (SCAN conversion ConTRoL)
						stack.Pop();
						break;
					case 0x8D: // SCANTYPE[ ] (SCANTYPE)
						stack.Pop();
						break;
					case 0x1D: // SCVTCI[ ] (Set Control Value Table Cut In)
						state.control_value_cut_in = (uint)stack.Pop();
						break;
					case 0x1E: // SSWCI[ ] (Set Single_Width_Cut_In)
						state.singe_width_cut_in = (uint)stack.Pop();
						break;
					case 0x1F: // SSW[ ] (Set Single-width)
						state.single_width_value = stack.Pop();
						break;
					case 0x4D: // FLIPON[ ] (Set the auto_flip Boolean to ON)
						state.auto_flip = true;
						break;
					case 0x4E: // FLIPOFF[ ] (Set the auto_flip Boolean to OFF)
						state.auto_flip = false;
						break;
					case 0x7E: // SANGW[ ] (Set Angle_Weight)
						stack.Pop();
						break;
					case 0x5E: // SDB[ ] (Set Delta_Base in the graphics state)
						state.delta_base = stack.Pop();
						break;
					case 0x5F: // SDS[ ] (Set Delta_Shift in the graphics state)
						state.delta_shift = stack.Pop();
						break;

					//-----------------------------------------
					// Reading and writing data
					//
					case 0x46: // GC[a] (Get Coordinate projected onto the projection_vector)
						stack.Push(
							GC(
								points[state.zp2][stack.Pop()],
								state.projection_vector
							)
						);
						break;
					case 0x47: // GC[a]
						stack.Push(
							GC(
								origin[stack.Pop()],
								state.projection_vector
							)
						);
						break;
					case 0x48: // SCFS[ ] (Sets Coordinate From the Stack using projection_vector and freedom_vector)
						{
							int value = stack.Pop();
							int p = stack.Pop();
							SCFS(
								ref points[state.zp2][p],
								value,
								state.freedom_vector,
								state.projection_vector
							);
						}
						break;
					// MD[a](Measure Distance)
					case 0x49: case 0x4A:
						{
							int p1 = stack.Pop();
							int p2 = stack.Pop();
							stack.Push(
								MD(
									points[state.zp1][p1],
									points[state.zp0][p2],
									(opcode & 1) == 0,
									state
								)
							);
						}
						break;
					case 0x4B: // MPPEM[ ] (Measure Pixels Per EM)
						stack.Push(
							MPPEM(state.projection_vector)
						);
						break;
					case 0x4C: // MPS[ ] (Measure Point Size)
						stack.Push(
							MPS()
						);
						break;

					//-----------------------------------------
					// Managing outlines
					//
					case 0x80: // FLIPPT[ ] (FLIP PoinT)
						{
							for (int i = 0; i < state.loop; i++) {
								FLIPPT(glyph, stack.Pop());
							}
							state.loop = 1;
						}
						break;
					case 0x81: // FLIPRGON[ ] (FLIP RanGe ON)
						FLIPRGON(glyph, stack.Pop(), stack.Pop());
						break;
					case 0x82: // FLIPRGOFF[ ] (FLIP RanGe OFF)
						FLIPRGOFF(glyph, stack.Pop(), stack.Pop());
						break;
					case 0x32: // SHP[a] (SHift Point by the last point)
						{
							for (int i = 0; i < state.loop; i++) {
								SHP(
									ref points[state.zp1][state.rp2],
									ref origin[state.rp2],
									ref points[state.zp2][stack.Pop()],
									state.freedom_vector
								);
							}
							state.loop = 1;
						}
						break;
					case 0x33: // SHP[a]
						{
							for (int i = 0; i < state.loop; i++) {
								SHP(
									ref points[state.zp0][state.rp1],
									ref origin[state.rp1],
									ref points[state.zp2][stack.Pop()],
									state.freedom_vector
								);
							}
							state.loop = 1;
						}
						break;
					// SHC[a] (SHift Contour by the last point)
					case 0x34: case 0x35:
						{
							SHC(
								glyph,
								points,
								origin,
								state,
								stack.Pop(),
								(opcode & 1) > 0
							);
						}
						break;
					case 0x36: // SHZ[a] (SHift Zone by the last pt)
						stack.Pop();
						stack.Pop();
						break;
					case 0x37: // SHZ[a]
						stack.Pop();
						stack.Pop();
						break;
					case 0x38: // SHPIX[ ] (SHift point by a PIXel amount)
						{
							float amount = stack.PopF26Dot6();
							for (int i = 0; i < state.loop; i++) {
								int p = stack.Pop();
								PointF point = points[state.zp2][p];
								point.X = Cos(state.freedom_vector) * amount;
								point.Y = Sin(state.freedom_vector) * amount;
							}
							state.loop = 1;
						}
						break;
					// MSIRP[a] (Move Stack Indirect Relative Point)
					case 0x3A: case 0x3B:
						{
							float d = stack.PopF26Dot6();
							int p = stack.Pop();
							MSIRP(
								ref points[state.zp0][state.rp0],
								ref points[state.zp1][p],
								d, p,
								(opcode & 1) > 0,
								state
							);
						}
						break;
					// MDAP[ a ] (Move Direct Absolute Point)
					case 0x2E: case 0x2F:
						{
							int p = stack.Pop();
							MDAP(
								ref points[state.zp0][p],
								p,
								(opcode & 1) > 0,
								state
							);
						}
						break;
					// MIAP[a] (Move Indirect Absolute Point)
					case 0x3E: case 0x3F:
						{
							int n = stack.Pop();
							int p = stack.Pop();
							/*
							MIAP(
								ref points[state.zp0][p],
								cvt.data[n],
								p,
								(opcode & 1) > 0,
								state
							);
							//*/
						}
						break;
					// MDRP[abcde] (Move Direct Relative Point)
					case 0xC0: case 0xC1: case 0xC2: case 0xC3:
					case 0xC4: case 0xC5: case 0xC6: case 0xC7:
					case 0xC8: case 0xC9: case 0xCA: case 0xCB:
					case 0xCC: case 0xCD: case 0xCE: case 0xCF:
					case 0xD0: case 0xD1: case 0xD2: case 0xD3:
					case 0xD4: case 0xD5: case 0xD6: case 0xD7:
					case 0xD8: case 0xD9: case 0xDA: case 0xDB:
					case 0xDC: case 0xDD: case 0xDE: case 0xDF:
						{
							int p = stack.Pop();
							MDRP(
								ref points[state.zp0][state.rp0],
								ref origin[state.rp0],
								ref points[state.zp1][p],
								state, p,
								(opcode & 1) > 0,
								(opcode & 2) > 0,
								(opcode & 4) > 0,
								(opcode >> 3) & 3
							);
						}
						break;
					// MIRP[abcde] (Move Indirect Relative Point)
					case 0xE0: case 0xE1: case 0xE2: case 0xE3:
					case 0xE4: case 0xE5: case 0xE6: case 0xE7:
					case 0xE8: case 0xE9: case 0xEA: case 0xEB:
					case 0xEC: case 0xED: case 0xEE: case 0xEF:
					case 0xF0: case 0xF1: case 0xF2: case 0xF3:
					case 0xF4: case 0xF5: case 0xF6: case 0xF7:
					case 0xF8: case 0xF9: case 0xFA: case 0xFB:
					case 0xFC: case 0xFD: case 0xFE: case 0xFF:
						{
							int n = stack.Pop();
							int p = stack.Pop();
							/*
							Console.WriteLine("stream.Depth: {0}", stream.Depth);
							Console.WriteLine("stream.Position: {0}", stream.Position);
							Console.WriteLine("state.zp0: {0}", state.zp0);
							Console.WriteLine("state.zp1: {0}", state.zp1);
							Console.WriteLine("state.rp0: {0}", state.rp0);
							//*/
							//*
							MIRP(
								ref points[state.zp0][state.rp0],
								ref points[state.zp1][p],
								state, p, cvt.data[n],
								(opcode & 1) > 0,
								(opcode & 2) > 0,
								(opcode & 4) > 0,
								(opcode >> 3) & 3
							);
							//*/
						}
						break;
					case 0x3C: // ALIGNRP[ ] (ALIGN Relative Point)
						{
							for (int i = 0; i < state.loop; i++) {
								int p = stack.Pop();
								ALIGNRP(
									ref points[state.zp1][p],
									ref points[state.zp0][state.rp0],
									state.freedom_vector,
									state.projection_vector
								);
							}
							state.loop = 1;
						}
						break;
					case 0x0F: // ISECT[ ] (moves point p to the InterSECTion of two lines)
						{
							int b1 = stack.Pop();
							int b0 = stack.Pop();
							int a1 = stack.Pop();
							int a0 = stack.Pop();
							int p = stack.Pop();
							ISECT(
								ref points[state.zp2][p],
								ref points[state.zp1][a0],
								ref points[state.zp1][a1],
								ref points[state.zp0][b0],
								ref points[state.zp0][b1]
							);
						}
						break;
					case 0x27: // ALIGNPTS[ ] (ALIGN Points)
						{
							int p1 = stack.Pop();
							int p2 = stack.Pop();
							ALIGNPTS(
								ref points[state.zp1][p1],
								ref points[state.zp0][p2],
								state.freedom_vector,
								state.projection_vector
							);
						}
						break;
					case 0x39: // IP[ ] (Interpolate Point by the last relative stretch)
						{
							for (int i = 0; i < state.loop; i++) {
								int p = stack.Pop();
								IP(
									points,
									origin,
									state,
									p
								);
							}
							state.loop = 1;
						}
						break;
					case 0x29: // UTP[ ] (UnTouch Point)
						stack.Pop();
						break;
					case 0x30: // IUP[a] (Interpolate Untouched Points through the outline)
						IUPY(glyph, origin, points[state.zp2]);
						break;
					case 0x31: // IUP[a]
						IUPX(glyph, origin, points[state.zp2]);
						break;

					//-----------------------------------------
					// Managing exceptions
					//
					case 0x5D: // DELTAP1[ ] (DELTA exception P1)
						{
							//Console.WriteLine("delta_base: {0}", state.delta_base);
							//Console.WriteLine("delta_shift: {0}", state.delta_shift);
							int[] steps = {
								-8, -7, -6, -5, -4, -3, -2, -1,
								1, 2, 3, 4, 5, 6, 7, 8
							};
							int n = stack.Pop();
							for (int i = 0; i < n; i++) {
								int p = stack.Pop();
								uint arg = (uint)stack.Pop();
								int ppem1 = state.delta_base + (int)(arg >> 4);
								if (ppem < ppem1) {
									int delta = steps[arg & 0xF] * state.delta_shift;
									points[state.zp0][p].X += Cos(state.projection_vector) * delta;
									points[state.zp0][p].Y += Sin(state.projection_vector) * delta;
								}
							}
						}
						break;
					case 0x71: // DELTAP2[ ] (DELTA exception P2)
						{
							int[] steps = {
								-8, -7, -6, -5, -4, -3, -2, -1,
								1, 2, 3, 4, 5, 6, 7, 8
							};
							int n = stack.Pop();
							for (int i = 0; i < n; i++) {
								int p = stack.Pop();
								uint arg = (uint)stack.Pop();
								int ppem1 = state.delta_base + 16 + (int)(arg >> 4);
								if (ppem < ppem1) {
									int delta = steps[arg & 0xF] * state.delta_shift;
									points[state.zp0][p].X += Cos(state.projection_vector) * delta;
									points[state.zp0][p].Y += Sin(state.projection_vector) * delta;
								}
							}
						}
						break;
					case 0x72: // DELTAP3[ ] (DELTA exception P3)
						{
							int[] steps = {
								-8, -7, -6, -5, -4, -3, -2, -1,
								1, 2, 3, 4, 5, 6, 7, 8
							};
							int n = stack.Pop();
							for (int i = 0; i < n; i++) {
								int p = stack.Pop();
								uint arg = (uint)stack.Pop();
								int ppem1 = state.delta_base + 32 + (int)(arg >> 4);
								if (ppem < ppem1) {
									int delta = steps[arg & 0xF] * state.delta_shift;
									points[state.zp0][p].X += Cos(state.projection_vector) * delta;
									points[state.zp0][p].Y += Sin(state.projection_vector) * delta;
								}
							}
						}
						break;
					case 0x73: // DELTAC1[ ] (DELTA exception C1)
						{
							int[] steps = {
								-8, -7, -6, -5, -4, -3, -2, -1,
								1, 2, 3, 4, 5, 6, 7, 8
							};
							int n = stack.Pop();
							for (int i = 0; i < n; i++) {
								int c = stack.Pop();
								uint arg = (uint)stack.Pop();
								int ppem1 = state.delta_base + (int)(arg >> 4);
								if (ppem < ppem1) {
									int delta = steps[arg & 0xF] * state.delta_shift;
									cvt.data[c] = (cvt.data[c] + delta);
								}
							}
						}
						break;
					case 0x74: // DELTAC2[ ] (DELTA exception C2)
						{
							int[] steps = {
								-8, -7, -6, -5, -4, -3, -2, -1,
								1, 2, 3, 4, 5, 6, 7, 8
							};
							int n = stack.Pop();
							for (int i = 0; i < n; i++) {
								int c = stack.Pop();
								uint arg = (uint)stack.Pop();
								int ppem1 = state.delta_base + 16 + (int)(arg >> 4);
								if (ppem < ppem1) {
									int delta = steps[arg & 0xF] * state.delta_shift;
									cvt.data[c] = (cvt.data[c] + delta);
								}
							}
						}
						break;
					case 0x75: // DELTAC3[ ] (DELTA exception C3)
						{
							int[] steps = {
								-8, -7, -6, -5, -4, -3, -2, -1,
								1, 2, 3, 4, 5, 6, 7, 8
							};
							int n = stack.Pop();
							for (int i = 0; i < n; i++) {
								int c = stack.Pop();
								uint arg = (uint)stack.Pop();
								int ppem1 = state.delta_base + 32 + (int)(arg >> 4);
								if (ppem < ppem1) {
									int delta = steps[arg & 0xF] * state.delta_shift;
									cvt.data[c] = (cvt.data[c] + delta);
								}
							}
						}
						break;

					//-----------------------------------------
					// Managing the stack
					//
					case 0x20: // DUP[ ] (Duplicate top stack element)
						stack.Dup();
						break;
					case 0x21: // POP[ ] (POP top stack element)
						stack.Pop();
						break;
					case 0x22: // CLEAR[ ] (Clear the entire stack)
						stack.Clear();
						break;
					case 0x23: // SWAP[ ] (SWAP the top two elements on the stack)
						stack.Swap();
						break;
					case 0x24: // DEPTH[ ] (Returns the DEPTH of the stack)
						stack.Depth();
						break;
					case 0x25: // CINDEX[ ] (Copy the INDEXed element to the top of the stack)
						stack.PushCopy(stack.Peek());
						break;
					case 0x26: // MINDEX[ ] (Move the INDEXed element to the top of the stack)
						stack.MoveToTop(stack.Peek());
						break;
					case 0x8A: // ROLL (ROLL the top three stack elements)
						stack.Roll();
						break;

					//-----------------------------------------
					// Managing the flow of control
					//
					case 0x58: // IF[ ] (IF test)
						stream.IF(stack.Pop());
						break;
					case 0x1B: // ELSE (ELSE)
						stream.ELSE();
						break;
					case 0x59: // EIF[ ] (End IF)
						break;
					case 0x78: // JROT[ ] (Jump Relative On True)
						stream.JROT(
							stack.Pop(),
							stack.Pop()
						);
						break;
					case 0x1C: // JMPR (JuMP)
						stream.JMPR(stack.Pop());
						break;
					case 0x79: // JROF[ ] (Jump Relative On False)
						stream.JROF(
							stack.Pop(),
							stack.Pop()
						);
						break;

					//-----------------------------------------
					// Logical functions
					//
					case 0x50: // LT[ ] (Less Than)
						stack.LT();
						break;
					case 0x51: // LTEQ[ ] (Less Than or Equal)
						stack.LTEQ();
						break;
					case 0x52: // GT[ ] (Greater Than)
						stack.GT();
						break;
					case 0x53: // GTEQ[ ] (Greater Than or Equal)
						stack.GTEQ();
						break;
					case 0x54: // EQ[ ] (EQual)
						stack.Equal();
						break;
					case 0x55: // NEQ[ ] (Not EQual)
						stack.NotEqual();
						break;
					case 0x56: // ODD[ ] (ODD)
						stack.Odd(state.round_state);
						break;
					case 0x57: // EVEN[ ] (EVEN)
						stack.Even(state.round_state);
						break;
					case 0x5A: // AND[ ] (logical AND)
						stack.And();
						break;
					case 0x5B: // OR[ ] (logical OR)
						stack.Or();
						break;
					case 0x5C: // NOT[ ] (logical NOT)
						stack.Not();
						break;

					//-----------------------------------------
					// Arithmetic and math instructions
					//
					case 0x60: // ADD[ ] (ADD)
						stack.Add();
						break;
					case 0x61: // SUB[ ] (SUBtract)
						stack.Sub();
						break;
					case 0x62: // DIV[ ] (DIVide)
						stack.Div();
						break;
					case 0x63: // MUL[ ] (MULtiply)
						stack.Mul();
						break;
					case 0x64: // ABS[ ] (ABSolute value)
						stack.Abs();
						break;
					case 0x65: // NEG[ ] (NEGate)
						stack.Neg();
						break;
					case 0x66: // FLOOR[ ] (FLOOR)
						stack.Floor();
						break;
					case 0x67: // CEILING[ ] (CEILING)
						stack.Ceil();
						break;
					case 0X8B: // MAX[ ] (MAXimum of top two stack elements)
						stack.Max();
						break;
					case 0X8C: // MIN[ ] (MINimum of top two stack elements)
						stack.Min();
						break;

					//------------------------------------------------
					// Compensating for the engine characteristics
					//
					case 0x68: // ROUND[ab] (ROUND value)
						{
							stack.Round(state);
						}
						break;
					case 0x69: // ROUND[ab]
						stack.Round(state);
						break;
					case 0x6A: // ROUND[ab]
						stack.Round(state);
						break;
					case 0x6B: // ROUND[ab]
						stack.Round(state);
						break;
					case 0x6C: // NROUND[ab] (No ROUNDing of value)
						stack.Round(state);
						break;
					case 0x6D: // NROUND[ab]
						stack.Round(state);
						break;
					case 0x6E: // NROUND[ab]
						stack.Round(state);
						break;
					case 0x6F: // NROUND[ab]
						stack.Round(state);
						break;

					//------------------------------------------------
					// Defining and using functions and instructions
					//
					case 0x2C: // FDEF[ ] (Function DEFinition)
						{
							int f = stack.Pop();
							//Console.WriteLine("FDEF: {0}", f);
							funcs.FDEF(f, stream.GetFunc());
						}
						break;
					case 0x2D: // ENDF[ ] (END Function definition)
						break;
					case 0x2B: // CALL[ ] (CALL function)
						{
							int f = stack.Pop();
							stream.Push(
								funcs.CALL(f)
							);
						}
						break;
					case 0x2A: // LOOPCALL[ ] (LOOP and CALL function)
						{
							int f = stack.Pop();
							stream.Push(
								funcs.CALL(f),
								stack.Pop()
							);
						}
						break;
					case 0x89: // IDEF[ ] (Instruction DEFinition)
						{
							int code = stack.Pop();
							funcs.IDEF(code, stream.GetFunc());
						}
						break;

					//------------------------------------------------
					// Debugging
					//
					case 0x4F: // DEBUG[ ] (DEBUG call)
						{
							int number = stack.Pop();
							if (Debug != null) {
								Debug(number);
							}
						}
						break;

					//------------------------------------------------
					// Miscellaneous instructions
					//
					case 0x88: // GETINFO[ ] (GET INFOrmation)
						{
							GetInfoSelector selector = (GetInfoSelector)stack.Pop();
							if (selector.HasFlag(GetInfoSelector.Version)) {
								getInfoResult.SetVersion(40);
							} else {
								getInfoResult.ClearVersion();
							}
							stack.Push((int)getInfoResult);
						}
						break;
					case 0x91: // GETVARIATION[ ] (GET VARIATION)
						stack.Push(0);
						stack.Push(0);
						break;
					case 0x7F: // AA[ ]
						stack.Pop();
						break;
					default: {
						byte[] inst = funcs.ICALL(opcode);
						if (inst != null) {
							stream.Push(inst);
							break;
						}
						throw new InvalidOperationException(
							string.Format("Opcode 0x{0:X2} is not supported.", opcode)
						);
					}
				}
			}

			if (glyph != null) {
				SimpleGlyph simpleGlyph = glyph.simpleGlyph;
				if (simpleGlyph != null) {
					for (int i = 0; i < points.Length; i++) {
						PointF point = points[1][i];
						simpleGlyph.xCoordinates[i] = (short)point.X;
						simpleGlyph.yCoordinates[i] = (short)point.Y;
					}
				}
			}
			return glyph;
		}

		protected float Sin(float value) {
			return (float)Math.Sin(value);
		}

		protected float Cos(float value) {
			return (float)Math.Cos(value);
		}

		protected float Floor(float value) {
			return (float)Math.Floor(value);
		}

		protected float Sqrt(float value) {
			return (float)Math.Sqrt(value);
		}

		protected int GC(PointF p, float projectionVector) {
			float x = p.X * Cos(projectionVector);
			float y = p.Y * Sin(projectionVector);
			float sign = p.X < 0 || p.Y < 0 ? -1 : 1;
			float d = Sqrt(x * x + y * y);
			return (int)(sign * d * 64);
		}

		protected void SCFS(ref PointF p, int value, float freedomVector, float projectionVector) {
			float f = (float)value / 64;
			p.X += f * Cos(freedomVector) * Cos(projectionVector);
			p.Y += f * Sin(freedomVector) * Sin(projectionVector);
		}

		protected int MD(PointF p1, PointF p2, bool a, GraphicsState state) {
			if (a) {
				p1.X = Round(p1.X, state.round_state);
				p1.Y = Round(p1.Y, state.round_state);
				p2.X = Round(p2.X, state.round_state);
				p2.Y = Round(p2.Y, state.round_state);
			}
			float d = Distance(p1, p2, state);
			return (int)(d * 64);
		}

		protected int MPPEM(float projectionVector) {
			float x = ppem * Cos(projectionVector);
			float y = ppem * Sin(projectionVector);
			return (int)Sqrt(x * x + y * y);
		}

		protected int MPS() {
			return (96 * ppem / 72);
		}

		protected void FLIPPT(Glyph glyph, int p) {
			if (glyph == null) {
				return;
			}
			SimpleGlyphFlags[] flags = glyph.simpleGlyph.flags;
			if ((flags[p] & SimpleGlyphFlags.ON_CURVE_POINT) > 0) {
				flags[p] &= (SimpleGlyphFlags)0xFE;
			} else {
				flags[p] |= SimpleGlyphFlags.ON_CURVE_POINT;
			}
			// TODO: touched
		}

		protected void FLIPRGON(Glyph glyph, int high, int low) {
			if (glyph == null) {
				return;
			}
			SimpleGlyphFlags[] flags = glyph.simpleGlyph.flags;
			for (int i = low; i <= high; i++) {
				flags[i] |= SimpleGlyphFlags.ON_CURVE_POINT;
			}
			// TODO: touched
		}

		protected void FLIPRGOFF(Glyph glyph, int high, int low) {
			if (glyph == null) {
				return;
			}
			SimpleGlyphFlags[] flags = glyph.simpleGlyph.flags;
			for (int i = low; i <= high; i++) {
				flags[i] &= (SimpleGlyphFlags)0xFE;
			}
			// TODO: touched
		}

		protected void SHP(ref PointF rp, ref PointF rpo, ref PointF p, float freedomVector) {
			float dx = (rp.X - rpo.X);
			float dy = (rp.Y - rpo.Y);
			dx *= Cos(freedomVector);
			dy *= Sin(freedomVector);
			p.X += dx;
			p.Y += dy;
		}

		protected void SHC(Glyph glyph, PointF[][] points, PointF[] origin, GraphicsState state, int c, bool a) {
			PointF[] ps = null;
			int rp = -1;
			//int zone = 0;
			if (a) {
				ps = points[state.zp0];
				rp = (int)state.rp1;
				//zone = state.zp0;
			} else {
				ps = points[state.zp1];
				rp = (int)state.rp2;
				//zone = state.zp1;
			}

			PointF p2 = ps[rp];
			PointF p1 = origin[rp];
			float dx = (p2.X - p1.X) * Cos(state.projection_vector);
			float dy = (p2.Y - p1.Y) * Sin(state.projection_vector);
			float d = Sqrt(dx * dx + dy + dy);
			if (d <= 0) {
				return;
			}

			ushort[] endPtsOfContours = glyph.simpleGlyph.endPtsOfContours;
			int start = 0;
			int end = endPtsOfContours[c];
			if (c > 0) {
				start = endPtsOfContours[c - 1];
			}

			dx += d * Cos(state.freedom_vector);
			dy += d * Sin(state.freedom_vector);
			for (int i = start; i < end; i++) {
				if (i == rp) {
					continue;
				}
				ps[i].X += dx;
				ps[i].Y += dy;
			}
		}

		protected void MSIRP(ref PointF rp, ref PointF p0, float d, int p, bool a, GraphicsState state) {
			float dx = p0.X - rp.X;
			float dy = p0.Y - rp.Y;
			float di = Sqrt(dx * dx + dy * dy);
			dx *= d / di;
			dy *= d / di;
			p0.X += dx * Cos(state.freedom_vector);
			p0.Y += dy * Sin(state.freedom_vector);

			state.zp1 = state.zp0;
			state.zp2 = p;
			if (a) {
				state.zp0 = p;
			}
		}

		protected void MDAP(ref PointF p0, int p, bool a, GraphicsState state) {
			if (a) {
				p0.X = Round(p0.X, state.round_state);
				p0.Y = Round(p0.Y, state.round_state);
			} else {
				// TODO: fix
			}
			state.rp0 = p;
			state.rp1 = p;
		}

		protected void MIAP(ref PointF p0, int n, int p, bool a, GraphicsState state) {
			float f = (float)n / 64;
			p0.X = f * Cos(state.projection_vector);
			p0.Y = f * Sin(state.projection_vector);
			if (a) {
				// TODO: control_value_cut_in
				p0.X = Round(p0.X, state.round_state);
				p0.Y = Round(p0.Y, state.round_state);
			}
			state.rp0 = p;
			state.rp1 = p;
		}

		protected void MDRP(ref PointF rp0, ref PointF rpo0, ref PointF p, GraphicsState state, int pi, bool a, bool b, bool c, int de) {
			float dx = rp0.X - rpo0.X;
			float dy = rp0.Y - rpo0.Y;
			float d = Sqrt(dx * dx + dy * dy);
			float minimumDistance = (float)state.minimum_distance / 64;
			float cutIn = (float)state.control_value_cut_in / 64;
			if (b) {
				if (d >= minimumDistance) {

				}
			}
			if (c) {
				//n = Round(n, state.round_state);
				d = Round(d, state.round_state);
			}
			//d = (((float)n) / 64) / d;
			dx *= d;
			dy *= d;
			if (d >= cutIn) {
				p.X += dx * Cos(state.freedom_vector);
				p.Y += dy * Sin(state.freedom_vector);
			}
			state.rp1 = state.rp0;
			state.rp2 = pi;
			if (a) {
				state.rp0 = pi;
			}
		}

		protected void MIRP(ref PointF rp0, ref PointF p, GraphicsState state, int pi, int n, bool a, bool b, bool c, int de) {
			float dx = p.X - rp0.X;
			float dy = p.Y - rp0.Y;
			float d = Sqrt(dx * dx + dy * dy);
			float minimumDistance = (float)state.minimum_distance / 64;
			float cutIn = (float)state.control_value_cut_in / 64;
			if (b) {
				if (d >= minimumDistance) {

				}
			}
			if (c) {
				n = (int)Round((uint)n, state.round_state);
				d = Round(d, state.round_state);
			}
			d = (((float)n) / 64) / d;
			dx *= d;
			dy *= d;
			if (d >= cutIn) {
				p.X += dx * Cos(state.freedom_vector);
				p.Y += dy * Sin(state.freedom_vector);
			}
			state.rp1 = state.rp0;
			state.rp2 = pi;
			if (a) {
				state.rp0 = pi;
			}
		}

		protected void ALIGNRP(ref PointF p1, ref PointF p2, float freedomVector, float projectionVector) {
			float dx = (p2.X - p1.X) * Cos(projectionVector);
			float dy = (p2.Y - p1.Y) * Sin(projectionVector);
			dx *= Cos(freedomVector);
			dy *= Sin(freedomVector);
			p1.X += dx;
			p1.Y += dy;
		}

		protected void ISECT(ref PointF p, ref PointF a0, ref PointF a1, ref PointF b0, ref PointF b1) {
			float d = (a1.X - a0.X) * (b1.Y - b0.Y) - (a1.Y - a0.Y) * (b1.X - b0.X);
			if (d.Equals(0f)) {
				// TODO: fix
				return;
			}
			float dx = b0.X - a0.X;
			float dy = b0.Y - a0.Y;
			float r = ((b1.Y - b0.Y) * dx - (b1.X - b0.X) * dy) / d;
			p.X = a0.X + r * (a1.X - a0.X);
			p.Y = a0.Y + r * (a1.Y - a0.Y);
		}

		protected void ALIGNPTS(ref PointF p1, ref PointF p2, float freedomVector, float projectionVector) {
			float dx = (p2.X - p1.X) * Cos(freedomVector);
			float dy = (p2.Y - p1.Y) * Sin(freedomVector);
			dx *= Cos(projectionVector);
			dy *= Sin(projectionVector);
			dx *= 0.5f;
			dy *= 0.5f;
			p1.X += dx;
			p1.Y += dy;
			p2.X -= dx;
			p2.Y -= dy;
		}

		protected float Distance(PointF p0, PointF p1, GraphicsState state) {
			float x = (p1.X - p0.X) * Cos(state.projection_vector);
			float y = (p1.Y - p0.Y) * Sin(state.projection_vector);
			float sign = x < 0 || y < 0 ? -1 : 1;
			return sign * Sqrt(x * x + y * y);
		}

		protected float Distance2(PointF p0, PointF p1, GraphicsState state) {
			float x = (p1.X - p0.X) * Cos(state.projection_vector);
			float y = (p1.Y - p0.Y) * Sin(state.projection_vector);
			return Sqrt(x * x + y * y);
		}

		protected PointF Intersect(PointF a, PointF b, PointF c, PointF d) {
			float bn = (b.X - a.X) * (d.Y - c.Y) - (b.Y - a.Y) * (d.X - c.X);
			if (Math.Abs(bn) < float.Epsilon) {
				return PointF.Empty;
			}
			PointF ac = new PointF(c.X - a.X, c.Y - a.Y);
			float dr = ((d.Y - c.Y) * ac.X - (d.X - c.X) * ac.Y) / bn;
			return new PointF(
				a.X + dr * (b.X - a.X),
				a.Y + dr * (b.Y - a.Y)
			);
		}

		protected void IP(PointF[][] points, PointF[] origin, GraphicsState state, int p) {
			/*
			PointF p1 = points[state.zp2][p];
			float d0 = Distance2(origin[p], origin[state.rp1], state);
			float d1 = Distance2(origin[p], origin[state.rp2], state);

			PointF rp1 = points[state.zp0][state.rp1];
			PointF rp2 = points[state.zp1][state.rp2];

			float drp = Distance2(rp1, rp2, state);

			rp1.X += Cos(state.projection_vector) * drp * d0 / (d0 + d1);
			rp1.Y += Sin(state.projection_vector) * drp * d0 / (d0 + d1);
			rp2.X -= Cos(state.projection_vector) * drp * d1 / (d0 + d1);
			rp2.Y -= Sin(state.projection_vector) * drp * d1 / (d0 + d1);

			PointF p2 = p1;
			p2.X += Cos(state.freedom_vector) * 10;
			p2.Y += Sin(state.freedom_vector) * 10;
			Console.WriteLine("###### p1:{0}, p2:{1}, drp:{2}", p1, p2, drp);
			p1 = Intersect(rp1, rp2, p1, p2);
			//p1.X += drp * d0 / (d0 + d1) * fvx;
			//p1.Y += drp * d0 / (d0 + d1) * fvy;
			if (p1.IsEmpty) {
				return;
			}
			points[state.zp2][p] = p1;

			float d2 = Distance2(p1, points[state.zp0][state.rp1], state);
			float d3 = Distance2(p1, points[state.zp1][state.rp2], state);
			Console.WriteLine("###### p1:{0}", p1);
			Console.WriteLine("##### p:{0}, rp1:{1}, rp1':{2}", origin[p], origin[state.rp1], points[state.zp0][state.rp1]);
			Console.WriteLine("##### p:{0}, rp2:{1}, rp2':{2}", origin[p], origin[state.rp2], points[state.zp1][state.rp2]);
			Console.WriteLine("#### d0:{0}, d1:{1}, d2:{2}, d3:{3}", d0, d1, d2, d3);
			//Console.WriteLine("#### d0 / d2:{0}, d1 / d3:{1}", d0 / d2, d1 / d3);
			Console.WriteLine("#### d0 / d1:{0}, d2 / d3:{1}", d0 / d1, d2 / d3);
			Console.WriteLine();
			//*/
		}

		protected void IUPX(Glyph glyph, PointF[] origin, PointF[] points) {
			if (glyph == null) {
				return;
			}
			ushort[] endPtsOfContours = glyph.simpleGlyph.endPtsOfContours;
			int contours = endPtsOfContours.Length;
			int start = 0;
			for (int contour = 0; contour < contours; contour++) {
				int end = endPtsOfContours[contour];
				for (int i = start; i < end; i++) {
					if (!origin[i].X.Equals(points[i].X)) {
						continue;
					}
					int p = i - 1;
					int n = i + 1;
					if (p < start) { p = end - 1; }
					if (n >= end) { n = 0; }
					float op = origin[p].X, on = origin[n].X;
					float pp = points[p].X, pn = points[n].X;
					if (!op.Equals(pp) && !on.Equals(pn)) {
						float dp = pp - op;
						float np = pn - on;
						points[i].X += (np + dp) / 2;
						continue;
					}
					if (!op.Equals(pp)) {
						points[i].X += pp - op;
						continue;
					}
					if (!on.Equals(pn)) {
						points[i].X += pn - on;
					}
				}
				start = end;
			}
		}

		protected void IUPY(Glyph glyph, PointF[] origin, PointF[] points) {
			if (glyph == null) {
				return;
			}
			ushort[] endPtsOfContours = glyph.simpleGlyph.endPtsOfContours;
			int contours = endPtsOfContours.Length;
			int start = 0;
			for (int contour = 0; contour < contours; contour++) {
				int end = endPtsOfContours[contour];
				for (int i = start; i < end; i++) {
					if (!origin[i].Y.Equals(points[i].Y)) {
						continue;
					}
					int p = i - 1;
					int n = i + 1;
					if (p < start) { p = end - 1; }
					if (n >= end) { n = 0; }
					float op = origin[p].Y, on = origin[n].Y;
					float pp = points[p].Y, pn = points[n].Y;
					if (!op.Equals(pp) && !on.Equals(pn)) {
						float dp = pp - op;
						float np = pn - on;
						points[i].Y += (np + dp) / 2;
						continue;
					}
					if (!op.Equals(pp)) {
						points[i].Y += pp - op;
						continue;
					}
					if (!on.Equals(pn)) {
						points[i].Y += pn - on;
					}
				}
				start = end;
			}
		}

		protected float Round(float value, int roundState) {
			return (float)Round((uint)(value * 64), roundState) / 64;
		}

		protected uint Round(uint f26d6, int roundState) {
			if (roundState == 5) {
				return f26d6;
			}
			switch (roundState) {
				case 0: // Round To Half Grid
					f26d6 &= 0xFFFFFFC0;
					f26d6 |= 0x20;
					break;
				case 1: // Round To Grid
					if ((f26d6 & 0x20) > 0) {
						f26d6 &= 0xFFFFFFC0;
						f26d6 += 0x40;
					} else {
						f26d6 &= 0xFFFFFFC0;
					}
					break;
				case 2: // Round To Double Grid
					f26d6 &= 0xFFFFFFE0;
					break;
				case 3: // Round Down To Grid
					f26d6 &= 0xFFFFFFC0;
					break;
				case 4: // Round Up To Grid
					if ((f26d6 & 0x3F) > 0) {
						f26d6 &= 0xFFFFFFC0;
						f26d6 += 0x40;
					} else {
						f26d6 &= 0xFFFFFFC0;
					}
					break;
			}
			return f26d6;
		}
	}
}
