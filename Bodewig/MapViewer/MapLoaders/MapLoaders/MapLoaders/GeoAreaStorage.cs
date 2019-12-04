using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.MapLoaders.Internal;
using Charlotte.Tools;

namespace Charlotte.MapLoaders
{
	public class GeoAreaStorage
	{
		public List<GeoArea> Areas = null;

		public void Load()
		{
			try
			{
				if (File.Exists(Consts.AREA_CACHE_FILE) == false)
					this.GMLToCache();

				DateTime t3 = DateTime.Now; // test
				this.LoadFromCache();
				DateTime t4 = DateTime.Now; // test

				Console.WriteLine("t3: " + (t4 - t3).TotalMilliseconds); // test
			}
			catch
			{
				this.Areas = null;
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

			this.Areas = null;
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

			Dictionary<string, List<GeoPoint>> curves = DictionaryTools.Create<List<GeoPoint>>(); // key: cv[0-9]+_[0-9]+
			Dictionary<string, GeoArea> areas = DictionaryTools.Create<GeoArea>(); // key: sf[0-9]+

			this.Reader = new StreamReader(Consts.AREA_GML_FILE);
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
									X = double.Parse(tokens[1]), // 順序注意
									Y = double.Parse(tokens[0]),
								},
							});
						}
						curves.Add(cvIdent, geoPoints);

						while (this.ReadLine() != "</gml:Curve>")
						{ }
					}
					else if (line.StartsWith("<gml:Surface "))
					{
						string sfIdent = StringTools.GetEnclosed(line, "\"", "\"").Inner; // sf[0-9]+
						List<GeoLine> geoLines = new List<GeoLine>();

						for (; ; )
						{
							line = this.ReadLine();

							if (line == "</gml:Surface>")
								break;

							if (line.StartsWith("<gml:curveMember "))
							{
								string cvIdent = StringTools.GetEnclosed(line, "#", "\"").Inner; // cv[0-9]+_[0-9]+
								List<GeoPoint> curve = curves[cvIdent];
								List<GeoLine> curveGeoLines = CurveToGeoLines(curve);

								geoLines.AddRange(curveGeoLines);
							}
						}
						areas.Add(sfIdent, new GeoArea()
						{
							GeoLines = geoLines,
						});
					}
					else if (line.StartsWith("<ksj:AdministrativeBoundary "))
					{
						string sfIdent = StringTools.GetEnclosed(this.ReadLine(), "#", "\"").Inner;
						string prm1 = StringTools.GetEnclosed(this.ReadLine(), ">", "<").Inner;
						string prm2 = StringTools.GetEnclosed(this.ReadLine(), ">", "<").Inner;
						string prm3 = StringTools.GetEnclosed(this.ReadLine(), ">", "<").Inner;
						string prm4 = StringTools.GetEnclosed(this.ReadLine(), ">", "<").Inner;
						string prm5 = StringTools.GetEnclosed(this.ReadLine(), ">", "<").Inner;

						GeoArea area = areas[sfIdent];

						area.PrefectureName = prm1;
						area.SubPrefectureName = prm2;
						area.CountyName = prm3;
						area.CityName = prm4;
						area.AdministrativeAreaCode = prm5;
					}
				}
			}
			finally
			{
				this.Reader.Dispose();
				this.Reader = null;
			}

			this.Areas = areas.Values.ToList();
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
				using (FileStream writer = new FileStream(Consts.AREA_CACHE_FILE, FileMode.Create, FileAccess.Write))
				{
					Common.WriteInt(writer, this.Areas.Count);

					foreach (GeoArea area in this.Areas)
					{
						Common.WriteString(writer, area.PrefectureName);
						Common.WriteString(writer, area.SubPrefectureName);
						Common.WriteString(writer, area.CountyName);
						Common.WriteString(writer, area.CityName);
						Common.WriteString(writer, area.AdministrativeAreaCode);
						Common.WriteInt(writer, area.GeoLines.Count);

						foreach (GeoLine geoLine in area.GeoLines)
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
				FileTools.Delete(Consts.AREA_CACHE_FILE); // FIXME デバッグ用に退避する？
				throw;
			}
		}

		private void LoadFromCache()
		{
			try
			{
				using (FileStream reader = new FileStream(Consts.AREA_CACHE_FILE, FileMode.Open, FileAccess.Read))
				{
					int areaCount = Common.ReadInt(reader);

					this.Areas = new List<GeoArea>(areaCount);

					for (int areaIndex = 0; areaIndex < areaCount; areaIndex++)
					{
						GeoArea area = new GeoArea();

						string prm1 = Common.ReadString(reader);
						string prm2 = Common.ReadString(reader);
						string prm3 = Common.ReadString(reader);
						string prm4 = Common.ReadString(reader);
						string prm5 = Common.ReadString(reader);

						area.PrefectureName = prm1;
						area.SubPrefectureName = prm2;
						area.CountyName = prm3;
						area.CityName = prm4;
						area.AdministrativeAreaCode = prm5;

						int geoLineCount = Common.ReadInt(reader);

						area.GeoLines = new List<GeoLine>(geoLineCount);

						for (int geoLineIndex = 0; geoLineIndex < geoLineCount; geoLineIndex++)
						{
							double pt1 = Common.ReadDouble(reader);
							double pt2 = Common.ReadDouble(reader);
							double pt3 = Common.ReadDouble(reader);
							double pt4 = Common.ReadDouble(reader);

							area.GeoLines.Add(new GeoLine()
							{
								A = new GeoPoint() { Pt = new D2Point(pt1, pt2) },
								B = new GeoPoint() { Pt = new D2Point(pt3, pt4) },
							});
						}
						this.Areas.Add(area);
					}
				}
			}
			catch
			{
				this.Areas = null;
				throw;
			}
		}
	}
}
