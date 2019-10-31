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
		public const string APP_IDENT = "{947de658-49e3-421b-afd0-0aa7854f0dcd}";
		public const string APP_TITLE = "t0001";

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
			//AutoTable<int> map = new AutoTable<int>(100, 100);
			AutoTable<int> map = new AutoTable<int>(30, 30);

			for (int x = 0; x < map.W; x++)
			{
				for (int y = 0; y < map.H; y++)
				{
					map[x, y] = (int)SecurityTools.CRandom.GetRandom(3);
				}
			}
			Save(map, 0);

			for (int c = 1; c < 1000; c++)
			//for (int c = 1; c < 10000; c++)
			{
				for (int d = 0; d < 100; d++)
				//for (int d = 0; d < 1000; d++)
				{
					Battle(map);
				}
				Save(map, c);
			}
		}

		private void Battle(AutoTable<int> map)
		{
			List<I2Point[]> pps = new List<I2Point[]>();

		restart:
			for (int x = 0; x < map.W - 1; x++)
				for (int y = 0; y < map.H; y++)
					if (map[x, y] != map[x + 1, y])
						pps.Add(new I2Point[] { new I2Point(x, y), new I2Point(x + 1, y) });

			for (int x = 0; x < map.W; x++)
				for (int y = 0; y < map.H - 1; y++)
					if (map[x, y] != map[x, y + 1])
						pps.Add(new I2Point[] { new I2Point(x, y), new I2Point(x, y + 1) });

			if (pps.Count == 0)
			{
				for (int x = 0; x < map.W; x++)
					map[x, map.H / 2] = x % 3;

				goto restart;
			}
			I2Point[] pp = SecurityTools.CRandom.ChooseOne(pps.ToArray());

			if (map[pp[0].X, pp[0].Y] == (map[pp[1].X, pp[1].Y] + 1) % 3)
				map[pp[0].X, pp[0].Y] = map[pp[1].X, pp[1].Y];
			else
				map[pp[1].X, pp[1].Y] = map[pp[0].X, pp[0].Y];
		}

		private Color[] Colors = new Color[]
		{
			Color.Cyan,
			Color.Purple,
			Color.Yellow,
		};

		private void Save(AutoTable<int> map, int index)
		{
			Canvas canvas = new Canvas(map.W, map.H);

			for (int x = 0; x < map.W; x++)
			{
				for (int y = 0; y < map.H; y++)
				{
					canvas.Set(x, y, Colors[map[x, y]]);
				}
			}
			canvas.Save(string.Format(@"C:\temp\{0}.png", index.ToString("D4")));
		}
	}
}
