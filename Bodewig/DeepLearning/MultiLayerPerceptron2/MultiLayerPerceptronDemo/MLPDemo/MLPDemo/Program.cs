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
		public const string APP_IDENT = "{efc61c74-02ac-49b0-a040-e1b078cafe87}";
		public const string APP_TITLE = "MLPDemo";

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
			if (ar.ArgIs("/FA"))
			{
				throw null; // TODO
			}
			if (ar.ArgIs("/A3"))
			{
				throw null; // TODO
			}
			if (ar.ArgIs("/A5"))
			{
				throw null; // TODO
			}
			if (ar.ArgIs("/FB"))
			{
				throw null; // TODO
			}
		}
	}
}
