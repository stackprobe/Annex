using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MapLoaders;

namespace Charlotte.Tests.MapLoaders
{
	public class MapLoaders0001Test // -- 0001
	{
		public void Test01()
		{
			string message = "MapLoaders9999";

			if (new MapLoaders0001().Echo(message) != message)
				throw null;

			Console.WriteLine("OK!");
		}
	}
}
