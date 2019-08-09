using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XReader
{
	public static class Tools
	{
		public static bool ContainsIgnoreCase(string str, string ptn)
		{
			for (int i = 0; i <= str.Length - ptn.Length; i++)
			{
				string part = str.Substring(i, ptn.Length);

				if (stricmp(part, ptn) == 0)
				{
					return true;
				}
			}
			return false;
		}

		public static int stricmp(string a, string b)
		{
			return string.Compare(a, b, true);
		}

		public static bool EndsWithIgnoreCase(string str, string endPtn)
		{
			return str.ToUpper().EndsWith(endPtn.ToUpper());
		}

		public static string Join(string[] list, string separator)
		{
			StringBuilder buff = new StringBuilder();

			for (int i = 0; i < list.Length; i++)
			{
				if (1 <= i)
				{
					buff.Append(separator);
				}
				buff.Append(list[i]);
			}
			return buff.ToString();
		}
	}

	public class ComparerStringIgnoreCase : IComparer<string>
	{
		public int Compare(string a, string b)
		{
			return Tools.stricmp(a, b);
		}
	}
}
