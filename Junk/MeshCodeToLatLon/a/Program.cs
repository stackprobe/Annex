using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.ReadLine();
		}

		private void Main2()
		{
			Test("53390000");
			Test("53397799");
			Test("54380000");
			Test("54387799");
		}

		private void Test(string meshCode)
		{
			Console.WriteLine("meshCode_1: " + meshCode);
			double[] latLon = new MeshTools().GetLatLon(meshCode);
			Console.WriteLine("lat: " + latLon[0] + ", lon: " + latLon[1]);
			meshCode = new MeshTools().GetMeshCode(latLon[0], latLon[1]);
			Console.WriteLine("meshCode_2: " + meshCode);
		}
	}
}
