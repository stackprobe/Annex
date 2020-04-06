using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class DFT
	{
		public static double Perform(double[][] src, int srcStartPos, int lrIdx, int src_hz, int ret_hz, int size)
		{
			double cc = 0.0;
			double ss = 0.0;

			double mon_hz = Math.PI * 2.0 / src_hz; // これで1Hzになる。

			mon_hz *= ret_hz; // これで(ret_hz)Hzになる。

			for (int offset = 0; offset < size; offset++)
			{
				double aa = mon_hz * offset;
				double vv = src[srcStartPos + offset][lrIdx] * Hamming(offset * 1.0 / (size - 1));

				cc += Math.Cos(aa) * vv;
				ss += Math.Sin(aa) * vv;
			}
			return Math.Sqrt(cc * cc + ss * ss);
		}

		private static double Hamming(double rate)
		{
			return 0.5 - 0.5 * Math.Cos(rate * Math.PI * 2.0);
		}
	}
}
