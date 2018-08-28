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

#if !true // 日本測地系
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
			Map.Pref pref = new Map.Pref();

			using (GISTxtReader gtr = new GISTxtReader(file)) // 道路データ
			{
				while (gtr.Read_EOF(3))
				{
					string layerCode = gtr.GetString();

					if (layerCode == "H")
					{
						ReadHeader(gtr, pref.H);
					}
					else if (layerCode == "N")
					{
						Map.Node node = new Map.Node();

						gtr.Read(6);
						node.Mesh = gtr.GetInt();

						gtr.Read(6);
						node.No = gtr.GetInt();

						gtr.Read(8);
						node.X = gtr.GetInt();

						gtr.Read(8);
						node.Y = gtr.GetInt();

						gtr.Read(2);
						node.ノード台帳有り = gtr.GetInt() == 1;

						gtr.Read(10);
						node.ノード属性番号 = gtr.GetInt();

						gtr.Read(3);
						node.接続リンク数 = gtr.GetInt();

						gtr.Read(2);
						node.図郭外 = gtr.GetInt() == 1;

						gtr.Read(6);
						node.対応点_Mesh = gtr.GetInt_Empty();

						gtr.Read(6);
						node.対応点_No = gtr.GetInt_Empty();

						gtr.ReadNewLine();

						pref.Ns.Add(node);
					}
					else if (layerCode == "L")
					{
						Map.Link link = new Map.Link();

						// 1行目

						gtr.Read(6);
						link.起点_Mesh = gtr.GetInt();

						gtr.Read(6);
						link.起点_No = gtr.GetInt();

						gtr.Read(6);
						link.終点_Mesh = gtr.GetInt();

						gtr.Read(6);
						link.終点_No = gtr.GetInt();

						gtr.Read(6);
						link.No = gtr.GetInt();

						gtr.Read(2);
						link.リンク台帳有り = gtr.GetInt() == 1;

						gtr.Read(10);
						link.リンク属性番号 = gtr.GetInt();

						gtr.Read(6);
						link.リンク構成中間点数 = gtr.GetInt();

						for (int index = 0; index < link.リンク構成中間点数; index++) // 2行目以降
						{
							Map.Point point = new Map.Point();

							if (index % 5 == 0)
								gtr.ReadNewLine();

							gtr.Read(8);
							point.X = gtr.GetInt();

							gtr.Read(8);
							point.Y = gtr.GetInt();

							link.Ps.Add(point);
						}
						gtr.ReadNewLine();

						pref.Ls.Add(link);
					}
					else
					{
						throw new Exception("不明なレイヤコード：" + layerCode);
					}
				}
			}

			using (GISTxtReader gtr = new GISTxtReader(daichou_file)) // リンク台帳
			{
				while (gtr.Read_EOF(3))
				{
					string layerCode = gtr.GetString();

					if (layerCode == "H")
					{
						ReadHeader(gtr, pref.DLH);
					}
					else if (layerCode == "DL")
					{
						Map.DLink dLink = new Map.DLink();

						gtr.Read(10);
						dLink.属性番号 = gtr.GetInt();

						gtr.Read(3);
						dLink.属性の行数 = gtr.GetInt();

						gtr.Read(2);
						dLink.道路種別コード = (Map.道路種別コード_e)gtr.GetInt();

						gtr.Read(24);
						dLink.路線名 = gtr.GetString();

						gtr.Read(22);
						dLink.線名 = gtr.GetString();

						gtr.Read(16);
						dLink.通称 = gtr.GetString();

						gtr.ReadNewLine();

						pref.DLs.Add(dLink);
					}
					else
					{
						throw new Exception("不明なレイヤコード：" + layerCode);
					}
				}
			}

			return pref;
		}

		private void ReadHeader(GISTxtReader gtr, Map.Header h)
		{
			// 1行目

			gtr.Read(10);
			h.作成機関 = gtr.GetString();

			gtr.Read(10);
			h.データコード = gtr.GetString();

			gtr.Read(2);
			h.データの種類 = gtr.GetInt();

			gtr.Read(4);
			h.作成年度 = gtr.GetInt();

			gtr.Read(4);
			h.行_桁数 = gtr.GetInt();

			gtr.ReadNewLine();

			// 2行目

			gtr.Read(8);
			h.行数_データ全体 = gtr.GetInt();

			gtr.Read(8);
			h.行数_ノードデータ = gtr.GetInt();

			gtr.Read(8);
			h.行数_リンクデータ = gtr.GetInt();

			gtr.Read(8);
			h.行数_ラインデータ = gtr.GetInt();

			gtr.Read(8);
			h.行数_ノード台帳 = gtr.GetInt();

			gtr.Read(8);
			h.行数_リンク台帳 = gtr.GetInt();

			gtr.Read(8);
			h.行数_ライン台帳 = gtr.GetInt();

			gtr.ReadNewLine();
		}
	}
}
