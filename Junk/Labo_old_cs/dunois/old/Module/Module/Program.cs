using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Charlotte.Test;
using Charlotte.Tools;
using Charlotte.Test.Tools;
using Charlotte.Test.Tools.Types;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{1611e518-4715-4f66-99f7-5bb9d5ff3910}";

		static void Main(string[] args)
		{
			try
			{
				DebugTools.WriteLog("START!");
				Main2();
				DebugTools.WriteLog("OK!");
			}
			catch (Exception e)
			{
				DebugTools.WriteLog("e: " + e);
			}
			Process.Start(@"C:\temp");
		}

		private static void Main2()
		{
			//WorkBenchDirTest.Test01();
			//ByteBufferTest.Test01();
			//HttpClientTest.Test01();
			//FileSorterTest.Test01();
			//MutectorTest.Test01();
			//MutectorTest.Test02();
			//NectarTest.Test01();
			//NectarTest.Test02();
			//Nectar2Test.Test01();
			//Nectar2Test.Test02();
			//Nectar2Test.Test03();
			//TimeDataTest.Test01();
			//CsvDataTest.Test01();
			//new Base_tTest().Test01();
			new Test1().Test01();
		}
	}
}
