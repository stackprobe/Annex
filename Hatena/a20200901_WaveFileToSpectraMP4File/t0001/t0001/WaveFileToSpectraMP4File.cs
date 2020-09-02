using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class WaveFileToSpectraMP4File
	{
		public void Conv(string wavFile, string mp4File)
		{
			const string FFMPEG_FILE = @"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe";
			const string WORK_DIR = @"C:\temp\wf2sMP4_tmp";
			const int FPS = 20;

			string audioFile = Path.Combine(WORK_DIR, "audio.wav");
			string imagesDir = Path.Combine(WORK_DIR, "images");
			string videoFile = Path.Combine(WORK_DIR, "video.mp4");
			string movieFile = Path.Combine(WORK_DIR, "movie.mp4");

			Directory.CreateDirectory(WORK_DIR); // 存在しないと削除で例外投げる。
			Directory.Delete(WORK_DIR, true);
			Directory.CreateDirectory(WORK_DIR);
			Directory.CreateDirectory(imagesDir);

			File.Copy(wavFile, audioFile);

			int hz;
			double[][] wave = this.ReadWaveFile(audioFile, out hz);
			double[][][] spectra = this.WaveToSpectra(wave, hz, FPS);
			this.SpectraToImageFiles(spectra, imagesDir);
			this.ImageFilesToVideoFile(FFMPEG_FILE, imagesDir, FPS, videoFile);
			this.MakeMovieFile(FFMPEG_FILE, wavFile, videoFile, movieFile);

			File.Copy(movieFile, mp4File);
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
				27.500, 29.135, 30.868,
				32.703, 34.648, 36.708, 38.891, 41.203, 43.654, 46.249, 48.999, 51.913, 55.000, 58.270, 61.735,
				65.406, 69.296, 73.416, 77.782, 82.407, 87.307, 92.499, 97.999, 103.826, 110.000, 116.541, 123.471,
				130.813, 138.591, 146.832, 155.563, 164.814, 174.614, 184.997, 195.998, 207.652, 220.000, 233.082, 246.942,
				261.626, 277.183, 293.665, 311.127, 329.628, 349.228, 369.994, 391.995, 415.305, 440.000, 466.164, 493.883,
				523.251, 554.365, 587.330, 622.254, 659.255, 698.456, 739.989, 783.991, 830.609, 880.000, 932.328, 987.767,
				1046.502, 1108.731, 1174.659, 1244.508, 1318.510, 1396.913, 1479.978, 1567.982, 1661.219, 1760.000, 1864.655, 1975.533,
				2093.005, 2217.461, 2349.318, 2489.016, 2637.020, 2793.826, 2959.955, 3135.963, 3322.438, 3520.000, 3729.310, 3951.066,
				4186.009,
			};

			const double AUDIO_DELAY_SEC = 0.2;
			const int WINDOW_SIZE = 1000;

			List<double[]>[] spectra = new List<double[]>[]
			{
				new List<double[]>(),
				new List<double[]>(),
			};

			int waveLen = wave[0].Length;
			double waveSecLen = (double)waveLen / wave_hz;
			int frameCount = (int)(waveSecLen * fps);

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
						int wavePos = waveStartPos + offset;
						double value;

						if (wavePos < waveLen)
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
						spectrum[spHzIndex] = v00 * v00 + v90 * v90;
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
			throw null; // TODO
		}

		private void ImageFilesToVideoFile(string ffmpegExe, string imgsDir, int fps, string videoFile)
		{
			throw null; // TODO
		}

		private void MakeMovieFile(string ffmpegExe, string wavFile, string videoFile, string movieFile)
		{
			throw null; // TODO
		}
	}
}
