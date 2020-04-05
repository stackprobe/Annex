using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class DFT
	{
		public static double[] Perform(Func<int, double> getValue, int size)
		{
			if (size < 1)
				throw null;

			double[] dest = new double[size];

			for (int i = 0; i < size; i++)
			{
				double cc = 0.0;
				double ss = 0.0;

				for (int j = 0; j < size; j++)
				{
					double aa = (Math.PI * 2.0 * i * j) / size;
					double vv = getValue(j) * Hamming(j * 1.0 / (size - 1));

					cc += Math.Cos(aa) * vv;
					ss += Math.Sin(aa) * vv;
				}
				dest[i] = Math.Sqrt(cc * cc + ss * ss);
			}
			return dest;
		}

		private static double Hamming(double rate)
		{
			return 0.5 - 0.5 * Math.Cos(rate * Math.PI * 2.0);
		}
	}
}
