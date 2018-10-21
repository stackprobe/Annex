using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class Tile
	{
		// LatMin == (Y + 0) * TILE_WH * (MeterPerMDot / 1000000.0) / MeterPerLat
		// LatMax == (Y + 1) * TILE_WH * (MeterPerMDot / 1000000.0) / MeterPerLat
		// LonMin == (X + 0) * TILE_WH * (MeterPerMDot / 1000000.0) / MeterPerLon
		// LonMax == (X + 1) * TILE_WH * (MeterPerMDot / 1000000.0) / MeterPerLon

		public ActiveTileTable Owner;
		public long X;
		public long Y;
		public Image BgImage;

		public void Added()
		{
			BgImage = new Bitmap(Consts.TILE_WH, Consts.TILE_WH);

			using (Graphics g = Graphics.FromImage(BgImage))
			{
				Pen pen = new Pen(Color.OrangeRed);

				g.FillRectangle(Brushes.White, 0, 0, Consts.TILE_WH, Consts.TILE_WH);
				g.DrawLine(pen, 0, 0, 0, Consts.TILE_WH - 1);
				g.DrawLine(pen, 0, 0, Consts.TILE_WH - 1, 0);
				g.DrawLine(pen, 0, Consts.TILE_WH - 1, Consts.TILE_WH - 1, Consts.TILE_WH - 1);
				g.DrawLine(pen, Consts.TILE_WH - 1, 0, Consts.TILE_WH - 1, Consts.TILE_WH - 1);

				double latMin = (Y + 0) * Consts.TILE_WH * (Owner.MeterPerMDot / 1000000.0) / Owner.MeterPerLat;
				double latMax = (Y + 1) * Consts.TILE_WH * (Owner.MeterPerMDot / 1000000.0) / Owner.MeterPerLat;
				double lonMin = (X + 0) * Consts.TILE_WH * (Owner.MeterPerMDot / 1000000.0) / Owner.MeterPerLon;
				double lonMax = (X + 1) * Consts.TILE_WH * (Owner.MeterPerMDot / 1000000.0) / Owner.MeterPerLon;

				List<string> dest = new List<string>();

				dest.Add("" + X);
				dest.Add("" + Y);
				dest.Add("(" + latMin.ToString("F9") + ", " + lonMin.ToString("F9") + ")");
				dest.Add("(" + latMax.ToString("F9") + ", " + lonMax.ToString("F9") + ")");

				g.DrawString(string.Join("\n", dest), new Font("メイリオ", 10F, FontStyle.Regular), Brushes.Navy, 0, 0);
			}


			Gnd.I.TileAddedDeleted++;
		}

		public void Deleted()
		{
			// todo ???


			Gnd.I.TileAddedDeleted--;
		}
	}
}
