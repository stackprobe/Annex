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
		}

		private void DoSort01(int[] values)
		{
			throw new NotImplementedException();
		}

		private void DoSort02(int[] values)
		{
			throw new NotImplementedException();
		}

		private void DoSort03(int[] values)
		{
			throw new NotImplementedException();
		}
	}
}
