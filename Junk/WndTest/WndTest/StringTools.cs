using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WndTest
{
	public static class StringTools
	{
		public const string DIGIT = "0123456789";
		public const string HEXDIGIT = "0123456789abcdef";

		public static string ToHex(ulong value, int minlen)
		{
			string str = "";

			while (0 < value)
			{
				str = HEXDIGIT[(int)(value % 16)] + str;
				value /= 16;
			}
			return ZPad(str, minlen);
		}

		public static string ZPad(string str, int minlen)
		{
			while (str.Length < minlen)
			{
				str = "0" + str;
			}
			return str;
		}
	}
}
