using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Text.RegularExpressions;
using System.IO;

namespace Charlotte
{
	public class MediaInfo
	{
		public class Stream
		{
			public int Index;
		}

		public class Audio : Stream
		{ }

		public class Video : Stream
		{ }

		public int TotalTimeCentisecond = -1;
		public List<Audio> AudioStreams = new List<Audio>();
		public List<Video> VideoStreams = new List<Video>();

		public MediaInfo(string file)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string outFile = wd.MakePath();

				ProcessTools.Batch(new string[]
				{
					string.Format(@"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffprobe.exe ""{0}"" 2> ""{1}""", file, outFile),
				});

				this.ReadOutFile(outFile);
				this.Check();
			}
		}

		private void ReadOutFile(string file)
		{
			string[] lines = File.ReadAllLines(file, Encoding.ASCII);

			foreach (string line in lines)
			{
				if (Regex.IsMatch(line, @"^\s*Duration: [0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{2},"))
				{
					string tLine = line.Trim();

					int h = int.Parse(tLine.Substring(10, 2));
					int m = int.Parse(tLine.Substring(13, 2));
					int s = int.Parse(tLine.Substring(16, 2));
					int c = int.Parse(tLine.Substring(19, 2));

					int t = h;
					t *= 60;
					t += m;
					t *= 60;
					t += s;
					t *= 100;
					t += c;

					this.TotalTimeCentisecond = t;
				}
				else if (Regex.IsMatch(line, @"^\s*Stream #0:[0-9]: (Audio|Video):"))
				{
					string tLine = line.Trim();

					int streamIndex = tLine[10] & 0x0f;
					bool isAudio = tLine[13] == 'A';

					if (isAudio)
					{
						this.AudioStreams.Add(new Audio()
						{
							Index = streamIndex,
						});
					}
					else
					{
						this.VideoStreams.Add(new Video()
						{
							Index = streamIndex,
						});
					}
				}
			}
		}

		private void Check()
		{
			if (this.TotalTimeCentisecond < 100 || IntTools.IMAX < this.TotalTimeCentisecond) // ? ! 1s ～
				throw new Exception("bad TotalTimeCentisecond: " + this.TotalTimeCentisecond);

			if (this.AudioStreams.Count + this.VideoStreams.Count == 0)
				throw new Exception("no Streams");
		}
	}
}
