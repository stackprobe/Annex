﻿using System;
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
		private int MarginFrame;
		private int DelayCompareFrame;

		private VideoCaptureDevice VCD;

		private DiffValueLog DiffValueLog1 = new DiffValueLog();
		private DiffValueLog DiffValueLog2 = new DiffValueLog();

		public Camera(string cameraNamePtn, string destDir, int quality, double diffValueBorder, bool diffValueMonitoringMode, int marginFrame, int delayCompFrame)
		{
			this.CameraNamePtn = cameraNamePtn;
			this.DestDir = destDir;
			this.Quality = quality;
			this.DiffValueBorder = diffValueBorder;
			this.DiffValueMonitoringMode = diffValueMonitoringMode;
			this.MarginFrame = marginFrame;
			this.DelayCompareFrame = delayCompFrame;

			//FileTools.Delete(destDir);
			//FileTools.CreateDir(destDir);

			FilterInfoCollection fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			string monikerString = this.GetVideoCaptureDevice(fic).MonikerString;
			VideoCaptureDevice vcd = new VideoCaptureDevice(monikerString);

			this.VCD = vcd;

			this.DiffValueLog1.LogFile = Path.Combine(destDir, "DiffValueLog1.log");
			this.DiffValueLog2.LogFile = Path.Combine(destDir, "DiffValueLog2.log");
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
			public double DiffValue1;
			public double DiffValue2;
			public Bitmap Bmp;
		}

		private const int SW = 100;
		private const int SH = 100;

		private Bitmap LastSBmp = null;
		private Queue<BmpInfo> RecentlyBmps = new Queue<BmpInfo>();
		private Queue<Bitmap> DelayCompareSBmps = new Queue<Bitmap>();
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

				if (
					this.LastSBmp != null &&
					1 <= this.DelayCompareSBmps.Count &&
					this.IsDifferent(
						sBmp,
						this.LastSBmp,
						this.DelayCompareSBmps.Peek()
						)
					)
				{
					if (this.DiffValueMonitoringMode)
						ProcMain.WriteLog(string.Format("{0:F9} {1:F9} ====> DETECTED", this.LastDiffValue1, this.LastDiffValue2));
					else
						ProcMain.WriteLog("DETECTED");

					this.DetectedFrameCount = this.MarginFrame;

					if (this.LastDifferent1) MarkDetected(bmp, 0, Color.Red);
					if (this.LastDifferent2) MarkDetected(bmp, 1, Color.Green);
				}
				else
				{
					if (this.DiffValueMonitoringMode)
						ProcMain.WriteLog(string.Format("{0:F9} {1:F9}", this.LastDiffValue1, this.LastDiffValue2));
				}
				this.LastSBmp = sBmp;
				this.DelayCompareSBmps.Enqueue(sBmp);

				this.RecentlyBmps.Enqueue(new BmpInfo()
				{
					BmpDateTime = DateTime.Now,
					DiffValue1 = this.LastDiffValue1,
					DiffValue2 = this.LastDiffValue2,
					Bmp = bmp,
				});
			}

			if (1 <= this.DetectedFrameCount)
			{
				this.SaveRecentlyBmp(); // 1
				this.SaveRecentlyBmp(); // 2
				this.DetectedFrameCount--;
			}
			while (this.MarginFrame < this.RecentlyBmps.Count) // 2bs if -> while
				this.RecentlyBmps.Dequeue();

			while (this.DelayCompareFrame < this.DelayCompareSBmps.Count) // 2bs if -> while
				this.DelayCompareSBmps.Dequeue();

			GC.Collect();
		}

		private double LastDiffValue1 = 1.0;
		private double LastDiffValue2 = 1.0;
		private bool LastDifferent1 = false;
		private bool LastDifferent2 = false;

		private bool IsDifferent(Bitmap bmp, Bitmap bmp1, Bitmap bmp2)
		{
			this.LastDiffValue1 = this.GetDifferent(bmp, bmp1);
			this.LastDiffValue2 = this.GetDifferent(bmp, bmp2);

			this.LastDifferent1 = this.DiffValueBorder < this.LastDiffValue1;
			this.LastDifferent2 = this.DiffValueBorder < this.LastDiffValue2;

			this.DiffValueLog1.Add(this.LastDiffValue1);
			this.DiffValueLog2.Add(this.LastDiffValue2);

			return this.LastDifferent1 || this.LastDifferent2;
		}

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
			string file = Path.Combine(this.DestDir, string.Format("{0}-{1:D3}-{2:F9}-{3:F9}.png", dt, this.LDT_Millis, info.DiffValue1, info.DiffValue2));

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

		private static void MarkDetected(Bitmap bmp, int blockIndex, Color color)
		{
			const int MARK_WH = 5;

			int l = 0 + blockIndex * MARK_WH;
			int t = 0;

			for (int x = 0; x < MARK_WH; x++)
			{
				for (int y = 0; y < MARK_WH; y++)
				{
					bmp.SetPixel(l + x, t + y, color);
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

			this.DiffValueLog1.WriteToFile(Path.Combine(this.DestDir, "DiffValueDistribution1.log"));
			this.DiffValueLog2.WriteToFile(Path.Combine(this.DestDir, "DiffValueDistribution2.log"));
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
