using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Charlotte
{
	public class Gnd
	{
		private static Gnd _i = null;

		public static Gnd I
		{
			get
			{
				if (_i == null)
					_i = new Gnd();

				return _i;
			}
		}

		public GeoPoint CenterLatLon = new GeoPoint(139.0, 35.0);
		public int DegreePerMDot = 1000;
		public int Delta;

		public Point? DownPoint = null;
		public GeoPoint DownCenterLatLon;

		public GeoPoint? ClickedPoint = null;

		public class Tile
		{
			public PictureBox Pic;

			public int DegreePerMDot;
			public int L;
			public int B;

			// 左座標 == L * DegreePerMDot * TILE_WH / 1,000,000
			// 下座標 == B * DegreePerMDot * TILE_WH / 1,000,000
		}

		public class ActiveTileInfo
		{
			public GeoPoint CenterLatLon;
			public int DegreePerMDot;

			// [0][0] == 左下
			// [W - 1][0] == 右下
			// [0][H - 1] == 左上
			// [W - 1][H - 1] == 右上

			public Tile[][] Table;
			public int L; // == Table[0][0].L
			public int B; // == Table[0][0].B
			public int W; // == Table.Length
			public int H; // == Table[0].Length
		}

		public ActiveTileInfo ActiveTiles = null;

		public int LastDegreePerMDot = -1;
		public int ZoomingCounter = 0;
	}
}
