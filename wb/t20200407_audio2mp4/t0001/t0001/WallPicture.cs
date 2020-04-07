using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Charlotte
{
	public static class WallPicture
	{
		private const string IMG_TOOLS_EXE_FILE = @"C:\app\Kit\ImgTools\ImgTools.exe";

		private const int IMAGE_WH_MIN = 30;
		private const int IMAGE_WH_MAX = 900;

		private const bool 画像を二重に表示 = true;
		private const int 画像を二重に表示_MonitorW = 1920;
		private const int 画像を二重に表示_MonitorH = 1080;
		private const int 画像を二重に表示_ぼかし = 30; // 0 ～ 100
		private const int 画像を二重に表示_明るさ = 50; // 0 ～ 100

		public static Canvas2 Perform(string imgFile)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				return ConvImage(imgFile, wd.GetPath("ConvImageTmp_"));
			}
		}

		private static Canvas2 ConvImage(string rFile, string midPathBase)
		{
			int w;
			int h;

			try // 画像読み込みテスト
			{
				using (Bitmap.FromFile(rFile))
				{ }
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);

				throw new Exception("画像ファイル読み込みエラー");
			}

			using (Image bmp = Bitmap.FromFile(rFile))
			{
				w = bmp.Width;
				h = bmp.Height;

				bmp.Save(midPathBase + "1.bmp", ImageFormat.Bmp); // 透過を無効にしたいだけ、、、
			}

			ProcMain.WriteLog("w: " + w);
			ProcMain.WriteLog("h: " + h);

			if (w < 1 || IntTools.IMAX < w)
				throw new Exception("画像ファイルの幅に問題があります。");

			if (h < 1 || IntTools.IMAX < h)
				throw new Exception("画像ファイルの高さに問題があります。");

			{
				int mon_w = 画像を二重に表示_MonitorW;
				int mon_h = 画像を二重に表示_MonitorH;

				// 高さと幅はそれぞれ偶数でなければならない。
				mon_w &= ~1;
				mon_h &= ~1;

				int ww = mon_w;
				int hh;

				{
					long t = h;
					t *= mon_w;
					t /= w;

					if (mon_h <= t)
					{
						hh = (int)t;
					}
					else
					{
						hh = mon_h;

						t = w;
						t *= mon_h;
						t /= h;

						ww = (int)t;
					}
				}

				string aa;

				//if (画像を二重に表示_明るさ == 100)
				//aa = "";
				//else
				aa = ":" + 画像を二重に表示_明るさ.ToString("D2");

				int ll = (ww - mon_w) / 2;
				int tt = (hh - mon_h) / 2;

				Run(IMG_TOOLS_EXE_FILE +
					" /rf " + midPathBase + "1.bmp /wf " + midPathBase + "1w.png /e " + ww + " " + hh +
					" /C " + ll + " " + tt + " " + mon_w + " " + mon_h +
					" /BOKASHI 0 0 " + mon_w + " " + mon_h + " " + 画像を二重に表示_ぼかし + " 1 " +
					" /DOTFLTR A R" + aa + " G" + aa + " B" + aa
					);

				if (File.Exists(midPathBase + "1w.png") == false)
					throw new Exception("画像処理エラー(1w)");

				ww = mon_w;

				{
					long t = h;
					t *= mon_w;
					t /= w;

					if (t <= mon_h)
					{
						hh = (int)t;
					}
					else
					{
						hh = mon_h;

						t = w;
						t *= mon_h;
						t /= h;

						ww = (int)t;
					}
				}

				Run(IMG_TOOLS_EXE_FILE + " /rf " + midPathBase + "1.bmp /wf " + midPathBase + "1f.png /e " + ww + " " + hh);

				if (File.Exists(midPathBase + "1f.png") == false)
					throw new Exception("画像処理エラー(1f)");

				ll = (mon_w - ww) / 2;
				tt = (mon_h - hh) / 2;

				Run(IMG_TOOLS_EXE_FILE + " /rf " + midPathBase + "1w.png /wf " + midPathBase + "2.png /2 " + midPathBase + "1f.png /PASTE " + ll + " " + tt);
			}

			if (File.Exists(midPathBase + "2.png") == false)
				throw new Exception("画像処理エラー(ImgTools)");

			return new Canvas2(midPathBase + "2.png");
		}

		private static void Run(string command)
		{
			command += " 1> stdout.tmp 2> stderr.tmp";

			ProcMain.WriteLog("command: " + command);

			ProcessTools.Batch(new string[] { command });

			ProcMain.WriteLog("stdout.tmp ----> " + File.ReadAllText("stdout.tmp", StringTools.ENCODING_SJIS) + " <---- ここまで");
			ProcMain.WriteLog("stderr.tmp ----> " + File.ReadAllText("stderr.tmp", StringTools.ENCODING_SJIS) + " <---- ここまで");
		}
	}
}
