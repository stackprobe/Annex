using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Text.RegularExpressions;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{664fe27f-a8e1-49cc-908b-c02b2af5e1eb}";
		public const string APP_TITLE = "Road2";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			Console.WriteLine("Press ENTER.");
			Console.ReadLine();
#endif
		}

		private void Main2(ArgsReader ar)
		{
			//Test01();
			Test02();
		}

		private void Test01()
		{
			foreach (string file in Directory.GetFiles(@"C:\wb\東京都地図", "*.xml", SearchOption.AllDirectories))
			{
				Console.WriteLine("*1 " + file);
				XmlNode.LoadFile(file);
				Console.WriteLine("*2");

				GC.Collect();
			}
		}

		private void Test02()
		{
			int interiorMax = -1;

			foreach (string file in Directory.GetFiles(@"C:\wb\東京都地図", "*.xml", SearchOption.AllDirectories))
			{
				if (StringTools.StartsWithIgnoreCase(Path.GetFileName(file), "FG-GML-"))
				{
					Console.WriteLine("*1 " + file);
					XmlNode root = XmlNode.LoadFile(file);
					Console.WriteLine("*2");

					foreach (XmlNode subRoot in root.Children)
					{
						XmlNode[] areas = subRoot.Collect("area");
						XmlNode[] locs = subRoot.Collect("loc");
						XmlNode[] poss = subRoot.Collect("pos");

						//Console.WriteLine("areas.Length: " + areas.Length); // test
						//Console.WriteLine("locs.Length: " + locs.Length); // test
						//Console.WriteLine("poss.Length: " + poss.Length); // test

						if (1 < areas.Length + locs.Length + poss.Length) throw null; // test

						foreach (XmlNode area in areas)
						{
							XmlNode[] exteriors = area.Collect("Surface/patches/PolygonPatch/exterior/Ring/curveMember/Curve/segments/LineStringSegment/posList");

							if (exteriors.Length != 1) throw null; // test

							XmlNode exterior = exteriors[0];

							CheckPointsValue(exterior);

							XmlNode[] interiors = area.Collect("Surface/patches/PolygonPatch/interior/Ring/curveMember/Curve/segments/LineStringSegment/posList");

							interiorMax = Math.Max(interiorMax, interiors.Length);
							Console.WriteLine("interiors.Length: " + interiors.Length + " (" + interiorMax + ")"); // test

							foreach (XmlNode interior in interiors)
							{
								CheckPointsValue(interior);
							}
						}
						foreach (XmlNode loc in locs)
						{
							XmlNode[] posLists = loc.Collect("Curve/segments/LineStringSegment/posList");

							if (posLists.Length != 1) throw null; // test

							XmlNode posList = posLists[0];

							CheckPointsValue(posList);
						}
						foreach (XmlNode pos in poss)
						{
							XmlNode[] nPoss = pos.Collect("Point/pos");

							if (nPoss.Length != 1) throw null; // test

							XmlNode nPos = nPoss[0];

							CheckPointsValue(nPos, false);
						}
					}

					GC.Collect();

					Console.WriteLine("LatMin: " + LatMin);
					Console.WriteLine("LatMax: " + LatMax);
					Console.WriteLine("LonMin: " + LonMin);
					Console.WriteLine("LonMax: " + LonMax);
					Console.WriteLine("Lat1LenMax: " + Lat1LenMax);
					Console.WriteLine("Lat2LenMax: " + Lat2LenMax);
					Console.WriteLine("Lon1LenMax: " + Lon1LenMax);
					Console.WriteLine("Lon2LenMax: " + Lon2LenMax);
				}
			}
		}

		private void CheckPointsValue(XmlNode exterior, bool multi = true)
		{
			string value = exterior.Value;

			value = value.Replace("\r", "");

			string[] lines = value.Split('\n');

			foreach (string line in lines)
				if (IsPointLine(line) == false)
					throw null; // test

			foreach (string line in lines)
				CheckPointLine(line);

			if (multi)
			{
				if (lines.Length < 2) throw null; // test
			}
			else
			{
				if (lines.Length != 1) throw null; // test
			}
		}

		private bool IsPointLine(string line)
		{
			return Regex.IsMatch(line, "^[0-9]{1,}.[0-9]{1,} [0-9]{1,}.[0-9]{1,}$");
		}

		private double LatMin = IntTools.IMAX;
		private double LatMax = -1.0;
		private double LonMin = IntTools.IMAX;
		private double LonMax = -1.0;
		private int Lat1LenMax = -1;
		private int Lat2LenMax = -1;
		private int Lon1LenMax = -1;
		private int Lon2LenMax = -1;

		private void CheckPointLine(string line)
		{
			string[] ts0 = line.Split(' ');

			double lat = double.Parse(ts0[0]);
			double lon = double.Parse(ts0[1]);

			LatMin = Math.Min(LatMin, lat);
			LatMax = Math.Max(LatMax, lat);
			LonMin = Math.Min(LonMin, lon);
			LonMax = Math.Max(LonMax, lon);

			string[] ts1 = ts0[0].Split('.');
			string[] ts2 = ts0[1].Split('.');

			Lat1LenMax = Math.Max(Lat1LenMax, ts1[0].Length);
			Lat2LenMax = Math.Max(Lat2LenMax, ts1[1].Length);
			Lon1LenMax = Math.Max(Lon1LenMax, ts2[0].Length);
			Lon2LenMax = Math.Max(Lon2LenMax, ts2[1].Length);
		}
	}
}
