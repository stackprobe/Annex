using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{160d61bf-0089-4209-9292-f946e7e581d9}";
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
			//new WaveFileToSpectraMP4File().Conv(@"C:\wb2\20200901_wav\pastel color1.wav", @"C:\temp\a.mp4");
			//new WaveFileToSpectraMP4File().Conv(@"C:\wb2\20200901_wav\short_song_shiho_shining_star.wav", @"C:\temp\a.mp4");
			//new WaveFileToSpectraMP4File().Conv(@"C:\wb2\20200901_wav\n28.wav", @"C:\temp\a.mp4");
			new WaveFileToSpectraMP4File().Conv(@"C:\wb2\20200901_wav\bgm_maoudamashii_piano_calendula.wav", @"C:\temp\a.mp4");
		}
	}
}
