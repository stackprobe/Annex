using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class Test0001
	{
		private class InheritPair
		{
			public string ParentName;
			public string Name;
		}

		private class InheritTreeNode
		{
			public string Name;
			public List<InheritTreeNode> Children = new List<InheritTreeNode>();

			public InheritTreeNode InitAdd(InheritTreeNode child)
			{
				this.Children.Add(child);
				return this;
			}
		}

		public void Test01()
		{
			List<InheritPair> inhPairs = new List<InheritPair>();

			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (Type t in asm.GetTypes())
				{
					if (t.BaseType != null)
					{
						inhPairs.Add(new InheritPair()
						{
							ParentName = Unempty(t.BaseType.FullName),
							Name = Unempty(t.FullName),
						});
					}
				}
			}
			File.WriteAllLines(@"C:\temp\1.txt", inhPairs.Select(v => v.ParentName + "\r\n\t" + v.Name), Encoding.UTF8);

			InheritTreeNode root = GetTree(inhPairs);

			foreach (InheritTreeNode v in GetAllTree(root))
				v.Children.Sort((a, b) => string.Compare(a.Name, b.Name));

			File.WriteAllLines(@"C:\temp\2.txt", ToLines(root), Encoding.UTF8);
		}

		private string Unempty(string value)
		{
			return string.IsNullOrEmpty(value) ? "<EMPTY>" : value;
		}

		private InheritTreeNode GetTree(List<InheritPair> pairs)
		{
			InheritTreeNode root = new InheritTreeNode()
			{
				Name = "<Root>",
			};

			foreach (InheritPair pair in pairs)
			{
				InheritTreeNode tree = GetAllTree(root).FirstOrDefault(v => v.Name == pair.ParentName);

				if (tree != null)
				{
					tree.Children.Add(new InheritTreeNode()
					{
						Name = pair.Name,
					});

					continue;
				}
				tree = root.Children.FirstOrDefault(v => v.Name == pair.Name);

				if (tree != null)
				{
					root.Children.RemoveAll(v => v.Name == pair.Name);
					root.Children.Add(new InheritTreeNode()
					{
						Name = pair.ParentName,
					}
					.InitAdd(tree));
				}
				else
				{
					root.Children.Add(new InheritTreeNode()
					{
						Name = pair.ParentName,
					}
					.InitAdd(new InheritTreeNode()
					{
						Name = pair.Name,
					}
					));
				}
			}
			return root;
		}

		private IEnumerable<InheritTreeNode> GetAllTree(InheritTreeNode root)
		{
			yield return root;

			foreach (InheritTreeNode child in root.Children)
				foreach (InheritTreeNode v in GetAllTree(child))
					yield return v;
		}

		private const string TL_INDENT = "\t";

		private IEnumerable<string> ToLines(InheritTreeNode root)
		{
			yield return root.Name;

			foreach (InheritTreeNode child in root.Children)
				foreach (string v in ToLines(child))
					yield return TL_INDENT + v;
		}
	}
}
