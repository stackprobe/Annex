using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MultiLayerPerceptron;
using Charlotte.Tools;

namespace Charlotte.Tests.MultiLayerPerceptron
{
	public class Test0003
	{
		public void Test01()
		{
			MultiLayer ml = new MultiLayer(8, 2, new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 });

			//for (int c = 0; c < 100; c++)
			//for (int c = 0; c < 1000; c++)
			for (int c = 0; c < 10000; c++)
			{
				//foreach (string line in ml.ToStrings())
				//Console.WriteLine(line);

				Console.WriteLine("Learn " + c);


				int v = SecurityTools.CRandom.GetInt(256);
				int m = v % 3;

				ml.Train(
					new double[]
					{
						(v & 0x01) == 0 ? 0.1 : 0.9,
						(v & 0x02) == 0 ? 0.1 : 0.9,
						(v & 0x04) == 0 ? 0.1 : 0.9,
						(v & 0x08) == 0 ? 0.1 : 0.9,
						(v & 0x10) == 0 ? 0.1 : 0.9,
						(v & 0x20) == 0 ? 0.1 : 0.9,
						(v & 0x40) == 0 ? 0.1 : 0.9,
						(v & 0x80) == 0 ? 0.1 : 0.9,
					},
					new double[]
					{
						(m & 1) == 0 ? 0.1 : 0.9,
						(m & 2) == 0 ? 0.1 : 0.9,
					});
			}

			for (int c = 0; c < 10; c++)
			{
				int v = SecurityTools.CRandom.GetInt(256);
				int m = v % 3;

				ml.Clear();
				ml.SetInputs(new double[]
				{
					(v & 0x01) == 0 ? 0.1 : 0.9,
					(v & 0x02) == 0 ? 0.1 : 0.9,
					(v & 0x04) == 0 ? 0.1 : 0.9,
					(v & 0x08) == 0 ? 0.1 : 0.9,
					(v & 0x10) == 0 ? 0.1 : 0.9,
					(v & 0x20) == 0 ? 0.1 : 0.9,
					(v & 0x40) == 0 ? 0.1 : 0.9,
					(v & 0x80) == 0 ? 0.1 : 0.9,
				});

				ml.Fire();

				double[] ret = ml.GetOutputs();

				Console.WriteLine(m + ", " + ret[0] + ", " + ret[1]);
			}
		}

		public void Test02()
		{
			//MultiLayer ml = new MultiLayer(3, 1, new int[] { 3, 3, 3 });
			//MultiLayer ml = new MultiLayer(3, 1, new int[] { 8, 8, 8, 8, 8, 8, 8, 8 });
			//MultiLayer ml = new MultiLayer(3, 1, new int[] { 2, 2 });
			//MultiLayer ml = new MultiLayer(3, 1, new int[] { 4, 4 });
			MultiLayer ml = new MultiLayer(3, 1, new int[] { 4, 4, 4, 4 });

		restart:
			//for (int c = 0; c < 1000000; c++)
			//for (int c = 0; c < 100000; c++)
			for (int c = 0; c < 10000; c++)
			//for (int c = 0; c < 1000; c++)
			//for (int c = 0; c < 100; c++)
			{
				//foreach (string line in ml.ToStrings())
				//Console.WriteLine(line);

				//Console.WriteLine("Learn " + c);


				int v = SecurityTools.CRandom.GetInt(8);
				int m = v % 3;

				try
				{
					ml.Train(
						new double[]
						{
							(v & 0x01) == 0 ? 0.001 : 1.0,
							(v & 0x02) == 0 ? 0.001 : 1.0,
							(v & 0x04) == 0 ? 0.001 : 1.0,
						},
						new double[]
						{
							m != 0 ? 0.001 : 1.0,
						});
				}
				catch (Exception e)
				{
					foreach (string line in ml.ToStrings())
						Console.WriteLine(line);

					throw new Exception("Relay", e);
				}
			}

#if true
			double[] rets = new double[8];
			double ret_min = double.MaxValue;
			double ret_max = double.MinValue;

			for (int c = 0; c < 8; c++)
			{
				int v = c;
				int m = v % 3;

				ml.Clear();
				ml.SetInputs(new double[]
				{
					(v & 0x01) == 0 ? 0.001 : 1.0,
					(v & 0x02) == 0 ? 0.001 : 1.0,
					(v & 0x04) == 0 ? 0.001 : 1.0,
				});

				ml.Fire();

				double ret = ml.GetOutputs()[0];

				rets[c] = ret;
				ret_min = Math.Min(ret_min, ret);
				ret_max = Math.Max(ret_max, ret);
			}
			for (int c = 0; c < 8; c++)
			{
				int v = c;
				int m = v % 3;

				double ret = rets[c];

				ret -= ret_min;
				//ret *= 1.0;
				ret /= ret_max - ret_min;

				Console.WriteLine(v + ", " + m + ", " + ret.ToString("F3") + ", " + rets[c].ToString("F9"));
			}
#else // old
			for (int c = 0; c < 8; c++)
			{
				int v = c;//SecurityTools.CRandom.GetInt(8);
				int m = v % 3;

				ml.Clear();
				ml.SetInputs(new double[]
				{
					(v & 0x01) == 0 ? 0.0 : 1.0,
					(v & 0x02) == 0 ? 0.0 : 1.0,
					(v & 0x04) == 0 ? 0.0 : 1.0,
				});

				ml.Fire();

				double[] ret = ml.GetOutputs();

				Console.WriteLine(v + ", " + m + ", " + ret[0]);
			}
#endif
			Console.WriteLine("");
			goto restart;
		}
	}
}
