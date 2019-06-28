using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;

namespace SharpGlyph {
	public class CFFCharString {
		public const int MaxStackSize = 48;
		public const int MaxStorageSize = 32;

		protected class CSValue {
			public bool isOperator;
			public int value;
			public int op;
			public byte[] trails;
		}

		public byte[][] globalSubr;
		public byte[][] localSubr;

		protected List<int> stack;
		protected CFFStream stream;
		protected CSValue value;
		protected Point point;
		protected GraphicsPath path;
		protected int stemCount;
		protected Random random;
		protected int[] storage;

		public CFFCharString() {
			stack = new List<int>();
			stream = new CFFStream();
			value = new CSValue();
			point = new Point();
			random = new Random();
			storage = new int[MaxStorageSize];
		}

		public GraphicsPath CreateGlyph(byte[] data, bool hasWidth) {
			if (data == null || data.Length <= 0) {
				return null;
			}
			stream.Push(data);
			//CSValue value = new CSValue();
			//int length = data.Length;
			int isHinting = 0;
			int width = 0;

			path = new GraphicsPath(FillMode.Alternate);
			stemCount = 0;

			//GraphicsPath temp = new GraphicsPath(FillMode.Alternate);
			//List<PointF> points = new List<PointF>();


			while (true) {
				if (stream.HasNext() == false) {
					stream.Pop();
					if (stream.Depth < 1) {
						break;
					}
				}
				ReadValue(stream, value, stemCount);
				if (value.isOperator == false) {
					stack.Add(value.value);
					continue;
				}
				// endchar
				if (value.value == 14) {
					stack.Clear();
					break;
				}
				if (hasWidth) {
					if (value.value != 10 && value.value != 29) {
						width = stack[0];
						stack.RemoveAt(0);
						hasWidth = false;
					}
				}
				/*
				if (isHinting == 1) {
					isHinting--;
					Exec(value, stack, temp, ref point);
					stack.Clear();
					continue;
				}
				*/
				if (isHinting > 0) {
					switch (value.value) {
						case 21: case 22: case 4:
							isHinting--;
							break;
					}
				}
				//if (isHinting == 0) {
					Exec(value);
				//} else {
				//	Exec(value);
				//}
			}
			path.CloseFigure();
			stream.Clear();
			stack.Clear();
			point.X = 0;
			point.Y = 0;

			GraphicsPath result = path;
			path = null;
			return result;
		}

		protected void LineTo(int x, int y) {
			x += point.X;
			y += point.Y;
			path.AddLine(point.X, point.Y, x, y);
			point.X = x;
			point.Y = y;
		}

		protected void RLineTo() {
			int length = stack.Count;
			for (int i = 0; i < length; i += 2) {
				LineTo(stack[i], stack[i + 1]);
			}
		}

		protected void HLineTo() {
			int length = stack.Count;
			if (length % 2 == 1) {
				LineTo(stack[0], 0);
				for (int i = 1; i < length; i += 2) {
					LineTo(0, stack[i]);
					LineTo(stack[i + 1], 0);
				}
			} else {
				for (int i = 0; i < length; i += 2) {
					LineTo(stack[i], 0);
					LineTo(0, stack[i + 1]);
				}
			}
		}

		protected void VLineTo() {
			int length = stack.Count;
			if (length % 2 == 1) {
				LineTo(0, stack[0]);
				for (int i = 1; i < length; i += 2) {
					LineTo(stack[i], 0);
					LineTo(0, stack[i + 1]);
				}
			} else {
				for (int i = 0; i < length; i += 2) {
					LineTo(0, stack[i]);
					LineTo(stack[i + 1], 0);
				}
			}
		}

		protected void CurveTo(int xa, int ya, int xb, int yb, int xc, int yc) {
			xa += point.X;
			ya += point.Y;
			xb += xa;
			yb += ya;
			xc += xb;
			yc += yb;
			path.AddBezier(point.X, point.Y, xa, ya, xb, yb, xc, yc);
			point.X = xc;
			point.Y = yc;
		}

		protected void RrCurveTo() {
			int length = stack.Count;
			for (int i = 0; i < length; i += 6) {
				CurveTo(
					stack[i    ], stack[i + 1],
					stack[i + 2], stack[i + 3],
					stack[i + 4], stack[i + 5]
				);
			}
		}

		protected void HhCurveTo() {
			int length = stack.Count;
			if (length % 2 == 1) {
				int y1 = stack[0];
				for (int i = 1; i < length; i += 4) {
					CurveTo(
						stack[i], y1,
						stack[i + 1], stack[i + 2],
						stack[i + 3], 0
					);
					y1 = 0;
				}
			} else {
				for (int i = 0; i < length; i += 4) {
					CurveTo(
						stack[i], 0,
						stack[i + 1], stack[i + 2],
						stack[i + 3], 0
					);
				}
			}
		}

		protected void HvCurveTo() {
			int length = stack.Count;
			int mod = length % 8;
			int yf = 0;
			if (mod == 0 || mod == 1) {
				length--;
				for (int i = 0; i < length; i += 8) {
					CurveTo(
						stack[i], 0,
						stack[i + 1], stack[i + 2],
						0, stack[i + 3]
					);
					if (i + 8 == length) {
						yf = stack[i + 8];
					}
					CurveTo(
						0, stack[i + 4],
						stack[i + 5], stack[i + 6],
						stack[i + 7], yf
					);
				}
			} else {
				length--;
				int xf = 0;
				if (4 == length) {
					xf = stack[4];
				}
				CurveTo(
					stack[0], 0,
					stack[1], stack[2],
					xf, stack[3]
				);
				xf = 0;
				for (int i = 4; i < length; i += 8) {
					CurveTo(
						0, stack[i],
						stack[i + 1], stack[i + 2],
						stack[i + 3], 0
					);
					if (i + 8 == length) {
						xf = stack[i + 8];
					}
					CurveTo(
						stack[i + 4], 0,
						stack[i + 5], stack[i + 6],
						xf, stack[i + 7]
					);
				}
			}
		}

		protected void RCurveLine() {
			int length = stack.Count - 2;
			for (int i = 0; i < length; i += 6) {
				CurveTo(
					stack[i], stack[i + 1],
					stack[i + 2], stack[i + 3],
					stack[i + 4], stack[i + 5]
				);
			}
			int n = length;
			LineTo(stack[n], stack[n + 1]);
		}

		protected void RLineCurve() {
			int length = stack.Count - 6;
			for (int i = 0; i < length; i += 2) {
				LineTo(stack[i], stack[i + 1]);
			}
			int n = length;
			CurveTo(
				stack[n], stack[n + 1],
				stack[n + 2], stack[n + 3],
				stack[n + 4], stack[n + 5]
			);
		}

		protected void VhCurveTo() {
			int length = stack.Count;
			int mod = length % 8;
			int xf = 0;
			if (mod == 0 || mod == 1) {
				length--;
				for (int i = 0; i < length; i += 8) {
					CurveTo(
						0, stack[i],
						stack[i + 1], stack[i + 2],
						stack[i + 3], 0
					);
					if (i + 8 == length) {
						xf = stack[i + 8];
					}
					CurveTo(
						stack[i + 4], 0,
						stack[i + 5], stack[i + 6],
						xf, stack[i + 7]
					);
				}
			} else {
				length--;
				int yf = 0;
				if (4 == length) {
					yf = stack[4];
				}
				CurveTo(
					0, stack[0],
					stack[1], stack[2],
					stack[3], yf
				);
				yf = 0;
				for (int i = 4; i < length; i += 8) {
					CurveTo(
						stack[i], 0,
						stack[i + 1], stack[i + 2],
						0, stack[i + 3]
					);
					if (i + 8 == length) {
						yf = stack[i + 8];
					}
					CurveTo(
						0, stack[i + 4],
						stack[i + 5], stack[i + 6],
						stack[i + 7], yf
					);
				}
			}
		}

		protected void VvCurveTo() {
			int length = stack.Count;
			if (length % 2 == 1) {
				int x1 = stack[0];
				for (int i = 1; i < length; i += 4) {
					CurveTo(
						x1, stack[i],
						stack[i + 1], stack[i + 2],
						0, stack[i + 3]
					);
					x1 = 0;
				}
			} else {
				for (int i = 0; i < length; i += 4) {
					CurveTo(
						0, stack[i],
						stack[i + 1], stack[i + 2],
						0, stack[i + 3]
					);
				}
			}
		}

		protected void Exec(CSValue value) {
			switch (value.value) {
				//-------------------------------------
				// Path Construction Operators
				//
				case 21: // rmoveto
					path.CloseFigure();
					point.X += stack[0];
					point.Y += stack[1];
					stack.Clear();
					break;
				case 22: // hmoveto
					path.CloseFigure();
					point.X += stack[0];
					stack.Clear();
					break;
				case 4: // vmoveto
					path.CloseFigure();
					point.Y += stack[0];
					stack.Clear();
					break;
				case 5: // rlineto
					RLineTo();
					stack.Clear();
					break;
				case 6: // hlineto
					HLineTo();
					stack.Clear();
					break;
				case 7: // vlineto
					VLineTo();
					stack.Clear();
					break;
				case 8: // rrcurveto
					RrCurveTo();
					stack.Clear();
					break;
				case 27: // hhcurveto
					HhCurveTo();
					stack.Clear();
					break;
				case 31: // hvcurveto
					HvCurveTo();
					stack.Clear();
					break;
				case 24: // rcurveline
					RCurveLine();
					stack.Clear();
					break;
				case 25: // rlinecurve
					RLineCurve();
					stack.Clear();
					break;
				case 30: // vhcurveto
					VhCurveTo();
					stack.Clear();
					break;
				case 26: // vvcurveto
					VvCurveTo();
					stack.Clear();
					break;

				//-------------------------------------
				// Hint Operators
				//
				case 1: // hstem
					stemCount += stack.Count / 2;
					stack.Clear();
					break;
				case 3: // vstem
					stemCount += stack.Count / 2;
					stack.Clear();
					break;
				case 18: // hstemhm
					stemCount += stack.Count / 2;
					stack.Clear();
					break;
				case 23: // vstemhm
					stemCount += stack.Count / 2;
					stack.Clear();
					break;
				case 19: // hintmask
					stemCount += stack.Count / 2;
					stack.Clear();
					break;
				case 20: // cntrmask
					stemCount += stack.Count / 2;
					stack.Clear();
					break;

				//-------------------------------------
				// Subroutine Operators
				//
				case 10: // callsubr;
					{
						int i = stack.Count - 1;
						int n = stack[i];
						stack.RemoveAt(i);
						int length = localSubr.Length;
						if (length <= 1240) {
							n += 107;
						} else if (length <= 33900) {
							n += 1131;
						} else {
							n += 32768;
						}
						//Console.WriteLine("Decode local subr {0}:\n{1}", n, Decode(localSubr[n]));
						stream.Push(localSubr[n]);
					}
					break;
				case 29: // callgsubr;
					{
						int i = stack.Count - 1;
						int n = stack[i];
						stack.RemoveAt(i);
						int length = globalSubr.Length;
						if (length <= 1240) {
							n += 107;
						} else if (length <= 33900) {
							n += 1131;
						} else {
							n += 32768;
						}
						//Console.WriteLine("Decode global subr: {0}\n{1}", n, Decode(globalSubr[n]));
						stream.Push(globalSubr[n]);
					}
					break;
				case 11: // return;
					{
						stream.Pop();
					}
					break;

				case 12:
					switch (value.op) {
						//-------------------------------------
						// Path Construction Operators
						//
						case 35: // flex
							{
								// TODO: fix fd
								int fd = stack[12];
								CurveTo(
									stack[0], stack[1],
									stack[2], stack[3],
									stack[4], stack[5]
								);
								CurveTo(
									stack[6], stack[7],
									stack[8], stack[9],
									stack[10], stack[11]
								);
							}
							stack.Clear();
							break;
						case 34: // hflex
							CurveTo(
								stack[0], 0,
								stack[1], stack[2],
								stack[3], 0
							);
							CurveTo(
								stack[4], 0,
								stack[5], 0,
								stack[6], 0
							);
							stack.Clear();
							break;
						case 36: // hflex1
							CurveTo(
								stack[0], stack[1],
								stack[2], stack[3],
								stack[4], 0
							);
							CurveTo(
								stack[5], 0,
								stack[6], stack[7],
								stack[8], 0
							);
							stack.Clear();
							break;
						case 37: // flex1
							{
								// TODO: fix d6
								int d6 = stack[10];
								CurveTo(
									stack[0], stack[1],
									stack[2], stack[3],
									stack[4], stack[5]
								);
								CurveTo(
									stack[6], stack[7],
									stack[8], stack[9],
									0, 0
								);
							}
							stack.Clear();
							break;

						//-------------------------------------
						// Arithmetic Operators
						case 9: // abs
							{
								int i = stack.Count - 1;
								stack[i] = Math.Abs(stack[i]);
							}
							break;
						case 10: // add
							{
								int i = stack.Count - 1;
								stack[i] = stack[i - 1] + stack[i];
								stack.RemoveAt(i);
							}
							break;
						case 11: // sub
							{
								int i = stack.Count - 1;
								stack[i] = stack[i - 1] - stack[i];
								stack.RemoveAt(i);
							}
							break;
						case 12: // div
							{
								int i = stack.Count - 1;
								stack[i] = stack[i - 1] / stack[i];
								stack.RemoveAt(i);
							}
							break;
						case 14: // neg
							{
								int i = stack.Count - 1;
								stack[i] = -stack[i];
							}
							break;
						case 23: // random
							{
								stack.Add(random.Next());
							}
							break;
						case 24: // mul
							{
								int i = stack.Count - 1;
								stack[i] = stack[i - 1] * stack[i];
								stack.RemoveAt(i);
							}
							break;
						case 26: // sqrt
							{
								int i = stack.Count - 1;
								stack[i] = (int)Math.Sqrt(stack[i]);
							}
							break;
						case 18: // drop
							{
								int i = stack.Count - 1;
								stack.RemoveAt(i);
							}
							break;
						case 28: // exch
							{
								int i = stack.Count - 1;
								int tmp = stack[i];
								stack[i] = stack[i - 1];
								stack[i - 1] = tmp;
							}
							break;
						case 29: // index
							{
								int i = stack.Count - 1;
								int n = stack[i];
								if (n < 0) {
									stack.Add(stack[i]);
								} else {
									for (int a = i - n; a <= i; a++) {
										stack.Add(stack[a]);
									}
								}
							}
							break;
						case 30: // roll
							// TODO:
							break;
						case 27: // dup
							{
								int i = stack.Count - 1;
								stack.Add(stack[i]);
							}
							break;

						//-------------------------------------
						// Storage Operators
						case 20: // put
							{
								int i = stack.Count - 1;
								storage[stack[i]] = stack[i - 1];
								stack.RemoveRange(i - 1, 2);
							}
							break;
						case 21: // get
							{
								int i = stack.Count - 1;
								stack.Add(storage[stack[i]]);
								stack.RemoveAt(i);
							}
							break;

						//-------------------------------------
						// Conditional Operators
						case 3: // and
							{
								int i = stack.Count - 1;
								int n1 = stack[i];
								int n2 = stack[i - 1];
								if (n1 != 0 && n2 != 0) {
									stack[i - 1] = 1;
								} else {
									stack[i - 1] = 0;
								}
								stack.RemoveAt(i);
							}
							break;
						case 4: // or
							{
								int i = stack.Count - 1;
								int n1 = stack[i];
								int n2 = stack[i - 1];
								if (n1 != 0 || n2 != 0) {
									stack[i - 1] = 1;
								} else {
									stack[i - 1] = 0;
								}
								stack.RemoveAt(i);
							}
							break;
						case 5: // not
							{
								int i = stack.Count - 1;
								int n1 = stack[i];
								if (n1 != 0) {
									stack[i] = 0;
								} else {
									stack[i] = 1;
								}
							}
							break;
						case 15: // eq
							{
								int i = stack.Count - 1;
								int n1 = stack[i];
								int n2 = stack[i - 1];
								if (n1 == n2) {
									stack[i - 1] = 1;
								} else {
									stack[i - 1] = 0;
								}
								stack.RemoveAt(i);
							}
							break;
						case 22: // ifelse
							{
								int i = stack.Count - 1;
								int v1 = stack[i];
								int v2 = stack[i - 1];
								if (v1 < v2) {
									stack[i - 3] = stack[i - 2];
									stack.RemoveRange(i - 2, 3);
								} else if (v1 > v2) {
									stack.RemoveRange(i - 2, 3);
								} else {
									stack.RemoveRange(i - 1, 2);
								}
							}
							break;
					}
					break;
			}
		}

		protected static void CurveTo(List<PointF> points, ref PointF point, int xa, int ya, int xb, int yb, int xc, int yc) {
			float step = 1f / Math.Max(Math.Abs(xc - point.X), Math.Abs(yc - point.Y));
			step = Math.Min(step, 0.1f);
			int length = (int)(1.0f / step) + 1;
			for (int i = 0; i < length; i++) {
				float t = (float)i / length;
				PointF p = new PointF(
					Bezier(t, (int)point.X, xa, xb, xc),
					Bezier(t, (int)point.Y, ya, yb, yc)
				);
				points.Add(p);
			}
			points.Add(new PointF(
				Bezier(1f, (int)point.X, xa, xb, xc),
				Bezier(1f, (int)point.Y, ya, yb, yc)
			));
			point.X = xc;
			point.Y = yc;
		}

		private static float Bezier(float t, int p0, int p1, int p2, int p3) {
			float dt = 1f - t;
			float t2 = t * t;
			float dt2 = dt * dt;
			return (
				(dt * dt2 * p0) +
				(3f * dt2 * t * p1) +
				(3f * dt * t2 * p2) +
				(t * t2 * p3)
			);
		}

		public static string Decode(byte[] data) {
			if (data == null || data.Length <= 0) {
				return "";
			}
			//MemoryStream stream = new MemoryStream(data);
			//BinaryReaderFont reader = new BinaryReaderFont(stream);
			CFFStream stream = new CFFStream();
			stream.Push(data);
			CSValue value = new CSValue();
			int stackCount = 0;
			int stemCount = 0;
			int length = data.Length;

			StringBuilder builder = new StringBuilder();

			while (stream.Position < length) {
				ReadValue(stream, value, stemCount);
				if (value.isOperator) {
					string name = ToOperatorName(value);
					if (name.Contains("stem")) {
						stemCount += stackCount / 2;
					}
					builder.Append(name);
					if (name.Contains("mask")) {
						stemCount += stackCount / 2;
						for (int i = 0; i < value.trails.Length; i++) {
							builder.AppendFormat(" 0x{0:X2}", value.trails[i]);
						}
					}
					builder.AppendLine();
					stackCount = 0;
					continue;
				}
				stackCount++;
				builder.AppendFormat("{0} ", value.value);
			}

			//reader.Close();
			//stream.Close();
			//reader.Dispose();
			//stream.Dispose();
			return builder.ToString();
		}

		protected static string ToOperatorName(CSValue value) {
			if (value.value == 12) {
				switch (value.op) {
					// Path Construction Operators
					case 35: return "flex";
					case 34: return "hflex";
					case 36: return "hflex1";
					case 37: return "flex1";

					// Arithmetic Operators
					case 9: return "abs";
					case 10: return "add";
					case 11: return "sub";
					case 12: return "div";
					case 14: return "neg";
					case 23: return "random";
					case 24: return "mul";
					case 26: return "sqrt";
					case 18: return "drop";
					case 28: return "exch";
					case 29: return "index";
					case 30: return "roll";
					case 27: return "dup";

					// Storage Operators
					case 20: return "put";
					case 21: return "get";

					// Conditional Operators
					case 3: return "and";
					case 4: return "or";
					case 5: return "not";
					case 15: return "eq";
					case 22: return "ifelse";
				}
				return "unknown";
			}
			switch (value.value) {
				// Path Construction Operators
				case 21: return "rmoveto";
				case 22: return "hmoveto";
				case 4: return "vmoveto";
				case 5: return "rlineto";
				case 6: return "hlineto";
				case 7: return "vlineto";
				case 8: return "rrcurveto";
				case 27: return "hhcurveto";
				case 31: return "hvcurveto";
				case 24: return "rcurveline";
				case 25: return "rlinecurve";
				case 30: return "vhcurveto";
				case 26: return "vvcurveto";

				// Operator for Finishing a Path
				case 14: return "endchar";

				// Hint Operators
				case 1: return "hstem";
				case 3: return "vstem";
				case 18: return "hstemhm";
				case 23: return "vstemhm";
				case 19: return "hintmask";
				case 20: return "cntrmask";

				// Subroutine Operators
				case 10: return "callsubr";
				case 29: return "callgsubr";
				case 11: return "return";
			}
			return "unknown";
		}

		protected static void ReadValue(CFFStream stream, CSValue value, int stemCount) {
			short v = stream.ReadByte();
			switch (v) {
				case 12:
					value.isOperator = true;
					value.value = 12;
					value.op = stream.ReadByte();
					return;
				case 19: case 20:
					value.isOperator = true;
					value.value = v;
					int count = (int)Math.Ceiling((double)stemCount / 8);
					if (count == 0) {
						count = 1;
					}
					value.trails = stream.ReadBytes(count);
					break;
				case 28:
					value.isOperator = false;
					value.value = stream.ReadInt16();
					return;
				case 247: case 248: case 249: case 250:
					value.isOperator = false;
					value.value = (v - 247) * 256 + stream.ReadByte() + 108;
					return;
				case 251: case 252: case 253: case 254:
					value.isOperator = false;
					value.value = -((v - 251) * 256) - stream.ReadByte() - 108;
					return;
				case 255:
					value.isOperator = false;
					value.value = stream.ReadInt32();
					return;
			}
			if (v >= 32 && v <= 246) {
				value.isOperator = false;
				value.value = v - 139;
				return;
			}
			value.isOperator = true;
			value.value = v;
		}
	}
}
