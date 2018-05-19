using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class KeyBundle
	{
		private string _name;
		private string _ident; // 128 bit
		private Node _root; // null ng

		private KeyBundle()
		{ }

		public class Node
		{
			public static Node load(XNode xRoot)
			{
				if (xRoot.get("group") == null)
				{
					return new KBKey()
					{
						ident = xRoot.get("key").get("ident").value,
					};
				}
				else
				{
					Group group = new Group()
					{
						combination = Utils.toCombination(xRoot.get("group").get("combination").value),
						nodes = new List<Node>(),
					};

					foreach (XNode xNode in xRoot.get("group").get("nodes").children)
						if (xNode.name == "node")
							group.nodes.Add(load(xNode.children[0]));

					return group;
				}
			}

			public virtual XNode save()
			{
				throw null;
			}
		}

		public class KBKey : Node
		{
			public string ident; // Key の ident

			public override XNode save()
			{
				return new XNode("node", "", ArrayTools.toList<XNode>(
					new XNode("key", "", ArrayTools.toList<XNode>(
						new XNode("ident", ident)
						))
					));
			}

			public override string ToString()
			{
				return ident;
			}
		}

		public class Group : Node
		{
			public Consts.Combination_e combination;
			public List<Node> nodes;

			public override XNode save()
			{
				List<XNode> xNodes = new List<XNode>();

				foreach (Node node in nodes)
					xNodes.Add(node.save());

				return new XNode("node", "", ArrayTools.toList<XNode>(
					new XNode("group", "", ArrayTools.toList<XNode>(
						new XNode("nodes", "", xNodes)
						))
					));
			}

			public override string ToString()
			{
				StringBuilder buff = new StringBuilder();

				buff.Append(Utils.toString(combination));
				buff.Append("(");

				foreach (Node node in nodes)
				{
					buff.Append(" ");
					buff.Append(node.ToString());
				}
				buff.Append(" )");

				return buff.ToString();
			}
		}

		public static KeyBundle create()
		{
			return create("名無しの鍵束さん" + Utils.getUniquePosix());
		}

		public static KeyBundle create(string name)
		{
			return new KeyBundle()
			{
				_name = name,
				_ident = StringTools.toHex(SecurityTools.getCRand(16)),
				_root = new KBKey()
				{
					ident = Consts.DUMMY_IDENT,
				},
			};
		}

		public static KeyBundle load(XNode xRoot)
		{
			return new KeyBundle()
			{
				_name = xRoot.get("name").value,
				_ident = xRoot.get("ident").value,
				_root = Node.load(xRoot.get("node")),
			};
		}

		public XNode save()
		{
			XNode xRoot = new XNode("key-bundle");

			xRoot.children.Add(new XNode("name", _name));
			xRoot.children.Add(new XNode("ident", _ident));
			xRoot.children.Add(_root.save());

			return xRoot;
		}

		public string getName()
		{
			return _name;
		}

		public void setName(string name)
		{
			_name = name;
		}

		public string getIdent()
		{
			return _ident;
		}

		public Node getRoot()
		{
			return _root;
		}

		public void setRoot(Node root)
		{
			_root = root;
		}
	}
}
