using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a
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
			List<int> answer = new List<int>();

			for (int n = 6; answer.Count < 5; n++)
				if (TryGame(n))
					answer.Add(n);

			Console.WriteLine(string.Join(", ", answer));
		}

		private bool TryGame(int n)
		{
			List<int> member = new List<int>();

			for (int c = 1; c <= n * 2; c++)
				member.Add(c);

			for (int c = 1; 2 <= member.Count; c = (c + 1) % member.Count)
				member.RemoveAt(c);

			return member[0] == n;
		}
	}
}
