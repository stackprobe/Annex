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
		public const string APP_IDENT = "{608a52a0-dcae-4f7a-8100-46690717ca22}";
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
			Test01();
		}

		private void Test01()
		{
			WallPicture.Perform(
				//////////////////////////////////////////////////////////// $_git:secret
				).Save(@"C:\temp\1.png");
			WallPicture.Perform(
				/////////////////////////////////////////////////////////////////////////////// $_git:secret
				).Save(@"C:\temp\2.png");
			WallPicture.Perform(
				//////////////////////////////////////////////////////////////////////////////////////// $_git:secret
				).Save(@"C:\temp\3.png");
		}
	}
}
