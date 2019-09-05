using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Threading;

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
			using (Mutex procMtx = MutexTools.CreateGlobal(Consts.PROC_MTX_NAME))
			{
				Ground.I = new Ground();

				if (ar.ArgIs("/S"))
				{
					for (int millis = 0; ; millis = Math.Min(millis + 1, 100))
					{
						Ground.I.EvStop.Set();

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
					if (procMtx.WaitOne(0))
					{
						{
							Camera camera = new Camera();

							camera.Start();

							Ground.I.EvStop.WaitForever();

							camera.End();
						}

						procMtx.ReleaseMutex();
					}
				}
			}
		}
	}
}
