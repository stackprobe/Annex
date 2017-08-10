using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AutoBall4
{
	public class FileTools
	{
		public static string[][] ReadCsvFile(string csvFile)
		{
			string[] lines = File.ReadAllLines(csvFile, Encoding.ASCII);
			List<string[]> rows = new List<string[]>();

			foreach (string line in lines)
			{
				rows.Add(line.Split(','));
			}
			return rows.ToArray();
		}
	}
}
