using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MapLoaders;

namespace Charlotte.Tests.MapLoaders
{
	public class MapLoader0001Test // -- 0001
	{
		public void Test01()
		{
			string message = "MapLoader9999";

			if (new MapLoader0001().Echo(message) != message)
				throw null;

			Console.WriteLine("OK!");
		}
	}
}
