using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Charlotte
{
	public class WaveFileToSpectraMP4File
	{
		/// <summary>
		/// .wavファイルからオーディオ・スペクトラム(.mp4ファイル)を生成する。
		/// </summary>
		/// <param name="wavFile">入力.wavファイル</param>
		/// <param name="mp4File">出力.mp4ファイル</param>
		public void Conv(string wavFile, string mp4File)
		{
			const string FFMPEG_FILE = @"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe";
			const string WORK_DIR = @"C:\temp\wf2sMP4_tmp";
			const int FPS = 20;

			string mid_wavFile = Path.Combine(WORK_DIR, "audio.wav");
			string imagesDir = Path.Combine(WORK_DIR, "images");
			string videoFile = Path.Combine(WORK_DIR, "video.mp4");
			string movieFile = Path.Combine(WORK_DIR, "movie.mp4");

			Directory.CreateDirectory(WORK_DIR); // 存在しないと削除で例外投げる。
			Directory.Delete(WORK_DIR, true);
			Directory.CreateDirectory(WORK_DIR);
			Directory.CreateDirectory(imagesDir);

			File.Delete(mp4File);

			File.Copy(wavFile, mid_wavFile);

			int hz;
			double[][] wave = this.ReadWaveFile(mid_wavFile, out hz);
			double[][][] spectra = this.WaveToSpectra(wave, hz, FPS);
			this.SpectraToImageFiles(spectra, imagesDir);
			this.ImageFilesToVideoFile(FFMPEG_FILE, imagesDir, FPS, videoFile);
			this.MakeMovieFile(FFMPEG_FILE, videoFile, mid_wavFile, movieFile);

			File.Copy(movieFile, mp4File);

			Directory.Delete(WORK_DIR, true);
		}

		private double[][] ReadWaveFile(string wavFile, out int hz)
		{
			Func<FileStream, int, int> readInt = (reader, width) =>
			{
				int value = 0;

				for (int index = 0; index < width; index++)
					value |= (int)(byte)reader.ReadByte() << (index * 8);

				return value;
			};

			hz = -1;

			using (FileStream reader = new FileStream(wavFile, FileMode.Open, FileAccess.Read))
			{
				if (reader.ReadByte() != (byte)'R') throw new Exception("Header.RIFF[0] is not 'R'");
				if (reader.ReadByte() != (byte)'I') throw new Exception("Header.RIFF[1] is not 'I'");
				if (reader.ReadByte() != (byte)'F') throw new Exception("Header.RIFF[2] is not 'F'");
				if (reader.ReadByte() != (byte)'F') throw new Exception("Header.RIFF[3] is not 'F'");

				readInt(reader, 4); // size

				if (reader.ReadByte() != (byte)'W') throw new Exception("Header.WAVE[0] is not 'W'");
				if (reader.ReadByte() != (byte)'A') throw new Exception("Header.WAVE[1] is not 'A'");
				if (reader.ReadByte() != (byte)'V') throw new Exception("Header.WAVE[2] is not 'V'");
				if (reader.ReadByte() != (byte)'E') throw new Exception("Header.WAVE[3] is not 'E'");

				int channelNum = -1;
				int bitPerSample = -1;
				byte[] rawData = null;

				for (; ; )
				{
					int chr = reader.ReadByte();

					if (chr == -1)
						break;

					byte[] name = new byte[4];

					name[0] = (byte)chr;
					name[1] = (byte)reader.ReadByte();
					name[2] = (byte)reader.ReadByte();
					name[3] = (byte)reader.ReadByte();

					int size = readInt(reader, 4);

					if (size < 1)
						throw new Exception("Bad chunk-size");

					if (
						name[0] == (byte)'f' &&
						name[1] == (byte)'m' &&
						name[2] == (byte)'t' &&
						name[3] == (byte)' '
						)
					{
						if (size < 16)
							throw new Exception("Bad FMT chunk-size");

						if (hz != -1)
							throw new Exception("Has 2nd FMT");

						int formatID = readInt(reader, 2);
						channelNum = readInt(reader, 2);
						hz = readInt(reader, 4);
						int bytePerSec = readInt(reader, 4);
						int blockSize = readInt(reader, 2);
						bitPerSample = readInt(reader, 2);

						if (formatID != 1) throw new Exception("formatID is not PCM");
						if (channelNum != 1 && channelNum != 2) throw new Exception("Bad channelNum");
						if (hz < 1 && 2100000000 / 4 < hz) throw new Exception("Bad hz");
						if (bitPerSample != 8 && bitPerSample != 16) throw new Exception("Bad bitPerSample");
						if (blockSize != channelNum * bitPerSample / 8) throw new Exception("Bad blockSize");
						if (bytePerSec != hz * blockSize) throw new Exception("Bad bytePerSec");

						reader.Seek((long)size - 16, SeekOrigin.Current);
					}
					else if (
						name[0] == (byte)'d' &&
						name[1] == (byte)'a' &&
						name[2] == (byte)'t' &&
						name[3] == (byte)'a'
						)
					{
						if (rawData != null)
							throw new Exception("Has 2nd DATA");

						rawData = new byte[size];

						if (reader.Read(rawData, 0, size) != size)
							throw new Exception("Read DATA Error");
					}
					else
					{
						reader.Seek((long)size, SeekOrigin.Current);
					}
				}
				if (hz == -1) throw new Exception("No FMT");
				if (channelNum == -1) throw null; // 2bs
				if (bitPerSample == -1) throw null; // 2bs
				if (rawData == null) throw new Exception("No DATA");

				double[] linear;
				double[][] wave;

				if (bitPerSample == 8)
				{
					linear = new double[rawData.Length];

					for (int index = 0; index < rawData.Length; index++)
						linear[index] = ((int)rawData[index] - 128) / 128.0; // 8ビットの場合は符号なし整数
				}
				else // 16
				{
					if (rawData.Length % 2 != 0)
						throw new Exception("Bad DATA (rawData size)");

					linear = new double[rawData.Length / 2];

					for (int index = 0; index < rawData.Length / 2; index++)
						linear[index] = ((((int)rawData[index * 2] | ((int)rawData[index * 2 + 1] << 8)) ^ 32768) - 32768) / 32768.0; // 16ビットの場合は符号あり整数
				}
				if (channelNum == 1) // monoral
				{
					wave = new double[][]
					{
						new double[linear.Length],
						new double[linear.Length],
					};

					for (int index = 0; index < linear.Length; index++)
					{
						wave[0][index] = linear[index];
						wave[1][index] = linear[index];
					}
				}
				else // stereo
				{
					if (linear.Length % 2 != 0)
						throw new Exception("Bad DATA (linear size)");

					wave = new double[][]
					{
						new double[linear.Length / 2],
						new double[linear.Length / 2],
					};

					for (int index = 0; index < linear.Length / 2; index++)
					{
						wave[0][index] = linear[index * 2 + 0]; // 左側の波形値
						wave[1][index] = linear[index * 2 + 1]; // 右側の波形値
					}
				}
				return wave;
			}
		}

		private double[][][] WaveToSpectra(double[][] wave, int wave_hz, int fps)
		{
			double[] SPECTRUM_HZS = new double[]
			{
				// --https://en.wikipedia.org/wiki/Piano_key_frequencies

				27.50000, 29.13524, 30.86771,
				32.70320, 34.64783, 36.70810, 38.89087, 41.20344, 43.65353, 46.24930, 48.99943, 51.91309, 55.00000, 58.27047, 61.73541,
				65.40639, 69.29566, 73.41619, 77.78175, 82.40689, 87.30706, 92.49861, 97.99886, 103.8262, 110.0000, 116.5409, 123.4708,
				130.8128, 138.5913, 146.8324, 155.5635, 164.8138, 174.6141, 184.9972, 195.9977, 207.6523, 220.0000, 233.0819, 246.9417,
				261.6256, 277.1826, 293.6648, 311.1270, 329.6276, 349.2282, 369.9944, 391.9954, 415.3047, 440.0000, 466.1638, 493.8833,
				523.2511, 554.3653, 587.3295, 622.2540, 659.2551, 698.4565, 739.9888, 783.9909, 830.6094, 880.0000, 932.3275, 987.7666,
				1046.502, 1108.731, 1174.659, 1244.508, 1318.510, 1396.913, 1479.978, 1567.982, 1661.219, 1760.000, 1864.655, 1975.533,
				2093.005, 2217.461, 2349.318, 2489.016, 2637.020, 2793.826, 2959.955, 3135.963, 3322.438, 3520.000, 3729.310, 3951.066,
				4186.009,
			};

			//const double AUDIO_DELAY_SEC = 0.1;
			const double AUDIO_DELAY_SEC = 0.2;

			//const int WINDOW_SIZE = 20000;
			//const int WINDOW_SIZE = 15000;
			//const int WINDOW_SIZE = 10000;
			const int WINDOW_SIZE = 7000;
			//const int WINDOW_SIZE = 5000;
			//const int WINDOW_SIZE = 3000;
			//const int WINDOW_SIZE = 1000;

			List<double[]>[] spectra = new List<double[]>[]
			{
				new List<double[]>(),
				new List<double[]>(),
			};

			int waveLen = wave[0].Length;
			double waveSecLen = (double)waveLen / wave_hz;
			int frameCount = (int)(waveSecLen * fps);

			frameCount = Math.Max(1, frameCount); // 極端に短くても少なくとも1フレームは生成する。

			for (int frame = 0; frame < frameCount; frame++)
			{
				double sec = (double)frame / fps;
				sec += AUDIO_DELAY_SEC;
				int waveStartPos = (int)(sec * wave_hz);

				for (int side = 0; side < 2; side++)
				{
					double[] window = new double[WINDOW_SIZE];

					for (int offset = 0; offset < WINDOW_SIZE; offset++)
					{
						int wavePos = waveStartPos + offset - WINDOW_SIZE / 2;
						double value;

						if (0 <= wavePos && wavePos < waveLen)
						{
							double rate = (double)offset / (WINDOW_SIZE - 1);
							double hamming = 0.5 - 0.5 * Math.Cos(rate * Math.PI * 2.0);

							value = wave[side][wavePos] * hamming;
						}
						else
							value = 0.0;

						window[offset] = value;
					}
					double[] spectrum = new double[SPECTRUM_HZS.Length];

					for (int spHzIndex = 0; spHzIndex < SPECTRUM_HZS.Length; spHzIndex++)
					{
						double v00 = 0.0;
						double v90 = 0.0;

						for (int offset = 0; offset < WINDOW_SIZE; offset++)
						{
							double angle_00 = offset * SPECTRUM_HZS[spHzIndex] * (Math.PI * 2.0) / wave_hz;
							double angle_90 = angle_00 + (Math.PI * 0.5);
							double value = window[offset];

							v00 += value * Math.Sin(angle_00);
							v90 += value * Math.Sin(angle_90);
						}
						double v = v00 * v00 + v90 * v90;

						// v to 0-1 range
						{
							//v /= 50.0;
							//v /= 40.0;
							//v /= 30.0;
							//v /= 20.0;
							v /= 10.0;
							//v /= 5.0;
							//v /= 4.0;
							//v /= 3.0;
							//v /= 2.0;

							double r = 1.0;

							for (; ; )
							{
								r *= 0.9;

								double b = 1.0 - r;

								if (v <= b)
									break;

								v -= b;
								v *= 0.5;
								v += b;
							}
						}

						spectrum[spHzIndex] = v;
					}
					spectra[side].Add(spectrum);
				}
			}

			return new double[][][]
			{
				spectra[0].ToArray(),
				spectra[1].ToArray(),
			};
		}

		private void SpectraToImageFiles(double[][][] spectra, string imgsDir)
		{
			Brush[] SHADOW_SP_BRUSHES = new Brush[]
			{
				new SolidBrush(Color.FromArgb(50, 100, 100)),
				new SolidBrush(Color.FromArgb(100, 100, 50)),
			};

			Brush[] SP_BRUSHES = new Brush[]
			{
				new SolidBrush(Color.FromArgb(0, 255, 255)),
				new SolidBrush(Color.FromArgb(255, 255, 0)),
			};

			double SHADOW_FALL_SPEED = 0.01;

			int BAR_W = 7;
			int BAR_H = 400;
			int SP_LEN = spectra[0][0].Length;
			int SP_W = BAR_W * SP_LEN;
			int SP_H = BAR_H;
			int W = SP_W * 2;
			int H = SP_H;

			int frameCount = spectra[0].Length;

			double[][] shadowSpectra = new double[][]
			{
				new double[SP_LEN],
				new double[SP_LEN],
			};

			using (EncoderParameters eps = new EncoderParameters(1))
			using (EncoderParameter ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90L))
			{
				eps.Param[0] = ep;

				ImageCodecInfo ici = ImageCodecInfo.GetImageEncoders().First(v => v.FormatID == ImageFormat.Jpeg.Guid);

				for (int frame = 0; frame < frameCount; frame++)
				{
					using (Bitmap bmp = new Bitmap(W, H))
					{
						using (Graphics g = Graphics.FromImage(bmp))
						{
							for (int side = 0; side < 2; side++)
							{
								for (int spHzIndex = 0; spHzIndex < SP_LEN; spHzIndex++)
								{
									double v = shadowSpectra[side][spHzIndex];
									v = Math.Max(v - SHADOW_FALL_SPEED, spectra[side][frame][spHzIndex]);
									shadowSpectra[side][spHzIndex] = v;

									int l = side * SP_W + (spHzIndex + 0) * BAR_W;
									int r = side * SP_W + (spHzIndex + 1) * BAR_W;
									int t = (int)(BAR_H * (1.0 - v));
									int b = BAR_H;
									int w = r - l;
									int h = b - t;

									if (1 <= h)
										g.FillRectangle(SHADOW_SP_BRUSHES[side], l, t, w, h);

									v = spectra[side][frame][spHzIndex];
									t = (int)(BAR_H * (1.0 - v));
									h = b - t;

									if (1 <= h)
										g.FillRectangle(SP_BRUSHES[side], l, t, w, h);
								}
							}
						}
						bmp.Save(Path.Combine(imgsDir, frame + ".jpg"), ici, eps);
					}
				}
			}
		}

		private void ImageFilesToVideoFile(string ffmpegExe, string imgsDir, int fps, string videoFile)
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = ffmpegExe;
			psi.Arguments = "-r " + fps + " -i \"" + imgsDir + "\\%d.jpg\" \"" + videoFile + "\"";
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;

			Process.Start(psi).WaitForExit();
		}

		private void MakeMovieFile(string ffmpegExe, string videoFile, string wavFile, string movieFile)
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = ffmpegExe;
			psi.Arguments = "-i \"" + videoFile + "\" -i \"" + wavFile + "\" -map 0:0 -map 1:0 -vcodec copy -ab 160k \"" + movieFile + "\"";
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;

			Process.Start(psi).WaitForExit();
		}
	}
}
