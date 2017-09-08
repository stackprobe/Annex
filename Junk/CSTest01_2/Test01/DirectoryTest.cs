using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class DirectoryTest
	{
		public void Test01()
		{
			Console.WriteLine("null=" + Directory.Exists(null)); // false
			Console.WriteLine("[]=" + Directory.Exists("")); // false
			Console.WriteLine("[*]=" + Directory.Exists("*")); // false
			Console.WriteLine("[\\]=" + Directory.Exists("\\")); // true
			Console.WriteLine("[.]=" + Directory.Exists(".")); // true
			Console.WriteLine("[..]=" + Directory.Exists("..")); // true
			Console.WriteLine("[\\.]=" + Directory.Exists("\\.")); // true
			Console.WriteLine("[\\..]=" + Directory.Exists("\\..")); // true <- は？ｗ

			//Console.WriteLine("null=" + Path.GetFullPath(null)); // 例外
			//Console.WriteLine("[]=" + Path.GetFullPath("")); // 例外
			//Console.WriteLine("[*]=" + Path.GetFullPath("*")); // 例外
			Console.WriteLine("[\\]=" + Path.GetFullPath("\\"));
			Console.WriteLine("[.]=" + Path.GetFullPath("."));
			Console.WriteLine("[..]=" + Path.GetFullPath(".."));
			Console.WriteLine("[\\.]=" + Path.GetFullPath("\\."));
			Console.WriteLine("[\\..]=" + Path.GetFullPath("\\.."));

			try
			{
				Directory.Delete(@"C:\temp\a", true);
			}
			catch
			{ }

			try
			{
				File.Delete(@"C:\temp\a");
			}
			catch
			{ }

			Directory.CreateDirectory(@"C:\temp\a\b\c");
			Directory.Delete(@"C:\temp\a\b\c");
			//Directory.Delete(@"C:\temp\a\b\c"); // 例外 -- 存在しない。

			Directory.CreateDirectory(@"C:\temp\a\b\c");
			//Directory.Delete(@"C:\temp\a"); // 例外 -- 空ではない。
			Directory.Delete(@"C:\temp\a\b\c");
			Directory.Delete(@"C:\temp\a\b");
			Directory.Delete(@"C:\temp\a");

			//File.Create(@"C:\temp\a\b\c").Close(); // 例外 -- 親が存在しない。
			Directory.CreateDirectory(@"C:\temp\a\b");
			File.Create(@"C:\temp\a\b\c").Close(); // 空ファイル作成
			File.Delete(@"C:\temp\a\b\c");
			Directory.Delete(@"C:\temp\a\b");
			Directory.Delete(@"C:\temp\a");

			using (File.Create(@"C:\temp\a")) // 空ファイル作成
			{ }

			File.Delete(@"C:\temp\a");
		}
	}
}
