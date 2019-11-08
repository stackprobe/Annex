using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public struct GeoPoint
	{
		public double X;
		public double Y;

		public GeoPoint(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
