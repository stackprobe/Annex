﻿using System;
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
			Test01_b(0, 1);

			Test01_a(1);
			Test01_a(2);
			Test01_a(3);
			Test01_a(4);
			Test01_a(5);
			Test01_a(6);
			Test01_a(7);
			Test01_a(8);
			Test01_a(9);
			Test01_a(10);
			Test01_a(30);
			Test01_a(100);
			Test01_a(300);
			Test01_a(1000);
			Test01_a(3000);
			Test01_a(10000);

			for (int c = 0; c < 10000; c++)
			{
				Test01_b(SecurityTools.CRandom.GetRange(0, 10000), SecurityTools.CRandom.GetRange(1, 10000));
			}
		}

		private void Test01_a(int valueNum)
		{
			Test01_b(valueNum, 1);

			for (int c = 0; c < 100; c++)
			{
				Test01_b(valueNum, 2);
				Test01_b(valueNum, 3);
				Test01_b(valueNum, 4);
				Test01_b(valueNum, 5);
				Test01_b(valueNum, 6);
				Test01_b(valueNum, 7);
				Test01_b(valueNum, 9);
				Test01_b(valueNum, 10);
				Test01_b(valueNum, 30);
				Test01_b(valueNum, 100);
				Test01_b(valueNum, 300);
				Test01_b(valueNum, 1000);
				Test01_b(valueNum, 3000);
				Test01_b(valueNum, 10000);
				Test01_b(valueNum, 100000);
				Test01_b(valueNum, 1000000);
				Test01_b(valueNum, 10000000);
				Test01_b(valueNum, 100000000);
			}
		}

		private void Test01_b(int valueNum, int valKindNum)
		{
			Console.WriteLine(valueNum + ", " + valKindNum);

			int[] origValues = new int[valueNum];

			int[] values1 = new int[valueNum];
			int[] values2 = new int[valueNum];
			int[] values3 = new int[valueNum];

			for (int index = 0; index < valueNum; index++)
			{
				origValues[index] = SecurityTools.CRandom.GetInt(valKindNum);

				values1[index] = origValues[index];
				values2[index] = origValues[index];
				values3[index] = origValues[index];
			}

			// ====

			DoSort01(values1);
			DoSort02(values2);
			DoSort03(values3);

			// ====

			for (int index = 0; index < valueNum; index++)
			{
				if (
					values1[index] != values2[index] ||
					values1[index] != values3[index]
					)
					throw null; // bugged !!!
			}

			Console.WriteLine("done");
		}

		private void DoSort01(int[] values)
		{
			Array.Sort(values, (a, b) => a - b);
		}

		private void DoSort02(int[] values)
		{
			int[] indexes = Enumerable.Range(0, values.Length).ToArray();

			Array.Sort(indexes, (a, b) => values[a] - values[b]);

			for (int index = 0; index < values.Length; index++)
			{
				if (indexes[index] != -1)
				{
					int ci = index;

					for (; ; )
					{
						int xi = indexes[ci];

						indexes[ci] = -1;

						if (xi == index)
							break;

						ArrayTools.Swap(values, ci, xi);
						ci = xi;
					}
				}
			}
		}

		private void DoSort03(int[] values)
		{
			int[] indexes = Enumerable.Range(0, values.Length).ToArray();

			Array.Sort(indexes, (a, b) => values[a] - values[b]);

			for (int index = 0; index < values.Length; index++)
			{
				if (indexes[index] != -1)
				{
					int escVal = values[index];
					int ci = index;

					for (; ; )
					{
						int xi = indexes[ci];

						indexes[ci] = -1;

						if (xi == index)
							break;

						values[ci] = values[xi];
						ci = xi;
					}
					values[ci] = escVal;
				}
			}
		}
	}
}
