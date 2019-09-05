using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public static class DiffValueLog
	{
		private static long[] CountList = new long[100];

		public static void Clear()
		{
			for (int index = 0; index < CountList.Length; index++)
			{
				CountList[index] = 0L;
			}
		}

		public static void Add(double diffValue)
		{
			CountList[IntTools.Range((int)(diffValue * 1000000.0), 0, CountList.Length - 1)]++;
		}

		public static void WriteToFile(string file)
		{
			File.WriteAllLines(file, GetLogLines());
		}

		private static string[] GetLogLines()
		{
			List<string> lines = new List<string>();

			for (int index = 0; index < CountList.Length; index++)
			{
				lines.Add("0.0000" + index.ToString("D2") + " ====> " + CountList[index]);
			}
			return lines.ToArray();
		}
	}
}
