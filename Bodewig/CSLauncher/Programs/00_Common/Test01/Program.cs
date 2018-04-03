using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tests.Tools;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
		}

		private void Main2()
		{
			//new ArgsReaderTest().Test01();
			//new FileToolsTest().Test01();
			//new MSectionTest().Test01();
			new MSectionTest().Test02();
			//new WorkingDirTest().Test01();
		}
	}
}
