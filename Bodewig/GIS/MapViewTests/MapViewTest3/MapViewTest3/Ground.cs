using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class Gnd
	{
		private static Gnd _i = null;

		public static Gnd I
		{
			get
			{
				if (_i == null)
					_i = new Gnd();

				return _i;
			}
		}

		public int MeterPerMDot = 1000000;
		public double MeterPerLat = 108000.0;
		public double MeterPerLon = 108000.0;
		public GeoPoint CenterPoint = new GeoPoint(35.6652, 139.7125);
		public Point? DownPoint = null;
		public GeoPoint DownCenterPoint;

		public ActiveTiles ActiveTiles = null;
	}
}
