using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XReader
{
	public class XNode
	{
		public XNode Parent = null;
		public string Name = "";
		public string Text = "";
		public List<XNode> Children = new List<XNode>();

		public List<XNode> GetChildren(string name)
		{
			List<XNode> result = new List<XNode>();

			foreach(XNode child in this.Children)
			{
				if(Tools.stricmp(child.Name, name) == 0)
				{
					result.Add(child);
				}
			}
			return result;
		}

		public List<XNode> ReferChildren(string xPath)
		{
			List<XNode> result = new List<XNode>();

			result.Add(this);

			foreach (string name in xPath.Split('/'))
			{
				List<XNode> tmp = new List<XNode>();

				foreach (XNode node in result)
				{
					tmp.AddRange(node.GetChildren(name));
				}
				result = tmp;
			}
			return result;
		}

		public bool HasChild(string name)
		{
			return GetChildren(name).Count != 0;
		}

		public XNode GetChild(string name)
		{
			return GetChildren(name)[0];
		}

		public string RefChildText(string name, string defval)
		{
			try
			{
				return GetChild(name).Text;
			}
			catch
			{
				return defval;
			}
		}

		public List<XNode> GetAllNode()
		{
			List<XNode> result = new List<XNode>();
			this.AddAllNode(result);
			return result;
		}

		private void AddAllNode(List<XNode> result)
		{
			result.Add(this);

			foreach(XNode child in this.Children)
			{
				child.AddAllNode(result);
			}
		}

		public void DebugPrint(int indent)
		{
			Console.WriteLine(GetIndent(indent) + "<" + this.Name + ">");
			indent++;

			if (this.Text != "")
			{
				Console.WriteLine(GetIndent(indent) + this.Text);
			}
			foreach (XNode child in this.Children)
			{
				child.DebugPrint(indent);
			}
			indent--;
			Console.WriteLine(GetIndent(indent) + "</" + this.Name + ">");
		}

		private static string GetIndent(int indent)
		{
			StringBuilder buff = new StringBuilder();

			for (int c = 0; c < indent; c++)
			{
				buff.Append('\t');
			}
			return buff.ToString();
		}
	}
}
