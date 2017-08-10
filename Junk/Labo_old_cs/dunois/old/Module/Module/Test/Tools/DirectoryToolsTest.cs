using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Test.Tools
{
	public class DirectoryToolsTest
	{
		public static void Test01()
		{
			Test01_a(
				///////////////////////// $_git:secret
				);

			using (new DirectoryTools.Into(
				////////////////////////////////////////// $_git:secret
				))
			{
				Test01_a(".."); // -> @"..\aaaaa_bbbbbb\1234567.jpg"
				Test01_a(Path.GetFullPath("..")); // -> @"C:\etc\画像\壁紙\aaaaa_bbbbbb\1234567.jpg"
			}
		}

		private static void Test01_a(string rootDir)
		{
			DebugTools.WriteLog("rootDir: " + rootDir);

			foreach (string dir in DirectoryTools.GetAllDir(rootDir))
			{
				DebugTools.WriteLog("dir: " + dir);
			}
			foreach (string file in DirectoryTools.GetAllFile(rootDir))
			{
				DebugTools.WriteLog("file: " + file);
			}
		}
	}
}
