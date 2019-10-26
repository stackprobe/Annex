using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MultiLayerPerceptron;
using Charlotte.Tools;

namespace Charlotte.Tests
{
	public class Test0001
	{
		private const double LEARNING_RATE = 0.1;

		/// <summary>
		/// 全加算器
		/// </summary>
		public void Test01()
		{
			TrainableML tml = new TrainableML(new int[] { 2, 3, 2 });

			for (int testCnt = 0; ; testCnt++)
			{
				// 評価
				{
					Console.WriteLine("testCnt: " + testCnt);

					for (int a = 0; a < 2; a++)
					{
						for (int b = 0; b < 2; b++)
						{
							double[] ret = new double[2];

							tml.Fire(
								new double[]
								{
									a,
									b,
								},
								ret
								);

							Console.WriteLine(a + " + " + b + " = " + ret[0].ToString("F9") + " " + ret[1].ToString("F9"));
						}
					}
				}

				Console.WriteLine("Press ENTER to continue.");
				Console.ReadLine();

				for (int c = 0; c < 1000; c++) // 学習
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
							(a + b) / 2,
							(a + b) % 2,
						},
						LEARNING_RATE
						);
				}
			}
		}

		/// <summary>
		/// 3ビット加算器
		/// 3 bit + 3 bit ==> 4 bit
		/// </summary>
		public void Test02()
		{
			TrainableML tml = new TrainableML(new int[] { 6, 7, 7, 7, 4 });

			for (int testCnt = 0; ; testCnt++)
			{
				// 評価
				{
					Console.WriteLine("testCnt: " + testCnt);

					int correctCnt = 0;

					for (int a = 0; a < 8; a++)
					{
						for (int b = 0; b < 8; b++)
						{
							double[] ret = new double[4];

							tml.Fire(
								new double[]
								{
									(a & 1) == 0 ? 0.0 : 1.0,
									(a & 2) == 0 ? 0.0 : 1.0,
									(a & 4) == 0 ? 0.0 : 1.0,
									(b & 1) == 0 ? 0.0 : 1.0,
									(b & 2) == 0 ? 0.0 : 1.0,
									(b & 4) == 0 ? 0.0 : 1.0,
								},
								ret
								);

							int v =
								(ret[0] < 0.5 ? 0 : 1) +
								(ret[1] < 0.5 ? 0 : 2) +
								(ret[2] < 0.5 ? 0 : 4) +
								(ret[3] < 0.5 ? 0 : 8);

							double w =
								ret[0] * 1.0 +
								ret[1] * 2.0 +
								ret[2] * 4.0 +
								ret[3] * 8.0;

							bool correct = a + b == v;

							if (correct)
								correctCnt++;

							Console.WriteLine(a + " + " + b + " = " + v.ToString("D2") + " ==> " + (correct ? "正解" : "違う") + " " +
								(ret[0] + 0.5).ToString("F3") + " " +
								(ret[1] + 0.5).ToString("F3") + " " +
								(ret[2] + 0.5).ToString("F3") + " " +
								(ret[3] + 0.5).ToString("F3") + " " +
								w.ToString("F9"));
						}
					}
					Console.WriteLine("correctCnt: " + correctCnt);
				}

				Console.WriteLine("Press ENTER to continue.");
				Console.ReadLine();

				for (int c = 0; c < 1000; c++) // 学習
				{
					int a = (int)SecurityTools.CRandom.GetRandom(8);
					int b = (int)SecurityTools.CRandom.GetRandom(8);
					int v = a + b;

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
							(v & 1) == 0 ? 0.0 : 1.0,
							(v & 2) == 0 ? 0.0 : 1.0,
							(v & 4) == 0 ? 0.0 : 1.0,
							(v & 8) == 0 ? 0.0 : 1.0,
						},
						LEARNING_RATE
						);
				}
			}
		}

		/// <summary>
		/// 5ビット加算器
		/// 5 bit + 5 bit ==> 6 bit
		/// </summary>
		public void Test03()
		{
			//TrainableML tml = new TrainableML(new int[] { 10, 11, 11, 11, 11, 11, 6 }); // ng
			//TrainableML tml = new TrainableML(new int[] { 10, 11, 11, 11, 11, 6 }); // good
			//TrainableML tml = new TrainableML(new int[] { 10, 11, 11, 11, 6 }); // not bad
			//TrainableML tml = new TrainableML(new int[] { 10, 11, 11, 6 }); // ng
			//TrainableML tml = new TrainableML(new int[] { 10, 11, 6 }); // ng

			//TrainableML tml = new TrainableML(new int[] { 10, 16, 16, 16, 6 }); // good
			//TrainableML tml = new TrainableML(new int[] { 10, 16, 16, 6 }); // ng

			TrainableML tml = new TrainableML(new int[] { 10, 20, 20, 20, 6 }); // good

			for (int testCnt = 0; ; testCnt++)
			{
				// 評価
				{
					Console.WriteLine("testCnt: " + testCnt);

					int correctCnt = 0;

					for (int a = 0; a < 32; a++)
					{
						for (int b = 0; b < 32; b++)
						{
							double[] ret = new double[6];

							tml.Fire(
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
								ret
								);

							int v =
								(ret[0] < 0.5 ? 0 : 1) +
								(ret[1] < 0.5 ? 0 : 2) +
								(ret[2] < 0.5 ? 0 : 4) +
								(ret[3] < 0.5 ? 0 : 8) +
								(ret[4] < 0.5 ? 0 : 16) +
								(ret[5] < 0.5 ? 0 : 32);

							double w =
								ret[0] * 1.0 +
								ret[1] * 2.0 +
								ret[2] * 4.0 +
								ret[3] * 8.0 +
								ret[4] * 16.0 +
								ret[5] * 32.0;

							bool correct = a + b == v;

							if (correct)
								correctCnt++;

							/*
							Console.WriteLine(a.ToString("D2") + " + " + b.ToString("D2") + " = " + v.ToString("D2") + " ==> " + (correct ? "正解" : "違う") + " " +
								(ret[0] + 0.5).ToString("F3") + " " +
								(ret[1] + 0.5).ToString("F3") + " " +
								(ret[2] + 0.5).ToString("F3") + " " +
								(ret[3] + 0.5).ToString("F3") + " " +
								(ret[4] + 0.5).ToString("F3") + " " +
								(ret[5] + 0.5).ToString("F3") + " " +
								w.ToString("F9")); */
						}
					}
					Console.WriteLine("correctCnt: " + correctCnt);
				}

				Console.WriteLine("Press ENTER to continue.");
				Console.ReadLine();

				for (int c = 0; c < 1000; c++) // 学習
				{
					int a = (int)SecurityTools.CRandom.GetRandom(32);
					int b = (int)SecurityTools.CRandom.GetRandom(32);
					int v = a + b;

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
							(v &  1) == 0 ? 0.0 : 1.0,
							(v &  2) == 0 ? 0.0 : 1.0,
							(v &  4) == 0 ? 0.0 : 1.0,
							(v &  8) == 0 ? 0.0 : 1.0,
							(v & 16) == 0 ? 0.0 : 1.0,
							(v & 32) == 0 ? 0.0 : 1.0,
						},
						LEARNING_RATE
						);
				}
			}
		}

		/// <summary>
		/// FizzBuzz
		/// 数値, 'Fizz', 'Buzz', 'FizzBuzz' の4通りを答えさせる。
		/// 数値そのものは出力させない。3の倍数と5の倍数と15の倍数を同時に判定するだけとも言える。
		/// ★学習データは 101 ～ 1023 を与え、評価は 1 ～ 100 で行う。
		/// </summary>
		public void Test04()
		{
			//TrainableML tml = new TrainableML(new int[] { 10, 100, 4 }); // not bad
			//TrainableML tml = new TrainableML(new int[] { 10, 30, 30, 30, 4 }); // good
			//TrainableML tml = new TrainableML(new int[] { 10, 20, 20, 20, 4 }); // good
			TrainableML tml = new TrainableML(new int[] { 10, 10, 10, 10, 4 }); // not bad

			for (int testCnt = 0; ; testCnt++)
			{
				// 評価
				{
					Console.WriteLine("testCnt: " + testCnt);

					int correctCnt = 0;

					for (int a = 1; a <= 100; a++)
					{
						double[] ret = new double[4];

						tml.Fire(
							new double[]
							{
								(a &    1) == 0 ? 0.0 : 1.0,
								(a &    2) == 0 ? 0.0 : 1.0,
								(a &    4) == 0 ? 0.0 : 1.0,
								(a &    8) == 0 ? 0.0 : 1.0,
								(a &   16) == 0 ? 0.0 : 1.0,
								(a &   32) == 0 ? 0.0 : 1.0,
								(a &   64) == 0 ? 0.0 : 1.0,
								(a &  128) == 0 ? 0.0 : 1.0,
								(a &  256) == 0 ? 0.0 : 1.0,
								(a &  512) == 0 ? 0.0 : 1.0,
							},
							ret
							);

						int x = 1;
						if (a % 3 == 0) x <<= 1;
						if (a % 5 == 0) x <<= 2;

						int v =
							(ret[0] < 0.5 ? 0 : 1) +
							(ret[1] < 0.5 ? 0 : 2) +
							(ret[2] < 0.5 ? 0 : 4) +
							(ret[3] < 0.5 ? 0 : 8);

						bool correct = x == v;

						if (correct)
							correctCnt++;

						Console.WriteLine(a.ToString("D3") + " ==> " + v.ToString("D2") + " (" + x.ToString("D2") + ") ==> " + (correct ? "正解" : "違う") + " " +
							(ret[0] + 0.5).ToString("F9") + " " +
							(ret[1] + 0.5).ToString("F9") + " " +
							(ret[2] + 0.5).ToString("F9") + " " +
							(ret[3] + 0.5).ToString("F9"));
					}
					Console.WriteLine("correctCnt: " + correctCnt);
				}

				//Console.WriteLine("Press ENTER to continue.");
				//Console.ReadLine();

				for (int c = 0; c < 100000; c++) // 学習
				{
					int a = (int)SecurityTools.CRandom.GetRange(101, 1023);
					int v = 1;
					if (a % 3 == 0) v <<= 1;
					if (a % 5 == 0) v <<= 2;

					tml.Train(
						new double[]
						{
							(a &    1) == 0 ? 0.0 : 1.0,
							(a &    2) == 0 ? 0.0 : 1.0,
							(a &    4) == 0 ? 0.0 : 1.0,
							(a &    8) == 0 ? 0.0 : 1.0,
							(a &   16) == 0 ? 0.0 : 1.0,
							(a &   32) == 0 ? 0.0 : 1.0,
							(a &   64) == 0 ? 0.0 : 1.0,
							(a &  128) == 0 ? 0.0 : 1.0,
							(a &  256) == 0 ? 0.0 : 1.0,
							(a &  512) == 0 ? 0.0 : 1.0,
						},
						new double[]
						{
							(v & 1) == 0 ? 0.0 : 1.0,
							(v & 2) == 0 ? 0.0 : 1.0,
							(v & 4) == 0 ? 0.0 : 1.0,
							(v & 8) == 0 ? 0.0 : 1.0,
						},
						LEARNING_RATE
						);
				}
			}
		}
	}
}
