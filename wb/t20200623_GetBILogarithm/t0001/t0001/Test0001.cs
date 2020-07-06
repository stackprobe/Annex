using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Charlotte.Tools;

namespace Charlotte
{
	public class Test0001
	{
		public void Test01()
		{
			for (int c = 0; c < 10000; c++) Test01_a(3);
			for (int c = 0; c < 10000; c++) Test01_a(10);
			for (int c = 0; c < 10000; c++) Test01_a(100);
			for (int c = 0; c < 1000; c++) Test01_a(1000);
			for (int c = 0; c < 100; c++) Test01_a(10000);
			for (int c = 0; c < 30; c++) Test01_a(100000);
		}

		public void Test01_a(int scale)
		{
			Console.WriteLine("Test01_a.scale: " + scale); // test

			BigInteger v = new BigInteger(BinTools.Join(SecurityTools.CRandom.GetBytes(SecurityTools.CRandom.GetRange(1, scale)), new byte[] { 0x00 }));
			BigInteger x = new BigInteger(BinTools.Join(SecurityTools.CRandom.GetBytes(SecurityTools.CRandom.GetRange(1, scale)), new byte[] { 0x00 }));

			if (v < 1)
				v = 1;

			if (x < 2)
				x = 2;

			int xe = BigIntegerUtils_v1.GetLogarithm(v, x);

			Console.WriteLine("xe: " + xe); // test

			BigInteger t1 = T01_PowerOf(x, xe);
			BigInteger t2 = t1 * x;

			if (t1 <= v && v < t2)
			{
				// noop
			}
			else
			{
				throw null; // bugged !!!
			}

			Console.WriteLine("ok"); // test
		}

		private BigInteger T01_PowerOf(BigInteger v, int x)
		{
			if (x < 0)
				throw null;

			if (x == 0)
				return 1;

			if (x == 1)
				return v;

			return T01_PowerOf(v * v, x / 2) * (x % 2 == 1 ? v : 1);
		}

		public void Test02()
		{
			for (int c = 0; c < 10000; c++) Test02_a(3);
			for (int c = 0; c < 10000; c++) Test02_a(10);
			for (int c = 0; c < 10000; c++) Test02_a(100);
			for (int c = 0; c < 1000; c++) Test02_a(1000);
			for (int c = 0; c < 100; c++) Test02_a(10000);
			//for (int c = 0; c < 30; c++) Test02_a(100000); // 遅い。
		}

		private static void Test02_a(int scale)
		{
			Console.WriteLine("Test02_a.scale: " + scale); // test

			BigInteger v = new BigInteger(BinTools.Join(SecurityTools.CRandom.GetBytes(SecurityTools.CRandom.GetRange(1, scale)), new byte[] { 0x00 }));

			Console.WriteLine("*1"); // test
			int t1 = BigIntegerUtils_v1.GetByteArrayLength(v);
			Console.WriteLine("*2"); // test
			int t2 = BigIntegerUtils.GetByteArrayLength(v);
			Console.WriteLine("*3"); // test

			if (t1 != t2)
				throw null; // bugged !!!

			Console.WriteLine("ok"); // test
		}
	}
}
