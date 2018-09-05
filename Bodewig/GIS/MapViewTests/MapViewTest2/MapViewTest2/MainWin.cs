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
					Gnd.I.DegreePerMDot -= Gnd.I.Delta;
					Gnd.I.DegreePerMDot = IntTools.Range(Gnd.I.DegreePerMDot, 1, IntTools.IMAX);

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
			}
		}

		private void MapPanelMouseWheel(object sender, MouseEventArgs e)
		{
			Gnd.I.Delta += e.Delta;
		}

		private void Tiling()
		{
			// ---- ズームによるタイル更新と、その抑止 ----

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
				//this.UpdateActiveTiles(); // TODO
			}

			// ---- タイルの再配置 ----

			if (Gnd.I.ActiveTiles != null)
			{
				// TODO
			}

			// ----

			GC.Collect();
		}
	}
}
