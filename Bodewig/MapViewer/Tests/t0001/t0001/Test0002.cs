using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class Test0002
	{
		private class GeoLineInfo
		{
			public D2Point A;
			public D2Point B;
		}

		private class RoadInfo
		{
			public string Location; // cv_[0-9]+-[0-9]+
			public List<GeoLineInfo> GeoLines = new List<GeoLineInfo>();
			public string RoadTypeCode;
			public string RouteName;
			public string LineName;
			public string PopularName;
		}

		private static Dictionary<string, RoadInfo> Roads = new Dictionary<string, RoadInfo>();

		private class AreaInfo
		{
			public string Bound; // sf[0-9]+
			public List<GeoLineInfo> GeoLines = new List<GeoLineInfo>();
			public string PrefectureName;
			public string SubPrefectureName;
			public string CountyName;
			public string CityName;
			public string AdministrativeAreaCode;
		}

		private static Dictionary<string, AreaInfo> Areas = new Dictionary<string, AreaInfo>();

		public void Test01()
		{
			// ==== Load Roads ====

			DateTime stTm = DateTime.Now;

			using (StreamReader reader = new StreamReader(@"C:\var2\res\国土数値情報\N01-07L-48-01.0a_GML\N01-07L-jgd.xml", Encoding.UTF8))
			{
				RoadInfo currRoad = null;

				for (; ; )
				{
					string line = reader.ReadLine();

					if (line == null)
						break;

					line = line.Trim();

					if (line.StartsWith("<gml:Curve "))
					{
						string location = StringTools.GetEnclosed(line, "\"", "\"").Inner;

						if (Roads.ContainsKey(location))
						{
							currRoad = Roads[location];
						}
						else
						{
							currRoad = new RoadInfo()
							{
								Location = location,
							};

							Roads.Add(location, currRoad);
						}
					}
					else if (line == "<gml:posList>")
					{
						List<D2Point> geoPoints = new List<D2Point>();

						for (; ; )
						{
							line = reader.ReadLine().Trim();

							if (line == "</gml:posList>")
								break;

							string[] tokens = line.Split(' ');

							geoPoints.Add(new D2Point(
								double.Parse(tokens[0]),
								double.Parse(tokens[1])
								));
						}
						for (int index = 1; index < geoPoints.Count; index++)
						{
							currRoad.GeoLines.Add(new GeoLineInfo()
							{
								A = geoPoints[index - 1],
								B = geoPoints[index],
							});
						}
						currRoad = null;
					}
					else if (line.StartsWith("<ksj:Road "))
					{
						string location = StringTools.GetEnclosed(reader.ReadLine(), "#", "\"").Inner;
						string prm1 = StringTools.GetEnclosed(reader.ReadLine(), ">", "<").Inner;
						string prm2 = StringTools.GetEnclosed(reader.ReadLine(), ">", "<").Inner;
						string prm3 = StringTools.GetEnclosed(reader.ReadLine(), ">", "<").Inner;
						string prm4 = StringTools.GetEnclosed(reader.ReadLine(), ">", "<").Inner;

						currRoad = Roads[location];
						currRoad.RoadTypeCode = prm1;
						currRoad.RouteName = prm2;
						currRoad.LineName = prm3;
						currRoad.PopularName = prm4;
						currRoad = null;
					}
				}
			}

			Console.WriteLine("t: " + (DateTime.Now - stTm).TotalMilliseconds);

			// ==== Load Areas ====

			stTm = DateTime.Now;

			using (StreamReader reader = new StreamReader(@"C:\var2\res\国土数値情報\N03-190101_GML\N03-19_190101.xml", Encoding.UTF8))
			{
				Dictionary<string, List<GeoLineInfo>> curves = new Dictionary<string, List<GeoLineInfo>>();
				string curveLocation = null;
				AreaInfo currArea = null;

				for (; ; )
				{
					string line = reader.ReadLine();

					if (line == null)
						break;

					line = line.Trim();

					if (line.StartsWith("<gml:Curve "))
					{
						curveLocation = StringTools.GetEnclosed(line, "\"", "\"").Inner;
					}
					else if (line == "<gml:posList>")
					{
						List<D2Point> geoPoints = new List<D2Point>();

						for (; ; )
						{
							line = reader.ReadLine().Trim();

							if (line == "</gml:posList>")
								break;

							string[] tokens = line.Split(' ');

							geoPoints.Add(new D2Point(
								double.Parse(tokens[0]),
								double.Parse(tokens[1])
								));
						}
						List<GeoLineInfo> geoLines;

						if (curveLocation == null)
							throw null;

						if (curves.ContainsKey(curveLocation))
						{
							geoLines = curves[curveLocation];
						}
						else
						{
							geoLines = new List<GeoLineInfo>();
							curves.Add(curveLocation, geoLines);
						}
						for (int index = 1; index < geoPoints.Count; index++)
						{
							geoLines.Add(new GeoLineInfo()
							{
								A = geoPoints[index - 1],
								B = geoPoints[index],
							});
						}
						curveLocation = null;
					}
					else if (line.StartsWith("<gml:Surface "))
					{
						string bound = StringTools.GetEnclosed(line, "\"", "\"").Inner;

						if (Areas.ContainsKey(bound))
						{
							currArea = Areas[bound];
						}
						else
						{
							currArea = new AreaInfo()
							{
								Bound = bound,
							};

							Areas.Add(bound, currArea);
						}
					}
					else if (line.StartsWith("<gml:curveMember "))
					{
						string location = StringTools.GetEnclosed(line, "#", "\"").Inner;
						List<GeoLineInfo> geoLines = curves[location];

						currArea.GeoLines.AddRange(geoLines);

						//currArea = null; // interior, exterior 両方ある場合があるので
					}
					else if (line.StartsWith("<ksj:AdministrativeBoundary "))
					{
						string bound = StringTools.GetEnclosed(reader.ReadLine(), "#", "\"").Inner;
						string prm1 = StringTools.GetEnclosed(reader.ReadLine(), ">", "<").Inner;
						string prm2 = StringTools.GetEnclosed(reader.ReadLine(), ">", "<").Inner;
						string prm3 = StringTools.GetEnclosed(reader.ReadLine(), ">", "<").Inner;
						string prm4 = StringTools.GetEnclosed(reader.ReadLine(), ">", "<").Inner;
						string prm5 = StringTools.GetEnclosed(reader.ReadLine(), ">", "<").Inner;

						currArea = Areas[bound];
						currArea.PrefectureName = prm1;
						currArea.SubPrefectureName = prm2;
						currArea.CountyName = prm3;
						currArea.CityName = prm4;
						currArea.AdministrativeAreaCode = prm5;
						currArea = null;
					}
				}
			}

			Console.WriteLine("t: " + (DateTime.Now - stTm).TotalMilliseconds);

			// ==== Load ここまで

			List<RoadInfo> roadList = Roads.Values.ToList();
			List<AreaInfo> areaList = Areas.Values.ToList();

			roadList.Sort((a, b) => StringTools.Comp(a.Location, b.Location));
			areaList.Sort((a, b) => StringTools.Comp(a.Bound, b.Bound));

			using (StreamWriter writer = new StreamWriter(@"C:\temp\Road.txt", false, Encoding.UTF8))
			{
				foreach (RoadInfo road in roadList)
				{
					writer.WriteLine(ToOutLine(road.Location));
					writer.WriteLine(ToOutLine(road.RoadTypeCode));
					writer.WriteLine(ToOutLine(road.RouteName));
					writer.WriteLine(ToOutLine(road.LineName));
					writer.WriteLine(ToOutLine(road.PopularName));

					foreach (GeoLineInfo geoLine in road.GeoLines)
					{
						writer.WriteLine(ToOutLine(geoLine));
					}
					writer.WriteLine("");
				}
			}
			for (int areaIndex = 0; areaIndex < areaList.Count; )
			{
				using (StreamWriter writer = new StreamWriter(@"C:\temp\Area_" + areaIndex + ".txt", false, Encoding.UTF8))
				{
					for (int c = 0; c < 10000 && areaIndex < areaList.Count; c++)
					{
						AreaInfo area = areaList[areaIndex++];

						writer.WriteLine(ToOutLine(area.Bound));
						writer.WriteLine(ToOutLine(area.PrefectureName));
						writer.WriteLine(ToOutLine(area.SubPrefectureName));
						writer.WriteLine(ToOutLine(area.CountyName));
						writer.WriteLine(ToOutLine(area.CityName));
						writer.WriteLine(ToOutLine(area.AdministrativeAreaCode));

						foreach (GeoLineInfo geoLine in area.GeoLines)
						{
							writer.WriteLine(ToOutLine(geoLine));
						}
						writer.WriteLine("");
					}
				}
			}
		}

		private static string ToOutLine(string line)
		{
			return line == null ? "<NULL>" : line == "" ? "<NONE>" : line;
		}

		private static string ToOutLine(GeoLineInfo geoLine)
		{
			return
				geoLine.A.X.ToString("F9") + " " +
				geoLine.A.Y.ToString("F9") + " " +
				geoLine.B.X.ToString("F9") + " " +
				geoLine.B.Y.ToString("F9");
		}
	}
}
