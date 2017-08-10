using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class MathTools
	{
		private static Random _random = new Random();

		public static int Random(int modulo) // ret: 0 ～ (module - 1)
		{
			return _random.Next(modulo);
		}

		public static int Random(int minval, int maxval) // ret: minval ～ maxval
		{
			return _random.Next(minval, maxval + 1);
		}
	}
}
