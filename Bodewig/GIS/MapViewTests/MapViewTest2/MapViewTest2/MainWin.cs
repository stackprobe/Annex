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

namespace Charlotte
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();
		}

		private static bool WroteLog;

		private void MainWin_Load(object sender, EventArgs e)
		{
			ProcMain.WriteLog = message =>
			{
				try
				{
					using (new MSection("{1730cb8a-2211-46f0-b6a5-e87636217def}"))
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
			this.MapPanel.MouseWheel += this.MapPanelMouseWheel;

			this.MTEnabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;
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
			this.Close();
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				if (Gnd.I.Delta != 0)
				{
					int vDlt = DoubleTools.ToInt(Gnd.I.DegreePerMDot * (Gnd.I.Delta / 1000.0));

					if (vDlt == 0)
						vDlt = Gnd.I.Delta < 0 ? -1 : 1;

					Gnd.I.DegreePerMDot -= vDlt;
					Gnd.I.DegreePerMDot = IntTools.Range(Gnd.I.DegreePerMDot, 1, 10000);

					Gnd.I.Delta = 0;
				}
				this.Text =
					Utils.ToString(Gnd.I.CenterLatLon) + ", " +
					Gnd.I.DegreePerMDot + ", " +
					Utils.ToString(Gnd.I.DownPoint) + ", " +
					Utils.ToString(Gnd.I.ClickedPoint);

				this.Tiling();
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

		private void MapPanel_Paint(object sender, PaintEventArgs e)
		{
			// noop
		}

		private void MapPanel_MouseDown(object sender, MouseEventArgs e)
		{
			Gnd.I.DownPoint = e.Location;
			Gnd.I.DownCenterLatLon = Gnd.I.CenterLatLon;
		}

		private void MapPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (
				Gnd.I.DownPoint != null &&
				Gnd.I.DownPoint.Value.X == e.Location.X &&
				Gnd.I.DownPoint.Value.Y == e.Location.Y
				)
			{
				int mX = e.Location.X - (this.MapPanel.Width / 2);
				int mY = e.Location.Y - (this.MapPanel.Height / 2);

				Gnd.I.ClickedPoint = new GeoPoint(
					Gnd.I.CenterLatLon.X + mX * (Gnd.I.DegreePerMDot / 1000000.0),
					Gnd.I.CenterLatLon.X - mY * (Gnd.I.DegreePerMDot / 1000000.0)
					);
			}
			Gnd.I.DownPoint = null;
		}

		private void MapPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (Gnd.I.DownPoint != null)
			{
				int mX = e.Location.X - Gnd.I.DownPoint.Value.X;
				int mY = e.Location.Y - Gnd.I.DownPoint.Value.Y;

				Gnd.I.CenterLatLon = new GeoPoint(
					Gnd.I.DownCenterLatLon.X - mX * (Gnd.I.DegreePerMDot / 1000000.0),
					Gnd.I.DownCenterLatLon.Y + mY * (Gnd.I.DegreePerMDot / 1000000.0)
					);

				// Range
				{
					double x = Gnd.I.CenterLatLon.X;
					double y = Gnd.I.CenterLatLon.Y;

					x = DoubleTools.Range(x, 120.0, 160.0);
					y = DoubleTools.Range(y, 20.0, 50.0);

					Gnd.I.CenterLatLon.X = x;
					Gnd.I.CenterLatLon.Y = y;
				}
			}
		}

		private void MapPanelMouseWheel(object sender, MouseEventArgs e)
		{
			Gnd.I.Delta += e.Delta;
		}

		private void Tiling()
		{
			// ---- タイル更新と、その抑止 ----

			if (Gnd.I.LastDegreePerMDot != Gnd.I.DegreePerMDot)
			{
				Gnd.I.LastDegreePerMDot = Gnd.I.DegreePerMDot;
				Gnd.I.ZoomingCounter = Consts.ZOOMING_COUNTER_MAX;
			}
			else if (1 <= Gnd.I.ZoomingCounter)
			{
				Gnd.I.ZoomingCounter--;
			}
			else
			{
				this.UpdateActiveTiles();
			}

			// ---- タイルの再配置 ----

			if (Gnd.I.ActiveTiles != null)
			{
				this.ChangeUIActiveTiles();
			}

			// ----

			GC.Collect();
		}

		private void UpdateActiveTiles()
		{
			// ? 中心座標＆ズーム変わっていない。-> 更新不要
			if (
				Gnd.I.ActiveTiles != null &&
				Utils.IsSameOrNear(Gnd.I.ActiveTiles.CenterLatLon.X, Gnd.I.CenterLatLon.X) &&
				Utils.IsSameOrNear(Gnd.I.ActiveTiles.CenterLatLon.Y, Gnd.I.CenterLatLon.Y) &&
				Gnd.I.ActiveTiles.DegreePerMDot == Gnd.I.DegreePerMDot &&
				Gnd.I.ActiveTiles.MapPanelW == this.MapPanel.Width &&
				Gnd.I.ActiveTiles.MapPanelH == this.MapPanel.Height
				)
				return;

			double x1 = Gnd.I.CenterLatLon.X - (this.MapPanel.Width / 2) * (Gnd.I.DegreePerMDot / 1000000.0); // MapPanel 左
			double x2 = Gnd.I.CenterLatLon.X + (this.MapPanel.Width / 2) * (Gnd.I.DegreePerMDot / 1000000.0); // MapPanel 右
			double y1 = Gnd.I.CenterLatLon.Y - (this.MapPanel.Height / 2) * (Gnd.I.DegreePerMDot / 1000000.0); // MapPanel 下
			double y2 = Gnd.I.CenterLatLon.Y + (this.MapPanel.Height / 2) * (Gnd.I.DegreePerMDot / 1000000.0); // MapPanel 上

			int l = GetTileLB(x1, Gnd.I.DegreePerMDot);
			int r = GetTileLB(x2, Gnd.I.DegreePerMDot);
			int b = GetTileLB(y1, Gnd.I.DegreePerMDot);
			int t = GetTileLB(y2, Gnd.I.DegreePerMDot);
			int w = (r - l) + 1;
			int h = (t - b) + 1;

			if (w < 1) throw null; // test
			if (h < 1) throw null; // test

			Gnd.ActiveTileTable activeTilesNew = new Gnd.ActiveTileTable();

			activeTilesNew.CenterLatLon = Gnd.I.CenterLatLon;
			activeTilesNew.DegreePerMDot = Gnd.I.DegreePerMDot;
			activeTilesNew.MapPanelW = this.MapPanel.Width;
			activeTilesNew.MapPanelH = this.MapPanel.Height;

			activeTilesNew.Table = new Gnd.Tile[w][];
			activeTilesNew.L = l;
			activeTilesNew.B = b;
			activeTilesNew.W = w;
			activeTilesNew.H = h;

			for (int x = 0; x < w; x++)
				activeTilesNew.Table[x] = new Gnd.Tile[h];

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					int tileL = l + x;
					int tileB = b + y;

					Gnd.Tile tile = TakeTile(tileL, tileB, Gnd.I.DegreePerMDot, Gnd.I.ActiveTiles);

					if (tile == null)
					{
						tile = CreateTile(tileL, tileB, Gnd.I.DegreePerMDot);
						activeTilesNew.AddedTiles.Add(tile);
					}
					activeTilesNew.Table[x][y] = tile;
				}
			}
			if (Gnd.I.ActiveTiles != null)
				for (int x = 0; x < Gnd.I.ActiveTiles.W; x++)
					for (int y = 0; y < Gnd.I.ActiveTiles.H; y++)
						if (Gnd.I.ActiveTiles.Table[x][y] != null)
							activeTilesNew.DeletedTiles.Add(Gnd.I.ActiveTiles.Table[x][y]);

			Gnd.I.ActiveTiles = activeTilesNew;
		}

		private Gnd.Tile CreateTile(int tileL, int tileB, int tileDegreePerMDot)
		{
			Gnd.Tile tile = new Gnd.Tile();

			tile.Pic = new PictureBox();
			tile.Pic.Image = CreateTilePic(tileL, tileB, tileDegreePerMDot);
			tile.Pic.SizeMode = PictureBoxSizeMode.StretchImage;

			tile.DegreePerMDot = tileDegreePerMDot;
			tile.L = tileL;
			tile.B = tileB;

			// イベント

			tile.Pic.MouseDown += (sender, e) =>
			{
				MouseEventArgs mea = new MouseEventArgs(
					MouseButtons.Left,
					0,
					tile.Pic.Left + e.Location.X,
					tile.Pic.Top + e.Location.Y,
					0
					);

				MapPanel_MouseDown(null, mea);
			};

			tile.Pic.MouseUp += (sender, e) =>
			{
				MouseEventArgs mea = new MouseEventArgs(
					MouseButtons.Left,
					0,
					tile.Pic.Left + e.Location.X,
					tile.Pic.Top + e.Location.Y,
					0
					);

				MapPanel_MouseUp(null, mea);
			};

			tile.Pic.MouseMove += (sender, e) =>
			{
				MouseEventArgs mea = new MouseEventArgs(
					MouseButtons.Left,
					0,
					tile.Pic.Left + e.Location.X,
					tile.Pic.Top + e.Location.Y,
					0
					);

				MapPanel_MouseMove(null, mea);
			};

			// ホイールのイベントは拾ってくれる。

			return tile;
		}

		private Image CreateTilePic(int tileL, int tileB, int tileDegreePerMDot)
		{
			double x1 = (tileL + 0) * tileDegreePerMDot * (Consts.TILE_WH / 1000000.0);
			double x2 = (tileL + 1) * tileDegreePerMDot * (Consts.TILE_WH / 1000000.0);
			double y1 = (tileB + 0) * tileDegreePerMDot * (Consts.TILE_WH / 1000000.0);
			double y2 = (tileB + 1) * tileDegreePerMDot * (Consts.TILE_WH / 1000000.0);

			Bitmap bmp = new Bitmap(Consts.TILE_WH, Consts.TILE_WH);

			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.White);
				g.DrawLine(new Pen(Color.Red, 1f), 0f, 0f, Consts.TILE_WH - 1f, 0f);
				g.DrawLine(new Pen(Color.Red, 1f), 0f, 0f, 0f, Consts.TILE_WH - 1f);
				g.DrawLine(new Pen(Color.Red, 1f), Consts.TILE_WH - 1f, 0f, Consts.TILE_WH - 1f, Consts.TILE_WH - 1f);
				g.DrawLine(new Pen(Color.Red, 1f), 0f, Consts.TILE_WH - 1f, Consts.TILE_WH - 1f, Consts.TILE_WH - 1f);
				g.DrawString(
					"(" + x1 + ", " + y1 + ")\r\n(" + x2 + ", " + y2 + ")",
					new Font("メイリオ", 10f, FontStyle.Regular),
					Brushes.Blue,
					1f,
					1f
					);
			}
			return bmp;
		}

		private Gnd.Tile TakeTile(int tileL, int tileB, int tileDegreePerMDot, Gnd.ActiveTileTable src)
		{
			if (
				src != null &&
				src.DegreePerMDot == tileDegreePerMDot &&
				src.L <= tileL && tileL < src.L + src.W &&
				src.B <= tileB && tileB < src.B + src.H
				)
			{
				int x = tileL - src.L;
				int y = tileB - src.B;

				Gnd.Tile tile = src.Table[x][y];

				src.Table[x][y] = null;

				if (tile.L != tileL) throw null; // test
				if (tile.B != tileB) throw null; // test

				return tile;
			}
			return null;
		}

		private int GetTileLB(double degree, int degreePerMDot)
		{
			int p1 = 0;
			int p2 = IntTools.IMAX;

			while (p1 + 1 < p2)
			{
				int m = (p1 + p2) / 2;
				double mDeg = m * degreePerMDot * (Consts.TILE_WH / 1000000.0);

				if (mDeg < degree)
				{
					p1 = m;
				}
				else
				{
					p2 = m;
				}
			}
			return p1;
		}

		private void ChangeUIActiveTiles()
		{
			foreach (Gnd.Tile tile in Gnd.I.ActiveTiles.DeletedTiles)
				this.MapPanel.Controls.Remove(tile.Pic);

			foreach (Gnd.Tile tile in Gnd.I.ActiveTiles.AddedTiles)
				this.MapPanel.Controls.Add(tile.Pic);

			// ---- 再配置 ----

			double x1 = Gnd.I.CenterLatLon.X - (this.MapPanel.Width / 2) * (Gnd.I.DegreePerMDot / 1000000.0); // MapPanel 左
			double x2 = Gnd.I.CenterLatLon.X + (this.MapPanel.Width / 2) * (Gnd.I.DegreePerMDot / 1000000.0); // MapPanel 右
			double y1 = Gnd.I.CenterLatLon.Y - (this.MapPanel.Height / 2) * (Gnd.I.DegreePerMDot / 1000000.0); // MapPanel 下
			double y2 = Gnd.I.CenterLatLon.Y + (this.MapPanel.Height / 2) * (Gnd.I.DegreePerMDot / 1000000.0); // MapPanel 上

			for (int x = 0; x < Gnd.I.ActiveTiles.W; x++)
			{
				for (int y = 0; y < Gnd.I.ActiveTiles.H; y++)
				{
					Gnd.Tile tile = Gnd.I.ActiveTiles.Table[x][y];

					double tx1 = (tile.L + 0) * tile.DegreePerMDot * (Consts.TILE_WH / 1000000.0);
					double tx2 = (tile.L + 1) * tile.DegreePerMDot * (Consts.TILE_WH / 1000000.0);
					double ty1 = (tile.B + 0) * tile.DegreePerMDot * (Consts.TILE_WH / 1000000.0);
					double ty2 = (tile.B + 1) * tile.DegreePerMDot * (Consts.TILE_WH / 1000000.0);

					double dL = (tx1 - x1) * this.MapPanel.Width / (x2 - x1);
					double dR = (tx2 - x1) * this.MapPanel.Width / (x2 - x1);
					double dB = (ty1 - y1) * this.MapPanel.Height / (y2 - y1);
					double dT = (ty2 - y1) * this.MapPanel.Height / (y2 - y1);
					double dW = dR - dL;
					double dH = dT - dB;

					bool fault = false;

					if (dW < 10.0) // ? 幅が狭すぎる。
					{
						dW = 10.0;
					}
					else if (this.MapPanel.Width * 2.0 < dW) // ? 幅が広すぎる。
					{
						fault = true;
					}
					if (dH < 10.0) // ? 高さが狭すぎる。
					{
						dH = 10.0;
					}
					else if (this.MapPanel.Height * 2.0 < dH) // ? 高さが広すぎる。
					{
						fault = true;
					}

					int[] ltwh;

					if (fault)
					{
						ltwh = new int[] { -300, -300, 200, 200 };
					}
					else
					{
						int iL = DoubleTools.ToInt(dL);
						int iR = DoubleTools.ToInt(dR);
						int iB = DoubleTools.ToInt(dB);
						int iT = DoubleTools.ToInt(dT);
						int iW = iR - iL;
						int iH = iT - iB;

						if (iW < 1) throw null; // test
						if (iH < 1) throw null; // test

						ltwh = new int[] { iL, this.MapPanel.Height - iT, iW, iH };
					}

					if (
						tile.Pic_LTWH != null &&
						tile.Pic_LTWH[0] == ltwh[0] &&
						tile.Pic_LTWH[1] == ltwh[1] &&
						tile.Pic_LTWH[2] == ltwh[2] &&
						tile.Pic_LTWH[3] == ltwh[3]
						)
					{
						// noop -- 位置が変わっていない。
					}
					else
					{
						tile.Pic.Left = ltwh[0];
						tile.Pic.Top = ltwh[1];
						tile.Pic.Width = ltwh[2];
						tile.Pic.Height = ltwh[3];

						tile.Pic_LTWH = ltwh;
					}
				}
			}

			// 再配置ここまで
		}
	}
}
