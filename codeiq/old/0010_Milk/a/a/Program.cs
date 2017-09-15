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
				new Program().Main2("codeiq_milk.csv");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.ReadLine();
		}

		private int[][] Matrix;

		private int NodeNum;
		private int Energy;
		private bool[] Passed;
		private int[] Route;

		private int MinEnergy;
		private int[] MinRoute;

		private void Main2(string rFile)
		{
			LoadMatrix(rFile);
			SearchMain();
			ShowAnswer();
		}

		private void LoadMatrix(string rFile)
		{
			List<int[]> rows = new List<int[]>();

			foreach (string line in File.ReadAllLines(rFile))
			{
				List<int> row = new List<int>();

				foreach (string cell in line.Split(','))
					row.Add(int.Parse(cell));

				rows.Add(row.ToArray());
			}
			Matrix = rows.ToArray();
		}

		private void SearchMain()
		{
			NodeNum = Matrix.Length;
			Energy = 0;
			Passed = new bool[NodeNum];
			Route = new int[NodeNum - 1];

			MinEnergy = int.MaxValue;
			MinRoute = new int[NodeNum - 1];

			TryNext(0, 0);
		}

		private void TryNext(int tryIndex, int currPos)
		{
			if (NodeNum - 1 <= tryIndex)
			{
				int e = Energy + Matrix[currPos][0];

				if (e < MinEnergy)
				{
					MinEnergy = e;
					Array.Copy(Route, MinRoute, Route.Length);
				}
				return;
			}

			for (int nextPos = 1; nextPos < NodeNum; nextPos++)
			{
				if (Passed[nextPos] == false)
				{
					int e = Matrix[currPos][nextPos];

					Energy += e;
					Passed[nextPos] = true;
					Route[tryIndex] = nextPos;

					TryNext(tryIndex + 1, nextPos);

					Energy -= e;
					Passed[nextPos] = false;
				}
			}
		}

		private void ShowAnswer()
		{
			Console.WriteLine("エネルギー量：" + MinEnergy);
			Console.Write("配達ルート：1");

			foreach (int node in MinRoute)
			{
				Console.Write(" -> " + (node + 1));
			}
			Console.WriteLine(" -> 1");
		}
	}
}
