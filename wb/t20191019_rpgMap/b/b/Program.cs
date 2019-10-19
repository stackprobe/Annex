﻿using System;
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
		public const string APP_IDENT = "{5fad1910-bc76-45b8-a3f0-a7add8ffeeef}";
		public const string APP_TITLE = "b";

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

		private const int W = 200;
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

				Map[x, y] = 5 <= GetArroundCount(x, y) ? 1 : 0;
			}
			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					Map[x, y] *= 15;
				}
			}
			for (int c = 0; c < W * H * 20; c++)
			{
				int x1 = (int)SecurityTools.CRandom.GetRandom((uint)W);
				int x2 = x1;
				int y1 = (int)SecurityTools.CRandom.GetRandom((uint)H);
				int y2 = y1;

				if (SecurityTools.CRandom.GetRandom(2) != 0)
					x2 = (x2 + 1) % W;
				else
					y2 = (y2 + 1) % H;

				int v1 = Map[x1, y1];
				int v2 = Map[x2, y2];

				if (v1 + 1 <= v2)
				{
					v1++;
					v2--;
				}
				else if (v2 + 1 <= v1)
				{
					v1--;
					v2++;
				}
				Map[x1, y1] = v1;
				Map[x2, y2] = v2;
			}
			Canvas canvas = new Canvas(W, H);

			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					int v = Map[x, y];
#if true
					{
						Color[] colors = new Color[]
						{
							Color.FromArgb(0, 0, 55),
							Color.FromArgb(20, 20, 105),
							Color.FromArgb(40, 40, 155),
							Color.FromArgb(60, 60, 205),
							Color.FromArgb(80, 80, 255),
							Color.FromArgb(120, 80, 40),
							Color.FromArgb(150, 100, 50),
							Color.FromArgb(180, 120, 60),
							Color.FromArgb(210, 140, 70),
							Color.FromArgb(240, 160, 80),
							Color.FromArgb(0, 180, 0),
							Color.FromArgb(0, 150, 0),
							Color.FromArgb(0, 120, 0),
							Color.FromArgb(10, 90, 0),
							Color.FromArgb(20, 60, 0),
							Color.FromArgb(40, 30, 0),
						};

						canvas.Set(x, y, colors[v]);
					}
#elif true
					canvas.Set(x, y, Color.FromArgb(
						((v >> 0) & 1) * 255,
						((v >> 1) & 1) * 255,
						((v >> 2) & 1) * 255
						));
#else
					v *= 17;
					canvas.Set(x, y, Color.FromArgb(v, v, 255 - v));
#endif
				}
			}
			canvas.Save(@"C:\temp\x1.bmp");
			canvas = EzExpand(canvas, 3);
			canvas.Save(@"C:\temp\x3.bmp");
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
