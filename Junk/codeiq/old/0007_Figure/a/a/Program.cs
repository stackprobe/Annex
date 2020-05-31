using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace a
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2("figure_price.csv");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private ValInfo[] PriceInfos;
		private List<ValInfo> SoldPtn = new List<ValInfo>();
		private ValInfo[] MaxSoldPtn;
		private int MaxSoldVal = int.MinValue;

		private void Main2(string priceFile)
		{
			PriceInfos = ReadPriceFile(priceFile);
			TrySell(10, 1, 0);
			Console.WriteLine("====");
			OutputSoldPtn(MaxSoldPtn, MaxSoldVal);
		}

		private ValInfo[] ReadPriceFile(string file)
		{
			List<ValInfo> infos = new List<ValInfo>();

			foreach (string row in File.ReadAllLines(file, Encoding.ASCII))
			{
				string[] cells = row.Split(',');

				if (cells.Length == 2)
				{
					ValInfo i = new ValInfo();

					i.Num = int.Parse(cells[0]);
					i.Value = int.Parse(cells[1]);

					infos.Add(i);
				}
			}
			return infos.ToArray();
		}

		private void TrySell(int remain, int minSellNum, int soldVal)
		{
			if (remain == 0)
			{
				OutputSoldPtn(SoldPtn.ToArray(), soldVal);

				if (MaxSoldVal < soldVal)
				{
					MaxSoldPtn = SoldPtn.ToArray();
					MaxSoldVal = soldVal;
				}
				return;
			}
			foreach (ValInfo i in PriceInfos)
			{
				if (minSellNum <= i.Num && i.Num <= remain)
				{
					SoldPtn.Add(i);
					TrySell(remain - i.Num, i.Num, soldVal + i.Value);
					SoldPtn.RemoveAt(SoldPtn.Count - 1);
				}
			}
		}

		private void OutputSoldPtn(ValInfo[] soldPtn, int soldVal)
		{
			Console.Write("set=");

			for (int index = 0; index < soldPtn.Length; index++)
			{
				if (1 <= index)
					Console.Write("+");

				Console.Write("" + soldPtn[index].Num);
			}
			Console.WriteLine("");
			Console.Write("price=");

			for (int index = 0; index < soldPtn.Length; index++)
			{
				if (1 <= index)
					Console.Write("+");

				Console.Write("" + soldPtn[index].Value);
			}
			Console.WriteLine("=" + soldVal);
		}
	}

	public class ValInfo
	{
		public int Num;
		public int Value;
	}
}
