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
			int ans = lines.Length - this.GetRingCount(this.GetLinkList(lines));
			Console.WriteLine("" + ans);
		}

		/// <summary>
		/// </summary>
		/// <param name="lines">全ての要素は「１～要素数」の重複のない整数の文字列であること</param>
		/// <returns></returns>
		private int[] GetLinkList(string[] lines)
		{
			int[] links = new int[lines.Length];

			for (int index = 0; index < lines.Length; index++)
				links[index] = int.Parse(lines[index]) - 1;

			return links;
		}

		private int GetRingCount(int[] links)
		{
			bool[] ringeds = new bool[links.Length];
			int ringCount = 0;

			for (int index = 0; index < links.Length; index++)
			{
				if (ringeds[index] == false)
				{
					int i = index;

					while (ringeds[i] == false)
					{
						ringeds[i] = true;
						i = links[i];
					}
					ringCount++;
				}
			}
			return ringCount;
		}
	}
}
