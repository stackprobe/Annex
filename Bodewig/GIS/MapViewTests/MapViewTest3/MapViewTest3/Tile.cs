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
		public Bitmap Bmp;

		public void Added()
		{
			Bmp = new Bitmap(Consts.TILE_WH, Consts.TILE_WH);

			using (Graphics g = Graphics.FromImage(Bmp))
			{
				g.FillRectangle(Brushes.White, 0, 0, Consts.TILE_WH, Consts.TILE_WH);
				g.DrawLine(new Pen(Color.Blue), 0, 0, 0, Consts.TILE_WH - 1);
				g.DrawLine(new Pen(Color.Blue), 0, 0, Consts.TILE_WH - 1, 0);
				g.DrawLine(new Pen(Color.Blue), 0, Consts.TILE_WH - 1, Consts.TILE_WH - 1, Consts.TILE_WH - 1);
				g.DrawLine(new Pen(Color.Blue), Consts.TILE_WH - 1, 0, Consts.TILE_WH - 1, Consts.TILE_WH - 1);
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
