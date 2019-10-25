using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MultiLayerPerceptron;
using Charlotte.Tools;

namespace Charlotte.Tests.MultiLayerPerceptron
{
	public class Test0004
	{
		public void Test01()
		{
			MultiLayer ml = new MultiLayer(4, 3, new int[] { 5, 5 });
			int testCnt = 0;

		restart:
			for (int c = 0; c < 1000; c++)
			{
				int a = (int)SecurityTools.CRandom.GetRandom(4);
				int b = (int)SecurityTools.CRandom.GetRandom(4);
				int v = a + b;

				ml.Train(
					new double[]
					{
						(a & 0x01) == 0 ? 0.0 : 1.0,
						(a & 0x02) == 0 ? 0.0 : 1.0,
						(b & 0x01) == 0 ? 0.0 : 1.0,
						(b & 0x02) == 0 ? 0.0 : 1.0,
					},
					new double[]
					{
						(v & 0x01) == 0 ? 0.0 : 1.0,
						(v & 0x02) == 0 ? 0.0 : 1.0,
						(v & 0x04) == 0 ? 0.0 : 1.0,
					});
			}

			int correct = 0;

			for (int a = 0; a < 4; a++)
			{
				for (int b = 0; b < 4; b++)
				{
					int v = a + b;

					ml.Clear();
					ml.SetInputs(new double[]
					{
						(a & 0x01) == 0 ? 0.0 : 1.0,
						(a & 0x02) == 0 ? 0.0 : 1.0,
						(b & 0x01) == 0 ? 0.0 : 1.0,
						(b & 0x02) == 0 ? 0.0 : 1.0,
					});

					ml.Fire();

					double[] ret = ml.GetOutputs();
					int w =
						(ret[0] < 0.5 ? 0 : 1) |
						(ret[1] < 0.5 ? 0 : 2) |
						(ret[2] < 0.5 ? 0 : 4);

					Console.WriteLine(a + " + " + b + " = " + w + " (" + v + ")");

					if (v == w)
						correct++;
				}
			}
			Console.WriteLine("[" + (++testCnt) + "] correct: " + correct);
			goto restart;
		}

		public void Test02()
		{
			MultiLayer ml = new MultiLayer(6, 4, new int[] { 7, 7 });
			int testCnt = 0;

		restart:
			for (int c = 0; c < 1000; c++)
			{
				int a = (int)SecurityTools.CRandom.GetRandom(8);
				int b = (int)SecurityTools.CRandom.GetRandom(8);
				int v = a + b;

				ml.Train(
					new double[]
					{
						(a & 0x01) == 0 ? 0.0 : 1.0,
						(a & 0x02) == 0 ? 0.0 : 1.0,
						(a & 0x04) == 0 ? 0.0 : 1.0,
						(b & 0x01) == 0 ? 0.0 : 1.0,
						(b & 0x02) == 0 ? 0.0 : 1.0,
						(b & 0x04) == 0 ? 0.0 : 1.0,
					},
					new double[]
					{
						(v & 0x01) == 0 ? 0.0 : 1.0,
						(v & 0x02) == 0 ? 0.0 : 1.0,
						(v & 0x04) == 0 ? 0.0 : 1.0,
						(v & 0x08) == 0 ? 0.0 : 1.0,
					});
			}

			int correct = 0;

			for (int a = 0; a < 8; a++)
			{
				for (int b = 0; b < 8; b++)
				{
					int v = a + b;

					ml.Clear();
					ml.SetInputs(new double[]
					{
						(a & 0x01) == 0 ? 0.0 : 1.0,
						(a & 0x02) == 0 ? 0.0 : 1.0,
						(a & 0x04) == 0 ? 0.0 : 1.0,
						(b & 0x01) == 0 ? 0.0 : 1.0,
						(b & 0x02) == 0 ? 0.0 : 1.0,
						(b & 0x04) == 0 ? 0.0 : 1.0,
					});

					ml.Fire();

					double[] ret = ml.GetOutputs();
					int w =
						(ret[0] < 0.5 ? 0 : 1) |
						(ret[1] < 0.5 ? 0 : 2) |
						(ret[2] < 0.5 ? 0 : 4) |
						(ret[3] < 0.5 ? 0 : 8);

					double dw =
						(ret[0] * 1.0) +
						(ret[1] * 2.0) +
						(ret[2] * 4.0) +
						(ret[3] * 8.0);

					Console.WriteLine(a + " + " + b + " = " + w.ToString("D2") + " (" + v.ToString("D2") + ") " + dw.ToString("F9"));

					if (v == w)
						correct++;
				}
			}
			Console.WriteLine("[" + (++testCnt) + "] correct: " + correct);
			goto restart;
		}
	}
}
