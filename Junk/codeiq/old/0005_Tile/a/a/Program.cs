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
			int count = 0;

			for (int index = 0; ; index++)
			{
				if (MakeBranches(index))
					break;

				if (IsDrawable())
					count++;
			}
			Console.WriteLine("answer: " + count);
		}

		// タイル数
		private int H = 3; // 縦
		private int W = 4; // 横

		private List<Node> Nodes = new List<Node>();
		private List<Branch> Branches = new List<Branch>();

		private int RC2I(int r, int c)
		{
			return r * W + c;
		}

		Node[][] NTbl;

		private bool MakeBranches(int index)
		{
			Nodes.Clear();
			Branches.Clear();

			NTbl = new Node[H + 1][];

			for (int r = 0; r <= H; r++)
			{
				NTbl[r] = new Node[W + 1];

				for (int c = 0; c <= W; c++)
				{
					NTbl[r][c] = new Node();
					Nodes.Add(NTbl[r][c]);
				}
			}
			for (int r = 0; r <= H; r++)
			{
				for (int c = 0; c <= W; c++)
				{
					if (r < H)
						Branches.Add(new Branch(NTbl[r][c], NTbl[r + 1][c]));

					if (c < W)
						Branches.Add(new Branch(NTbl[r][c], NTbl[r][c + 1]));

					if (r < H && c < W)
					{
						AddDiag(index % 4, r, c);
						index /= 4;
					}
				}
			}
			return 1 <= index;
		}

		private void AddDiag(int index, int r, int c)
		{
			switch (index)
			{
				case 0:
					break;

				case 1:
					Branches.Add(new Branch(NTbl[r][c], NTbl[r + 1][c + 1]));
					break;

				case 2:
					Branches.Add(new Branch(NTbl[r + 1][c], NTbl[r][c + 1]));
					break;

				case 3:
					{
						Node node = new Node();
						Nodes.Add(node);

						Branches.Add(new Branch(node, NTbl[r][c]));
						Branches.Add(new Branch(node, NTbl[r + 1][c]));
						Branches.Add(new Branch(node, NTbl[r][c + 1]));
						Branches.Add(new Branch(node, NTbl[r + 1][c + 1]));
					}
					break;

				default:
					throw new Exception();
			}
		}

		private bool IsDrawable()
		{
			return GetOddBranchNodeCount() <= 2;
		}

		private int GetOddBranchNodeCount()
		{
			int count = 0;

			foreach (Node node in Nodes)
				if (GetBranchCount(node) % 2 == 1)
					count++;

			return count;
		}

		private int GetBranchCount(Node node)
		{
			int count = 0;

			foreach (Branch branch in Branches)
				if (branch.A == node || branch.B == node)
					count++;

			return count;
		}
	}

	public class Node
	{ }

	public class Branch
	{
		public Node A;
		public Node B;

		public Branch(Node a, Node b)
		{
			A = a;
			B = b;
		}
	}
}
