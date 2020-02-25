using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MapDrawers;
using System.Drawing;
using Charlotte.Tools;
using System.IO;
using System.Drawing.Imaging;

namespace Charlotte.Tests.MapDrawers
{
	public class MapDrawerTest
	{
		public void Test01()
		{
			MapDrawer md = new MapDrawer();

			Test01_B(md.Draw(35.0, 135.0, 10.0));
			Test01_B(md.Draw(35.0, 135.0, 30.0));
			Test01_B(md.Draw(35.0, 135.0, 100.0));
			Test01_B(md.Draw(35.0, 135.0, 300.0));
		}

		private int ImageFileCount = 0;

		private void Test01_B(Image img)
		{
			new Canvas2(img).Save(string.Format(@"C:\temp\{0}.png", ImageFileCount++));
		}

		public void Test02()
		{
			//Test02b(35.0, 135.0, 10.0, 100.0, 100, 10, 0.5); // ng
			//Test02b(35.0, 135.0, 10.0, 100.0, 100, 10, 0.9); // ng
			//Test02b(35.0, 135.0, 10.0, 100.0, 100, 10, 1.0);
			Test02b(35.0, 135.0, 10.0, 600.0, 400, 20, 1.0);
		}

		private void Test02b(double lat, double lon, double mpdStart, double mpdEnd, int frameCount, int fps, double curveExp)
		{
			MapDrawer md = new MapDrawer();

			using (WorkingDir wd = new WorkingDir())
			{
				string imgsDir = wd.MakePath();

				FileTools.CreateDir(imgsDir);

				for (int frame = 0; frame < frameCount; frame++)
				{
					double rate = (double)frame / (frameCount - 1);
					double rate2 = GetRate2(rate);

					rate *= 2.0;
					rate -= 1.0;
					rate = UPow(rate, curveExp);
					rate += 1.0;
					rate *= 0.5;

					Console.WriteLine(frame + ", " + rate + ", " + rate2);

					//double mpd = mpdStart + (mpdEnd - mpdStart) * rate;
					double mpd = mpdStart + (mpdEnd - mpdStart) * rate * rate2;

					new Canvas2(md.Draw(lat, lon, mpd)).Save(Path.Combine(imgsDir, string.Format("{0}.jpg", frame)), ImageFormat.Jpeg, 100);
				}

				ProcessTools.Batch(new string[]
				{
					@"DEL C:\temp\Map.mp4",
					string.Format(@"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe -r {0} -i %%d.jpg C:\temp\Map.mp4", fps),
					@"START C:\temp",
				},
				imgsDir
				);
			}
		}

		private double GetRate2(double rate)
		{
			double b = 2.0;
			double e = 2.0;
			double x = Math.Pow(b, e);
			return (Math.Pow(b, rate * (e - 1.0) + 1.0) - b) / (x - b);
		}

		private static double UPow(double x, double y)
		{
			if (x < 0.0)
				return -Math.Pow(-x, y);
			else
				return Math.Pow(x, y);
		}

		public void Test03()
		{
			//Test03b(35.0, 135.0, 10.0, 30.0, 100, 10, 0.01, 0.1);
			//Test03b(35.0, 135.0, 30.0, 10.0, 100, 10, 0.01, 0.1);

			//Test03b(35.0, 135.0, 10.0, 600.0, 400, 20, 0.01, 0.1);

			Test03b(35.0, 135.0, 10.0, 600.0, 200, 20, 0.07, 0.1);
			//Test03b(35.0, 135.0, 10.0, 600.0, 200, 20, 0.05, 0.2);
			//Test03b(35.0, 135.0, 10.0, 600.0, 200, 20, 0.037, 0.3);
		}

		private void Test03b(double lat, double lon, double mpdStart, double mpdEnd, int frameCount, int fps, double acceleMax, double approachRate)
		{
			MapDrawer md = new MapDrawer();

			using (WorkingDir wd = new WorkingDir())
			{
				string imgsDir = wd.MakePath();

				FileTools.CreateDir(imgsDir);

				double mpd = mpdStart;
				double lastSpeed = 0.0;

				for (int frame = 0; frame < frameCount; frame++)
				{
					double speed = (mpdEnd - mpd) * approachRate;

					speed = AdjustSpeed(speed, lastSpeed, acceleMax);

					mpd += speed;
					lastSpeed = speed;

					Console.WriteLine(frame + ", " + mpd);

					new Canvas2(md.Draw(lat, lon, mpd)).Save(Path.Combine(imgsDir, string.Format("{0}.jpg", frame)), ImageFormat.Jpeg, 100);
				}

				ProcessTools.Batch(new string[]
				{
					@"DEL C:\temp\Map.mp4",
					string.Format(@"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe -r {0} -i %%d.jpg C:\temp\Map.mp4", fps),
					@"START C:\temp",
				},
				imgsDir
				);
			}
		}

		private static double AdjustSpeed(double speed, double lastSpeed, double acceleMax)
		{
			if (acceleMax < Math.Abs(speed) - Math.Abs(lastSpeed))
			{
				if (speed < lastSpeed)
					speed = lastSpeed - acceleMax;
				else
					speed = lastSpeed + acceleMax;
			}
			return speed;
		}
	}
}
