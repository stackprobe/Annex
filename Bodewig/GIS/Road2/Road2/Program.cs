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
		public const string APP_IDENT = "{664fe27f-a8e1-49cc-908b-c02b2af5e1eb}";
		public const string APP_TITLE = "Road2";

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
			//Test01();
			Test02();
		}

		private void Test01()
		{
			foreach (string file in Directory.GetFiles(@"C:\wb\東京都地図", "*.xml", SearchOption.AllDirectories))
			{
				Console.WriteLine("*1 " + file);
				XmlNode.LoadFile(file);
				Console.WriteLine("*2");

				GC.Collect();
			}
		}

		private void Test02()
		{
			foreach (string file in Directory.GetFiles(@"C:\wb\東京都地図", "*.xml", SearchOption.AllDirectories))
			{
				XmlNode root = XmlNode.LoadFile(file);
			}
		}
	}
}
