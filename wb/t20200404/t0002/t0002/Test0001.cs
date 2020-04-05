using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Test0001
	{
		public void Test01()
		{
			for (int c = 1; c <= 1000; c++)
			{
				Console.WriteLine(c + "\t" + Test01_f(c));
			}
		}

		private int Test01_f(int c)
		{
			int l = 0;
			int r = c;

			while (l + 1 < r)
			{
				int m = (l + r) / 2;
				int p = GetP(m);

				if (p < c)
					l = m;
				else
					r = m;
			}
			return r;
		}

		private int GetP(int t)
		{
			int p = 0;

			for (int i = 1; i <= t; i++)
				p += t / i;

			return p;
		}
	}
}
