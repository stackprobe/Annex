using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Utils;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();
		}

		private static bool WroteLog = false;

		private void MainWin_Load(object sender, EventArgs e)
		{
			ProcMain.WriteLog = message =>
			{
				try
				{
					using (new MSection("{afedb605-7ae5-435f-9294-ca227a9301be}"))
					using (StreamWriter writer = new StreamWriter(@"C:\temp\MapViewTest2.log", WroteLog, Encoding.UTF8))
					{
						writer.WriteLine("[" + DateTime.Now + "] " + message);
						WroteLog = true;
					}
				}
				catch
				{ }
			};
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.MapPicture.MouseWheel += MapPictureMouseWheel;

			// ----

			this.MTEnabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;

			// ----

			// -- 9999
		}

		private void BeforeDialog()
		{
			this.MTEnabled = false;
		}

		private void AfterDialog()
		{
			this.MTEnabled = true;
		}

		private void CloseWindow()
		{
			this.MTEnabled = false;

			// ----

			// -- 9000

			// ----

			this.Close();
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private int ZoomingCounter = 0;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				if (1 <= this.ZoomingCounter)
				{
					this.ZoomingCounter--;
				}
				else
				{
					this.ChangeActiveTiles();
				}
				this.ActiveTilesToUI();

				GC.Collect();
			}
			catch (Exception ex)
			{
				ProcMain.WriteLog(ex);
			}
			finally
			{
				this.MTBusy = false;
				this.MTCount++;
			}
		}

		private void MapPicture_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void MapPicture_MouseDown(object sender, MouseEventArgs e)
		{
			Gnd.I.DownPoint = e.Location;
			Gnd.I.DownCenterPoint = Gnd.I.CenterPoint;
		}

		private void MapPicture_MouseUp(object sender, MouseEventArgs e)
		{
			if (Gnd.I.DownPoint != null)
			{
				int diffX = e.Location.X - Gnd.I.DownPoint.Value.X;
				int diffY = e.Location.Y - Gnd.I.DownPoint.Value.Y;

				Gnd.I.DownPoint = null;

				double diffLat = diffY * (Gnd.I.MeterPerMDot / 1000000.0) / Gnd.I.MeterPerLat * -1;
				double diffLon = diffX * (Gnd.I.MeterPerMDot / 1000000.0) / Gnd.I.MeterPerLon;

				Gnd.I.CenterPoint.Lat = Gnd.I.DownCenterPoint.Lat - diffLat;
				Gnd.I.CenterPoint.Lon = Gnd.I.DownCenterPoint.Lon - diffLon;
			}
		}

		private void MapPicture_MouseMove(object sender, MouseEventArgs e)
		{
			if (Gnd.I.DownPoint != null)
			{
				int diffX = e.Location.X - Gnd.I.DownPoint.Value.X;
				int diffY = e.Location.Y - Gnd.I.DownPoint.Value.Y;

				double diffLat = diffY * (Gnd.I.MeterPerMDot / 1000000.0) / Gnd.I.MeterPerLat * -1;
				double diffLon = diffX * (Gnd.I.MeterPerMDot / 1000000.0) / Gnd.I.MeterPerLon;

				Gnd.I.CenterPoint.Lat = Gnd.I.DownCenterPoint.Lat - diffLat;
				Gnd.I.CenterPoint.Lon = Gnd.I.DownCenterPoint.Lon - diffLon;
			}
		}

		private void MapPictureMouseWheel(object sender, MouseEventArgs e)
		{
			int dlt = e.Delta;
			int vDlt = DoubleTools.ToInt(Gnd.I.MeterPerMDot * (dlt / 1000.0));

			if (vDlt == 0)
				vDlt = dlt < 0 ? -1 : 1;

			Gnd.I.MeterPerMDot -= vDlt;
			Gnd.I.MeterPerMDot = IntTools.Range(Gnd.I.MeterPerMDot, Consts.MPMD_MIN, Consts.MPMD_MAX);

			Gnd.I.ActiveTiles.StateChanged = true;

			this.ZoomingCounter = Consts.ZOOMING_COUNTER_MAX;
		}

		private void MapPicture_Resize(object sender, EventArgs e)
		{
			Gnd.I.ActiveTiles.StateChanged = true;
		}

		private void ChangeActiveTiles()
		{
			if (
				Gnd.I.ActiveTiles != null &&
				Gnd.I.ActiveTiles.MeterPerMDot == Gnd.I.MeterPerMDot &&
				Gnd.I.ActiveTiles.MeterPerLat == Gnd.I.MeterPerLat &&
				Gnd.I.ActiveTiles.MeterPerLon == Gnd.I.MeterPerLon &&
				CrashUtils.IsCrashed(Gnd.I.ActiveTiles.CenterPoint, Gnd.I.CenterPoint)
				)
				return;

			if (Gnd.I.ActiveTiles == null)
				Gnd.I.ActiveTiles = new ActiveTiles();

			Gnd.I.ActiveTiles.MeterPerMDot = Gnd.I.MeterPerMDot;
			Gnd.I.ActiveTiles.MeterPerLat = Gnd.I.MeterPerLat;
			Gnd.I.ActiveTiles.MeterPerLon = Gnd.I.MeterPerLon;
			Gnd.I.ActiveTiles.CenterPoint = Gnd.I.CenterPoint;
			Gnd.I.ActiveTiles.StateChanged = true;

			double latPerDot = (Gnd.I.ActiveTiles.MeterPerMDot / 1000000.0) / Gnd.I.ActiveTiles.MeterPerLat;
			double lonPerDot = (Gnd.I.ActiveTiles.MeterPerMDot / 1000000.0) / Gnd.I.ActiveTiles.MeterPerLon;

			double latPerTileH = latPerDot * Consts.TILE_WH;
			double lonPerTileW = lonPerDot * Consts.TILE_WH;

			double centerTileLat = DoubleTools.ToInt(Gnd.I.ActiveTiles.CenterPoint.Lat / latPerTileH) * latPerTileH;
			double centerTileLon = DoubleTools.ToInt(Gnd.I.ActiveTiles.CenterPoint.Lon / lonPerTileW) * lonPerTileW;

			for (int index = 0; index < Gnd.I.ActiveTiles.Tiles.Count; index++)
			{
				Tile tile = Gnd.I.ActiveTiles.Tiles[index];

				if (
					tile.CenterPoint.Lat < centerTileLat - latPerTileH * Consts.DELETING_XY_RANGE ||
					tile.CenterPoint.Lat > centerTileLat + latPerTileH * Consts.DELETING_XY_RANGE ||
					tile.CenterPoint.Lon < centerTileLon - lonPerTileW * Consts.DELETING_XY_RANGE ||
					tile.CenterPoint.Lon > centerTileLon + lonPerTileW * Consts.DELETING_XY_RANGE
					)
				{
					tile.Deleted();
					tile = null;

					Gnd.I.ActiveTiles.Tiles.RemoveAt(index);
				}
			}

			for (int x = -Consts.ADDING_XY_RANGE; x <= Consts.ADDING_XY_RANGE; x++)
			{
				for (int y = -Consts.ADDING_XY_RANGE; y <= Consts.ADDING_XY_RANGE; y++)
				{
					GeoPoint gPoint = new GeoPoint(
						centerTileLat + latPerTileH * y,
						centerTileLon + lonPerTileW * x
						);

					if (Gnd.I.ActiveTiles.Tiles.Any(tile => CrashUtils.IsCrashed(gPoint, tile.CenterPoint)) == false)
					{
						Tile tile = new Tile()
						{
							CenterPoint = gPoint,
							Bmp = null,
						};

						tile.Added();

						Gnd.I.ActiveTiles.Tiles.Add(tile);
					}
				}
			}
		}

		private void ActiveTilesToUI()
		{
			if (Gnd.I.ActiveTiles == null)
				return;

			if (Gnd.I.ActiveTiles.StateChanged == false)
				return;

			Gnd.I.ActiveTiles.StateChanged = false;

			Bitmap bmp = new Bitmap(this.MapPicture.Width, this.MapPicture.Height);
			int bmp_w = bmp.Width;
			int bmp_h = bmp.Height;

			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.FillRectangle(Brushes.Blue, 0, 0, bmp_w, bmp_h);

				foreach (Tile tile in Gnd.I.ActiveTiles.Tiles)
				{
					double meterPerDot = Gnd.I.ActiveTiles.MeterPerMDot / 1000000.0;
					double latPerDot = (Gnd.I.ActiveTiles.MeterPerMDot / 1000000.0) / Gnd.I.ActiveTiles.MeterPerLat;
					double lonPerDot = (Gnd.I.ActiveTiles.MeterPerMDot / 1000000.0) / Gnd.I.ActiveTiles.MeterPerLon;

					GeoPoint sw = new GeoPoint(
						tile.CenterPoint.Lat - (Consts.TILE_WH / 2) * latPerDot,
						tile.CenterPoint.Lon - (Consts.TILE_WH / 2) * lonPerDot
						);

					GeoPoint ne = new GeoPoint(
						tile.CenterPoint.Lat + (Consts.TILE_WH / 2) * latPerDot,
						tile.CenterPoint.Lon + (Consts.TILE_WH / 2) * lonPerDot
						);

					meterPerDot = Gnd.I.MeterPerMDot / 1000000.0; // ここでは Gnd.I. の MPMD を使う。
					latPerDot = meterPerDot / Gnd.I.ActiveTiles.MeterPerLat;
					lonPerDot = meterPerDot / Gnd.I.ActiveTiles.MeterPerLon;

					int l = DoubleTools.ToInt((sw.Lon - Gnd.I.ActiveTiles.CenterPoint.Lon) / lonPerDot);
					int r = DoubleTools.ToInt((ne.Lon - Gnd.I.ActiveTiles.CenterPoint.Lon) / lonPerDot);
					int t = DoubleTools.ToInt((ne.Lat - Gnd.I.ActiveTiles.CenterPoint.Lat) / latPerDot) * -1;
					int b = DoubleTools.ToInt((sw.Lat - Gnd.I.ActiveTiles.CenterPoint.Lat) / latPerDot) * -1;

					l += bmp_w / 2;
					r += bmp_w / 2;
					t += bmp_h / 2;
					b += bmp_h / 2;

					if (CrashUtils.IsCrashed_Rect_Rect(0, 0, bmp_w, bmp_h, l, t, r, b))
					{
						g.DrawImage(tile.Bmp, l, t, r - l, b - t);
					}
				}
			}
			this.MapPicture.Image = bmp;
		}
	}
}
