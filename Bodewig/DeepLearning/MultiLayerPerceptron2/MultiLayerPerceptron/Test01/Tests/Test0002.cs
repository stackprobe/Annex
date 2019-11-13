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
		public void Test01()
		{
			InitPrimeFlags();

			TrainableML tml = new TrainableML(new int[] { 10, 11, 11, 11, 1 }); // ok
			//TrainableML tml = new TrainableML(new int[] { 10, 11, 11, 1 }); // ng ???
			//TrainableML tml = new TrainableML(new int[] { 10, 11, 1 }); // ng

			for (int testCnt = 0; ; testCnt++)
			{
				// 評価
				{
					Console.WriteLine("testCnt: " + testCnt);

					int correct = 0;
					int correct2 = 0;
					int correct3 = 0;

					for (int value = 0; value < 1024; value++)
					{
						double[] ret = new double[1];

						tml.Fire(
							new double[]
							{
								((value >> 0) & 1) * 1.0,
								((value >> 1) & 1) * 1.0,
								((value >> 2) & 1) * 1.0,
								((value >> 3) & 1) * 1.0,
								((value >> 4) & 1) * 1.0,
								((value >> 5) & 1) * 1.0,
								((value >> 6) & 1) * 1.0,
								((value >> 7) & 1) * 1.0,
								((value >> 8) & 1) * 1.0,
								((value >> 9) & 1) * 1.0,
							},
							ret
							);

						//Console.WriteLine(value + " = " + (IsPrime(value) ? 1 : 0) + " --> " + (ret[0] + 0.5).ToString("F9"));

						if (IsPrime(value))
						{
							if (0.6 < ret[0]) correct++;
							if (0.9 < ret[0]) correct2++;
							if (0.99 < ret[0]) correct3++;
						}
						else
						{
							if (ret[0] < 0.4) correct++;
							if (ret[0] < 0.1) correct2++;
							if (ret[0] < 0.01) correct3++;
						}
					}
					Console.WriteLine(correct + " " + correct2 + " " + correct3);
				}

				//Console.WriteLine("Press ENTER to continue.");
				//Console.ReadLine();

				for (int c = 0; c < 1000000; c++) // 学習
				{
					int value = (int)SecurityTools.CRandom.GetRandom(1024);

					tml.Train(
						new double[]
						{
							((value >> 0) & 1) * 1.0,
							((value >> 1) & 1) * 1.0,
							((value >> 2) & 1) * 1.0,
							((value >> 3) & 1) * 1.0,
							((value >> 4) & 1) * 1.0,
							((value >> 5) & 1) * 1.0,
							((value >> 6) & 1) * 1.0,
							((value >> 7) & 1) * 1.0,
							((value >> 8) & 1) * 1.0,
							((value >> 9) & 1) * 1.0,
						},
						new double[]
						{
							IsPrime(value) ? 1.0 : 0.0,
						},
						0.1
						);
				}
			}
		}

		public void Test01_B()
		{
			InitPrimeFlags();

			TrainableML tml = new TrainableML(new int[] { 10, 11, 11, 11, 1 });
			//TrainableML tml = new TrainableML(new int[] { 10, 10, 10, 10, 1 }); // ng ???

			int c3count = 0;

			for (int testCnt = 0; ; testCnt++)
			{
				// 評価
				{
					Console.WriteLine("testCnt: " + testCnt);

					int correct = 0;
					int correct2 = 0;
					int correct3 = 0;

					for (int value = 0; value < 1024; value++)
					{
						double[] ret = new double[1];

						tml.Fire(
							new double[]
							{
								((value >> 0) & 1) * 1.0,
								((value >> 1) & 1) * 1.0,
								((value >> 2) & 1) * 1.0,
								((value >> 3) & 1) * 1.0,
								((value >> 4) & 1) * 1.0,
								((value >> 5) & 1) * 1.0,
								((value >> 6) & 1) * 1.0,
								((value >> 7) & 1) * 1.0,
								((value >> 8) & 1) * 1.0,
								((value >> 9) & 1) * 1.0,
							},
							ret
							);

						//Console.WriteLine(value + " = " + (IsPrime(value) ? 1 : 0) + " --> " + (ret[0] + 0.5).ToString("F9"));

						if (IsPrime(value))
						{
							if (0.6 < ret[0]) correct++;
							if (0.9 < ret[0]) correct2++;
							if (0.99 < ret[0]) correct3++;
						}
						else
						{
							if (ret[0] < 0.4) correct++;
							if (ret[0] < 0.1) correct2++;
							if (ret[0] < 0.01) correct3++;
						}
					}
					Console.WriteLine(correct + " " + correct2 + " " + correct3);

					//if (900 < correct) break; // test

					if (correct3 == 1024)
					{
						c3count++;

						if (5 <= c3count)
							break;
					}
					else
						c3count = 0;
				}

				//Console.WriteLine("Press ENTER to continue.");
				//Console.ReadLine();

				for (int c = 0; c < 1000000; c++) // 学習
				{
					int value = (int)SecurityTools.CRandom.GetRandom(1024);

					tml.Train(
						new double[]
						{
							((value >> 0) & 1) * 1.0,
							((value >> 1) & 1) * 1.0,
							((value >> 2) & 1) * 1.0,
							((value >> 3) & 1) * 1.0,
							((value >> 4) & 1) * 1.0,
							((value >> 5) & 1) * 1.0,
							((value >> 6) & 1) * 1.0,
							((value >> 7) & 1) * 1.0,
							((value >> 8) & 1) * 1.0,
							((value >> 9) & 1) * 1.0,
						},
						new double[]
						{
							IsPrime(value) ? 1.0 : 0.0,
						},
						0.1
						);
				}
			}

			File.WriteAllLines(@"C:\temp\IsPrime.cs.txt", ToCode(tml));
		}

		public void Test02() // ng -- 学習出来てる様子が無い。@ 2019.11.11
		{
			InitPrimeFlags();

			//TrainableML tml = new TrainableML(new int[] { 1, 11, 11, 11, 1 }); // ng
			TrainableML tml = new TrainableML(new int[] { 1, 30, 30, 30, 1 }); // ng

			for (int testCnt = 0; ; testCnt++)
			{
				// 評価
				{
					Console.WriteLine("testCnt: " + testCnt);

					int correct = 0;

					for (int value = 0; value < 1024; value++)
					{
						double[] ret = new double[1];

						tml.Fire(
							new double[]
							{
								value / 1023,
							},
							ret
							);

						Console.WriteLine(value + " = " + (IsPrime(value) ? 1 : 0) + " --> " + (ret[0] + 0.5).ToString("F9"));

						{
							bool a = IsPrime(value);
							bool b = 0.5 < ret[0];

							if (a == b)
								correct++;
						}
					}
					Console.WriteLine(correct);
				}

				//Console.WriteLine("Press ENTER to continue.");
				//Console.ReadLine();

				for (int c = 0; c < 1000000; c++) // 学習
				{
					int value = (int)SecurityTools.CRandom.GetRandom(1024);

					tml.Train(
						new double[]
						{
							value / 1023,
						},
						new double[]
						{
							IsPrime(value) ? 1.0 : 0.0,
						},
						0.1
						);
				}
			}
		}

		private bool[] PrimeFlags;

		private void InitPrimeFlags()
		{
			PrimeFlags = new bool[1024];

			for (int value = 2; value < 1024; value++)
				if (IPF_IsPrime(value))
					PrimeFlags[value] = true;
		}

		private bool IPF_IsPrime(int value)
		{
			for (int c = 2; c < value; c++)
				if (value % c == 0)
					return false;

			return true;
		}

		private bool IsPrime(int value)
		{
			return PrimeFlags[value];
		}

		private string[] ToCode(TrainableML tml)
		{
			List<string> lines = new List<string>();

			lines.Add("double[] Code_01(double[] n0) // gen @ " + DateTime.Now);
			lines.Add("{");
			lines.Add("Func<double, double> activator = v => 1.0 / (1.0 + Math.Pow(Math.E, v * -1.0));");
			lines.Add("const double nBias = 1.0;");
			lines.Add("");

			MultiLayer ml = tml.GetInnerML();

			for (int index = 1; index < ml.Layers.Length; index++)
			{
				Layer cl = ml.Layers[index - 1];
				Layer nl = ml.Layers[index];

				lines.Add(string.Format("double[] n{0} = new double[{1}];", index, nl.Neurons.Length));
				lines.Add("");

				for (int n = 0; n < nl.Neurons.Length; n++)
				{
					for (int c = 0; c < cl.Neurons.Length; c++)
					{
						lines.Add(string.Format("n{0}[{1}] += n{2}[{3}] * {4};", index, n, index - 1, c, TC_WeightToCode(cl.Axons[c, n].Weight)));
					}
					lines.Add(string.Format("n{0}[{1}] += nBias * {2};", index, n, TC_WeightToCode(cl.Axons[cl.Neurons.Length, n].Weight)));
				}
				if (index + 1 < ml.Layers.Length)
				{
					for (int n = 0; n < nl.Neurons.Length; n++)
					{
						lines.Add(string.Format("n{0}[{1}] = activator(n{0}[{1}]);", index, n));
					}
				}
				lines.Add("");
			}
			lines.Add(string.Format("return n{0};", ml.Layers.Length - 1));
			lines.Add("}");

			return lines.ToArray();
		}

		private static string TC_WeightToCode(double weight)
		{
			string ret = weight.ToString("F30");

			if (ret[0] != '-')
				ret = "+" + ret;

			return ret;
		}
	}
}
