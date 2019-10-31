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
		public const string APP_IDENT = "{501a779b-ae46-4867-a829-a86f8a598133}";
		public const string APP_TITLE = "t0002";

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
			Test01(@"C:\var2\res\img\GameFrame.png", @"C:\temp\GameFrame.png");
		}

		private void Test01(string rFile, string wFile)
		{
			Canvas rCanvas = new Canvas(rFile);
			int w = rCanvas.GetWidth();
			int h = rCanvas.GetHeight();
			Canvas wCanvas = new Canvas(w, h);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					Color color = Color.FromArgb(0, 0, 0, 0);

					if (rCanvas.Get(x, y).B != 0)
					{
						double rate = (x + y) * 1.0 / (w + h - 2);

						rate = 1.0 - rate;

						color = Color.FromArgb(
							(int)(40 + 10 * rate),
							(int)(40 + 20 * rate),
							(int)(40 + 30 * rate)
							);
					}
					wCanvas.Set(x, y, color);
				}
			}
			wCanvas.Save(wFile);
		}
	}
}
