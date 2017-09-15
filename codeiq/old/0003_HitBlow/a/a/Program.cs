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
				//new Program().Main2("sample.in.txt", "a.tmp");
				new Program().Main2("testdata.in.txt", "testdata.out.txt");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void Main2(string rFile, string wFile)
		{
			List<string> wLines = new List<string>();

			foreach (string line in File.ReadAllLines(rFile))
			{
				string[] codes = line.Split(' ');

				if (codes.Length == 2)
					wLines.Add(GetResult(codes[0], codes[1]));
			}
			File.WriteAllLines(wFile, wLines);
		}

		private string GetResult(string ansCd, string tryCd)
		{
			int hit = 0;
			int blow = 0;

			bool[] hitted = new bool[ansCd.Length];

			for (int i = 0; i < ansCd.Length; i++)
				if (ansCd[i] == tryCd[i])
				{
					hit++;
					hitted[i] = true;
				}

			bool[] blowed = new bool[tryCd.Length];

			for (int i = 0; i < ansCd.Length; i++)
				if (hitted[i] == false)
					for (int j = 0; j < tryCd.Length; j++)
						if (i != j && ansCd[i] == tryCd[j] && hitted[j] == false && blowed[j] == false)
						{
							blow++;
							blowed[j] = true;
							break;
						}

			return hit + "H" + blow + "B";
		}
	}
}
