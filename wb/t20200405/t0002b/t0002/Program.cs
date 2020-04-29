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
		public const string APP_IDENT = "{42ce69d5-b67d-4935-ace3-17af956de147}";
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
			Test01();
		}

		private void Test01()
		{
			Test01_a(@"C:\temp\jho.wav", @"C:\temp\jho.csv", @"C:\temp\jho.hz", @"C:\temp\jho.mp4");
			Test01_a(@"C:\temp\ddd.wav", @"C:\temp\ddd.csv", @"C:\temp\ddd.hz", @"C:\temp\ddd.mp4");
			Test01_a(@"C:\temp\hbn.wav", @"C:\temp\hbn.csv", @"C:\temp\hbn.hz", @"C:\temp\hbn.mp4");
			Test01_a(@"C:\temp\rlg.wav", @"C:\temp\rlg.csv", @"C:\temp\rlg.hz", @"C:\temp\rlg.mp4");
		}

		private const double SOUND_DELAY_SEC = 0.2;

		private const int DFT_SIZE = 1000;
		//private const int DFT_SIZE = 2000;
		//private const int DFT_SIZE = 3000;
		//private const int DFT_SIZE = 5000;
		//private const int FPS = 5;  // 再生に支障
		//private const int FPS = 10; // 再生に支障
		private const int FPS = 20;

#if false
		private const int BAR_W = 8;
		private const int BAR_SPAN = 10;
#elif false
		private const int BAR_W = 4;
		private const int BAR_SPAN = 5;
#else
		private const int BAR_W = 2;
		private const int BAR_SPAN = 3;
#endif
		private const int IMG_H = 300;

		private static int[] RetHzList = null;

		private void Test01_a(string rWavFile, string rCsvFile, string rHzFile, string wMP4File)
		{
			if (RetHzList == null)
			{
				List<int> dest = new List<int>();

#if false // 刻みが荒い...
				for (int hz = 10; hz < 10000; hz = (hz * 11) / 10)
					dest.Add(hz);
#elif false
				for (double hz = 20.0; hz < 10000.0; hz *= 1.05)
					dest.Add(DoubleTools.ToInt(hz));
#elif false
				for (int hz = 20; hz < 1000; hz += 20)
					dest.Add(hz);
#else
				for (int hz = 20; hz <=
					//2000
					//3000
					4200 // ピアノの周波数： 27.5 Hz ～ 4186.009 Hz
					; hz +=
					//10
					20
					)
					dest.Add(hz);
#endif

				RetHzList = dest.ToArray();
			}
			double[][] wavData;

			using (CsvFileReader reader = new CsvFileReader(rCsvFile))
			{
				wavData = reader.ReadToEnd().Select(v => new double[]
				{
					(int.Parse(v[0]) / 65536.0 - 0.5) * 2.0,
					(int.Parse(v[1]) / 65536.0 - 0.5) * 2.0,
				})
				.ToArray();
			}
			int src_hz = int.Parse(File.ReadAllText(rHzFile).Trim());

			if (src_hz < 1 || IntTools.IMAX < src_hz)
				throw null; // souteigai !!!

			using (WorkingDir wd = new WorkingDir())
			{
				string imgDir = wd.MakePath();
				string audioFile = wd.GetPath("audio.wav");
				string outputFile = wd.GetPath("output.mp4");

				FileTools.CreateDir(imgDir);
				File.Copy(rWavFile, audioFile);

				double[] lastSpctList = new double[RetHzList.Length];

				for (int i = 0; ; i++)
				{
					int wavDataPos = (i * src_hz) / FPS;

					if (wavData.Length <= wavDataPos)
						break;

					wavDataPos += DoubleTools.ToInt(SOUND_DELAY_SEC * src_hz);

					{
						int img_w = RetHzList.Length * BAR_SPAN;

						if (img_w % 2 == 1) // 幅(と高さも？)は２の倍数じゃないと ffmpeg は処理出来ない。
							img_w++;

						Canvas2 canvas = new Canvas2(img_w, IMG_H);

						using (Graphics g = canvas.GetGraphics(false))
						{
							DFT dftL = new DFT(wavData, wavDataPos, 0, src_hz, DFT_SIZE);
							DFT dftR = new DFT(wavData, wavDataPos, 1, src_hz, DFT_SIZE);
							int x = 0;

							for (int retHzIndex = 0; retHzIndex < RetHzList.Length; retHzIndex++)
							{
								double retHzRate = retHzIndex * 1.0 / (RetHzList.Length - 1);
								int ret_hz = RetHzList[retHzIndex];

								double spctL = dftL.Perform(ret_hz);
								double spctR = dftR.Perform(ret_hz);
								double spct = (spctL + spctR) / 2.0;

								//spct *= 3.0 + 7.0 * retHzRate;
								spct *= 4.0 + 8.0 * retHzRate;

								lastSpctList[retHzIndex] -= 10.0;
								lastSpctList[retHzIndex] = Math.Max(lastSpctList[retHzIndex], spct);

								int y = DoubleTools.ToInt(spct);
								int y2 = DoubleTools.ToInt(lastSpctList[retHzIndex]);

								g.FillRectangle(Brushes.Cyan, x, IMG_H - y2, BAR_W, y2);
								g.FillRectangle(Brushes.Orange, x, IMG_H - y, BAR_W, y);

								x += BAR_SPAN;
							}
						}
						canvas.Save(Path.Combine(imgDir, i + ".jpg"), ImageFormat.Jpeg, 100);
					}
				}

				ProcessTools.Batch(new string[]
				{
					string.Format(@"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe -r {0} -i %%d.jpg -i ..\{1} ..\{2}", FPS, Path.GetFileName(audioFile), Path.GetFileName(outputFile)),
				},
				imgDir
				);

				FileTools.Delete(wMP4File);
				File.Move(outputFile, wMP4File);
			}
		}
	}
}
