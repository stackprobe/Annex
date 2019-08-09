using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron.Utils
{
	public static class CommonUtils
	{
		public static string ToString<T>(T value)
		{
			return ToString(value, v => v.ToString());
		}

		public static string ToString<T>(T value, Func<T, string> valueToString)
		{
			return value == null ? "null" : valueToString(value);
		}

		public static string PutSign(string str)
		{
			if (str[0] != '-')
			{
				str = " " + str;
				//str = "+" + str;
			}
			return str;
		}
	}
}
