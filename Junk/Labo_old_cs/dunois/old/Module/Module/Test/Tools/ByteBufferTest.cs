using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class ByteBufferTest
	{
		public static void Test01()
		{
			for (int c = 0; c < 100; c++) Test01_b(0, 100);
			for (int c = 0; c < 10000; c++) Test01_b(0, 10000);
			for (int c = 0; c < 100; c++) Test01_b(0, 5000000);
		}

		private static void Test01_b(int minSize, int maxSize)
		{
			int size = MathTools.Random(minSize, maxSize);
			Console.WriteLine("size: " + size);
			byte[] src = DebugTools.MakeRandBytes(size);

			ByteBuffer buff = new ByteBuffer();

			foreach (byte chr in src)
				buff.Add(chr);

			if (ArrayTools.IsSame<byte>(src, buff.Join(), delegate(byte a, byte b) { return (int)a - (int)b; }) == false)
				throw null;

			Console.WriteLine("OK!");
		}
	}
}
