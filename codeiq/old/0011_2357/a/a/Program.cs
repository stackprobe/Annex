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
				new Program().Main2("magic_number.txt");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.ReadLine();
		}

		private void Main2(string rFile)
		{
			foreach (string line in File.ReadAllLines(rFile))
			{
				Console.WriteLine("" + new FindPtnNum(line).GetPtnNum());
			}
		}
	}

	public class FindPtnNum
	{
		private string Line;

		public FindPtnNum(string line)
		{
			this.Line = line;
		}

		private int PtnNum;

		public int GetPtnNum()
		{
			this.PtnNum = 0;
			this.TryNext(0, 0);
			return this.PtnNum;
		}

		private void TryNext(int currPos, int sum)
		{
			if (this.Line.Length <= currPos)
			{
				if(
					1 <= sum &&
					(
						sum % 2 == 0 ||
						sum % 3 == 0 ||
						sum % 5 == 0 ||
						sum % 7 == 0
						)
					)
					this.PtnNum++;

				return;
			}

			for (int nextLen = 1; currPos + nextLen <= this.Line.Length; nextLen++)
			{
				int nextVal = int.Parse(this.Line.Substring(currPos, nextLen));
				int nextPos = currPos + nextLen;

				this.TryNext(nextPos, sum + nextVal);

				if(1 <= currPos)
					this.TryNext(nextPos, sum - nextVal);
			}
		}
	}
}
