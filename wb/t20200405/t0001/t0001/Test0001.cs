using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Test0001
	{
		public void Test01()
		{
			double[] src = new double[5000];

			for (int i = 0; i < src.Length; i++)
			{
#if !true
				src[i] =
					Math.Sin(i / 1.0) * 1.0 +
					Math.Sin(i / 1.2) * 2.0 +
					Math.Sin(i / 1.5) * 5.0 +
					Math.Sin(i / 6.0) * 6.0 +
					Math.Sin(i / 7.0) * 7.0 +
					Math.Sin(i / 8.0) * 8.0;
#else
				src[i] =
					Math.Sin(i / 1.0) * 1.0 +
					Math.Sin(i / 2.0) * 2.0 +
					Math.Sin(i / 3.0) * 3.0 +
					Math.Sin(i / 4.0) * 4.0 +
					Math.Sin(i / 5.0) * 5.0 +
					Math.Sin(i / 6.0) * 6.0 +
					Math.Sin(i / 7.0) * 7.0 +
					Math.Sin(i / 8.0) * 8.0 +
					Math.Sin(i / 9.0) * 9.0;
#endif
			}
			double[] dest = FourierTransform(src);

			for (int i = 0; i < dest.Length; i++)
				Console.WriteLine(dest[i]);

			using (CsvFileWriter writer = new CsvFileWriter(@"C:\temp\1.csv"))
			{
				for (int i = 0; i < src.Length; i++)
					writer.WriteRow(new string[] { src[i].ToString("F9"), dest[i].ToString("F9") });
			}
		}

		private double[] FourierTransform(double[] src)
		{
			double[] dest = new double[src.Length];

			for (int i = 0; i < src.Length; i++)
			{
				double cc = 0.0;
				double ss = 0.0;

				for (int j = 0; j < src.Length; j++)
				{
					double aa = (Math.PI * 2.0 * i * j) / src.Length;

					cc += Math.Cos(aa) * src[j] * Hamming(j / (double)(src.Length - 1));
					ss += Math.Sin(aa) * src[j] * Hamming(j / (double)(src.Length - 1));
				}
				dest[i] = Math.Sqrt(cc * cc + ss * ss);
			}
			return dest;
		}

		private double Hamming(double rate)
		{
#if true
			return 0.5 - 0.5 * Math.Cos(rate * Math.PI * 2.0);
#else
			return 1.0; // ハミング無効
#endif
		}
	}
}
