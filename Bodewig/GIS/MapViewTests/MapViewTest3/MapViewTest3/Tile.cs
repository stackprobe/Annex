using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class Tile
	{
		public GeoPoint CenterPoint;
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
		}

		public void Deleted()
		{
			// todo ???
		}
	}
}
