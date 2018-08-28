using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Map
	{
		public class Pref // Prefecture
		{
			public Header H = new Header();
			public List<Node> Ns = new List<Node>();
			public List<Link> Ls = new List<Link>();
			public Header DLH = new Header(); // Link台帳のヘッダ
			public List<DLink> DLs = new List<DLink>();
		}

		public class Header
		{
			// 1行目
			public string 作成機関; // "NLA" (National Land Agency)
			public string データコード; // "N01-07L"
			public int データの種類; // 2(線)
			public int 作成年度; // 西暦
			public int 行_桁数; // 80

			// 2行目
			public int 行数_データ全体;
			public int 行数_ノードデータ;
			public int 行数_リンクデータ;
			public int 行数_ラインデータ;
			public int 行数_ノード台帳;
			public int 行数_リンク台帳;
			public int 行数_ライン台帳;
		}

		public class Node
		{
			public int Mesh; // 2次メッシュ
			public int No; // ノード番号(1～)
			public int X; // (1/10)秒_東経
			public int Y; // (1/10)秒_北緯
			public bool ノード台帳有り;
			public int ノード属性番号;
			public int 接続リンク数;
			public bool 図郭外; // true:図郭内, false:図郭外(図郭上)
			public int 対応点_Mesh;
			public int 対応点_No;
		}

		public class Link
		{
			public int 起点_Mesh;
			public int 起点_No;
			public int 終点_Mesh;
			public int 終点_No;
			public int No; // 起点ノードのあるメッシュ内連番(1～)
			public bool リンク台帳有り;
			public int リンク属性番号;
			public int リンク構成中間点数; // 両端も含む

			// 2行目以降
			public List<Point> Ps = new List<Point>();
		}

		public class Point
		{
			public int X; // (1/10)秒_東経
			public int Y; // (1/10)秒_北緯
		}

		public enum 道路種別コード_e
		{
			高速道路 = 1,
			一般国道,
			主要地方道,
			一般都道府県道,
			特例都道,
			市町村道,
			私道,
		}

		public class DLink
		{
			public int 属性番号; // 路線コード
			public int 属性の行数;
			public 道路種別コード_e 道路種別コード;
			public string 路線名;
			public string 線名;
			public string 通称;
		}

		public enum Pref_e
		{
			茨城,
			栃木,
			群馬,
			埼玉,
			千葉,
			東京,
			神奈川,
			山梨,
		}

		public List<Pref> Prefs = new List<Pref>(); // Pref_e の並びであること。

		public Pref 茨城 { get { return Prefs[(int)Pref_e.茨城]; } }
		public Pref 栃木 { get { return Prefs[(int)Pref_e.栃木]; } }
		public Pref 群馬 { get { return Prefs[(int)Pref_e.群馬]; } }
		public Pref 埼玉 { get { return Prefs[(int)Pref_e.埼玉]; } }
		public Pref 千葉 { get { return Prefs[(int)Pref_e.千葉]; } }
		public Pref 東京 { get { return Prefs[(int)Pref_e.東京]; } }
		public Pref 神奈川 { get { return Prefs[(int)Pref_e.神奈川]; } }
		public Pref 山梨 { get { return Prefs[(int)Pref_e.山梨]; } }
	}
}
