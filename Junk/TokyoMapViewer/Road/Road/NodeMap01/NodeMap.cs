using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.NodeMap01
{
	public class NodeMap
	{
		public Map Map;

		// <---- prms

		public class Node
		{
			public int X;
			public int Y;
			public Dictionary<string, Node> Links = DictionaryTools.Create<Node>();

			public string GetKey()
			{
				return X + "_" + Y;
			}

			public void PutLink(Node link)
			{
				if (Links.ContainsKey(link.GetKey()) == false)
					Links.Add(link.GetKey(), link);
			}

			// Work ---->

			public bool Reached;
		}

		public Dictionary<string, Node> Ns = DictionaryTools.Create<Node>();

		public void Load()
		{
			foreach (Map.Pref pref in Map.Prefs)
				foreach (Map.Link link in pref.Ls)
					for (int index = 0; index + 1 < link.Ps.Count; index++)
						Add(link.Ps[index], link.Ps[index + 1]);
		}

		private void Add(Map.Point p1, Map.Point p2)
		{
			Node n1 = GetOrCreateNode(p1);
			Node n2 = GetOrCreateNode(p2);

			n1.PutLink(n2);
			n2.PutLink(n1);
		}

		private Node GetOrCreateNode(Map.Point p)
		{
			Node node = new Node() { X = p.X, Y = p.Y };

			if (Ns.ContainsKey(node.GetKey()))
			{
				return Ns[node.GetKey()];
			}
			else
			{
				Ns.Add(node.GetKey(), node);
				return node;
			}
		}

		public void Output()
		{
			using (StreamWriter writer = new StreamWriter(@"C:\temp\1.txt", false, StringTools.ENCODING_SJIS))
			{
				foreach (Node n in Ns.Values)
				{
					Node[] links = n.Links.Values.ToArray();

					Array.Sort(links, (a, b) => StringTools.Comp(a.GetKey(), b.GetKey()));

					writer.WriteLine(n.Links.Count + " " + n.GetKey() + " " + string.Join(" ", links.Select(a => a.GetKey())));
				}
			}
		}

		public void 全部つながっているかどうか確認する()
		{
			// 準備

			foreach (Node n in Ns.Values)
				n.Reached = false;

			// 探索

			Queue<Node> nq = new Queue<Node>();

			nq.Enqueue(Ns.Values.ToArray()[0]); // 適当なノードから

			while (1 <= nq.Count)
			{
				Node n = nq.Dequeue();

				if (n.Reached == false)
				{
					n.Reached = true;

					foreach (Node link in n.Links.Values)
						nq.Enqueue(link);
				}
			}

			// チェック

			//foreach (Node n in Ns.Values)
			//if (n.Reached == false)
			//throw null;

			int count = 0;

			foreach (Node n in Ns.Values)
				if (n.Reached == false)
					count++;

			Console.WriteLine(count + " / " + Ns.Count);
		}

		public void 島を数える()
		{
			int lrc = -1;

			for (int i = 1; ; i++)
			{
				int count = 0;

				foreach (Node n in Ns.Values)
					if (n.Reached == false)
						count++;

				Console.WriteLine(i + " " + count + " " + lrc);

				if (count == 0)
					break;

				lrc = 0;

				{
					Queue<Node> nq = new Queue<Node>();

					nq.Enqueue(Ns.Values.Where(n => n.Reached == false).ToArray()[0]);

					while (1 <= nq.Count)
					{
						Node n = nq.Dequeue();

						if (n.Reached == false)
						{
							n.Reached = true;
							lrc++;

							foreach (Node link in n.Links.Values)
								nq.Enqueue(link);
						}
					}
				}
			}
		}
	}
}
