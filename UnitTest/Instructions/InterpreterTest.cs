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
			stack.Push(0);
			stack.Push(0x1234);
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
			stack.Push(0);
			stack.Push(0x1234);
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
			Assert.Fail();
		}

		[Test()]
		public void SFVTL() {
			Assert.Fail();
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
			Assert.Fail();
		}

		[Test()]
		public void SFVFS() {
			Assert.Fail();
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
			interpreter.state.round_state = 10;
			interpreter.Interpret(new byte[] {
				0x19 // RTHG[]
			});
			Assert.AreEqual(0, interpreter.state.round_state);
		}

		[Test()]
		public void RTG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = 10;
			interpreter.Interpret(new byte[] {
				0x18 // RTG[]
			});
			Assert.AreEqual(1, interpreter.state.round_state);
		}

		[Test()]
		public void RTDG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = 10;
			interpreter.Interpret(new byte[] {
				0x3D // RTDG[]
			});
			Assert.AreEqual(2, interpreter.state.round_state);
		}

		[Test()]
		public void RDTG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = 10;
			interpreter.Interpret(new byte[] {
				0x7D // RDTG[]
			});
			Assert.AreEqual(3, interpreter.state.round_state);
		}

		[Test()]
		public void RUTG() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = 10;
			interpreter.Interpret(new byte[] {
				0x7C // RUTG[]
			});
			Assert.AreEqual(4, interpreter.state.round_state);
		}

		[Test()]
		public void ROFF() {
			Interpreter interpreter = new Interpreter(null);
			interpreter.IsDebug = true;
			interpreter.state.round_state = 10;
			interpreter.Interpret(new byte[] {
				0x7A // ROFF[]
			});
			Assert.AreEqual(5, interpreter.state.round_state);
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
			Assert.Fail();
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
			Assert.Fail();
		}

		[Test()]
		public void SSWCI() {
			Assert.Fail();
		}

		[Test()]
		public void SSW() {
			Assert.Fail();
		}

		[Test()]
		public void FLIPON() {
			Assert.Fail();
		}

		[Test()]
		public void FLIPOFF() {
			Assert.Fail();
		}

		[Test()]
		public void SANGW() {
			Assert.Fail();
		}

		[Test()]
		public void SDB() {
			Assert.Fail();
		}

		[Test()]
		public void SDS() {
			Assert.Fail();
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
			Assert.Fail();
		}

		[Test()]
		public void FLIPRGON() {
			Assert.Fail();
		}

		[Test()]
		public void FLIPRGOFF() {
			Assert.Fail();
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
			Assert.Fail();
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
			Assert.Fail();
		}

		[Test()]
		public void POP() {
			Assert.Fail();
		}

		[Test()]
		public void CLEAR() {
			Assert.Fail();
		}

		[Test()]
		public void SWAP() {
			Assert.Fail();
		}

		[Test()]
		public void DEPTH() {
			Assert.Fail();
		}

		[Test()]
		public void CINDEX() {
			Assert.Fail();
		}

		[Test()]
		public void MINDEX() {
			Assert.Fail();
		}

		[Test()]
		public void ROLL() {
			Assert.Fail();
		}

		[Test()]
		public void IF() {
			Assert.Fail();
		}

		[Test()]
		public void ELSE() {
			Assert.Fail();
		}

		[Test()]
		public void EIF() {
			Assert.Fail();
		}

		[Test()]
		public void JROT() {
			Assert.Fail();
		}

		[Test()]
		public void JMPR() {
			Assert.Fail();
		}

		[Test()]
		public void JROF() {
			Assert.Fail();
		}

		[Test()]
		public void LT() {
			Assert.Fail();
		}

		[Test()]
		public void LTEQ() {
			Assert.Fail();
		}

		[Test()]
		public void GT() {
			Assert.Fail();
		}

		[Test()]
		public void GTEQ() {
			Assert.Fail();
		}

		[Test()]
		public void EQ() {
			Assert.Fail();
		}

		[Test()]
		public void NEQ() {
			Assert.Fail();
		}

		[Test()]
		public void ODD() {
			Assert.Fail();
		}

		[Test()]
		public void EVEN() {
			Assert.Fail();
		}

		[Test()]
		public void AND() {
			Assert.Fail();
		}

		[Test()]
		public void OR() {
			Assert.Fail();
		}

		[Test()]
		public void NOT() {
			Assert.Fail();
		}

		[Test()]
		public void ADD() {
			Assert.Fail();
		}

		[Test()]
		public void SUB() {
			Assert.Fail();
		}

		[Test()]
		public void DIV() {
			Assert.Fail();
		}

		[Test()]
		public void MUL() {
			Assert.Fail();
		}

		[Test()]
		public void ABS() {
			Assert.Fail();
		}

		[Test()]
		public void NEG() {
			Assert.Fail();
		}

		[Test()]
		public void FLOOR() {
			Assert.Fail();
		}

		[Test()]
		public void CEILING() {
			Assert.Fail();
		}

		[Test()]
		public void MAX() {
			Assert.Fail();
		}

		[Test()]
		public void MIN() {
			Assert.Fail();
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
			Assert.Fail();
		}

		[Test()]
		public void ENDF() {
			Assert.Fail();
		}

		[Test()]
		public void CALL() {
			Assert.Fail();
		}

		[Test()]
		public void LOOPCALL() {
			Assert.Fail();
		}

		[Test()]
		public void IDEF() {
			Assert.Fail();
		}

		[Test()]
		public void DEBUG() {
			Assert.Fail();
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
			Assert.Fail();
		}
	}
}
