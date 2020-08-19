using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Drawing.Imaging;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{518c6a10-8cca-45ea-8a37-9ce1b2c2a3d4}";
		public const string APP_TITLE = "InsBlack28";

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

		private const string BLACK_BMP = @"C:\temp\black.bmp";
		private const string R_DIR = @"C:\temp\imgs";
		private const string W_DIR = @"C:\temp\imgs2";

		private void Main2(ArgsReader ar)
		{
			FileTools.Delete(W_DIR);
			FileTools.CreateDir(W_DIR);

			int wc;

			{
				Canvas2 black = new Canvas2(BLACK_BMP);

				for (wc = 1; wc <= 28; wc++)
				{
					string wFile = Path.Combine(W_DIR, wc + ".jpg");

					black.Save(wFile, ImageFormat.Jpeg, 90);
				}
			}

			for (int rc = 1; rc < 10000; rc++)
			{
				Console.WriteLine("rc: " + rc); // test

				string rFile = Path.Combine(R_DIR, rc + ".jpg");

				if (File.Exists(rFile))
				{
					string wFile = Path.Combine(W_DIR, wc + ".jpg");

					File.Copy(rFile, wFile);

					wc++;
				}
			}
		}
	}
}
