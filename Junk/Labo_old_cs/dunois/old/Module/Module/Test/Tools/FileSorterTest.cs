using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;
using System.Diagnostics;

namespace Charlotte.Test.Tools
{
	public class FileSorterTest
	{
		public static void Test01()
		{
			Test01(1000000, 3); // zantei

			Test01(0, 0);

			for (int linecnt = 1; linecnt <= 10; linecnt++)
			{
				for (int chrcnt = 1; chrcnt <= 10; chrcnt++)
				{
					Test01(linecnt, chrcnt);
				}
			}

			Test01(1, 100);
			Test01(10, 100);
			Test01(100, 100);

			Test01(100, 1);
			Test01(100, 10);
			Test01(100, 100);

			Test01(1000, 1);
			Test01(10000, 1);
			Test01(100000, 1);
			Test01(1000000, 1);
			Test01(3000000, 1);
			Test01(10000000, 1);
			Test01(30000000, 1);

			Test01(1000000, 3);
			Test01(3000000, 3);
			Test01(1000000, 4);
			Test01(3000000, 4);
			Test01(1000000, 5);
			Test01(3000000, 5);

			Test01(1000, 30);
			Test01(10000, 30);
			Test01(100000, 30);
			Test01(1000000, 30);

			Test01(1000, 1000);
			Test01(10000, 1000);
			Test01(100000, 1000);
			Test01(1000000, 1000);
		}

		private static void Test01(int linecnt, int chrcnt)
		{
			DebugTools.WriteLog_Console("begin " + linecnt + ", " + chrcnt);

			DebugTools.WriteLog("begin make");
			DebugTools.MakeRandTextFile(
				@"C:\temp\1.txt",
				StringTools.ENCODING_SJIS,
				StringTools.ASCII,
				"\r\n",
				linecnt,
				0,
				chrcnt
				);

			DebugTools.WriteLog("begin delete");
			File.Delete(
				@"C:\temp\2.txt"
				);

			DebugTools.WriteLog("begin copy");
			File.Copy(
				@"C:\temp\1.txt",
				@"C:\temp\2.txt"
				);

			DebugTools.WriteLog("begin sort 1/2");

#if true
			{
				string[] lines = File.ReadAllLines(@"C:\temp\2.txt", StringTools.ENCODING_SJIS);

				ArrayTools.Sort<string>(lines, StringTools.Comp);

				File.WriteAllLines(@"C:\temp\3.txt", lines, StringTools.ENCODING_SJIS);
			}
#else // old
			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = @"C:\Factory\Tools\TextSort.exe";
				psi.Arguments = @"C:\temp\2.txt C:\temp\3.txt";
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;

				Process.Start(psi).WaitForExit();
			}
#endif

			DebugTools.WriteLog("begin sort 2/2");
			new TextFileSorter(StringTools.ENCODING_SJIS).MergeSort(@"C:\temp\1.txt");

			DebugTools.WriteLog("begin comp");
			bool ret = FileTools.IsSame(
				@"C:\temp\1.txt",
				@"C:\temp\3.txt"
				);

			if (ret == false)
			{
				throw null;
			}
			DebugTools.WriteLog("done");
		}
	}
}
