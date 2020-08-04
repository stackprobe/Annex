using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{a45b8c23-65b1-4a2b-92ec-117db3d0dc30}";
		public const string APP_TITLE = "TokyoMap";

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
			Test01();
		}

		private const double LAT_MIN = 35.583333333;
		private const double LAT_MAX = 35.75;
		private const double LON_MIN = 139.625;
		private const double LON_MAX = 139.875;

		private void Test01()
		{
			for (int c = 0; c < 20; c++)
			{
				Layers[c] = new Layer();
			}

			LoadMap();

			for (int c = 0; c < 20; c++)
			{
				Console.WriteLine("Layer: " + c + ", " + Layers[c].Lines.Count + ", " + Layers[c].Points.Count);
			}

			//MakeMap(35.587, 139.63, 0.001, 0.001, 10, 10); // 左下：武蔵新城駅の南口あたり
			MakeMap(35.587, 139.63, 0.01, 0.01, 10, 10); // 左下：武蔵新城駅の南口あたり
		}

		private void LoadMap()
		{
			foreach (string file in Directory.GetFiles(@"C:\var2\res\GIS\東京都地図", "FG-GML-*.xml", SearchOption.AllDirectories))
			{
				Console.WriteLine("file: " + file);

				Counters[0] = 0;
				Counters[1] = 0;
				Counters[2] = 0;
				Counters[3] = 0;

				XmlNode root = XmlNode.LoadFile(file);
				int c = 0;

				LoadMap(root, "AdmArea", c++);
				LoadMap(root, "AdmBdry", c++);
				LoadMap(root, "AdmPt", c++);
				LoadMap(root, "BldA", c++);
				LoadMap(root, "BldL", c++);
				LoadMap(root, "Cntr", c++);
				LoadMap(root, "CommBdry", c++);
				LoadMap(root, "CommPt", c++);
				LoadMap(root, "Cstline", c++);
				LoadMap(root, "ElevPt", c++);
				LoadMap(root, "GCP", c++);
				LoadMap(root, "RailCL", c++);
				LoadMap(root, "RdCompt", c++);
				LoadMap(root, "RdEdg", c++);
				LoadMap(root, "SBAPt", c++);
				LoadMap(root, "SBBdry", c++);
				LoadMap(root, "WA", c++);
				LoadMap(root, "WL", c++);
				LoadMap(root, "WStrA", c++);
				LoadMap(root, "WStrL", c++);

				Console.WriteLine("exterior: " + Counters[0]);
				Console.WriteLine("interior: " + Counters[1]);
				Console.WriteLine("line:     " + Counters[2]);
				Console.WriteLine("point:    " + Counters[3]);

				GC.Collect();
				Thread.Sleep(100); // HACK: GC完了待ち。意味ないかも
			}
		}

		private class GeoPoint
		{
			public double Lat;
			public double Lon;
		}

		private class GeoLine
		{
			public GeoPoint A;
			public GeoPoint B;
		}

		private class Layer
		{
			public List<GeoPoint> Points = new List<GeoPoint>();
			public List<GeoLine> Lines = new List<GeoLine>();
		}

		private Layer[] Layers = new Layer[20];
		private int[] Counters = new int[4];

		private void LoadMap(XmlNode root, string subRootName, int colorIdx)
		{
			foreach (XmlNode subRoot in root.Collect(subRootName))
			{
				foreach (XmlNode node in subRoot.Collect("area/Surface/patches/PolygonPatch/exterior/Ring/curveMember/Curve/segments/LineStringSegment/posList"))
				{
					LoadPolygon(Layers[colorIdx], node);
					Counters[0]++;
				}
				foreach (XmlNode node in subRoot.Collect("area/Surface/patches/PolygonPatch/interior/Ring/curveMember/Curve/segments/LineStringSegment/posList"))
				{
					LoadPolygon(Layers[colorIdx], node);
					Counters[1]++;
				}
				foreach (XmlNode node in subRoot.Collect("loc/Curve/segments/LineStringSegment/posList"))
				{
					LoadPolygon(Layers[colorIdx], node);
					Counters[2]++;
				}
				foreach (XmlNode node in subRoot.Collect("pos/Point/pos"))
				{
					LoadPoint(Layers[colorIdx], node);
					Counters[3]++;
				}
			}
		}

		private void LoadPolygon(Layer layer, XmlNode node)
		{
			string value = node.Value;
			value = value.Replace("\r", "");
			string[] lines = value.Split('\n');
			GeoPoint lastPoint = null;

			foreach (string line in lines)
			{
				GeoPoint point = CreatePoint(line);

				if (lastPoint != null)
				{
					layer.Lines.Add(new GeoLine()
					{
						A = lastPoint,
						B = point,
					});
				}
				lastPoint = point;
			}
		}

		private void LoadPoint(Layer layer, XmlNode node)
		{
			layer.Points.Add(CreatePoint(node.Value));
		}

		private GeoPoint CreatePoint(string line)
		{
			string[] tokens = line.Split(' ');

			return new GeoPoint()
			{
				Lat = double.Parse(tokens[0]),
				Lon = double.Parse(tokens[1]),
			};
		}

		private const int BMP_W = 1000;
		private const int BMP_H = 1000;

		private void MakeMap(double lat, double lon, double latSpan, double lonSpan, int latNum, int lonNum)
		{
			for (int latIdx = 0; latIdx < latNum; latIdx++)
			{
				for (int lonIdx = 0; lonIdx < lonNum; lonIdx++)
				{
					Console.WriteLine(latIdx + "_" + lonIdx);

					double lat1 = lat + latSpan * latIdx;
					double lat2 = lat + latSpan * (latIdx + 1);
					double lon1 = lon + lonSpan * lonIdx;
					double lon2 = lon + lonSpan * (lonIdx + 1);

					using (Bitmap bmp = new Bitmap(BMP_W, BMP_H))
					{
						using (Graphics grph = Graphics.FromImage(bmp))
						{
							grph.Clear(Color.White);

							for (int layer_index = 0; layer_index < 20; layer_index++)
							{
								Color color;

								{
									int r = layer_index / 9;
									int g = (layer_index / 3) % 3;
									int b = layer_index % 3;

									r = new int[] { 0, 127, 255 }[r];
									g = new int[] { 0, 127, 255 }[g];
									b = new int[] { 0, 127, 255 }[b];

									color = Color.FromArgb(r, g, b);
								}

								Layer layer = Layers[layer_index];

								foreach (GeoLine line in layer.Lines)
								{
									if (line.A.Lat < lat1 && line.B.Lat < lat1) continue;
									if (line.A.Lat > lat2 && line.B.Lat > lat2) continue;
									if (line.A.Lon < lon1 && line.B.Lon < lon1) continue;
									if (line.A.Lon > lon2 && line.B.Lon > lon2) continue;

									double x1 = (line.A.Lon - lon1) * BMP_W / (lon2 - lon1);
									double x2 = (line.B.Lon - lon1) * BMP_W / (lon2 - lon1);
									double y1 = (line.A.Lat - lat1) * BMP_H / (lat2 - lat1);
									double y2 = (line.B.Lat - lat1) * BMP_H / (lat2 - lat1);

									y1 = BMP_H - y1;
									y2 = BMP_H - y2;

									//grph.DrawLine(new Pen(color, 2f), (float)x1, (float)y1, (float)x2, (float)y2);
									grph.DrawLine(new Pen(color, 1f), (float)x1, (float)y1, (float)x2, (float)y2);
								}
								foreach (GeoPoint point in layer.Points)
								{
									if (point.Lat < lat1) continue;
									if (point.Lat > lat2) continue;
									if (point.Lon < lon1) continue;
									if (point.Lon > lon2) continue;

									double x = (point.Lon - lon1) * BMP_W / (lon2 - lon1);
									double y = (point.Lat - lat1) * BMP_H / (lat2 - lat1);

									y = BMP_H - y;

									int POINT_WH = 5;

									grph.FillRectangle(new SolidBrush(color), (float)(x - POINT_WH / 2.0), (float)(y - POINT_WH / 2.0), (float)POINT_WH, (float)POINT_WH);
								}
							}
						}
						bmp.Save(@"C:\temp\" + latIdx + "_" + lonIdx + ".png", ImageFormat.Png);
					}
					GC.Collect();
				}
			}
		}
	}
}
