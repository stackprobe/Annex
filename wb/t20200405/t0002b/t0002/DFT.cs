using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class DFT
	{
		private double[] WSrc;
		private int SrcHz;

		public DFT(double[][] src, int srcStartPos, int lrIdx, int src_hz, int size)
		{
			this.WSrc = new double[size];

			for (int offset = 0; offset < size; offset++)
			{
				int srcIdx = Math.Min(srcStartPos + offset, src.Length - 1);
				double srcVal = src[srcIdx][lrIdx];
				double hamVal = Hamming(offset * 1.0 / (size - 1));

				this.WSrc[offset] = srcVal * hamVal;
			}
			this.SrcHz = src_hz;
		}

		private static double Hamming(double rate)
		{
			return 0.5 - 0.5 * Math.Cos(rate * Math.PI * 2.0);
		}

		public double Perform(int ret_hz)
		{
			double cc = 0.0;
			double ss = 0.0;

			double mon_hz = Math.PI * 2.0 / this.SrcHz; // これで1Hzになる。
			mon_hz *= ret_hz; // これで(ret_hz)Hzになる。

			for (int offset = 0; offset < this.WSrc.Length; offset++)
			{
				double aa = mon_hz * offset;
				double vv = this.WSrc[offset];

				cc += Math.Cos(aa) * vv;
				ss += Math.Sin(aa) * vv;
			}
			return Math.Sqrt(cc * cc + ss * ss);
		}
	}
}
