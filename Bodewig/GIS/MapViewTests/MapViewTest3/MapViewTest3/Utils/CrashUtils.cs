using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Utils
{
	public class CrashUtils
	{
		public static bool IsSame_Double_Double(double a, double b)
		{
			return Math.Abs(a - b) < 0.000000001; // < LatLonの精度
		}

		public static bool IsCrashed(GeoPoint a, GeoPoint b)
		{
			return
				IsSame_Double_Double(a.Lat, b.Lat) &&
				IsSame_Double_Double(a.Lon, b.Lon);
		}
	}
}
