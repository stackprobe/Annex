using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Drawing.Drawing2D;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;

namespace Charlotte
{
	public class Camera
	{
		private string CameraNamePtn;
		private string DestDir;
		private int Quality; // 0 ～ 100 : JPEG , 101 : PNG
		private double DiffValueBorder;
		private bool DiffValueMonitoringMode;

		private VideoCaptureDevice VCD;

		public Camera(string cameraNamePtn, string destDir, int quality, double diffValueBorder, bool diffValueMonitoringMode)
		{
			this.CameraNamePtn = cameraNamePtn;
			this.DestDir = destDir;
			this.Quality = quality;
			this.DiffValueBorder = diffValueBorder;
			this.DiffValueMonitoringMode = diffValueMonitoringMode;

			//FileTools.Delete(destDir);
			//FileTools.CreateDir(destDir);

			FilterInfoCollection fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			string monikerString = this.GetVideoCaptureDevice(fic).MonikerString;
			VideoCaptureDevice vcd = new VideoCaptureDevice(monikerString);

			this.VCD = vcd;
		}

		private FilterInfo GetVideoCaptureDevice(FilterInfoCollection fic)
		{
			for (int index = 0; ; index++)
			{
				FilterInfo fi = fic[index];

				if (StringTools.ContainsIgnoreCase(fi.Name, this.CameraNamePtn))
				{
					return fi;
				}
			}
		}

		public void Start()
		{
			DiffValueLog.Clear();

			this.VCD.NewFrame += (object sender, NewFrameEventArgs ea) =>
			{
				this.NewFrameGot(ea.Frame);
			};

			this.VCD.Start();
		}

		private static object SYNCROOT = new object();
		private Thread Th = null;
		private bool Ended = false;

		private void NewFrameGot(Bitmap bmp)
		{
			lock (SYNCROOT)
			{
				if (this.Ended) // 2bs
					return;

				if (this.Th != null)
					return;
			}

			// bmp 複製
			{
				int w = bmp.Width;
				int h = bmp.Height;

				Bitmap bmp2 = new Bitmap(w, h);

				using (Graphics g = Graphics.FromImage(bmp2))
				{
					g.DrawImage(bmp, 0, 0, w, h);
				}
				bmp = bmp2;
			}

			lock (SYNCROOT)
			{
				if (this.Ended) // 2bs
					return;

				this.Th = new Thread(() =>
				{
					try
					{
						this.NewFrameGotTh(bmp);
					}
					catch (Exception e)
					{
						ProcMain.WriteLog(e);
					}

					lock (SYNCROOT)
					{
						this.Th = null;
					}
				});
			}

			this.Th.Start();
		}

		private class BmpInfo
		{
			public DateTime BmpDateTime;
			public double DiffValue;
			public Bitmap Bmp;
		}

		private const int FRAME_MARGIN_BACKWARD = 200;
		private const int FRAME_MARGIN_FORWARD = 210; // FRAME_MARGIN_BACKWARD < であること。

		private const int SW = 100;
		private const int SH = 100;

		private Bitmap LastSBmp = null;
		private Queue<BmpInfo> RecentlyBmps = new Queue<BmpInfo>();
		private int DetectedFrameCount = 0;

		private void NewFrameGotTh(Bitmap bmp)
		{
			{
				Bitmap sBmp = new Bitmap(SW, SH);

				using (Graphics g = Graphics.FromImage(sBmp))
				{
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.DrawImage(bmp, 0, 0, SW, SH);
				}

				if (this.LastSBmp != null && this.IsDifferent(sBmp, this.LastSBmp))
				{
					if (this.DiffValueMonitoringMode)
						ProcMain.WriteLog(this.LastDiffValue.ToString("F9") + " ====> DETECTED");
					else
						ProcMain.WriteLog("DETECTED");

					this.DetectedFrameCount = FRAME_MARGIN_FORWARD;

					MarkDetected(bmp);
				}
				else
				{
					if (this.DiffValueMonitoringMode)
						ProcMain.WriteLog(this.LastDiffValue.ToString("F9"));
				}
				this.LastSBmp = sBmp;
			}

			this.RecentlyBmps.Enqueue(new BmpInfo()
			{
				BmpDateTime = DateTime.Now,
				DiffValue = this.LastDiffValue,
				Bmp = bmp,
			});

			if (1 <= this.DetectedFrameCount)
			{
				this.SaveRecentlyBmp(); // 1
				this.SaveRecentlyBmp(); // 2
				this.DetectedFrameCount--;
			}
			while (FRAME_MARGIN_BACKWARD < this.RecentlyBmps.Count) // 2bs -> while
				this.RecentlyBmps.Dequeue();

			GC.Collect();
		}

		private bool IsDifferent(Bitmap bmp1, Bitmap bmp2)
		{
			return this.DiffValueBorder < this.GetDifferent(bmp1, bmp2);
			//return 0.00003 < this.GetDifferent(bmp1, bmp2);
		}

		private double LastDiffValue = 1.0;

		private double GetDifferent(Bitmap bmp1, Bitmap bmp2)
		{
			double diffValue = 0.0;

			for (int x = 0; x < SW; x++)
			{
				for (int y = 0; y < SH; y++)
				{
					Color color1 = bmp1.GetPixel(x, y);
					Color color2 = bmp2.GetPixel(x, y);

					diffValue += GetDifferent(color1.R, color2.R);
					diffValue += GetDifferent(color1.G, color2.G);
					diffValue += GetDifferent(color1.B, color2.B);
				}
			}
			DiffValueLog.Add(diffValue);
			this.LastDiffValue = diffValue;
			return diffValue;
		}

		private static double GetDifferent(byte v1, byte v2)
		{
			double d = (double)v1 - v2;

			d /= 255.0;

			d *= d;
			d *= d;
			d *= d;

			return d;
		}

		private string LastDateTime = null;
		private int LDT_Millis = 0;

		private void SaveRecentlyBmp()
		{
			if (this.RecentlyBmps.Count == 0)
				return;

			BmpInfo info = this.RecentlyBmps.Dequeue();
			string dt = info.BmpDateTime.ToString("yyyy-MM-dd_HH-mm-ss");

			if (this.LastDateTime == dt)
			{
				this.LDT_Millis++;
			}
			else
			{
				this.LDT_Millis = 0;
				this.LastDateTime = dt;
			}
			string file = Path.Combine(this.DestDir, string.Format("{0}-{1:D3}-{2:F9}.png", dt, this.LDT_Millis, info.DiffValue));

			if (this.Quality == 101)
			{
				info.Bmp.Save(file, ImageFormat.Png);
			}
			else
			{
				using (EncoderParameters eps = new EncoderParameters(1))
				using (EncoderParameter ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)this.Quality))
				{
					eps.Param[0] = ep;
					ImageCodecInfo ici = GetICI(ImageFormat.Jpeg);
					info.Bmp.Save(file, ici, eps);
				}
			}
		}

		private static ImageCodecInfo GetICI(ImageFormat imgFmt)
		{
			return (from ici in ImageCodecInfo.GetImageEncoders() where ici.FormatID == imgFmt.Guid select ici).ToList()[0];
		}

		private static void MarkDetected(Bitmap bmp)
		{
			for (int x = 0; x < 5; x++)
			{
				for (int y = 0; y < 5; y++)
				{
					bmp.SetPixel(x, y, Color.Red);
				}
			}
		}

		public void End()
		{
			this.Ended = true;

			this.VCD.Stop();

			for (int millis = 0; ; millis = Math.Min(millis + 1, 100))
			{
				lock (SYNCROOT)
				{
					if (this.Th == null)
						break;
				}
				Thread.Sleep(millis);
			}

			DiffValueLog.WriteToFile(Path.Combine(this.DestDir, "DiffValueLog.log"));
		}

		public static void ShowList()
		{
			FilterInfoCollection fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);

			foreach (FilterInfo fi in fic)
			{
				Console.WriteLine(fi.Name + " ====> " + fi.MonikerString);
			}
		}
	}
}
