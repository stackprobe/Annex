using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public class ffmpegUtils
	{
		private const string _ffmpeg = @"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe";

		public static void MakeMovieFile(string audioFile, string picListDir, string movieFile)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string videoFile = wd.MakePath() + ".mp4";
				MakeVideoFile(picListDir, videoFile);

				MediaInfo mi = new MediaInfo(audioFile);

				File.Delete(movieFile);
				Run(string.Format(@"{0} -i ""{1}"" -i ""{2}"" -map 0:0 -map 1:{3} ""{4}""", _ffmpeg, videoFile, audioFile, mi.AudioStreams[0].Index, movieFile));

				if (File.Exists(movieFile) == false)
					throw null;
			}
		}

		public static void MakeVideoFile(string picListDir, string videoFile)
		{
			File.Delete(videoFile);
			Run(string.Format(@"{0} -r {1} -i ""{2}\%%08d.jpg"" ""{3}""", _ffmpeg, Consts.FPS, picListDir, videoFile));

			if (File.Exists(videoFile) == false)
				throw null;
		}

		public static void Run(string command)
		{
			ProcMain.WriteLog("command: " + command);

			File.Delete(@"tmp\1.tmp");
			File.Delete(@"tmp\2.tmp");

			ProcessTools.Batch(new string[]
			{
				command + @" 1> tmp\1.tmp 2> tmp\2.tmp",
			});

			ProcMain.WriteLog("stdout: " + File.ReadAllText(@"tmp\1.tmp", StringTools.ENCODING_SJIS) + " <---- ここまで");
			ProcMain.WriteLog("stderr: " + File.ReadAllText(@"tmp\2.tmp", StringTools.ENCODING_SJIS) + " <---- ここまで");
		}
	}
}
