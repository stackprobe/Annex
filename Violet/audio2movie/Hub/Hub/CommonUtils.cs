using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class CommonUtils
	{
		public static void Approach(ref double value, double dest, double rate)
		{
			value -= dest;
			value *= rate;
			value += dest;
		}
	}
}
