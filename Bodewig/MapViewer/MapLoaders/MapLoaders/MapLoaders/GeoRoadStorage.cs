using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.MapLoaders.Internal;
using Charlotte.Tools;

namespace Charlotte.MapLoaders
{
	public class GeoRoadStorage
	{
		public List<GeoRoad> Roads = null;

		public void Load()
		{
			try
			{
				if (File.Exists(Consts.ROAD_CACHE_FILE) == false)
					this.GMLToCache();

				DateTime t3 = DateTime.Now; // test
				this.LoadFromCache();
				DateTime t4 = DateTime.Now; // test

				Console.WriteLine("t3: " + (t4 - t3).TotalMilliseconds); // test
			}
			catch
			{
				this.Roads = null;
				throw;
			}
		}

		private void GMLToCache()
		{
			DateTime t1 = DateTime.Now; // test
			this.LoadFromGML();
			DateTime t2 = DateTime.Now; // test
			this.SaveToCache();
			DateTime t3 = DateTime.Now; // test

			Console.WriteLine("t1: " + (t2 - t1).TotalMilliseconds); // test
			Console.WriteLine("t2: " + (t3 - t2).TotalMilliseconds); // test

			this.Roads = null;
			GC.Collect();
		}

		private StreamReader Reader = null;

		private string ReadLine()
		{
			return this.Reader.ReadLine().Trim();
		}

		private void LoadFromGML()
		{
			string line;
			string[] tokens;

			Dictionary<string, GeoRoad> roads = DictionaryTools.Create<GeoRoad>(); // key: cv[0-9]+_[0-9]+

			this.Reader = new StreamReader(Consts.ROAD_GML_FILE);
			try
			{
				for (; ; )
				{
					line = this.ReadLine();

					if (line == "</ksj:Dataset>")
						break;

					if (line.StartsWith("<gml:Curve "))
					{
						string cvIdent = StringTools.GetEnclosed(line, "\"", "\"").Inner; // cv[0-9]+_[0-9]+
						List<GeoPoint> geoPoints = new List<GeoPoint>();

						while (this.ReadLine() != "<gml:posList>")
						{ }

						for (; ; )
						{
							line = this.ReadLine();

							if (line == "</gml:posList>")
								break;

							tokens = line.Split(' ');

							geoPoints.Add(new GeoPoint()
							{
								Pt = new D2Point()
								{
									X = double.Parse(tokens[0]),
									Y = double.Parse(tokens[1]),
								},
							});
						}
						List<GeoLine> geoLines = CurveToGeoLines(geoPoints);

						roads.Add(cvIdent, new GeoRoad()
						{
							GeoLines = geoLines,
						});

						while (this.ReadLine() != "</gml:Curve>")
						{ }
					}
					else if (line.StartsWith("<ksj:Road "))
					{
						string cvIdent = StringTools.GetEnclosed(this.ReadLine(), "#", "\"").Inner;
						string prm1 = StringTools.GetEnclosed(this.ReadLine(), ">", "<").Inner;
						string prm2 = StringTools.GetEnclosed(this.ReadLine(), ">", "<").Inner;
						string prm3 = StringTools.GetEnclosed(this.ReadLine(), ">", "<").Inner;
						string prm4 = StringTools.GetEnclosed(this.ReadLine(), ">", "<").Inner;

						GeoRoad road = roads[cvIdent];

						road.RoadTypeCode = prm1;
						road.RouteName = prm2;
						road.LineName = prm3;
						road.PopularName = prm4;
					}
				}
			}
			finally
			{
				this.Reader.Dispose();
				this.Reader = null;
			}

			this.Roads = roads.Values.ToList();
		}

		private List<GeoLine> CurveToGeoLines(List<GeoPoint> geoPoints)
		{
			List<GeoLine> geoLines = new List<GeoLine>();

			for (int index = 1; index < geoPoints.Count; index++)
			{
				geoLines.Add(new GeoLine()
				{
					A = geoPoints[index - 1],
					B = geoPoints[index],
				});
			}
			return geoLines;
		}

		private void SaveToCache()
		{
			try
			{
				using (FileStream writer = new FileStream(Consts.ROAD_CACHE_FILE, FileMode.Create, FileAccess.Write))
				{
					Common.WriteInt(writer, this.Roads.Count);

					foreach (GeoRoad road in this.Roads)
					{
						Common.WriteString(writer, road.RoadTypeCode);
						Common.WriteString(writer, road.RouteName);
						Common.WriteString(writer, road.LineName);
						Common.WriteString(writer, road.PopularName);
						Common.WriteInt(writer, road.GeoLines.Count);

						foreach (GeoLine geoLine in road.GeoLines)
						{
							Common.WriteDouble(writer, geoLine.A.Pt.X);
							Common.WriteDouble(writer, geoLine.A.Pt.Y);
							Common.WriteDouble(writer, geoLine.B.Pt.X);
							Common.WriteDouble(writer, geoLine.B.Pt.Y);
						}
					}
				}
			}
			catch
			{
				FileTools.Delete(Consts.ROAD_CACHE_FILE); // FIXME デバッグ用に退避する？
				throw;
			}
		}

		private void LoadFromCache()
		{
			try
			{
				using (FileStream reader = new FileStream(Consts.ROAD_CACHE_FILE, FileMode.Open, FileAccess.Read))
				{
					int roadCount = Common.ReadInt(reader);

					this.Roads = new List<GeoRoad>(roadCount);

					for (int roadIndex = 0; roadIndex < roadCount; roadIndex++)
					{
						GeoRoad road = new GeoRoad();

						string prm1 = Common.ReadString(reader);
						string prm2 = Common.ReadString(reader);
						string prm3 = Common.ReadString(reader);
						string prm4 = Common.ReadString(reader);

						road.RoadTypeCode = prm1;
						road.RouteName = prm2;
						road.LineName = prm3;
						road.PopularName = prm4;

						int geoLineCount = Common.ReadInt(reader);

						road.GeoLines = new List<GeoLine>(geoLineCount);

						for (int geoLineIndex = 0; geoLineIndex < geoLineCount; geoLineIndex++)
						{
							double pt1 = Common.ReadDouble(reader);
							double pt2 = Common.ReadDouble(reader);
							double pt3 = Common.ReadDouble(reader);
							double pt4 = Common.ReadDouble(reader);

							road.GeoLines.Add(new GeoLine()
							{
								A = new GeoPoint() { Pt = new D2Point(pt1, pt2) },
								B = new GeoPoint() { Pt = new D2Point(pt3, pt4) },
							});
						}
						this.Roads.Add(road);
					}
				}
			}
			catch
			{
				this.Roads = null;
				throw;
			}
		}
	}
}
