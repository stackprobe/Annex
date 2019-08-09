using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XReader
{
	public static class XLoader
	{
		public static XNode Load(string xmlFile)
		{
			XmlReader reader = XmlReader.Create(xmlFile);
			XNode node = new XNode();

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

							while(reader.MoveToNextAttribute())
							{
								XNode attr = new XNode();

								attr.Parent = node;
								attr.Name = reader.Name;
								attr.Text = reader.Value;

								node.Children.Add(attr);
							}
							if (singleTag)
							{
								node = node.Parent;
							}
						}
						break;

					case XmlNodeType.Text:
						node.Text = reader.Value;
						break;

					case XmlNodeType.EndElement:
						node = node.Parent;
						break;

					default:
						break;
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
			return node;
		}
	}
}
