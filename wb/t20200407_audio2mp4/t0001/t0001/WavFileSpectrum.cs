using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public class WavFileSpectrum
	{
		private const string WAV2CSV_EXE_FILE = @"C:\Dev\Annex\wb\t20200407_audio2mp4\Tools\wav2csv.exe";

		private List<double> WavDataL = new List<double>();
		private List<double> WavDataR = new List<double>();
		private int WavHz;

		public WavFileSpectrum(string wavFile)
		{
			if (File.Exists(WAV2CSV_EXE_FILE) == false)
				throw new Exception("no wav2csv.exe");

			if (wavFile == null || File.Exists(wavFile) == false)
				throw new Exception("Bad wavFile: " + wavFile);

			using (WorkingDir wd = new WorkingDir())
			{
				string csvFile = wd.MakePath();
				string hzFile = wd.MakePath();

				ProcessTools.Batch(new string[]
				{
					string.Format("{0} \"{1}\" {2} {3}", WAV2CSV_EXE_FILE, wavFile, csvFile, hzFile),
				},
				"",
				ProcessTools.WindowStyle_e.MINIMIZED
				);

				using (CsvFileReader reader = new CsvFileReader(csvFile))
				{
					for (; ; )
					{
						string[] row = reader.ReadRow();

						if (row == null)
							break;

						this.WavDataL.Add((int.Parse(row[0]) / 65536.0 - 0.5) * 2.0);
						this.WavDataR.Add((int.Parse(row[1]) / 65536.0 - 0.5) * 2.0);
					}
				}
				this.WavHz = int.Parse(File.ReadAllText(hzFile));

				if (this.WavHz < 1 || IntTools.IMAX < this.WavHz)
					throw new Exception("Bad WavHz: " + this.WavHz);
			}
		}

		public double[] GetSpectrum_L(int startIndex)
		{
			return this.GetSpectrum(startIndex, this.WavDataL);
		}

		public double[] GetSpectrum_R(int startIndex)
		{
			return this.GetSpectrum(startIndex, this.WavDataR);
		}

		private const double SOUND_DELAY_SEC = 0.2;
		private const int WINDOW_SIZE = 1000;

		private double[] GetSpectrum(int startIndex, List<double> wavData)
		{
			double[] wavPart = new double[WINDOW_SIZE];

			for (int offset = 0; offset < WINDOW_SIZE; offset++)
				wavPart[offset] = wavData[IntTools.Range(startIndex + (int)(WavHz * SOUND_DELAY_SEC) + offset, 0, wavData.Count - 1)];

			List<double> spectrum = new List<double>();

			for (int hz = 20; hz <= 4200; hz += 20)
			{
				double monHz = hz * (Math.PI * 2.0) / this.WavHz;
				double cc = 0.0;
				double ss = 0.0;

				for (int offset = 0; offset < WINDOW_SIZE; offset++)
				{
					double aa = monHz * offset;
					double vv = wavPart[offset];
					double rate = offset * 1.0 / (WINDOW_SIZE - 1);
					double hh = Hamming(rate);

					cc += Math.Cos(aa) * vv * hh;
					ss += Math.Sin(aa) * vv * hh;
				}
				spectrum.Add(cc * cc + ss * ss);
			}
			return spectrum.ToArray();
		}

		private static double Hamming(double rate)
		{
			return 0.5 - 0.5 * Math.Cos(rate * Math.PI * 2.0);
		}

		public int GetLength()
		{
			return this.WavDataL.Count;
		}

		public int GetHz()
		{
			return this.WavHz;
		}
	}
}
