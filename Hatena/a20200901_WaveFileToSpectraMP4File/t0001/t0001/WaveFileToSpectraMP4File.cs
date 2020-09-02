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
			const int VIDEO_FPS = 20;
			const string TMP_WAVE_FILE = @"C:\temp\a.wav";
			const string TMP_IMAGES_DIR = @"C:\temp\imgs";
			const string TMP_VIDEO_FILE = @"C:\temp\video.mp4";
			const string TMP_MOVIE_FILE = @"C:\temp\movie.mp4";
			const string FFMPEG_EXE = @"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe";

			// cleanup
			{
				File.Delete(TMP_WAVE_FILE);
				Directory.CreateDirectory(TMP_IMAGES_DIR); // 存在しないと削除で例外投げる。
				Directory.Delete(TMP_IMAGES_DIR, true);
				Directory.CreateDirectory(TMP_IMAGES_DIR);
				File.Delete(TMP_VIDEO_FILE);
				File.Delete(TMP_MOVIE_FILE);
			}

			File.Copy(wavFile, TMP_WAVE_FILE);

			int hz;
			double[][] wave = this.ReadWaveFile(TMP_WAVE_FILE, out hz);
			double[][] spectra = this.WaveToSpectra(wave, hz, VIDEO_FPS);
			this.SpectraToImageFiles(spectra, TMP_IMAGES_DIR);
			this.ImageFilesToVideoFile(FFMPEG_EXE, TMP_IMAGES_DIR, VIDEO_FPS, TMP_VIDEO_FILE);
			this.MakeMovieFile(FFMPEG_EXE, wavFile, TMP_VIDEO_FILE, TMP_MOVIE_FILE);

			File.Copy(TMP_MOVIE_FILE, mp4File);
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
						linear[index] = ((int)rawData[index] - 128) / 128.0;
				}
				else // 16
				{
					if (rawData.Length % 2 != 0)
						throw new Exception("Bad DATA (rawData size)");

					linear = new double[rawData.Length / 2];

					for (int index = 0; index < rawData.Length / 2; index++)
						linear[index] = ((((int)rawData[index * 2] | ((int)rawData[index * 2 + 1] << 8)) ^ 32768) - 32768) / 32768.0;
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
						wave[0][index] = linear[index * 2 + 0];
						wave[1][index] = linear[index * 2 + 1];
					}
				}
				return wave;
			}
		}

		private double[][] WaveToSpectra(double[][] wave, int hz, int fps)
		{
			throw null; // TODO
		}

		private void SpectraToImageFiles(double[][] spectra, string imgsDir)
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
