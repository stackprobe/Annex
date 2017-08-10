﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Charlotte
{
	public class XNode
	{
		public XNode Parent;
		public string Name;
		public string Value;
		public List<XNode> Children = new List<XNode>();

		public XNode()
		{ }

		public static XNode Load(string xmlFile)
		{
			XNode node = new XNode();

			using (XmlReader reader = XmlReader.Create(xmlFile))
			{
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							{
								XNode child = new XNode();

								child.Parent = node;
								child.Name = reader.LocalName;

								node.Children.Add(child);
								node = child;

								bool singleTag = reader.IsEmptyElement;

								while (reader.MoveToNextAttribute())
								{
									XNode attr = new XNode();

									attr.Parent = node;
									attr.Name = reader.Name;
									attr.Value = reader.Value;

									node.Children.Add(attr);
								}
								if (singleTag)
								{
									node = node.Parent;
								}
							}
							break;

						case XmlNodeType.Text:
							node.Value = reader.Value;
							break;

						case XmlNodeType.EndElement:
							node = node.Parent;
							break;

						default:
							break;
					}
				}
			}
			if (node.Parent != null)
			{
				throw new Exception();
			}
			if (node.Children.Count != 1)
			{
				throw new Exception();
			}
			node = node.Children[0];
			node.Parent = null;
			Normalize(node);
			return node;
		}

		public static void Normalize(XNode node)
		{
			node.Name = ValueFltr(node.Name);
			node.Value = ValueFltr(node.Value);

			if (node.Children == null)
				node.Children = new List<XNode>();

			foreach (XNode child in node.Children)
				child.Parent = node;

#if false
			// _namespace
			{
				int colonPos = node.Name.IndexOf(':');

				if (colonPos != -1)
				{
					XNode ns_node = new XNode();

					ns_node.Name = "_namespace";
					ns_node.Value = node.Name.Substring(0, colonPos);
					ns_node.Parent = node;

					node.Name = node.Name.Substring(colonPos + 1);
					node.Children.Add(ns_node);
				}
			}
#endif

			foreach (XNode child in node.Children)
				Normalize(child);
		}

		public static string ValueFltr(string value)
		{
			value = "" + value;
			value = value.Trim();
			return value;
		}

		public void PrintDebug()
		{
			Console.WriteLine("<" + this.Name + ">" + this.Value);

			foreach (XNode child in this.Children)
				child.PrintDebug();

			Console.WriteLine("</" + this.Name + ">");
		}
	}
}
