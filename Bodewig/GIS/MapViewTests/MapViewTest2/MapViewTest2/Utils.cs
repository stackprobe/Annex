using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class Utils
	{
		public static string ToString(Point? p)
		{
			return p == null ? "null" : ToString(p.Value);
		}

		public static string ToString(Point p)
		{
			return "(" + p.X + ", " + p.Y + ")";
		}

		public static string ToString(GeoPoint? p)
		{
			return p == null ? "null" : ToString(p.Value);
		}

		public static string ToString(GeoPoint p)
		{
			return "(" + p.X + ", " + p.Y + ")";
		}
	}
}
