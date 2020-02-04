using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Drawing;
using Charlotte.MapLoaders;

namespace Charlotte.MapDrawers
{
	public class MapDrawer
	{
		public I2Size ImageSize = new I2Size(800, 600);

		// <---- prm

		private GeoAreaStorage AreaStorage = new GeoAreaStorage();
		private GeoRoadStorage RoadStorage = new GeoRoadStorage();

		public MapDrawer()
		{
			this.AreaStorage.Load();
			this.RoadStorage.Load();
		}

		public Image Draw(double lat, double lon, double meterPerDot)
		{
			double meter_w = this.ImageSize.W * meterPerDot;
			double meter_h = this.ImageSize.H * meterPerDot;

			double meterPerLat = MeterLatLon.MeterPerLat();
			double meterPerLon = MeterLatLon.MeterPerLon(lat);

			double lon_w = meter_w / meterPerLon;
			double lat_h = meter_h / meterPerLat;

			double lon1 = lon - lon_w / 2.0;
			double lon2 = lon + lon_w / 2.0;
			double lat1 = lat - lat_h / 2.0;
			double lat2 = lat + lat_h / 2.0;

			Bitmap image = new Bitmap(this.ImageSize.W, this.ImageSize.H);

			using (Graphics g = Graphics.FromImage(image))
			{
				g.FillRectangle(Brushes.White, 0, 0, this.ImageSize.W, this.ImageSize.H);

				Action<GeoLine, Color> drawLine = (geoLine, color) =>
				{
					double x1 = Math.Min(geoLine.A.Pt.X, geoLine.B.Pt.X);
					double x2 = Math.Max(geoLine.A.Pt.X, geoLine.B.Pt.X);
					double y1 = Math.Min(geoLine.A.Pt.Y, geoLine.B.Pt.Y);
					double y2 = Math.Max(geoLine.A.Pt.Y, geoLine.B.Pt.Y);

					if (x2 < lon1 || lon2 < x1 || y2 < lat1 || lat2 < y1)
						return;

					double ax = (geoLine.A.Pt.X - lon1) * this.ImageSize.W / lon_w;
					double ay = (geoLine.A.Pt.Y - lat1) * this.ImageSize.H / lat_h;
					double bx = (geoLine.B.Pt.X - lon1) * this.ImageSize.W / lon_w;
					double by = (geoLine.B.Pt.Y - lat1) * this.ImageSize.H / lat_h;

					ay = this.ImageSize.H - ay;
					by = this.ImageSize.H - by;

					g.DrawLine(new Pen(new SolidBrush(color)), (float)ax, (float)ay, (float)bx, (float)by);
				};

				foreach (GeoArea area in this.AreaStorage.Areas)
					foreach (GeoLine line in area.GeoLines)
						drawLine(line, Color.Orange);

				foreach (GeoRoad road in this.RoadStorage.Roads)
					foreach (GeoLine line in road.GeoLines)
						drawLine(line, Color.DarkGreen);
			}
			return image;
		}
	}
}
