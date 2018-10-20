using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Utils;

namespace Charlotte
{
	public struct GeoPoint
	{
		public double Lat;
		public double Lon;

		public GeoPoint(double lat, double lon)
		{
			this.Lat = lat;
			this.Lon = lon;
		}

		public static bool operator ==(GeoPoint a, GeoPoint b)
		{
			return
				CrashUtils.IsSame_Double_Double(a.Lat, b.Lat) &&
				CrashUtils.IsSame_Double_Double(a.Lon, b.Lon);
		}

		public static bool operator !=(GeoPoint a, GeoPoint b)
		{
			return !(a == b);
		}
	}
}
