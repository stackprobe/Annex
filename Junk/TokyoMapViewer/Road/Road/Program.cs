using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{39bbffa2-fd31-4035-b648-801028c8697d}";
		public const string APP_TITLE = "Road";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			Console.WriteLine("Press ENTER.");
			Console.ReadLine();
#endif
		}

		private void Main2(ArgsReader ar)
		{
			MapLoader ml = new MapLoader();

			ml.Load();

			Map m = ml.Map;

			// ----

			//new BranchMap01.Test0001() { Map = m }.Test01();
			//new NodeMap01.Test0001() { Map = m }.Test01();
			//new NodeMap01.Test0001() { Map = m }.Test02();
			//new NodeMap01.Test0001() { Map = m }.Test03();
			new NodeMap01.Test0001() { Map = m }.Test04();
		}
	}
}
