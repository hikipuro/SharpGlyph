using System;
using NUnit.Framework;
using SharpGlyph;

namespace UnitTest.Instructions {
	[TestFixture]
	public class InterpreterFuncsTest {
		[Test]
		public void Constructor() {
			InterpreterFuncs funcs = new InterpreterFuncs();
		}

		[Test]
		public void GetFuncCount() {
			InterpreterFuncs funcs = new InterpreterFuncs();
			Assert.AreEqual(0, funcs.GetFuncCount());
			funcs.FDEF(0, new byte[] { 0 });
			Assert.AreEqual(1, funcs.GetFuncCount());
			funcs.FDEF(100, new byte[] { 0 });
			Assert.AreEqual(2, funcs.GetFuncCount());
			Assert.AreEqual(0, funcs.GetInstCount());
		}

		[Test]
		public void GetInstCount() {
			InterpreterFuncs funcs = new InterpreterFuncs();
			Assert.AreEqual(0, funcs.GetInstCount());
			funcs.IDEF(0, new byte[] { 0 });
			Assert.AreEqual(1, funcs.GetInstCount());
			funcs.IDEF(100, new byte[] { 0 });
			Assert.AreEqual(2, funcs.GetInstCount());
			Assert.AreEqual(0, funcs.GetFuncCount());
		}

		[Test]
		public void FDEF() {
			InterpreterFuncs funcs = new InterpreterFuncs();
			Assert.AreEqual(0, funcs.GetFuncCount());
			funcs.FDEF(0, new byte[] { 0 });
			Assert.AreEqual(1, funcs.GetFuncCount());
			funcs.FDEF(0, new byte[] { 0 });
			Assert.AreEqual(1, funcs.GetFuncCount());
		}

		[Test]
		public void CALL() {
			InterpreterFuncs funcs = new InterpreterFuncs();
			Assert.AreEqual(0, funcs.GetFuncCount());
			funcs.FDEF(0, new byte[] { 0x10, 0x20 });
			Assert.AreEqual(1, funcs.GetFuncCount());
			byte[] data = funcs.CALL(0);
			Assert.AreEqual(2, data.Length);
			Assert.AreEqual(0x10, data[0]);
			Assert.AreEqual(0x20, data[1]);
			data = funcs.CALL(1);
			Assert.IsNull(data);
		}

		[Test]
		public void IDEF() {
			InterpreterFuncs funcs = new InterpreterFuncs();
			Assert.AreEqual(0, funcs.GetInstCount());
			funcs.IDEF(0, new byte[] { 0 });
			Assert.AreEqual(1, funcs.GetInstCount());
			funcs.IDEF(0, new byte[] { 0 });
			Assert.AreEqual(1, funcs.GetInstCount());
		}

		[Test]
		public void ICALL() {
			InterpreterFuncs funcs = new InterpreterFuncs();
			Assert.AreEqual(0, funcs.GetInstCount());
			funcs.IDEF(0, new byte[] { 0x10, 0x20 });
			Assert.AreEqual(1, funcs.GetInstCount());
			byte[] data = funcs.ICALL(0);
			Assert.AreEqual(2, data.Length);
			Assert.AreEqual(0x10, data[0]);
			Assert.AreEqual(0x20, data[1]);
			data = funcs.ICALL(1);
			Assert.IsNull(data);
		}
	}
}
