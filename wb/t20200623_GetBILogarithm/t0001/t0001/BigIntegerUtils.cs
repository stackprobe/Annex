using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Charlotte
{
	public static class BigIntegerUtils
	{
		public static int GetByteArrayLength(BigInteger v)
		{
			if (v < 0)
				throw new ArgumentException();

			if (v == 0)
				return 0;

			return GetLogarithm(v, 256) + 1;
		}

		public static int GetLogarithm(BigInteger v, BigInteger b) // ret: Log(v, x) 以下の最大の整数, b: 底
		{
			if (v < 1)
				throw new ArgumentException();

			if (b < 2)
				throw new ArgumentException();

			int r = 1;

			{
				BigInteger t = b;

				while (t <= v)
				{
					t *= t;
					r *= 2;
				}
			}

			int l = r / 2;

			while (l + 1 < r)
			{
				int m = (l + r) / 2;
				BigInteger t = GL_PowerOf(b, m);

				if (t <= v)
					l = m;
				else
					r = m;
			}
			return l;
		}

		private static BigInteger GL_PowerOf(BigInteger v, int x)
		{
			if (x < 1)
				throw null; // bugged !!!

			if (x == 1)
				return v;

			BigInteger t = GL_PowerOf(v * v, x / 2);

			if (x % 2 == 1)
				t *= v;

			return t;
		}
	}
}
