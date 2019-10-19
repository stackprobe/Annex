using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Drawing;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{6a3bf3aa-aad6-4564-b8b6-fc462731b1ca}";
		public const string APP_TITLE = "c";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			Test0001();
		}

		private const int W = 400;
		private const int H = 200;

		private AutoTable<int> Map = new AutoTable<int>(W, H);

		private int GetArroundCount(int x, int y)
		{
			int count = 0;

			for (int xc = -1; xc <= 1; xc++)
			{
				for (int yc = -1; yc <= 1; yc++)
				{
					int xx = (x + W + xc) % W;
					int yy = (y + H + yc) % H;

					if (Map[xx, yy] == 1)
					{
						count++;
					}
				}
			}
			return count;
		}

		private void Test0001()
		{
			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					Map[x, y] = (int)SecurityTools.CRandom.GetRandom(2u);
				}
			}
			for (int c = 0; c < W * H * 10; c++)
			{
				int x = (int)SecurityTools.CRandom.GetRandom((uint)W);
				int y = (int)SecurityTools.CRandom.GetRandom((uint)H);

				if (y < 100)
				{
					if (x < 100)
					{
						Map[x, y] = GetArroundCount(x, y) <= 2 ? 1 : 0;
					}
					else if (x < 300)
					{
						Map[x, y] = GetArroundCount(x, y) <= 3 ? 1 : 0;
					}
					else
					{
						Map[x, y] = GetArroundCount(x, y) <= 4 ? 1 : 0;
					}
				}
				else
				{
					if (x < 100)
					{
						Map[x, y] = GetArroundCount(x, y) % 2 == 0 ? 1 : 0; // 偶数
					}
					else if (x < 200)
					{
						Map[x, y] = GetArroundCount(x, y) % 2 == 1 ? 1 : 0; // 奇数
					}
					else if (x < 300)
					{
						Map[x, y] = new int[] { 2, 3, 5, 7 }.Contains(GetArroundCount(x, y)) ? 1 : 0; // 素数
					}
					else
					{
						Map[x, y] = new int[] { 2, 3, 5, 7 }.Contains(GetArroundCount(x, y)) == false ? 1 : 0; // ! 素数
					}
				}
			}
			Canvas canvas = new Canvas(W, H);

			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					int v = Map[x, y];

					canvas.Set(x, y, Color.FromArgb(v * 255, v * 255, v * 255));
				}
			}
			canvas.Save(@"C:\temp\x1.bmp");
			EzExpand(canvas, 3).Save(@"C:\temp\x3.bmp");
			EzExpand(canvas, 5).Save(@"C:\temp\x5.bmp");
		}

		private Canvas EzExpand(Canvas src, int mul)
		{
			Canvas dest = new Canvas(W * mul, H * mul);

			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					for (int xc = 0; xc < mul; xc++)
					{
						for (int yc = 0; yc < mul; yc++)
						{
							dest.Set(x * mul + xc, y * mul + yc, src.Get(x, y));
						}
					}
				}
			}
			return dest;
		}
	}
}
