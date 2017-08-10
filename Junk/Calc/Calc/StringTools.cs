using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc
{
	public class StringTools
	{
		public static string Reverse(string str)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = str.Length - 1; 0 <= index; index--)
			{
				buff.Append(str[index]);
			}
			return buff.ToString();
		}

		public static string RPad(string str, int minlen, char chrPad)
		{
			while (str.Length < minlen)
			{
				str = chrPad + str;
			}
			return str;
		}

		public static bool HasSameChar(string str)
		{
			for (int j = 1; j < str.Length; j++)
				for (int i = 0; i < j; i++)
					if (str[i] == str[j])
						return true;

			return false;
		}
	}
}
