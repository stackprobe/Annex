using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tartelette
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.ReadLine();
		}

		private void Main2()
		{
			Test_SortedList();

			Console.WriteLine("OK!");
		}

		private class IntBox
		{
			public int Value;
		}

		private static int CompIntBox(IntBox v1, IntBox v2)
		{
			return v1.Value - v2.Value;
		}

		private void Test_SortedList()
		{
			Random r = new Random();

			for (int c = 0; c < 10000; c++)
			{
				Console.WriteLine("c: " + c);

				SortedList<IntBox> list = new SortedList<IntBox>(CompIntBox);
				int n = r.Next(10000);

				for (int i = 0; i < n; i++)
					list.Add(new IntBox() { Value = r.Next(10000) });

				for (int d = 0; d < 100; d++)
				{
					int target = r.Next(10000);
					int index = list.IndexOf(new IntBox() { Value = target });

					if (index == -1)
					{
						for (int i = 0; i < n; i++)
							if (list[i].Value == target)
								throw new Exception();
					}
					else
					{
						if (list[index].Value != target)
							throw new Exception();
					}
				}
			}
		}
	}
}
