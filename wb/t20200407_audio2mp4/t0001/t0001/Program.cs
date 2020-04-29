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
		public const string APP_IDENT = "{608a52a0-dcae-4f7a-8100-46690717ca22}";
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
			Test02();
		}

		private void Test02()
		{
			Canvas2 canvas = new Canvas2(@"C:\wb2\20200408_Wall\w0001.png");
			string wavFile = @"C:\var\mp4\mp4\hbn.wav";
			//string wavFile = @"C:\Dev\wb\t20200330_時報\media\jihou-sine-3f.wav"; // test
			string mp4File = @"C:\temp\1.mp4";

			List<double[]> spctList = new List<double[]>();

			using (WorkingDir wd = new WorkingDir())
			{
				string imgDir = wd.GetPath("img");
				FileTools.CreateDir(imgDir);

				WavFileSpectrum wfSpct = new WavFileSpectrum(wavFile);

				for (double sec = 0.0; sec < wfSpct.GetLength() * 1.0 / wfSpct.GetHz(); sec += 1.0 / 20.0)
				{
					Console.WriteLine("sec: " + sec);

					double[] spctL = wfSpct.GetSpectrum_L(DoubleTools.ToInt(sec * wfSpct.GetHz()));
					double[] spctR = wfSpct.GetSpectrum_R(DoubleTools.ToInt(sec * wfSpct.GetHz()));

					for (int i = 0; i < spctL.Length; i++)
						spctL[i] += spctR[i];

					spctList.Add(spctL);
				}
				for (int i = 0; i < spctList.Count; i++)
				{
					double[] spct = spctList[i];

					Canvas2 cvs = new Canvas2(canvas.GetWidth(), canvas.GetHeight());
					using (Graphics g = cvs.GetGraphics(false))
					{
						g.Clear(Color.Transparent);

						const double Y_EXP = 5.0;
						int yMax = -1;

						for (int idx = 0; idx < spct.Length; idx++)
						{
							int x1 = (cvs.GetWidth() * (idx + 0)) / spct.Length;
							int x2 = (cvs.GetWidth() * (idx + 1)) / spct.Length;
							int y = DoubleTools.ToInt(spct[idx] * Y_EXP);

							yMax = Math.Max(yMax, y);

							g.FillRectangle(new SolidBrush(Color.FromArgb(128, 255, 255, 255)), x1, 0, x2 - x1, y);
						}
						Console.WriteLine("yMax: " + yMax);
					}
					Canvas2 cvs2 = new Canvas2(canvas.GetBytes());
					using (Graphics g = cvs2.GetGraphics())
					{
						g.DrawImage(cvs.GetImage(), 0, 0);
					}
					cvs2.Save(Path.Combine(imgDir, i + ".jpg"), ImageFormat.Jpeg, 90);
				}

				string audioFile = wd.GetPath("audio.mp4");
				string outputFile = wd.GetPath("output.mp4");

				File.Copy(wavFile, audioFile);

				ProcessTools.Batch(new string[]
				{
					string.Format(@"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe -r 20 -i %%d.jpg -i ..\{0} ..\{1}", Path.GetFileName(audioFile), Path.GetFileName(outputFile)),
				},
				imgDir
				);

				FileTools.Delete(mp4File);
				File.Move(outputFile, mp4File);
			}
		}

		private void Test01()
		{
			WallPicture.Perform(
				//////////////////////////////////////////////////////////// $_git:secret
				).Save(@"C:\temp\1.png");
			WallPicture.Perform(
				/////////////////////////////////////////////////////////////////////////////// $_git:secret
				).Save(@"C:\temp\2.png");
			WallPicture.Perform(
				//////////////////////////////////////////////////////////////////////////////////////// $_git:secret
				).Save(@"C:\temp\3.png");
			WallPicture.Perform(
				///////////////////////////////////////////////////////////////// $_git:secret
				).Save(@"C:\temp\4.png");
			WallPicture.Perform(
				//////////////////////////////////////////////////////////// $_git:secret
				).Save(@"C:\temp\5.png");
		}
	}
}
