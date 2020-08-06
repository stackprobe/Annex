using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace BusyDlg
{
	public static class Utils
	{
		public static string GetString(string[] list, int index, string defval)
		{
			try
			{
				return list[index];
			}
			catch
			{ }

			return defval;
		}

#if false // not used
		private static string TempDir;

		public static string GetTempDir()
		{
			if (Utils.TempDir == null)
			{
				string dir = Environment.GetEnvironmentVariable("TMP");

				if (string.IsNullOrEmpty(dir))
					dir = Environment.GetEnvironmentVariable("TEMP");

				if (string.IsNullOrEmpty(dir) || Directory.Exists(dir) == false)
					throw new Exception("BAD_TMP");

				Utils.TempDir = dir;
			}
			return Utils.TempDir;
		}
#endif

		public static string[] GetAllMainWindowTitle()
		{
			List<string> result = new List<string>();

			foreach (Process p in Process.GetProcesses())
			{
				result.Add(p.MainWindowTitle);
			}
			return result.ToArray();
		}

		public static bool Contains(string[] list, string target)
		{
			foreach (string str in list)
			{
				if (str == target)
				{
					return true;
				}
			}
			return false;
		}
	}
}
