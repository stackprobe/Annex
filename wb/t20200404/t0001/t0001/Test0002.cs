using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Charlotte
{
	public class Test0002
	{
		private const string INDENT = "\t";
		private const string S_EMPTY = "<EMPTY>";

		private class InheritInfo
		{
			public string Name;
			public List<string> ParentNames = new List<string>();
		}

		public void Test01()
		{
			List<InheritInfo> infos = new List<InheritInfo>();

			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (Type t in asm.GetTypes())
				{
					InheritInfo info = new InheritInfo()
					{
						Name = Unempty(t.FullName),
					};

					if (t.BaseType != null)
						info.ParentNames.Add(Unempty(t.BaseType.FullName));

					info.ParentNames.AddRange(t.GetInterfaces().Select(v => Unempty(v.FullName)));
					infos.Add(info);
				}
			}

			using (StreamWriter writer = new StreamWriter(@"C:\temp\1.txt", false, Encoding.UTF8))
			{
				foreach (InheritInfo info in infos)
				{
					foreach (string parentName in info.ParentNames)
					{
						writer.WriteLine(info.Name);
						writer.WriteLine(INDENT + parentName);
					}
				}
			}

			using (StreamWriter writer = new StreamWriter(@"C:\temp\2.txt", false, Encoding.UTF8))
			{
				foreach (InheritInfo info in infos)
				{
					writer.WriteLine(info.Name);

					foreach (string parentName in info.ParentNames)
						writer.WriteLine(INDENT + parentName);
				}
			}

			////
		}

		private string Unempty(string value)
		{
			return string.IsNullOrEmpty(value) ? S_EMPTY : value;
		}
	}
}
