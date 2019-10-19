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
		public const string APP_IDENT = "{302d8d2f-2eca-4484-9786-b6fc9ab53d99}";
		public const string APP_TITLE = "ptn1024";

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
			for (int pattern = 0; pattern < 1024; pattern++)
			{
				Test0001(pattern);
			}
		}

		private const int W = 100;
		private const int H = 100;

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

		private void Test0001(int pattern)
		{
			Console.WriteLine("pattern: " + pattern); // test

			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					Map[x, y] = (int)SecurityTools.CRandom.GetRandom(2u);
				}
			}
			for (int c = 0; c < W * H * 30; c++)
			{
				int x = (int)SecurityTools.CRandom.GetRandom((uint)W);
				int y = (int)SecurityTools.CRandom.GetRandom((uint)H);

				Map[x, y] = (pattern & (1 << GetArroundCount(x, y))) != 0 ? 1 : 0;
			}
			Canvas canvas = new Canvas(W, H);

			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					int v = Map[x, y];

					canvas.Set(x, y, Color.FromArgb(255 * v, 255 * v, 255 * v));
				}
			}
			EzExpand(canvas, 5).Save(@"C:\temp\P_" + pattern.ToString("D4") + ".bmp");
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
