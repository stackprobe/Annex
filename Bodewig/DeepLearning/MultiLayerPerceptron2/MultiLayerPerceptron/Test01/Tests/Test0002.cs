using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MultiLayerPerceptron;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Tests
{
	public class Test0002
	{
		private StringBuilder OutBuff;

		public void Test01_TestForHatena()
		{
			OutBuff = new StringBuilder();

			Test01_a("テスト0001 - 全加算器", 10, 10, 1000, new int[] { 2, 3, 2 }, tml =>
			{
				int a = (int)SecurityTools.CRandom.GetRandom(2);
				int b = (int)SecurityTools.CRandom.GetRandom(2);

				tml.Train(
					new double[]
					{
						a,
						b,
					},
					new double[]
					{
						((a + b) & 1) == 0 ? 0.0 : 1.0,
						((a + b) & 2) == 0 ? 0.0 : 1.0,
					},
					0.1
					);
			},
			(tml, r) =>
			{
				for (int a = 0; a < 2; a++)
				{
					for (int b = 0; b < 2; b++)
					{
						r(FireDoTest(
							tml,
							new double[]
							{
								a,
								b,
							},
							new int[]
							{
								((a + b) & 1) == 0 ? 0 : 1,
								((a + b) & 2) == 0 ? 0 : 1,
							}
							));
					}
				}
			});

			Test01_a("テスト0002 - 3ビット加算器", 10, 10, 10000, new int[] { 6, 7, 7, 7, 4 }, tml =>
			{
				int a = (int)SecurityTools.CRandom.GetRandom(8);
				int b = (int)SecurityTools.CRandom.GetRandom(8);

				tml.Train(
					new double[]
					{
						(a & 1) == 0 ? 0.0 : 1.0,
						(a & 2) == 0 ? 0.0 : 1.0,
						(a & 4) == 0 ? 0.0 : 1.0,
						(b & 1) == 0 ? 0.0 : 1.0,
						(b & 2) == 0 ? 0.0 : 1.0,
						(b & 4) == 0 ? 0.0 : 1.0,
					},
					new double[]
					{
						((a + b) & 1) == 0 ? 0.0 : 1.0,
						((a + b) & 2) == 0 ? 0.0 : 1.0,
						((a + b) & 4) == 0 ? 0.0 : 1.0,
						((a + b) & 8) == 0 ? 0.0 : 1.0,
					},
					0.1
					);
			},
			(tml, r) =>
			{
				for (int a = 0; a < 8; a++)
				{
					for (int b = 0; b < 8; b++)
					{
						r(FireDoTest(
							tml,
							new double[]
							{
								(a & 1) == 0 ? 0.0 : 1.0,
								(a & 2) == 0 ? 0.0 : 1.0,
								(a & 4) == 0 ? 0.0 : 1.0,
								(b & 1) == 0 ? 0.0 : 1.0,
								(b & 2) == 0 ? 0.0 : 1.0,
								(b & 4) == 0 ? 0.0 : 1.0,
							},
							new int[]
							{
								((a + b) & 1) == 0 ? 0 : 1,
								((a + b) & 2) == 0 ? 0 : 1,
								((a + b) & 4) == 0 ? 0 : 1,
								((a + b) & 8) == 0 ? 0 : 1,
							}
							));
					}
				}
			});

			Test01_a("テスト0003 - 5ビット加算器", 10, 10, 20000, new int[] { 10, 20, 20, 20, 6 }, tml =>
			{
				int a = (int)SecurityTools.CRandom.GetRandom(32);
				int b = (int)SecurityTools.CRandom.GetRandom(32);

				tml.Train(
					new double[]
					{
						(a &  1) == 0 ? 0.0 : 1.0,
						(a &  2) == 0 ? 0.0 : 1.0,
						(a &  4) == 0 ? 0.0 : 1.0,
						(a &  8) == 0 ? 0.0 : 1.0,
						(a & 16) == 0 ? 0.0 : 1.0,
						(b &  1) == 0 ? 0.0 : 1.0,
						(b &  2) == 0 ? 0.0 : 1.0,
						(b &  4) == 0 ? 0.0 : 1.0,
						(b &  8) == 0 ? 0.0 : 1.0,
						(b & 16) == 0 ? 0.0 : 1.0,
					},
					new double[]
					{
						((a + b) &  1) == 0 ? 0.0 : 1.0,
						((a + b) &  2) == 0 ? 0.0 : 1.0,
						((a + b) &  4) == 0 ? 0.0 : 1.0,
						((a + b) &  8) == 0 ? 0.0 : 1.0,
						((a + b) & 16) == 0 ? 0.0 : 1.0,
						((a + b) & 32) == 0 ? 0.0 : 1.0,
					},
					0.1
					);
			},
			(tml, r) =>
			{
				for (int a = 0; a < 32; a++)
				{
					for (int b = 0; b < 32; b++)
					{
						r(FireDoTest(
							tml,
							new double[]
							{
								(a &  1) == 0 ? 0.0 : 1.0,
								(a &  2) == 0 ? 0.0 : 1.0,
								(a &  4) == 0 ? 0.0 : 1.0,
								(a &  8) == 0 ? 0.0 : 1.0,
								(a & 16) == 0 ? 0.0 : 1.0,
								(b &  1) == 0 ? 0.0 : 1.0,
								(b &  2) == 0 ? 0.0 : 1.0,
								(b &  4) == 0 ? 0.0 : 1.0,
								(b &  8) == 0 ? 0.0 : 1.0,
								(b & 16) == 0 ? 0.0 : 1.0,
							},
							new int[]
							{
								((a + b) &  1) == 0 ? 0 : 1,
								((a + b) &  2) == 0 ? 0 : 1,
								((a + b) &  4) == 0 ? 0 : 1,
								((a + b) &  8) == 0 ? 0 : 1,
								((a + b) & 16) == 0 ? 0 : 1,
								((a + b) & 32) == 0 ? 0 : 1,
							}
							));
					}
				}
			});

			Test01_a("テスト0004 - FizzBuzz", 10, 10, 100000, new int[] { 10, 30, 30, 30, 4 }, tml =>
			{
				int a = (int)SecurityTools.CRandom.GetRange(101, 1023);
				int x = 1;
				if (a % 3 == 0) x <<= 1;
				if (a % 5 == 0) x <<= 2;

				tml.Train(
					new double[]
					{
						(a &   1) == 0 ? 0.0 : 1.0,
						(a &   2) == 0 ? 0.0 : 1.0,
						(a &   4) == 0 ? 0.0 : 1.0,
						(a &   8) == 0 ? 0.0 : 1.0,
						(a &  16) == 0 ? 0.0 : 1.0,
						(a &  32) == 0 ? 0.0 : 1.0,
						(a &  64) == 0 ? 0.0 : 1.0,
						(a & 128) == 0 ? 0.0 : 1.0,
						(a & 256) == 0 ? 0.0 : 1.0,
						(a & 512) == 0 ? 0.0 : 1.0,
					},
					new double[]
					{
						(x & 1) == 0 ? 0.0 : 1.0,
						(x & 2) == 0 ? 0.0 : 1.0,
						(x & 4) == 0 ? 0.0 : 1.0,
						(x & 8) == 0 ? 0.0 : 1.0,
					},
					0.1
					);
			},
			(tml, r) =>
			{
				for (int a = 1; a <= 100; a++)
				{
					int x = 1;
					if (a % 3 == 0) x <<= 1;
					if (a % 5 == 0) x <<= 2;

					r(FireDoTest(
						tml,
						new double[]
						{
							(a &   1) == 0 ? 0.0 : 1.0,
							(a &   2) == 0 ? 0.0 : 1.0,
							(a &   4) == 0 ? 0.0 : 1.0,
							(a &   8) == 0 ? 0.0 : 1.0,
							(a &  16) == 0 ? 0.0 : 1.0,
							(a &  32) == 0 ? 0.0 : 1.0,
							(a &  64) == 0 ? 0.0 : 1.0,
							(a & 128) == 0 ? 0.0 : 1.0,
							(a & 256) == 0 ? 0.0 : 1.0,
							(a & 512) == 0 ? 0.0 : 1.0,
						},
						new int[]
						{
							(x & 1) == 0 ? 0 : 1,
							(x & 2) == 0 ? 0 : 1,
							(x & 4) == 0 ? 0 : 1,
							(x & 8) == 0 ? 0 : 1,
						}
						));
				}
			});

			{
				string dir = ExtraTools.MakeFreeDir();
				string file = Path.Combine(dir, "MLPTestOutput.txt");

				File.WriteAllLines(file, FileTools.TextToLines(OutBuff.ToString()), Encoding.UTF8);

				ProcessTools.Batch(new string[] { "START " + dir });
			}
		}

		private bool FireDoTest(TrainableML tml, double[] inputs, int[] expectedOutputs)
		{
			double[] ret = new double[expectedOutputs.Length];

			tml.Fire(inputs, ret);

			for (int index = 0; index < expectedOutputs.Length; index++)
			{
				if (ret[index] < 0.5)
				{
					if (expectedOutputs[index] != 0)
						return false;
				}
				else
				{
					if (expectedOutputs[index] != 1)
						return false;
				}
			}
			return true;
		}

		private void Test01_a(string title, int testCount, int loopCount, int trainingCount, int[] neuronCounts, Action<TrainableML> train, Action<TrainableML, Action<bool>> testRtn)
		{
			OutBuff.Append("*** " + title + " - 正答率\n");
			OutBuff.Append("|*テストNo. ＼ 学習回数|");

			for (int c = 0; c <= loopCount; c++)
			{
				OutBuff.Append("*" + (c * trainingCount) + "|");
			}
			OutBuff.Append("\n");

			for (int t = 0; t < testCount; t++)
			{
				TrainableML tml = new TrainableML(neuronCounts);

				OutBuff.Append("|*" + t + "|");

				for (int c = 0; c <= loopCount; c++)
				{
					int numer = 0;
					int denom = 0;

					testRtn(tml, corrected =>
					{
						if (corrected)
							numer++;

						denom++;
					});

					OutBuff.Append((numer * 1.0 / denom).ToString("F3") + "|");

					for (int n = 0; n < trainingCount; n++)
					{
						train(tml);
					}

					Console.Write("*");
				}
				OutBuff.Append("\n");
			}
			OutBuff.Append(" \n");
			OutBuff.Append(" \n");

			Console.WriteLine("");
		}
	}
}
