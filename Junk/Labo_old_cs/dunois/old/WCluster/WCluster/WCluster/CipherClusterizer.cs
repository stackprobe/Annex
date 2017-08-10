using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace WCluster
{
	public class CipherClusterizer : Clusterizer
	{
		private string Passphrase;

		public CipherClusterizer(string passphrase)
		{
			Passphrase = passphrase;

			if (!IsFairPassphrase(Passphrase))
				throw new Exception("パスフレーズの書式に問題があります。");
		}

		public override void DirectoryToFile(string rDir, string wFile)
		{
			string midFile = GetTempPath();

			try
			{
				base.DirectoryToFile(rDir, midFile);

				CheckCancel();
				Clusterizer.Status.SetString("Encrypting cluster file");

				Cipher(midFile, true);

#if false
				CheckCancel();
				Clusterizer.Status.SetString("Moving cluster file");

				File.Delete(wFile); // File.Move は既存のファイルを上書き出来ない。
				File.Move(midFile, wFile);
#else
				CheckCancel();
				Clusterizer.Status.SetString("Copying cluster file");

				CopyFile(midFile, wFile);
#endif
			}
			finally
			{
				File.Delete(midFile);
			}
		}

		public override void FileToDirectory(string rFile, string wDir)
		{
			string midFile = GetTempPath();

			try
			{
				CheckCancel();
				Clusterizer.Status.SetString("Copying cluster file");

				File.Copy(rFile, midFile);

				CheckCancel();
				Clusterizer.Status.SetString("Decrypting cluster file");

				if (Cipher(midFile, false) != 0)
					throw new Exception("復号に失敗しました。\nパスフレーズが間違っているか、入力ファイルが破損しています。");

				base.FileToDirectory(midFile, wDir);
			}
			finally
			{
				File.Delete(midFile);
			}
		}

		private static string GetTempPath()
		{
			return Path.Combine(Environment.GetEnvironmentVariable("TMP"), Guid.NewGuid().ToString("B"));
		}

		private static readonly Encoding Encoding_SJIS = Encoding.GetEncoding(932);

		private int Cipher(string file, bool encFlag)
		{
			string exeFile = null;
			string prmFile = null;

			try
			{
				if (!IsFairPassphrase(Passphrase)) // 2bs -- コンストラクタで確認しているはず。
					throw null;

				exeFile = GetTempPath() + ".exe";
				prmFile = GetTempPath() + ".prm";

				File.Copy(GetZClusterFile(), exeFile);

				File.WriteAllLines(
					prmFile,
					new string[]
					{
						"/KB",
						"*" + Passphrase,
						encFlag ? "/E+" : "/D+",
						file,
					},
					Encoding_SJIS
					);

				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = exeFile;
				psi.Arguments = "//R \"" + prmFile + "\"";
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;

				if (ShowConsoleFlag)
				{
					psi.CreateNoWindow = false;
					psi.UseShellExecute = true;
				}

				Process p = Process.Start(psi);
				p.WaitForExit();
				int ret = p.ExitCode;

				return ret;
			}
			finally
			{
				if (exeFile != null)
					File.Delete(exeFile);

				if (prmFile != null)
					File.Delete(prmFile);
			}
		}

		public static bool IsFairPassphrase(string passphrase)
		{
			// 空文字列は /KB * オプションに指定出来ない！

			if (passphrase == "")
				return false;

			foreach (char chr in passphrase)
				if (chr < ' ')
					return false;

			if (passphrase != Encoding_SJIS.GetString(Encoding_SJIS.GetBytes(passphrase)))
				return false;

			return true;
		}

		private static string ZClusterFile;

		public static string GetZClusterFile()
		{
			if (ZClusterFile == null)
			{
				string selfFile = System.Reflection.Assembly.GetEntryAssembly().Location;
				string selfDir = Path.GetDirectoryName(selfFile);

				string file = Path.Combine(selfDir, "ZCluster.exe_");

				if (File.Exists(file) == false)
					file = @"C:\Factory\Tools\ZCluster.exe";

				ZClusterFile = file;
			}
			return ZClusterFile;
		}
	}
}
