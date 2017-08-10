using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a
{
	public class NumTools
	{
		public static bool IsRange(int value, int minval, int maxval)
		{
			if (value < minval) return false;
			if (maxval < value) return false;
			return true;
		}
	}

	public class StringTools
	{
		public static string ZPad(string sVal, int len)
		{
			while (sVal.Length < len)
			{
				sVal = "0" + sVal;
			}
			return sVal;
		}

		public static string ReplaceNum9(string str)
		{
			return ReplaceChar(str, "012345678", '9');
		}

		public static string ReplaceChar(string str, string srcChrs, char destChr)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char chr in str)
			{
				if (srcChrs.IndexOf(chr) != -1)
					buff.Append(destChr);
				else
					buff.Append(chr);
			}
			return buff.ToString();
		}
	}
}
