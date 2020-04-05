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
		public const string APP_IDENT = "{56e576cb-2a4c-44f3-b35a-5d21a03b3ff4}";
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

		private const int DFT_SIZE = 1000;
		private const int FPS = 20;

		private const int IMG_H = 1000;

		private const double SPCT_HI_RATE = 0.01;

		private void Test01_a(string rWavFile, string rCsvFile, string rHzFile, string wMP4File)
		{
			double[][] wavData;

			using (CsvFileReader reader = new CsvFileReader(rCsvFile))
			{
				wavData = reader.ReadToEnd().Select(v => new double[]
				{
					int.Parse(v[0]) / 65535.0,
					int.Parse(v[1]) / 65535.0,
				})
				.ToArray();
			}
			int hz = int.Parse(File.ReadAllText(rHzFile).Trim());

			if (hz < DFT_SIZE * FPS || IntTools.IMAX < hz)
				throw null; // souteigai !!!

			using (WorkingDir wd = new WorkingDir())
			{
				string imgDir = wd.MakePath();
				string audioFile = wd.GetPath("audio.wav");
				string outputFile = wd.GetPath("output.mp4");

				FileTools.CreateDir(imgDir);
				File.Copy(rWavFile, audioFile);

				for (int i = 0; ; i++)
				{
					int wavDataPos = (i * hz) / FPS;

					if (wavData.Length < wavDataPos + DFT_SIZE)
						break;

					double[] lSpct = DFT.Perform(offset => wavData[wavDataPos + offset][0], DFT_SIZE);
					double[] rSpct = DFT.Perform(offset => wavData[wavDataPos + offset][1], DFT_SIZE);

					{
						Canvas2 canvas = new Canvas2(DFT_SIZE, IMG_H);

						using (Graphics g = canvas.GetGraphics())
						{
							for (int j = 0; j < DFT_SIZE; j++)
							{
								int l = IntTools.Range(DoubleTools.ToInt(lSpct[j] * SPCT_HI_RATE * IMG_H), 0, IMG_H);
								int r = IntTools.Range(DoubleTools.ToInt(rSpct[j] * SPCT_HI_RATE * IMG_H), 0, IMG_H);
								int m = (l + r) / 2;

								g.DrawLine(new Pen(Color.Orange), new Point(j, IMG_H - m), new Point(j, IMG_H));
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
