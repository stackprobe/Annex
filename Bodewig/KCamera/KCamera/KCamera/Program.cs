using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{210c760d-e2c6-43cf-8cb2-d6ca372fb76e}";
		public const string APP_TITLE = "KCamera";

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
			if (ar.ArgIs("/L"))
			{
				Camera.ShowList();
				return;
			}

			NamedEventUnit evStop = new NamedEventUnit(Consts.EV_STOP);

			using (Mutex procMtx = MutexTools.CreateGlobal(Consts.PROC_MTX_NAME))
			{
				if (ar.ArgIs("/S"))
				{
					for (int millis = 0; ; millis = Math.Min(millis + 1, 100))
					{
						evStop.Set();

						if (procMtx.WaitOne(0))
						{
							procMtx.ReleaseMutex();
							break;
						}
						Thread.Sleep(millis);
					}
				}
				else
				{
					bool diffValueMonitoringMode = ar.ArgIs("/M");

					string cameraNamePtn = ar.NextArg();
					string destDir = ar.NextArg();
					int quality = int.Parse(ar.NextArg());
					double diffValueBorder = double.Parse(ar.NextArg());
					int marginFrame = int.Parse(ar.NextArg());

					if (string.IsNullOrEmpty(cameraNamePtn))
						throw new ArgumentException("cameraNamePtn is null or empty");

					destDir = FileTools.MakeFullPath(destDir);

					FileTools.CreateDir(destDir);

					if (quality < 0 || 101 < quality)
						throw new ArgumentException("quality is not 0 ～ 101");

					if (marginFrame < 10 || IntTools.IMAX < marginFrame)
						throw new ArgumentException("marginFrame is not 10 ～ IMAX");

					if (procMtx.WaitOne(0))
					{
						{
							Camera camera = new Camera(cameraNamePtn, destDir, quality, diffValueBorder, diffValueMonitoringMode, marginFrame);

							camera.Start();

							while (Console.KeyAvailable == false && evStop.WaitForMillis(1000) == false)
							{ }

							camera.End();
						}

						procMtx.ReleaseMutex();
					}
				}
			}
		}
	}
}
