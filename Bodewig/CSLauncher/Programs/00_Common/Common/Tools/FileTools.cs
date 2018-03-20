using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	public class FileTools
	{
		public static void Delete(string path)
		{
			if (File.Exists(path))
			{
				for (int c = 1; ; c++)
				{
					try
					{
						File.Delete(path);
					}
					catch (Exception e)
					{
						Console.WriteLine("ファイル " + path + " の削除に失敗しました。リトライします。" + e.Message);
					}
					if (File.Exists(path) == false)
						break;

					if (10 <= c)
						throw new Exception("ファイル " + path + " の削除に失敗しました。");

					Thread.Sleep(c * 100);
					Console.WriteLine("ファイル " + path + " の削除をリトライします。");
				}
			}
			else if (Directory.Exists(path))
			{
				for (int c = 1; ; c++)
				{
					try
					{
						Directory.Delete(path, true);
					}
					catch (Exception e)
					{
						Console.WriteLine("ディレクトリ " + path + " の削除に失敗しました。リトライします。" + e.Message);
					}
					if (Directory.Exists(path) == false)
						break;

					if (10 <= c)
						throw new Exception("ディレクトリ " + path + " の削除に失敗しました。");

					Thread.Sleep(c * 100);
					Console.WriteLine("ディレクトリ " + path + " の削除をリトライします。");
				}
			}
		}

		public static void CreateDir(string dir)
		{
			for (int c = 1; ; c++)
			{
				try
				{
					Directory.CreateDirectory(dir); // dirが存在するときは何もしない。
				}
				catch (Exception e)
				{
					Console.WriteLine("ディレクトリ " + dir + " の作成に失敗しました。リトライします。" + e.Message);
				}
				if (Directory.Exists(dir))
					break;

				if (10 <= c)
					throw new Exception("ディレクトリ " + dir + " を作成出来ません。");

				Thread.Sleep(c * 100);
				Console.WriteLine("ディレクトリ " + dir + " の作成をリトライします。");
			}
		}

		public static void CleanupDir(string dir)
		{
			foreach (Func<IEnumerable<string>> getPaths in new Func<IEnumerable<string>>[]
			{
				() => Directory.EnumerateFiles(dir),
				() => Directory.EnumerateDirectories(dir),
			})
			{
				foreach (string path in getPaths())
				{
					Delete(path);
				}
			}
		}
	}
}
