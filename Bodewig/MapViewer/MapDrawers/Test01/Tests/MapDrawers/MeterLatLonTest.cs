using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MapDrawers;

namespace Charlotte.Tests.MapDrawers
{
	public class MeterLatLonTest
	{
		public void Test01()
		{
			Console.WriteLine(MeterLatLon.MeterPerLat());
			Console.WriteLine(MeterLatLon.MeterPerLat());
			Console.WriteLine(MeterLatLon.MeterPerLat());

			Console.WriteLine(MeterLatLon.MeterPerLon(0.0));
			Console.WriteLine(MeterLatLon.MeterPerLon(15.0));
			Console.WriteLine(MeterLatLon.MeterPerLon(30.0));
			Console.WriteLine(MeterLatLon.MeterPerLon(45.0));
			Console.WriteLine(MeterLatLon.MeterPerLon(60.0));
			Console.WriteLine(MeterLatLon.MeterPerLon(75.0));
			Console.WriteLine(MeterLatLon.MeterPerLon(89.999));
		}
	}
}
