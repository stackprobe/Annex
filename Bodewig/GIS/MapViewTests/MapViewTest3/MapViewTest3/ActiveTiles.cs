using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class ActiveTiles
	{
		public int MeterPerMDot;
		public double MeterPerLat;
		public double MeterPerLon;
		public GeoPoint CenterPoint;
		public bool SomethingChanged;

		// <---- state

		public List<Tile> Tiles = new List<Tile>();
	}
}
