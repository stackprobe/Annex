using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{34a89d1c-886f-4edc-8205-8146e2ea64a5}";
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
			//Test01();
			//Test02();
			//Test03();
			Test04();
		}

		private void Test01()
		{
			FilterInfoCollection fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);

			foreach (FilterInfo fi in fic)
			{
				Console.WriteLine(fi.Name);
				Console.WriteLine(fi.MonikerString);
			}
		}

		private void Test02()
		{
			FilterInfoCollection fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			string monikerString = fic[0].MonikerString;
			VideoCaptureDevice vcd = new VideoCaptureDevice(monikerString);

			FileTools.Delete(@"C:\temp\Camera");
			FileTools.CreateDir(@"C:\temp\Camera");

			int counter = 1;

			vcd.NewFrame += (object sender, NewFrameEventArgs ea) =>
			{
				ea.Frame.Save(@"C:\temp\Camera\" + counter++.ToString("D8") + ".png");
			};

			vcd.Start();

			Thread.Sleep(5000);

			vcd.Stop();
		}

		private void Test03()
		{
			FilterInfoCollection fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			string monikerString = fic[0].MonikerString;
			VideoCaptureDevice vcd = new VideoCaptureDevice(monikerString);

			FileTools.Delete(@"C:\temp\Camera");
			FileTools.CreateDir(@"C:\temp\Camera");

			int counter = 1;

			vcd.NewFrame += (object sender, NewFrameEventArgs ea) =>
			{
				ea.Frame.Save(@"C:\temp\Camera\" + counter++.ToString("D8") + ".png");
			};

			foreach (var capa in vcd.VideoCapabilities)
			{
				Console.WriteLine(capa.FrameSize.Width + ", " + capa.FrameSize.Height + ", " + capa.AverageFrameRate);
			}

			// 設定するには？
		}

		private void Test04()
		{
			FilterInfoCollection fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			string monikerString = fic[0].MonikerString;
			VideoCaptureDevice vcd = new VideoCaptureDevice(monikerString);

			FileTools.Delete(@"C:\temp\Camera");
			FileTools.CreateDir(@"C:\temp\Camera");

			object SYNCROOT = new object();
			Thread th = null;

			vcd.NewFrame += (object sender, NewFrameEventArgs ea) =>
			{
				lock (SYNCROOT)
				{
					if (th == null)
					{
						Bitmap bmp = new Bitmap(SW, SH);

						using (Graphics g = Graphics.FromImage(bmp))
						{
							g.InterpolationMode = InterpolationMode.HighQualityBicubic;
							g.DrawImage(ea.Frame, 0, 0, SW, SH);
						}

						th = new Thread(() =>
						{
							this.Captured(bmp);

							lock (SYNCROOT)
							{
								th = null;
							}
						});

						th.Start();
					}
				}
			};

			vcd.Start();

			while (Console.KeyAvailable == false)
				Thread.Sleep(100);

			vcd.Stop();

			for (int millis = 0; ; millis = Math.Min(millis + 1, 100))
			{
				lock (SYNCROOT)
				{
					if (th == null)
						break;
				}
				Thread.Sleep(millis);
			}
		}

		private const int SW = 100;
		private const int SH = 100;
		private Bitmap LastBmp = null;

		private void Captured(Bitmap bmp)
		{
			if (this.LastBmp != null)
			{
				double diff = this.GetDiffValue(bmp, this.LastBmp);

				Console.WriteLine(diff.ToString("F9"));
			}
			this.LastBmp = bmp;
		}

		private double GetDiffValue(Bitmap bmp1, Bitmap bmp2)
		{
			double ret = 0.0;

			for (int x = 0; x < SW; x++)
			{
				for (int y = 0; y < SH; y++)
				{
					Color color1 = bmp1.GetPixel(x, y);
					Color color2 = bmp2.GetPixel(x, y);

					double r = (double)color1.R - color2.R;
					double g = (double)color1.G - color2.G;
					double b = (double)color1.B - color2.B;

					r /= 255.0;
					g /= 255.0;
					b /= 255.0;

					for (int c = 0; c < 3; c++)
					{
						r *= r;
						g *= g;
						b *= b;
					}

					ret += r;
					ret += g;
					ret += b;
				}
			}
			return ret;

			// 変化無ければ 0.000001 ～ 0.000005 あたりを推移する。たまに 0.000010, 0.000015 とか出る。
			// ----> 0.0001 を変化有り無しのしきい値にすれば良いのでは...
		}
	}
}
