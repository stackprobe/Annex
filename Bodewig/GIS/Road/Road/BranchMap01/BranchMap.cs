using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.BranchMap01
{
	public class BranchMap
	{
		public Map Map;

		// <---- prms

		public class Node
		{
			public int X;
			public int Y;

			public string GetKey()
			{
				return X + "_" + Y;
			}
		}

		public class Branch
		{
			public Node S;
			public Node T;

			public int Count = 1; // 重複している数

			public string GetKey()
			{
				return S.GetKey() + "_" + T.GetKey();
			}
		}

		public Dictionary<string, Branch> Bs = DictionaryTools.Create<Branch>();

		public void Load()
		{
			foreach (Map.Pref pref in Map.Prefs)
				foreach (Map.Link link in pref.Ls)
					for (int index = 0; index + 1 < link.Ps.Count; index++)
						Add(link.Ps[index], link.Ps[index + 1]);
		}

		private void Add(Map.Point p1, Map.Point p2)
		{
			Node n1 = new Node() { X = p1.X, Y = p1.Y };
			Node n2 = new Node() { X = p2.X, Y = p2.Y };

			Branch b = new Branch();

			if (StringTools.Comp(n1.GetKey(), n2.GetKey()) < 0) // ? n1 < n2
			{
				b.S = n1;
				b.T = n2;
			}
			else
			{
				b.S = n2;
				b.T = n1;
			}
			// b.S < b.T

			// 追加

			if (Bs.ContainsKey(b.GetKey()))
			{
				Bs[b.GetKey()].Count++;
			}
			else
			{
				Bs.Add(b.GetKey(), b);
			}
		}

		public void Output()
		{
			using (StreamWriter writer = new StreamWriter(@"C:\temp\1.txt", false, StringTools.ENCODING_SJIS))
			{
				foreach (Branch b in Bs.Values)
				{
					writer.WriteLine(b.Count + " " + b.GetKey());
				}
			}
		}
	}
}
