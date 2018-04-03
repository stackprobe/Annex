using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class FileToolsTest
	{
		public void Test01()
		{
			string dir = @"C:\temp\CSLauncher_Test01_FileToolsTest";

			FileTools.CreateDir(dir);
			FileTools.Delete(dir);
		}
	}
}
