using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class MovieMaker
	{
		public string AudioFile = @"res\Singing.mp3";
		public PictureList PicList = new PictureList();
		public string MovieFile = @"out\Movie.mp4"; // 出力

		// <---- prm

		public void Perform()
		{
			MediaInfo mi = new MediaInfo(this.AudioFile);

			this.PicList.TotalTimeCentisecond = mi.TotalTimeCentisecond;
			this.PicList.Init();
			this.PicList.Perform();

			ffmpegUtils.MakeMovieFile(this.AudioFile, this.PicList.OutputDir, this.MovieFile);
		}
	}
}
