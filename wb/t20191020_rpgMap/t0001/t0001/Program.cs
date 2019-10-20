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
		public const string APP_IDENT = "{99b2f9dd-00ae-4909-ba6a-ada52fc526f4}";
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

		private const int W = 100;
		private const int H = 100;

		private AutoTable<double> Map = new AutoTable<double>();

		private void Main2(ArgsReader ar)
		{
			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					Map[x, y] = SecurityTools.CRandom.GetReal();
				}
			}
			for (int c = 0; c < W * H * 30; c++)
			{
				int x = (int)SecurityTools.CRandom.GetRandom(W);
				int y = (int)SecurityTools.CRandom.GetRandom(H);
				double value = 0.0;

				for (int xc = -1; xc <= 1; xc++)
				{
					for (int yc = -1; yc <= 1; yc++)
					{
						value += Map[(x + W + xc) % W, (y + H + yc) % H];
					}
				}
				Map[x, y] = value / 9.0;
			}

			{
				double min = 1.0;
				double max = 0.0;

				for (int x = 0; x < W; x++)
				{
					for (int y = 0; y < H; y++)
					{
						min = Math.Min(min, Map[x, y]);
						max = Math.Max(max, Map[x, y]);
					}
				}
				for (int x = 0; x < W; x++)
				{
					for (int y = 0; y < H; y++)
					{
						Map[x, y] -= min;
						Map[x, y] /= max - min;
					}
				}
			}

			Canvas canvas = new Canvas(W, H);

			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
#if true
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
						Color.FromArgb(175, 175, 175),
						Color.FromArgb(195, 195, 195),
						Color.FromArgb(215, 215, 215),
						Color.FromArgb(235, 235, 235),
						Color.FromArgb(255, 255, 255),
					};

					int v = DoubleTools.ToInt(Map[x, y] * (colors.Length - 1));

					canvas.Set(x, y, colors[v]);
#else
					int v = DoubleTools.ToInt(Map[x, y] * 255.0);

					canvas.Set(x, y, Color.FromArgb(v, v, v));
#endif
				}
			}
			canvas.Save(@"C:\temp\1.bmp", ImageFormat.Bmp);
		}
	}
}
