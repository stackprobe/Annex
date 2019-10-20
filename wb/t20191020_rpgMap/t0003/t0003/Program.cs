using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Drawing;
using System.Drawing.Imaging;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{9a830a30-2fbf-4ce5-8ce0-74f8ef6ed2d8}";
		public const string APP_TITLE = "t0003";

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
			MakeLikeAFieldMap();
		}

		private static void MakeLikeAFieldMap()
		{
			const int w = 100; // マップの幅
			const int h = 100; // マップの高さ

			Random rand = new Random();
			int[][] map = new int[w][];

			for (int x = 0; x < w; x++)
			{
				map[x] = new int[h];

				for (int y = 0; y < h; y++)
				{
					double dx = x - w / 2;
					double dy = y - h / 2;
					double d = dx * dx + dy * dy;
					d = Math.Sqrt(d);
					d /= w / 2;
					//double r = 0.9 - 0.5 * d;
					//double r = 0.8 - 0.4 * d;
					//double r = 0.7 - 0.3 * d;
					double r = 0.6 - 0.2 * d;

					map[x][y] = rand.NextDouble() < r ? 1 : 0;
				}
			}
			for (int c = 0; c < w * h * 10; c++)
			{
				int x = rand.Next(w);
				int y = rand.Next(h);
				int count = 0;

				for (int xc = -1; xc <= 1; xc++)
				{
					for (int yc = -1; yc <= 1; yc++)
					{
						count += map[(x + w + xc) % w][(y + h + yc) % h];
					}
				}
				map[x][y] = count / 5;
			}
			using (Bitmap bmp = new Bitmap(w, h))
			{
				for (int x = 0; x < w; x++)
				{
					for (int y = 0; y < h; y++)
					{
						bmp.SetPixel(x, y, map[x][y] == 0 ? Color.Turquoise : Color.Sienna);
					}
				}
				bmp.Save(@"C:\temp\fm_0001.bmp", ImageFormat.Bmp); // 適当な場所にビットマップで出力する。
			}

			// ----

			Color[] tileColors = new Color[] // 各地形の色
			{
				Color.FromArgb(0, 0, 55), // ここから海？
				Color.FromArgb(20, 20, 105),
				Color.FromArgb(40, 40, 155),
				Color.FromArgb(60, 60, 205),
				Color.FromArgb(80, 80, 255),
				Color.FromArgb(120, 80, 40), // ここから地面？
				Color.FromArgb(150, 100, 50),
				Color.FromArgb(180, 120, 60),
				Color.FromArgb(210, 140, 70),
				Color.FromArgb(240, 160, 80),
				Color.FromArgb(0, 180, 0), // ここから森？
				Color.FromArgb(0, 150, 0),
				Color.FromArgb(0, 120, 0),
				Color.FromArgb(10, 90, 0),
				Color.FromArgb(20, 60, 0),
				Color.FromArgb(40, 30, 0),
			};

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					map[x][y] *= tileColors.Length - 1;
				}
			}
			for (int c = 0; c < w * h * 20; c++)
			{
				int x1 = rand.Next(w);
				int x2 = x1;
				int y1 = rand.Next(h);
				int y2 = y1;

				if (rand.Next(2) == 0)
					x2 = (x2 + 1) % w;
				else
					y2 = (y2 + 1) % h;

				if (map[x1][y1] < map[x2][y2])
				{
					map[x1][y1]++;
					map[x2][y2]--;
				}
				else if (map[x2][y2] < map[x1][y1])
				{
					map[x1][y1]--;
					map[x2][y2]++;
				}
			}
			using (Bitmap bmp = new Bitmap(w, h))
			{
				for (int x = 0; x < w; x++)
				{
					for (int y = 0; y < h; y++)
					{
						bmp.SetPixel(x, y, tileColors[map[x][y]]);
					}
				}
				bmp.Save(@"C:\temp\fm_0002.bmp", ImageFormat.Bmp); // 適当な場所にビットマップで出力する。
			}
		}
	}
}
