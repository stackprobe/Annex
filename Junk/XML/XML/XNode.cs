using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XML
{
	public class XNode
	{
		public XNode Parent = null;
		public string Name = "";
		public string Text = "";
		public List<XNode> Children = new List<XNode>();

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
