﻿using System;
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
		public const string APP_IDENT = "{c58318ef-d6cb-4a19-a295-12d72818fa1d}";
		public const string APP_TITLE = "DenebolaToolsTest";

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
			Test01("aaa", @"C:\aaa");

			Test01("ゆきだるま☃ゆきだるま⛄トレードマーク™", @"C:\temp");

			Test01("****aaaa????bbbb////.txt", @"C:\temp");

			Test01("長いパス_あああああいいいいいうううううえええええおおおお_あああああいいいいいうううううえええええおおおお.dat", @"C:\LongLongPath_aaaaaaaaaabbbbbbbbbbaaaaaaaaaabbbbbbbbbbaaaaaaaaaabbbbbbbbbbaaaaaaaaaabbbbbbbbbbaaaaaaaaaabbbbbbbbbb_aaaaaaaaaabbbbbbbbbbaaaaaaaaaabbbbbbbbbbaaaaaaaaaabbbbbbbbbbaaaaaaaaaabbbbbbbbbbaaaaaaaaaabbbbbbbbbb");

			Test01("aaa", "");
			Test01("", "aaa");
			Test01("", "");

			Test01("\n", "\n");
			Test01("\t", "\t");
			Test01("\n\t", "\n\t");
			Test01("\t\n", "\t\n");

			Test02(@"C:\aaa");
			Test02(@"C:\bbb\ccc日本語あいうえお");

			Test02_Catch(@"C:\temp\ゆきだるま☃ゆきだるま⛄トレードマーク™");
			Test02_Catch(@"C:\temp\..\..\..\****aaaa????bbbb////.txt");
		}

		private void Test01(string localPath, string dir)
		{
			string ret = DenebolaToolkit.GetFairLocalPath(localPath, dir);

			Console.WriteLine("localPath: [" + localPath + "]");
			Console.WriteLine("dir: [" + dir + "]");
			Console.WriteLine("ret: [" + ret + "]");
			Console.WriteLine("");
		}

		private void Test02_Catch(string path)
		{
			try
			{
				Test02(path);
			}
			catch (Exception e)
			{
				Console.WriteLine("cought_message: " + e.Message);
			}
		}

		private void Test02(string path)
		{
			string ret = DenebolaToolkit.GetFairFullPath(path);

			Console.WriteLine("path: [" + path + "]");
			Console.WriteLine("ret: [" + ret + "]");
			Console.WriteLine("");
		}
	}
}
