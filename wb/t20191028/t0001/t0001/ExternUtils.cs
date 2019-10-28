using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;

namespace Charlotte
{
	public static class ExternUtils
	{
		public static int[,] MakeLikeADungeonMap(int pattern) // pattern: 0 ～ 1023
		{
			Random rand = new Random();
			int[,] map = new int[Consts.MAP_W, Consts.MAP_H];

			for (int x = 0; x < Consts.MAP_W; x++)
			{
				for (int y = 0; y < Consts.MAP_H; y++)
				{
					map[x, y] = rand.Next(2);
				}
			}
			for (int c = 0; c < Consts.MAP_W * Consts.MAP_H * 30; c++)
			{
				int x = rand.Next(Consts.MAP_W);
				int y = rand.Next(Consts.MAP_H);
				int count = 0;

				for (int xc = -1; xc <= 1; xc++)
				{
					for (int yc = -1; yc <= 1; yc++)
					{
						count += map[(x + Consts.MAP_W + xc) % Consts.MAP_W, (y + Consts.MAP_H + yc) % Consts.MAP_H];
					}
				}
				map[x, y] = (pattern >> count) & 1;
			}
			return map;
		}

		private static int WMIndex = 0;

		public static void WriteMap(int[,] map)
		{
			Canvas canvas = new Canvas(Consts.MAP_W, Consts.MAP_H);

			for (int x = 0; x < Consts.MAP_W; x++)
			{
				for (int y = 0; y < Consts.MAP_H; y++)
				{
					Color color;

					switch (map[x, y])
					{
						case 0: color = Color.Gray; break;
						case 1: color = Color.Yellow; break;
						case 2: color = Color.Red; break;

						default:
							throw null; // never
					}
					canvas.Set(x, y, color);
				}
			}
			canvas = EzExpand(canvas, 3);
			canvas.Save(string.Format(@"C:\temp\{0}.png", (++WMIndex).ToString("D4")));
		}

		private static Canvas EzExpand(Canvas src, int mul)
		{
			int w = src.GetWidth();
			int h = src.GetHeight();

			Canvas dest = new Canvas(w * mul, h * mul);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
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
