using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc
{
	public class ArrayTools
	{
		public static List<T> ShallowCopy<T>(List<T> src)
		{
			List<T> dest = new List<T>();

			foreach(T e in src)
				dest.Add(e);

			return dest;
		}
	}
}
