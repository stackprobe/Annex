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
		public const string APP_IDENT = "{e2f8396c-a8c0-44bc-8043-287053636fe4}"; // 再採番 @ 2020.2.11
		public const string APP_TITLE = "ptn1024h";

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
			Test01();
		}

		private const int PATTERN_MIN = -1;
		//private const int PATTERN_MAX = 2; // test
		private const int PATTERN_MAX = 1023;
		private const int SAMPLE_NUM = 3;

		private void Test01()
		{
			string dir = ExtraTools.MakeFreeDir();

			for (int pattern = PATTERN_MIN; pattern <= PATTERN_MAX; pattern++)
			{
				Console.WriteLine("pattern: " + pattern); // test

				for (int pct = 0; pct <= 100; pct += 10)
				{
					for (int c = 1; c <= SAMPLE_NUM; c++)
					{
						MakeLikeADungeonMap(pattern, pct / 100.0);

						Canvas canvas = new Canvas(@"C:\temp\LikeADungeonMap_Percentage.bmp");
						canvas = EzExpand(canvas, 3);
						canvas.Save(Path.Combine(dir, "Output_" + pattern.ToString("D4") + "_" + pct.ToString("D3") + "_" + c + ".png"));
					}
				}
			}

			// gen html
			{
				List<string> lines = new List<string>();

				lines.Add("<html>");
				lines.Add("<head>");
				lines.Add("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>");
				lines.Add("</head>");
				lines.Add("<body>");
				lines.Add("<table border=\"1\">");

				{
					lines.Add("<tr>");
					lines.Add("<th>pattern</th>");

					for (int pct = 0; pct <= 100; pct += 10)
					{
						lines.Add("<th>" + pct + " %</th>");
					}
					lines.Add("</tr>");
				}

				for (int pattern = PATTERN_MIN; pattern <= PATTERN_MAX; pattern++)
				{
					for (int c = 1; c <= SAMPLE_NUM; c++)
					{
						lines.Add("<tr>");

						if (c == 1)
						{
							lines.Add(string.Format("<td rowspan=\"{0}\">", SAMPLE_NUM));

							if (pattern == -1)
								lines.Add("-1 (PLAIN)");
							else
								lines.Add(string.Format("{0} (0b{1})", pattern.ToString("D4"), LPad(Convert.ToString(pattern, 2), 9, '0')));

							lines.Add("</td>");
						}
						for (int pct = 0; pct <= 100; pct += 10)
						{
							lines.Add("<td>");
#if true
							{
								string file = Path.Combine(dir, "Output_" + pattern.ToString("D4") + "_" + pct.ToString("D3") + "_" + c + ".png");

								lines.Add("<img src=\"" + GetPngDataUrl(file) + "\"></img>");

								FileTools.Delete(file); // 不要になったので削除する。
							}
#else
							lines.Add("<img src=\"Output_" + pattern.ToString("D4") + "_" + pct.ToString("D3") + "_" + c + ".png\"></img>");
#endif
							lines.Add("</td>");
						}
						lines.Add("</tr>");
					}
				}
				lines.Add("</table>");
				lines.Add("</body>");
				lines.Add("</html>");

				File.WriteAllLines(Path.Combine(dir, "table-pattern-0-1023-percentage.html"), lines, Encoding.UTF8);
			}

			ProcessTools.Batch(new string[] { "START " + dir });
		}

		private string LPad(string str, int minlen, char padding)
		{
			while (str.Length < minlen)
				str = padding + str;

			return str;
		}

		private string GetPngDataUrl(string file)
		{
			return "data:image/png;base64," + new Base64Unit().Encode(File.ReadAllBytes(file));
		}

		private Canvas EzExpand(Canvas src, int mul)
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

		// NextDouble
		// このメソッドによって返される乱数の実際の上限は0.99999999999999978 です。
		// @ https://docs.microsoft.com/ja-jp/dotnet/api/system.random.nextdouble?view=netframework-4.8

		// ====

		public static void MakeLikeADungeonMap(int pattern, double rate) // pattern: -1 ～ 1023, rate: 0.0 ～ 1.0
		{
			const int w = 100; // マップの幅
			const int h = 100; // マップの高さ

			Random rand = new Random();
			int[,] map = new int[w, h];

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					map[x, y] = rand.NextDouble() < rate ? 1 : 0;
				}
			}
			if (pattern != -1)
			{
				for (int c = 0; c < w * h * 30; c++)
				{
					int x = rand.Next(w);
					int y = rand.Next(h);
					int count = 0;

					for (int xc = -1; xc <= 1; xc++)
					{
						for (int yc = -1; yc <= 1; yc++)
						{
							count += map[(x + w + xc) % w, (y + h + yc) % h];
						}
					}
					map[x, y] = (pattern >> count) & 1;
				}
			}
			using (Bitmap bmp = new Bitmap(w, h))
			{
				for (int x = 0; x < w; x++)
				{
					for (int y = 0; y < h; y++)
					{
						bmp.SetPixel(x, y, map[x, y] == 0 ? Color.Blue : Color.Yellow);
					}
				}
				bmp.Save(@"C:\temp\LikeADungeonMap_Percentage.bmp", ImageFormat.Bmp); // 適当な場所にビットマップで出力する。
			}
		}
	}
}
