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
		public const string APP_IDENT = "{6a0e2ad0-9464-4ff1-93d4-1ae59aee6118}";
		public const string APP_TITLE = "PngToCutTBJpg";

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

		private const string R_DIR = @"C:\temp\images";
		private const string W_DIR = @"C:\temp\imgs";

		private void Main2(ArgsReader ar)
		{
			FileTools.Delete(W_DIR);
			FileTools.CreateDir(W_DIR);

			for (int c = 1; c < 10000; c++)
			{
				Console.WriteLine("c: " + c); // test

				string rFile = Path.Combine(R_DIR, c + ".png");
				string wFile = Path.Combine(W_DIR, c + ".jpg");

				if (File.Exists(rFile))
				{
					Canvas2 src = new Canvas2(rFile);
					Canvas2 dest = new Canvas2(640, 270);

					using (Graphics g = dest.GetGraphics())
					{
						g.DrawImage(src.GetImage(), 0, 0, new Rectangle(0, 45, 640, 270), GraphicsUnit.Pixel);
					}
					dest.Save(wFile, ImageFormat.Jpeg, 90);
				}
			}
		}
	}
}
