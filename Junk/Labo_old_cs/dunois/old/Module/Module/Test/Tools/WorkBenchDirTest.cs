using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class WorkBenchDirTest
	{
		public static void Test01()
		{
			FileTools.CreateFile(WorkBenchDir.I.MakePath());
			FileTools.CreateFile(WorkBenchDir.I.MakePath());
			FileTools.CreateFile(WorkBenchDir.I.MakePath());

			Console.WriteLine("Press ENTER !!!");
			Console.ReadLine();
		}
	}
}
