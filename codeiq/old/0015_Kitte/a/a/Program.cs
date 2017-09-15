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
				//new Program().Main2("sample.in.txt", "1.txt"); // test
				new Program().Main2("testdata.in.txt", "testdata.out.txt");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void Main2(string rFile, string wFile)
		{
			string[] lines = File.ReadAllLines(rFile, Encoding.ASCII);
			int num = int.Parse(lines[0]);
			string[] ansLines = new string[num];

			for (int index = 0; index < num; index++)
			{
				string line = lines[index + 1];
				string[] tokens = line.Split(' ');
				int c = 0;
				int h = int.Parse(tokens[c++]);
				int w = int.Parse(tokens[c++]);
				int n = int.Parse(tokens[c++]);

				int ans = this.GetPtnNum(h, w, n);

				ansLines[index] = "" + ans;
			}
			File.WriteAllLines(wFile, ansLines, Encoding.ASCII);
		}

		private Stamp[][] Sheet;
		private int Sheet_H;
		private int Sheet_W;
		private int CutNum;
		private int PtnNum;

		private class Stamp
		{
			public bool Cut;
			public bool Reached;
		}

		private int GetPtnNum(int h, int w, int n)
		{
			if (h < 1 || w < 1 || n < 1)
				throw new ArgumentException();

			this.Sheet = new Stamp[h][];

			for (int r = 0; r < h; r++)
			{
				this.Sheet[r] = new Stamp[w];

				for (int c = 0; c < w; c++)
					this.Sheet[r][c] = new Stamp();
			}
			this.Sheet_H = h;
			this.Sheet_W = w;
			this.CutNum = n;
			this.PtnNum = 0;

			this.TryCut(0, this.CutNum);

			return this.PtnNum;
		}

		private void TryCut(int startPos, int remain)
		{
			if (remain == 0)
			{
				if (this.IsOnePiece())
					this.PtnNum++;

				return;
			}

			for (int i = startPos; i < this.Sheet_H * this.Sheet_W; i++)
			{
				int r = i / this.Sheet_W;
				int c = i % this.Sheet_W;

				this.Sheet[r][c].Cut = true;
				this.TryCut(i + 1, remain - 1);
				this.Sheet[r][c].Cut = false;
			}
		}

		private int FndR;
		private int FndC;

		private bool IsOnePiece()
		{
			this.FindCut();
			this.Access(this.FndR, this.FndC);
			return this.HasNotReached_Clear() == false;
		}

		private void FindCut()
		{
			for (int r = 0; r < this.Sheet_H; r++)
			{
				for (int c = 0; c < this.Sheet_W; c++)
				{
					if (this.Sheet[r][c].Cut)
					{
						this.FndR = r;
						this.FndC = c;
						return;
					}
				}
			}
		}

		private void Access(int r, int c)
		{
			if (r < 0 || this.Sheet_H <= r)
				return;

			if (c < 0 || this.Sheet_W <= c)
				return;

			if (this.Sheet[r][c].Cut == false)
				return;

			if (this.Sheet[r][c].Reached)
				return;

			this.Sheet[r][c].Reached = true;

			this.Access(r - 1, c);
			this.Access(r + 1, c);
			this.Access(r, c - 1);
			this.Access(r, c + 1);
		}

		private bool HasNotReached_Clear()
		{
			bool result = false;

			for (int r = 0; r < this.Sheet_H; r++)
			{
				for (int c = 0; c < this.Sheet_W; c++)
				{
					Stamp s = this.Sheet[r][c];

					if (s.Cut)
					{
						if (s.Reached)
							s.Reached = false;
						else
							result = true;
					}
				}
			}
			return result;
		}
	}
}
