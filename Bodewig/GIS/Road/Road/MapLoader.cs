using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class MapLoader
	{
		public Map Map;

		public void Load()
		{
			Map = new Map();

#if true // 日本測地系
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-08-01.0"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-09-01.0"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-10-01.0"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-11-01.0"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-12-01.0"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-13-01.0"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-14-01.0"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-19-01.0"));
#else // 世界測地系
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-08-01.0a"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-09-01.0a"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-10-01.0a"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-11-01.0a"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-12-01.0a"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-13-01.0a"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-14-01.0a"));
			Map.Prefs.Add(LoadDir(@"C:\wb\首都圏道路\txt\N01-07L-19-01.0a"));
#endif
		}

		private Map.Pref LoadDir(string dir)
		{
			string[] f = Directory.EnumerateFiles(dir).Where(file => Path.GetExtension(file).ToLower() == ".txt" && Path.GetFileNameWithoutExtension(file).Contains("台") == false).ToArray();
			string[] d = Directory.EnumerateFiles(dir).Where(file => Path.GetExtension(file).ToLower() == ".txt" && Path.GetFileNameWithoutExtension(file).Contains("台")).ToArray();

			if (f.Length != 1)
				throw new Exception("道路データファイルを１つに絞れません。");

			if (d.Length != 1)
				throw new Exception("道路データファイルを１つに絞れません。");

			return LoadFile(f[0], d[0]);
		}

		private Map.Pref LoadFile(string file, string daichou_file)
		{
			throw null; // TODO
		}
	}
}
