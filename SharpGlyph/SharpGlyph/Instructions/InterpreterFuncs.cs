﻿using System;
using System.Collections.Generic;

namespace SharpGlyph {
	public class InterpreterFuncs {
		protected Dictionary<int, byte[]> funcs;
		protected Dictionary<byte, byte[]> insts;

		public InterpreterFuncs() {
			funcs = new Dictionary<int, byte[]>();
			insts = new Dictionary<byte, byte[]>();
		}

		public int GetFuncCount() {
			return funcs.Count;
		}

		public int GetInstCount() {
			return insts.Count;
		}

		public void FDEF(int index, byte[] data) {
			if (funcs.ContainsKey(index)) {
				funcs[index] = data;
				return;
			}
			funcs.Add(index, data);
		}

		public byte[] CALL(int index) {
			if (funcs.ContainsKey(index)) {
				#if DEBUG
				Console.WriteLine("##### Call: " + index);
				Console.WriteLine("\tDecode func:\n\t{0}", Interpreter.Decode(funcs[index]).Replace("\n", "\n\t"));
				#endif
				return funcs[index];
			}
			//throw new InvalidOperationException(
			//	"Call invalid function: " + index
			//);
			return null;
		}

		public void IDEF(byte index, byte[] data) {
			if (insts.ContainsKey(index)) {
				insts[index] = data;
				return;
			}
			insts.Add(index, data);
		}

		public byte[] ICALL(byte index) {
			if (insts.ContainsKey(index)) {
				return insts[index];
			}
			return null;
		}
	}
}
