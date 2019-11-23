using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MapLoaders.Internal
{
	public static class Common
	{
		public static string GeoValueToString(double value)
		{
			return value.ToString("F13");
		}
	}
}
