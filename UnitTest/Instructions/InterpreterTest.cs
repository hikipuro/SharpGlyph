using System;
using NUnit.Framework;
using SharpGlyph;

namespace UnitTest.Instructions {
	[TestFixture()]
	public class InterpreterTest {
		[Test()]
		public void Constructor() {
			Interpreter interpreter = new Interpreter(null);
		}

		[Test()]
		public void NPUSHB() {
			Interpreter interpreter = new Interpreter(null);
			InterpreterStack stack = interpreter.stack;
			interpreter.Interpret(new byte[] {
				0x40, 0x00 // NPUSHB[]
			});
			Assert.AreEqual(0, stack.Depth);

			interpreter.Interpret(new byte[] {
				0x40, 0x01, // NPUSHB[]
				0x10
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x10, stack.Pop());

			byte[] data = new byte[257];
			data[0] = 0x40;
			data[1] = 0xFF;
			for (int i = 2; i < 257; i++) {
				data[i] = (byte)(i - 1);
			}
			interpreter.Interpret(data);
			Assert.AreEqual(255, stack.Depth);
		}

		[Test()]
		public void NPUSHW() {
			Interpreter interpreter = new Interpreter(null);
			InterpreterStack stack = interpreter.stack;
			interpreter.Interpret(new byte[] {
				0x41, 0x00 // NPUSHW[]
			});
			Assert.AreEqual(0, stack.Depth);

			interpreter.Interpret(new byte[] {
				0x41, 0x01, // NPUSHW[]
				0x10, 0x01
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x1001, stack.Pop());

			byte[] data = new byte[255 * 2 + 2];
			data[0] = 0x41;
			data[1] = 0xFF;
			for (int i = 2; i < data.Length; i++) {
				data[i] = (byte)(i - 1);
			}
			interpreter.Interpret(data);
			Assert.AreEqual(255, stack.Depth);
		}

		[Test()]
		public void PUSHB() {
			Interpreter interpreter = new Interpreter(null);
			InterpreterStack stack = interpreter.stack;
			interpreter.Interpret(new byte[] {
				0xB0, // PUSHB[000]
				0x10
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x10, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xB1, // PUSHB[001]
				0x10, 0x11
			});
			Assert.AreEqual(2, stack.Depth);
			Assert.AreEqual(0x11, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xB2, // PUSHB[010]
				0x10, 0x11, 0x12
			});
			Assert.AreEqual(3, stack.Depth);
			Assert.AreEqual(0x12, stack.Pop());
			Assert.AreEqual(0x11, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xB3, // PUSHB[011]
				0x10, 0x11, 0x12, 0x13
			});
			Assert.AreEqual(4, stack.Depth);
			Assert.AreEqual(0x13, stack.Pop());
			Assert.AreEqual(0x12, stack.Pop());
			Assert.AreEqual(0x11, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xB4, // PUSHB[100]
				0x10, 0x11, 0x12, 0x13,
				0x14
			});
			Assert.AreEqual(5, stack.Depth);
			Assert.AreEqual(0x14, stack.Pop());
			Assert.AreEqual(0x13, stack.Pop());
			Assert.AreEqual(0x12, stack.Pop());
			Assert.AreEqual(0x11, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xB5, // PUSHB[101]
				0x10, 0x11, 0x12, 0x13,
				0x14, 0x15
			});
			Assert.AreEqual(6, stack.Depth);
			Assert.AreEqual(0x15, stack.Pop());
			Assert.AreEqual(0x14, stack.Pop());
			Assert.AreEqual(0x13, stack.Pop());
			Assert.AreEqual(0x12, stack.Pop());
			Assert.AreEqual(0x11, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xB6, // PUSHB[110]
				0x10, 0x11, 0x12, 0x13,
				0x14, 0x15, 0x16
			});
			Assert.AreEqual(7, stack.Depth);
			Assert.AreEqual(0x16, stack.Pop());
			Assert.AreEqual(0x15, stack.Pop());
			Assert.AreEqual(0x14, stack.Pop());
			Assert.AreEqual(0x13, stack.Pop());
			Assert.AreEqual(0x12, stack.Pop());
			Assert.AreEqual(0x11, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xB7, // PUSHB[111]
				0x10, 0x11, 0x12, 0x13,
				0x14, 0x15, 0x16, 0x17
			});
			Assert.AreEqual(8, stack.Depth);
			Assert.AreEqual(0x17, stack.Pop());
			Assert.AreEqual(0x16, stack.Pop());
			Assert.AreEqual(0x15, stack.Pop());
			Assert.AreEqual(0x14, stack.Pop());
			Assert.AreEqual(0x13, stack.Pop());
			Assert.AreEqual(0x12, stack.Pop());
			Assert.AreEqual(0x11, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());
		}

		[Test()]
		public void PUSHW() {
			Interpreter interpreter = new Interpreter(null);
			InterpreterStack stack = interpreter.stack;
			interpreter.Interpret(new byte[] {
				0xB8, // PUSHW[000]
				0x10, 0x01
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x1001, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xB9, // PUSHW[001]
				0x10, 0x01, 0x11, 0x11
			});
			Assert.AreEqual(2, stack.Depth);
			Assert.AreEqual(0x1111, stack.Pop());
			Assert.AreEqual(0x1001, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xBA, // PUSHW[010]
				0x10, 0x01, 0x11, 0x11,
				0x12, 0x21
			});
			Assert.AreEqual(3, stack.Depth);
			Assert.AreEqual(0x1221, stack.Pop());
			Assert.AreEqual(0x1111, stack.Pop());
			Assert.AreEqual(0x1001, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xBB, // PUSHW[011]
				0x10, 0x01, 0x11, 0x11,
				0x12, 0x21, 0x13, 0x31
			});
			Assert.AreEqual(4, stack.Depth);
			Assert.AreEqual(0x1331, stack.Pop());
			Assert.AreEqual(0x1221, stack.Pop());
			Assert.AreEqual(0x1111, stack.Pop());
			Assert.AreEqual(0x1001, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xBC, // PUSHW[100]
				0x10, 0x01, 0x11, 0x11,
				0x12, 0x21, 0x13, 0x31,
				0x14, 0x41
			});
			Assert.AreEqual(5, stack.Depth);
			Assert.AreEqual(0x1441, stack.Pop());
			Assert.AreEqual(0x1331, stack.Pop());
			Assert.AreEqual(0x1221, stack.Pop());
			Assert.AreEqual(0x1111, stack.Pop());
			Assert.AreEqual(0x1001, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xBD, // PUSHW[101]
				0x10, 0x01, 0x11, 0x11,
				0x12, 0x21, 0x13, 0x31,
				0x14, 0x41, 0x15, 0x51
			});
			Assert.AreEqual(6, stack.Depth);
			Assert.AreEqual(0x1551, stack.Pop());
			Assert.AreEqual(0x1441, stack.Pop());
			Assert.AreEqual(0x1331, stack.Pop());
			Assert.AreEqual(0x1221, stack.Pop());
			Assert.AreEqual(0x1111, stack.Pop());
			Assert.AreEqual(0x1001, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xBE, // PUSHW[110]
				0x10, 0x01, 0x11, 0x11,
				0x12, 0x21, 0x13, 0x31,
				0x14, 0x41, 0x15, 0x51,
				0x16, 0x61
			});
			Assert.AreEqual(7, stack.Depth);
			Assert.AreEqual(0x1661, stack.Pop());
			Assert.AreEqual(0x1551, stack.Pop());
			Assert.AreEqual(0x1441, stack.Pop());
			Assert.AreEqual(0x1331, stack.Pop());
			Assert.AreEqual(0x1221, stack.Pop());
			Assert.AreEqual(0x1111, stack.Pop());
			Assert.AreEqual(0x1001, stack.Pop());

			interpreter.Interpret(new byte[] {
				0xBF, // PUSHW[111]
				0x10, 0x01, 0x11, 0x11,
				0x12, 0x21, 0x13, 0x31,
				0x14, 0x41, 0x15, 0x51,
				0x16, 0x61, 0x17, 0x71
			});
			Assert.AreEqual(8, stack.Depth);
			Assert.AreEqual(0x1771, stack.Pop());
			Assert.AreEqual(0x1661, stack.Pop());
			Assert.AreEqual(0x1551, stack.Pop());
			Assert.AreEqual(0x1441, stack.Pop());
			Assert.AreEqual(0x1331, stack.Pop());
			Assert.AreEqual(0x1221, stack.Pop());
			Assert.AreEqual(0x1111, stack.Pop());
			Assert.AreEqual(0x1001, stack.Pop());
		}

		[Test()]
		public void RS() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			interpreter.storage[0] = 0x1234;
			stack.Init(32);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x43 // RS[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x1234, stack.Pop());
		}

		[Test()]
		public void WS() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(0x1234);
			interpreter.Interpret(new byte[] {
				0x42 // WS[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0x1234, interpreter.storage[0]);
		}

		[Test()]
		public void WCVTP() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0); // location
			stack.Push(0x1234); // value
			interpreter.Interpret(new byte[] {
				0x44 // WCVTP[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0x1234 * 0x40, interpreter.cvt.data[0]);
		}

		[Test()]
		public void WCVTF() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0); // location
			stack.Push(0x1234); // value
			interpreter.Interpret(new byte[] {
				0x70 // WCVTF[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0x1234, interpreter.cvt.data[0]);
		}

		[Test()]
		public void RCVT() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			interpreter.cvt.data[0] = 0x1234;
			stack.Init(32);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x45 // RCVT[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x1234, stack.Pop());
		}

		[Test()]
		public void SVTCA() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			interpreter.Interpret(new byte[] {
				0x00 // SVTCA[0]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI / 2), interpreter.state.freedom_vector);
			Assert.AreEqual((float)(Math.PI / 2), interpreter.state.projection_vector);
			interpreter.Interpret(new byte[] {
				0x01 // SVTCA[1]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0f, interpreter.state.freedom_vector);
			Assert.AreEqual(0f, interpreter.state.projection_vector);
		}

		[Test()]
		public void SPVTCA() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			interpreter.Interpret(new byte[] {
				0x02 // SPVTCA[0]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI / 2), interpreter.state.projection_vector);
			interpreter.Interpret(new byte[] {
				0x03 // SPVTCA[1]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0f, interpreter.state.projection_vector);
		}

		[Test()]
		public void SFVTCA() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			interpreter.Interpret(new byte[] {
				0x04 // SFVTCA[0]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI / 2), interpreter.state.freedom_vector);
			interpreter.Interpret(new byte[] {
				0x05 // SFVTCA[1]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0f, interpreter.state.freedom_vector);
		}

		[Test()]
		public void SPVTL() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			Point2D[] points = new Point2D[2];
			points[0] = new Point2D(1, 1);
			points[1] = new Point2D(2, 1);
			interpreter.debugPoints = points;
			GraphicsState state = interpreter.state;
			InterpreterStack stack = interpreter.stack;
			state.projection_vector = 1;
			stack.Init(32);
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x06 // SDPVTL[0]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, state.projection_vector);
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x07 // SDPVTL[1]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI / 2), state.projection_vector);
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x06 // SDPVTL[0]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)Math.PI, state.projection_vector);
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x07 // SDPVTL[1]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI * 1.5), state.projection_vector);
		}

		[Test()]
		public void SFVTL() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			Point2D[] points = new Point2D[2];
			points[0] = new Point2D(1, 1);
			points[1] = new Point2D(2, 1);
			interpreter.debugPoints = points;
			GraphicsState state = interpreter.state;
			InterpreterStack stack = interpreter.stack;
			state.freedom_vector = 1;
			stack.Init(32);
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x08 // SFVTL[0]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, state.freedom_vector);
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x09 // SFVTL[1]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI / 2), state.freedom_vector);
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x08 // SFVTL[0]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)Math.PI, state.freedom_vector);
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x09 // SFVTL[1]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI * 1.5), state.freedom_vector);
		}

		[Test()]
		public void SFVTPV() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.projection_vector = 1.234f;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			interpreter.Interpret(new byte[] {
				0x0E // SFVTPV[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(1.234f, interpreter.state.freedom_vector);
		}

		[Test()]
		public void SDPVTL() {
			Assert.Fail();
		}

		[Test()]
		public void SPVFS() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			GraphicsState state = interpreter.state;
			InterpreterStack stack = interpreter.stack;
			state.projection_vector = 1;
			stack.Init(32);
			stack.Push(1); // x
			stack.Push(0); // y
			interpreter.Interpret(new byte[] {
				0x0A // SPVFS[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, state.projection_vector);
			stack.Push(0); // x
			stack.Push(1); // y
			interpreter.Interpret(new byte[] {
				0x0A // SPVFS[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI / 2), state.projection_vector);
		}

		[Test()]
		public void SFVFS() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			GraphicsState state = interpreter.state;
			InterpreterStack stack = interpreter.stack;
			state.freedom_vector = 1;
			stack.Init(32);
			stack.Push(1); // x
			stack.Push(0); // y
			interpreter.Interpret(new byte[] {
				0x0B // SFVFS[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, state.freedom_vector);
			stack.Push(0); // x
			stack.Push(1); // y
			interpreter.Interpret(new byte[] {
				0x0B // SFVFS[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((float)(Math.PI / 2), state.freedom_vector);
		}

		[Test()]
		public void GPV() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			interpreter.state.projection_vector = 0;
			interpreter.Interpret(new byte[] {
				0x0C // GPV[]
			});
			Assert.AreEqual(2, stack.Depth);
			Assert.AreEqual(0, stack.Pop());
			Assert.AreEqual(1 * 0x4000, stack.Pop());

			interpreter.state.projection_vector = (float)(Math.PI / 4);
			interpreter.Interpret(new byte[] {
				0x0C // GPV[]
			});
			Assert.AreEqual(0x2D41, stack.Pop());
			Assert.AreEqual(0x2D41, stack.Pop());

			interpreter.state.projection_vector = (float)(Math.PI / 2);
			interpreter.Interpret(new byte[] {
				0x0C // GPV[]
			});
			Assert.AreEqual(0x4000, stack.Pop());
			Assert.AreEqual(0, stack.Pop());

			interpreter.state.projection_vector = (float)(Math.PI);
			interpreter.Interpret(new byte[] {
				0x0C // GPV[]
			});
			Assert.AreEqual(0, stack.Pop());
			Assert.AreEqual(-0x4000, stack.Pop());

			interpreter.state.projection_vector = (float)(Math.PI * 1.5);
			interpreter.Interpret(new byte[] {
				0x0C // GPV[]
			});
			Assert.AreEqual(-0x4000, stack.Pop());
			Assert.AreEqual(0, stack.Pop());
		}

		[Test()]
		public void GFV() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			interpreter.state.freedom_vector = 0;
			interpreter.Interpret(new byte[] {
				0x0D // GFV[]
			});
			Assert.AreEqual(2, stack.Depth);
			Assert.AreEqual(0, stack.Pop());
			Assert.AreEqual(1 * 0x4000, stack.Pop());

			interpreter.state.freedom_vector = (float)(Math.PI / 4);
			interpreter.Interpret(new byte[] {
				0x0D // GFV[]
			});
			Assert.AreEqual(0x2D41, stack.Pop());
			Assert.AreEqual(0x2D41, stack.Pop());

			interpreter.state.freedom_vector = (float)(Math.PI / 2);
			interpreter.Interpret(new byte[] {
				0x0D // GFV[]
			});
			Assert.AreEqual(0x4000, stack.Pop());
			Assert.AreEqual(0, stack.Pop());

			interpreter.state.freedom_vector = (float)(Math.PI);
			interpreter.Interpret(new byte[] {
				0x0D // GFV[]
			});
			Assert.AreEqual(0, stack.Pop());
			Assert.AreEqual(-0x4000, stack.Pop());

			interpreter.state.freedom_vector = (float)(Math.PI * 1.5);
			interpreter.Interpret(new byte[] {
				0x0D // GFV[]
			});
			Assert.AreEqual(-0x4000, stack.Pop());
			Assert.AreEqual(0, stack.Pop());
		}

		[Test()]
		public void SRP0() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.rp0 = 0;
			interpreter.state.rp1 = 0;
			interpreter.state.rp2 = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x10 // SRP0[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(1, interpreter.state.rp0);
			Assert.AreEqual(0, interpreter.state.rp1);
			Assert.AreEqual(0, interpreter.state.rp2);
		}

		[Test()]
		public void SRP1() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.rp0 = 0;
			interpreter.state.rp1 = 0;
			interpreter.state.rp2 = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x11 // SRP1[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, interpreter.state.rp0);
			Assert.AreEqual(1, interpreter.state.rp1);
			Assert.AreEqual(0, interpreter.state.rp2);
		}

		[Test()]
		public void SRP2() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.rp0 = 0;
			interpreter.state.rp1 = 0;
			interpreter.state.rp2 = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x12 // SRP2[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, interpreter.state.rp0);
			Assert.AreEqual(0, interpreter.state.rp1);
			Assert.AreEqual(1, interpreter.state.rp2);
		}

		[Test()]
		public void SZP0() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.zp0 = 0;
			interpreter.state.zp1 = 0;
			interpreter.state.zp2 = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x13 // SZP0[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(1, interpreter.state.zp0);
			Assert.AreEqual(0, interpreter.state.zp1);
			Assert.AreEqual(0, interpreter.state.zp2);
		}

		[Test()]
		public void SZP1() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.zp0 = 0;
			interpreter.state.zp1 = 0;
			interpreter.state.zp2 = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x14 // SZP1[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, interpreter.state.zp0);
			Assert.AreEqual(1, interpreter.state.zp1);
			Assert.AreEqual(0, interpreter.state.zp2);
		}

		[Test()]
		public void SZP2() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.zp0 = 0;
			interpreter.state.zp1 = 0;
			interpreter.state.zp2 = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x15 // SZP2[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0, interpreter.state.zp0);
			Assert.AreEqual(0, interpreter.state.zp1);
			Assert.AreEqual(1, interpreter.state.zp2);
		}

		[Test()]
		public void SZPS() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.zp0 = 0;
			interpreter.state.zp1 = 0;
			interpreter.state.zp2 = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x16 // SZPS[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(1, interpreter.state.zp0);
			Assert.AreEqual(1, interpreter.state.zp1);
			Assert.AreEqual(1, interpreter.state.zp2);
		}

		[Test()]
		public void RTHG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = RoundState.Off;
			interpreter.Interpret(new byte[] {
				0x19 // RTHG[]
			});
			Assert.AreEqual(RoundState.HalfGrid, interpreter.state.round_state);
		}

		[Test()]
		public void RTG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = RoundState.Off;
			interpreter.Interpret(new byte[] {
				0x18 // RTG[]
			});
			Assert.AreEqual(RoundState.Grid, interpreter.state.round_state);
		}

		[Test()]
		public void RTDG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = RoundState.Off;
			interpreter.Interpret(new byte[] {
				0x3D // RTDG[]
			});
			Assert.AreEqual(RoundState.DoubleGrid, interpreter.state.round_state);
		}

		[Test()]
		public void RDTG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = RoundState.Off;
			interpreter.Interpret(new byte[] {
				0x7D // RDTG[]
			});
			Assert.AreEqual(RoundState.DownToGrid, interpreter.state.round_state);
		}

		[Test()]
		public void RUTG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = RoundState.Off;
			interpreter.Interpret(new byte[] {
				0x7C // RUTG[]
			});
			Assert.AreEqual(RoundState.UpToGrid, interpreter.state.round_state);
		}

		[Test()]
		public void ROFF() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = RoundState.HalfGrid;
			interpreter.Interpret(new byte[] {
				0x7A // ROFF[]
			});
			Assert.AreEqual(RoundState.Off, interpreter.state.round_state);
		}

		[Test()]
		public void SROUND() {
			Assert.Fail();
		}

		[Test()]
		public void S45ROUND() {
			Assert.Fail();
		}

		[Test()]
		public void SLOOP() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(3);
			interpreter.state.loop = 10;
			interpreter.Interpret(new byte[] {
				0x17 // SLOOP[]
			});
			Assert.AreEqual(3, interpreter.state.loop);
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void SMD() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.minimum_distance = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x1234); // distance
			interpreter.Interpret(new byte[] {
				0x1A // SMD[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0x1234, interpreter.state.minimum_distance);
		}

		[Test()]
		public void INSTCTRL() {
			Assert.Fail();
		}

		[Test()]
		public void SCANCTRL() {
			Assert.Fail();
		}

		[Test()]
		public void SCANTYPE() {
			Assert.Fail();
		}

		[Test()]
		public void SCVTCI() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			Assert.AreEqual(0x44, interpreter.state.control_value_cut_in);
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x1234); // n
			interpreter.Interpret(new byte[] {
				0x1D // SCVTCI[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0x1234, interpreter.state.control_value_cut_in);
		}

		[Test()]
		public void SSWCI() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.singe_width_cut_in = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x1E // SSWCI[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0x10, interpreter.state.singe_width_cut_in);
		}

		[Test()]
		public void SSW() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.single_width_value = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x1F // SSW[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0x10, interpreter.state.single_width_value);
		}

		[Test()]
		public void FLIPON() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.auto_flip = false;
			interpreter.Interpret(new byte[] {
				0x4D // FLIPON[]
			});
			Assert.IsTrue(interpreter.state.auto_flip);
		}

		[Test()]
		public void FLIPOFF() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.auto_flip = false;
			interpreter.Interpret(new byte[] {
				0x4E // FLIPOFF[]
			});
			Assert.IsFalse(interpreter.state.auto_flip);
		}

		[Test()]
		public void SANGW() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x7E // SANGW[]
			});
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void SDB() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			Assert.AreEqual(9, interpreter.state.delta_base);
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x1234); // n
			interpreter.Interpret(new byte[] {
				0x5E // SDB[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0x1234, interpreter.state.delta_base);
		}

		[Test()]
		public void SDS() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			Assert.AreEqual(3, interpreter.state.delta_shift);
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x1234); // n
			interpreter.Interpret(new byte[] {
				0x5F // SDS[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(0x1234, interpreter.state.delta_shift);
		}

		[Test()]
		public void GC() {
			Assert.Fail();
		}

		[Test()]
		public void SCFS() {
			Assert.Fail();
		}

		[Test()]
		public void MD() {
			Assert.Fail();
		}

		[Test()]
		public void MPPEM() {
			Assert.Fail();
		}

		[Test()]
		public void MPS() {
			Assert.Fail();
		}

		[Test()]
		public void FLIPPT() {
			SimpleGlyph simpleGlyph = new SimpleGlyph();
			simpleGlyph.endPtsOfContours = new ushort[] { 2 };
			simpleGlyph.flags = new SimpleGlyphFlags[] {
				0,
				SimpleGlyphFlags.ON_CURVE_POINT
			};
			Glyph glyph = new Glyph();
			glyph.numberOfContours = 1;
			glyph.simpleGlyph = simpleGlyph;
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			interpreter.state.loop = 2;
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x80 // FLIPPT[]
			}, glyph);
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(1, interpreter.state.loop);
			Assert.AreEqual(SimpleGlyphFlags.ON_CURVE_POINT, simpleGlyph.flags[0]);
			Assert.AreEqual((SimpleGlyphFlags)0, simpleGlyph.flags[1]);
		}

		[Test()]
		public void FLIPRGON() {
			SimpleGlyph simpleGlyph = new SimpleGlyph();
			simpleGlyph.endPtsOfContours = new ushort[] { 2 };
			simpleGlyph.flags = new SimpleGlyphFlags[] {
				0, 0, 0
			};
			Glyph glyph = new Glyph();
			glyph.numberOfContours = 1;
			glyph.simpleGlyph = simpleGlyph;
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x81 // FLIPRGON[]
			}, glyph);
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(SimpleGlyphFlags.ON_CURVE_POINT, simpleGlyph.flags[0]);
			Assert.AreEqual(SimpleGlyphFlags.ON_CURVE_POINT, simpleGlyph.flags[1]);
			Assert.AreEqual((SimpleGlyphFlags)0, simpleGlyph.flags[2]);
		}

		[Test()]
		public void FLIPRGOFF() {
			SimpleGlyph simpleGlyph = new SimpleGlyph();
			simpleGlyph.endPtsOfContours = new ushort[] { 2 };
			simpleGlyph.flags = new SimpleGlyphFlags[] {
				SimpleGlyphFlags.ON_CURVE_POINT,
				SimpleGlyphFlags.ON_CURVE_POINT,
				SimpleGlyphFlags.ON_CURVE_POINT
			};
			Glyph glyph = new Glyph();
			glyph.numberOfContours = 1;
			glyph.simpleGlyph = simpleGlyph;
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x82 // FLIPRGOFF[]
			}, glyph);
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual((SimpleGlyphFlags)0, simpleGlyph.flags[0]);
			Assert.AreEqual((SimpleGlyphFlags)0, simpleGlyph.flags[1]);
			Assert.AreEqual(SimpleGlyphFlags.ON_CURVE_POINT, simpleGlyph.flags[2]);
		}

		[Test()]
		public void SHP() {
			Assert.Fail();
		}

		[Test()]
		public void SHC() {
			Assert.Fail();
		}

		[Test()]
		public void SHZ() {
			Assert.Fail();
		}

		[Test()]
		public void SHPIX() {
			Assert.Fail();
		}

		[Test()]
		public void MSIRP() {
			Assert.Fail();
		}

		[Test()]
		public void MDAP() {
			Assert.Fail();
		}

		[Test()]
		public void MIAP() {
			Assert.Fail();
		}

		[Test()]
		public void MDRP() {
			Assert.Fail();
		}

		[Test()]
		public void MIRP() {
			Assert.Fail();
		}

		[Test()]
		public void ALIGNRP() {
			Assert.Fail();
		}

		[Test()]
		public void ISECT() {
			Assert.Fail();
		}

		[Test()]
		public void ALIGNPTS() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.projection_vector = 0;
			interpreter.state.freedom_vector = 0;
			Point2D[] points = new Point2D[2];
			points[0] = new Point2D(1, 1);
			points[1] = new Point2D(3, 2);
			interpreter.debugPoints = points;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x27 // ALIGNPTS[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.AreEqual(2, points[0].GetDistance(points[1], 0));
			Assert.AreEqual(2, interpreter.points[1][0].x);
			Assert.AreEqual(1, interpreter.points[1][0].y);
			Assert.AreEqual(2, interpreter.points[1][1].x);
			Assert.AreEqual(2, interpreter.points[1][1].y);
		}

		[Test()]
		public void IP() {
			Assert.Fail();
		}

		[Test()]
		public void UTP() {
			Assert.Fail();
		}

		[Test()]
		public void IUP() {
			Assert.Fail();
		}

		[Test()]
		public void DELTAP1() {
			Assert.Fail();
		}

		[Test()]
		public void DELTAP2() {
			Assert.Fail();
		}

		[Test()]
		public void DELTAP3() {
			Assert.Fail();
		}

		[Test()]
		public void DELTAC1() {
			Assert.Fail();
		}

		[Test()]
		public void DELTAC2() {
			Assert.Fail();
		}

		[Test()]
		public void DELTAC3() {
			Assert.Fail();
		}

		[Test()]
		public void DUP() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			interpreter.Interpret(new byte[] {
				0x20 // DUP[]
			});
			Assert.AreEqual(3, stack.Depth);
			Assert.AreEqual(0x20, stack.Pop());
			Assert.AreEqual(0x20, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());
		}

		[Test()]
		public void POP() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(0x30);
			stack.Push(0x40);
			interpreter.Interpret(new byte[] {
				0x21 // POP[]
			});
			Assert.AreEqual(3, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
		}

		[Test()]
		public void CLEAR() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(0x30);
			stack.Push(0x40);
			interpreter.Interpret(new byte[] {
				0x22 // CLEAR[]
			});
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void SWAP() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x20);
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x23 // SWAP[]
			});
			Assert.AreEqual(2, stack.Depth);
			Assert.AreEqual(0x20, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());
		}

		[Test()]
		public void DEPTH() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			interpreter.Interpret(new byte[] {
				0x24 // DEPTH[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0, stack.Pop());
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x24 // DEPTH[]
			});
			Assert.AreEqual(2, stack.Depth);
			Assert.AreEqual(1, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());
		}

		[Test()]
		public void CINDEX() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(0x30);
			stack.Push(0x40);
			stack.Push(2);
			interpreter.Interpret(new byte[] {
				0x25 // CINDEX[]
			});
			Assert.AreEqual(5, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
			Assert.AreEqual(0x40, stack.Pop());
			Assert.AreEqual(0x30, stack.Pop());
			Assert.AreEqual(0x20, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());
			stack.Push(0);
			try {
				interpreter.Interpret(new byte[] {
					0x25 // CINDEX[]
				});
				Assert.Fail();
			} catch (InvalidOperationException) {
			}
		}

		[Test()]
		public void MINDEX() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(0x30);
			stack.Push(0x40);
			stack.Push(2);
			interpreter.Interpret(new byte[] {
				0x26 // MINDEX[]
			});
			Assert.AreEqual(4, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
			Assert.AreEqual(0x40, stack.Pop());
			Assert.AreEqual(0x20, stack.Pop());
			Assert.AreEqual(0x10, stack.Pop());
			stack.Push(0);
			try {
				interpreter.Interpret(new byte[] {
					0x26 // MINDEX[]
				});
				Assert.Fail();
			} catch (InvalidOperationException) {
			}
		}

		[Test()]
		public void ROLL() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.singe_width_cut_in = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			stack.Push(2);
			stack.Push(3);
			interpreter.Interpret(new byte[] {
				0x8A // ROLL[]
			});
			Assert.AreEqual(3, stack.Depth);
			Assert.AreEqual(1, stack.Pop());
			Assert.AreEqual(3, stack.Pop());
			Assert.AreEqual(2, stack.Pop());
		}

		[Test()]
		public void IF() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x58, // IF[]
				0x60  // ADD[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
			stack.Push(0x30);
			stack.Push(0x20);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x58, // IF[]
				0x60, // ADD[]
				0x1B, // ELSE[]
				0x61, // SUB[]
				0x59  // EIF[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x50, stack.Pop());
			stack.Push(0x20);
			stack.Push(0x10);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x58, // IF[]
				0x60, 0x60, 0x60,  // ADD[]
				0x1B, // ELSE[]
				0x61, // SUB[]
				0x59  // EIF[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x10, stack.Pop());
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x58, // IF[]
				0x60, 0x60, 0x60,  // ADD[]
				0x59  // EIF[]
			});
			Assert.AreEqual(0, stack.Depth);
			stack.Push(0x30);
			stack.Push(0x10);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x58, // IF[]
				0x60, 0x60, 0x60,  // ADD[]
				0x58, // IF[]
				0x60, 0x60, 0x60,  // ADD[]
				0x59, // EIF[]
				0x60, 0x60, 0x60,  // ADD[]
				0x1B, // ELSE[]
				0x61, // SUB[]
				0x59  // EIF[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x20, stack.Pop());
		}

		[Test()]
		public void JROT() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(2);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x78, // JROT[]
				0x60, // ADD[]
				0x60, // ADD[]
				0x60  // ADD[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(2);
			stack.Push(0);
			try {
				interpreter.Interpret(new byte[] {
					0x78, // JROT[]
					0x60, // ADD[]
					0x60, // ADD[]
					0x60  // ADD[]
				});
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
		}

		[Test()]
		public void JMPR() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(2);
			interpreter.Interpret(new byte[] {
				0x1C, // JMPR[]
				0x60, // ADD[]
				0x60, // ADD[]
				0x60  // ADD[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
		}

		[Test()]
		public void JROF() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(2);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x79, // JROF[]
				0x60, // ADD[]
				0x60, // ADD[]
				0x60  // ADD[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(2);
			stack.Push(1);
			try {
				interpreter.Interpret(new byte[] {
					0x79, // JROF[]
					0x60, // ADD[]
					0x60, // ADD[]
					0x60  // ADD[]
				});
				Assert.Fail();
			} catch (IndexOutOfRangeException) {
			}
		}

		[Test()]
		public void LT() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.singe_width_cut_in = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x50 // LT[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0, stack.Pop());
			stack.Push(1);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x50 // LT[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x50 // LT[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x50 // LT[]
			});
			Assert.AreEqual(1, stack.Pop());
		}

		[Test()]
		public void LTEQ() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.singe_width_cut_in = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x51 // LTEQ[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(1, stack.Pop());
			stack.Push(1);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x51 // LTEQ[]
			});
			Assert.AreEqual(1, stack.Pop());
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x51 // LTEQ[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x51 // LTEQ[]
			});
			Assert.AreEqual(1, stack.Pop());
		}

		[Test()]
		public void GT() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.singe_width_cut_in = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x52 // GT[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0, stack.Pop());
			stack.Push(1);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x52 // GT[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x52 // GT[]
			});
			Assert.AreEqual(1, stack.Pop());
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x52 // GT[]
			});
			Assert.AreEqual(0, stack.Pop());
		}

		[Test()]
		public void GTEQ() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.singe_width_cut_in = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x53 // GTEQ[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(1, stack.Pop());
			stack.Push(1);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x53 // GTEQ[]
			});
			Assert.AreEqual(1, stack.Pop());
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x53 // GTEQ[]
			});
			Assert.AreEqual(1, stack.Pop());
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x53 // GTEQ[]
			});
			Assert.AreEqual(0, stack.Pop());
		}

		[Test()]
		public void EQ() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.singe_width_cut_in = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x54 // EQ[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(1, stack.Pop());
			stack.Push(1);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x54 // EQ[]
			});
			Assert.AreEqual(1, stack.Pop());
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x54 // EQ[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x54 // EQ[]
			});
			Assert.AreEqual(0, stack.Pop());
		}

		[Test()]
		public void NEQ() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.singe_width_cut_in = 0;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x55 // NEQ[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0, stack.Pop());
			stack.Push(1);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x55 // NEQ[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x55 // NEQ[]
			});
			Assert.AreEqual(1, stack.Pop());
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x55 // NEQ[]
			});
			Assert.AreEqual(1, stack.Pop());
		}

		[Test()]
		public void ODD() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = RoundState.Off;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x56 // ODD[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0, stack.Pop());
			stack.Push(0x40);
			interpreter.Interpret(new byte[] {
				0x56 // ODD[]
			});
			Assert.AreEqual(1, stack.Pop());
			stack.Push(0x80);
			interpreter.Interpret(new byte[] {
				0x56 // ODD[]
			});
			Assert.AreEqual(0, stack.Pop());
			// TODO: add another roundState test
		}

		[Test()]
		public void EVEN() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = RoundState.Off;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x57 // EVEN[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(1, stack.Pop());
			stack.Push(0x40);
			interpreter.Interpret(new byte[] {
				0x57 // EVEN[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(0x80);
			interpreter.Interpret(new byte[] {
				0x57 // EVEN[]
			});
			Assert.AreEqual(1, stack.Pop());
			// TODO: add another roundState test
		}

		[Test()]
		public void AND() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			interpreter.Interpret(new byte[] {
				0x5A // AND[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(1, stack.Pop());
			stack.Push(0);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x5A // AND[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x5A // AND[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x5A // AND[]
			});
			Assert.AreEqual(0, stack.Pop());
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void OR() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			interpreter.Interpret(new byte[] {
				0x5B // OR[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(1, stack.Pop());
			stack.Push(0);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x5B // OR[]
			});
			Assert.AreEqual(0, stack.Pop());
			stack.Push(1);
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x5B // OR[]
			});
			Assert.AreEqual(1, stack.Pop());
			stack.Push(0);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x5B // OR[]
			});
			Assert.AreEqual(1, stack.Pop());
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void NOT() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x5C // NOT[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0, stack.Pop());
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x5C // NOT[]
			});
			Assert.AreEqual(1, stack.Pop());
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x5C // NOT[]
			});
			Assert.AreEqual(0, stack.Pop());
			Assert.AreEqual(0, stack.Depth);
		}

		[Test()]
		public void ADD() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			interpreter.Interpret(new byte[] {
				0x60 // ADD[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
		}

		[Test()]
		public void SUB() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x20);
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x61 // SUB[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x10, stack.Pop());
		}

		[Test()]
		public void DIV() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			interpreter.Interpret(new byte[] {
				0x62 // DIV[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x10 * 64 / 0x20, stack.Pop());
		}

		[Test()]
		public void MUL() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x10);
			stack.Push(0x20);
			interpreter.Interpret(new byte[] {
				0x63 // MUL[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x10 * 0x20 / 64, stack.Pop());
		}

		[Test()]
		public void ABS() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(-0x10);
			interpreter.Interpret(new byte[] {
				0x64 // ABS[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x10, stack.Pop());
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x64 // ABS[]
			});
			Assert.AreEqual(0x10, stack.Pop());
		}

		[Test()]
		public void NEG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(-0x10);
			interpreter.Interpret(new byte[] {
				0x65 // NEG[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x10, stack.Pop());
			stack.Push(0x10);
			interpreter.Interpret(new byte[] {
				0x65 // NEG[]
			});
			Assert.AreEqual(-0x10, stack.Pop());
		}

		[Test()]
		public void FLOOR() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x3F);
			interpreter.Interpret(new byte[] {
				0x66 // FLOOR[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0, stack.Pop());
			stack.Push(0xFF);
			interpreter.Interpret(new byte[] {
				0x66 // FLOOR[]
			});
			Assert.AreEqual(0xC0, stack.Pop());
			// TODO: add minus
		}

		[Test()]
		public void CEILING() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x3F);
			interpreter.Interpret(new byte[] {
				0x67 // CEILING[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x40, stack.Pop());
			stack.Push(0xFF);
			interpreter.Interpret(new byte[] {
				0x67 // CEILING[]
			});
			Assert.AreEqual(0x100, stack.Pop());
			stack.Push(0x40);
			interpreter.Interpret(new byte[] {
				0x67 // CEILING[]
			});
			Assert.AreEqual(0x40, stack.Pop());
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x67 // CEILING[]
			});
			Assert.AreEqual(0x40, stack.Pop());
			stack.Push(0);
			interpreter.Interpret(new byte[] {
				0x67 // CEILING[]
			});
			Assert.AreEqual(0, stack.Pop());
		}

		[Test()]
		public void MAX() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			stack.Push(10);
			interpreter.Interpret(new byte[] {
				0x8B // MAX[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(10, stack.Pop());
		}

		[Test()]
		public void MIN() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			stack.Push(10);
			interpreter.Interpret(new byte[] {
				0x8C // MIN[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(1, stack.Pop());
		}

		[Test()]
		public void ROUND() {
			Assert.Fail();
		}

		[Test()]
		public void NROUND() {
			Assert.Fail();
		}

		[Test()]
		public void FDEF() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.maxFunctionDefs = 10;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x2C, // FDEF[]
				0x60, // ADD[]
				0x2D  // ENDF[]
			});
			Assert.AreEqual(0, stack.Depth);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(3);
			stack.Push(2);
			interpreter.Interpret(new byte[] {
				0x2C, // FDEF[]
				0x60, // ADD[]
				0x58, // IF[]
				0x60, // ADD[]
				0x59, // EIF[]
				0x2D, // ENDF[]
				0x2C, // FDEF[]
				0x2D, // ENDF[]
				0x60  // ADD[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
			Assert.AreEqual(3, interpreter.funcs.GetFuncCount());
			byte[] bytes = interpreter.funcs.CALL(1);
			Assert.AreEqual(1, bytes.Length);
			bytes = interpreter.funcs.CALL(2);
			Assert.AreEqual(4, bytes.Length);
			bytes = interpreter.funcs.CALL(3);
			Assert.AreEqual(0, bytes.Length);
			bytes = interpreter.funcs.CALL(0);
			Assert.IsNull(bytes);
		}

		[Test()]
		public void CALL() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.maxFunctionDefs = 10;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x2C, // FDEF[]
				0x60, // ADD[]
				0x2D  // ENDF[]
			});
			Assert.AreEqual(0, stack.Depth);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x2B  // CALL[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
		}

		[Test()]
		public void LOOPCALL() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.maxFunctionDefs = 10;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x2C, // FDEF[]
				0x60, // ADD[]
				0x2D  // ENDF[]
			});
			Assert.AreEqual(0, stack.Depth);
			stack.Push(0x10);
			stack.Push(0x20);
			stack.Push(0x30);
			stack.Push(0x40);
			stack.Push(3); // loop count
			stack.Push(1); // function number
			interpreter.Interpret(new byte[] {
				0x2A  // LOOPCALL[]
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0xA0, stack.Pop());
		}

		[Test()]
		public void IDEF() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(0x28);
			interpreter.Interpret(new byte[] {
				0x89, // IDEF[]
				0x60, // ADD[]
				0x2D  // ENDF[]
			});
			Assert.AreEqual(0, stack.Depth);
			stack.Push(0x10);
			stack.Push(0x20);
			interpreter.Interpret(new byte[] {
				0x28  // undefined opcode
			});
			Assert.AreEqual(1, stack.Depth);
			Assert.AreEqual(0x30, stack.Pop());
		}

		[Test()]
		public void DEBUG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			bool success = false;
			interpreter.Debug += (int n) => {
				success = true;
			};
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x4F // DEBUG[]
			});
			Assert.AreEqual(0, stack.Depth);
			Assert.IsTrue(success);
		}

		[Test()]
		public void GETINFO() {
			Assert.Fail();
		}

		[Test()]
		public void GETVARIATION() {
			Assert.Fail();
		}

		[Test()]
		public void AA() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			InterpreterStack stack = interpreter.stack;
			stack.Init(32);
			stack.Push(1);
			interpreter.Interpret(new byte[] {
				0x7F // AA[]
			});
			Assert.AreEqual(0, stack.Depth);
		}
	}
}
