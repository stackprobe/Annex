using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using Charlotte.Chocomint.Dialogs;

namespace Charlotte
{
	public static class Ground
	{
		public static string RootDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public static string LastFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "filelist.txt");

		public static int MainWin_L;
		public static int MainWin_T;
		public static int MainWin_W = -1; // -1 == MainWin_LTWH 未設定
		public static int MainWin_H;

		public static int TreeSheetWin_L;
		public static int TreeSheetWin_T;
		public static int TreeSheetWin_W = -1; // -1 == TreeSheetWin_LTWH 未設定
		public static int TreeSheetWin_H;

		private static string DatFile
		{
			get
			{
				return Path.Combine(ProcMain.SelfDir, Path.GetFileNameWithoutExtension(ProcMain.SelfFile) + ".dat");
			}
		}

		public static void LoadDatFile()
		{
			try
			{
				if (File.Exists(DatFile) == false)
					return;

				string[] lines = File.ReadAllLines(DatFile, Encoding.UTF8);
				int c = 0;

				// ---- Items ----

				RootDir = lines[c++];
				LastFile = lines[c++];

				MainWin_L = int.Parse(lines[c++]);
				MainWin_T = int.Parse(lines[c++]);
				MainWin_W = int.Parse(lines[c++]);
				MainWin_H = int.Parse(lines[c++]);

				TreeSheetWin_L = int.Parse(lines[c++]);
				TreeSheetWin_T = int.Parse(lines[c++]);
				TreeSheetWin_W = int.Parse(lines[c++]);
				TreeSheetWin_H = int.Parse(lines[c++]);

				// ----
			}
			catch (Exception e)
			{
				MessageDlgTools.Error("データファイルが破損しています。\r\n" + DatFile, e);
			}
		}

		public static void SaveDatFile()
		{
			{
				List<string> lines = new List<string>();

				// ---- Items ----

				lines.Add(RootDir);
				lines.Add(LastFile);

				lines.Add("" + MainWin_L);
				lines.Add("" + MainWin_T);
				lines.Add("" + MainWin_W);
				lines.Add("" + MainWin_H);

				lines.Add("" + TreeSheetWin_L);
				lines.Add("" + TreeSheetWin_T);
				lines.Add("" + TreeSheetWin_W);
				lines.Add("" + TreeSheetWin_H);

				// ----

				File.WriteAllLines(DatFile, lines.ToArray(), Encoding.UTF8);
			}
		}

		public static string OpenedRootDir = "";
	}
}
