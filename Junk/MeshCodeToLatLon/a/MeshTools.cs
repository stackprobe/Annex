using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a
{
	public class MeshTools
	{
		public string GetMeshCode(double lat, double lon)
		{
			this.Lat = lat;
			this.Lon = lon;
			this.LatLonToXY();
			this.XYToMeshCode();
			return this.MeshCode;
		}

		public double[] GetLatLon(string meshCode)
		{
			this.MeshCode = meshCode;
			this.MeshCodeToXY();
			this.XYToLatLon();
			return new double[] { this.Lat, this.Lon };
		}

		public double Lat;
		public double Lon;
		public int X;
		public int Y;
		public string MeshCode;

		public void LatLonToXY()
		{
			double y = this.Lat * 1.5;
			double x = this.Lon - 100.0;

			x *= 80.0;
			y *= 80.0;

			this.X = (int)x;
			this.Y = (int)y;
		}

		public void XYToLatLon()
		{
			if (!NumTools.IsRange(this.X, 0, 7999)) throw new Exception();
			if (!NumTools.IsRange(this.Y, 0, 7999)) throw new Exception();

			double x = (double)this.X;
			double y = (double)this.Y;

			x /= 80.0;
			y /= 80.0;

			this.Lat = y / 1.5;
			this.Lon = x + 100.0;
		}

		public void XYToMeshCode()
		{
			int x = this.X;
			int y = this.Y;

			if (!NumTools.IsRange(x, 0, 7999)) throw new Exception();
			if (!NumTools.IsRange(y, 0, 7999)) throw new Exception();

			int mc3X = x % 10;
			int mc3Y = y % 10;
			x /= 10;
			y /= 10;
			int mc2X = x % 8;
			int mc2Y = y % 8;
			x /= 8;
			y /= 8;
			int mc1X = x;
			int mc1Y = y;

			this.MeshCode =
				StringTools.ZPad("" + mc1Y, 2) +
				StringTools.ZPad("" + mc1X, 2) +
				StringTools.ZPad("" + mc2Y, 1) +
				StringTools.ZPad("" + mc2X, 1) +
				StringTools.ZPad("" + mc3Y, 1) +
				StringTools.ZPad("" + mc3X, 1);
		}

		public void MeshCodeToXY()
		{
			string mc = this.MeshCode;

			if (StringTools.ReplaceNum9(mc) != "99999999") throw new Exception();

			int mc1Y = int.Parse(mc.Substring(0, 2));
			int mc1X = int.Parse(mc.Substring(2, 2));
			int mc2Y = int.Parse(mc.Substring(4, 1));
			int mc2X = int.Parse(mc.Substring(5, 1));
			int mc3Y = int.Parse(mc.Substring(6, 1));
			int mc3X = int.Parse(mc.Substring(7, 1));

			if (!NumTools.IsRange(mc1Y, 0, 99)) throw new Exception();
			if (!NumTools.IsRange(mc1X, 0, 99)) throw new Exception();
			if (!NumTools.IsRange(mc2Y, 0, 7)) throw new Exception();
			if (!NumTools.IsRange(mc2X, 0, 7)) throw new Exception();
			if (!NumTools.IsRange(mc3Y, 0, 9)) throw new Exception();
			if (!NumTools.IsRange(mc3X, 0, 9)) throw new Exception();

			this.X = mc1X * 80 + mc2X * 10 + mc3X;
			this.Y = mc1Y * 80 + mc2Y * 10 + mc3Y;
		}
	}
}
