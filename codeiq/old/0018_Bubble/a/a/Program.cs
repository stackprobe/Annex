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
				new Program().Main2(args[0]);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void Main2(string file)
		{
			string[] lines = File.ReadAllLines(file, Encoding.ASCII);
			int len = lines.Length;
			int[] list = new int[len];
			int[] from = new int[len];

			for (int index = 0; index < len; index++)
			{
				list[index] = int.Parse(lines[index]) - 1;
				from[list[index]] = index;
			}
			int ans = 0;
			int lra = 0;
			int rla = 0;
			int lrd = 0;
			int rld = 0;
			int[] lral = new int[len];
			int[] rlal = new int[len];
			int[] lrdl = new int[len];
			int[] rldl = new int[len];

			for (int index = 0; index < len; index++)
			{
				int t = list[index];
				int f = from[index];

				if (index < t)
				{
					lra++;
				}
				else
				{
					rla--;
					rld++;
				}

				if (index < f)
				{
					rla++;
				}
			}
			Console.WriteLine("" + ans);
		}
	}
}
