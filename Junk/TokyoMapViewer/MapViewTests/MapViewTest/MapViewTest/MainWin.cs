using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

			PictureBox pb = new PictureBox();

			pb.Image = new Bitmap(400, 300);
			using (Graphics g = Graphics.FromImage(pb.Image))
			{
				g.Clear(Color.Blue);
			}

			pb.Left = 50;
			pb.Top = 50;
			pb.Width = 400;
			pb.Height = 300;

			//pb.Height = 500;
			pb.SizeMode = PictureBoxSizeMode.StretchImage;

			pb.MouseDown += (sdr, ev) =>
			{
				this.MainPanel_MouseDown(sdr, ev);
			};

			pb.MouseMove += (sdr, ev) =>
			{
				MPoint = new Point(pb.Location.X + ev.Location.X, pb.Location.Y + ev.Location.Y);
				MRefresh();
			};

			pb.MouseUp += (sdr, ev) =>
			{
				this.MainPanel_MouseUp(sdr, ev);
			};

			pb.MouseWheel += (sdr, ev) =>
			{
				MDelta += ev.Delta;
				MRefresh();

				// 面倒なので、MainPanel にはイベント無し！
			};

			// Windows7の場合、こっち(フォーム)でしかイベントを拾えない！！
			this.MouseWheel += (sdr, ev) =>
			{
				MDelta_Win7 += ev.Delta;
				MRefresh();
			};

			this.MainPanel.Controls.Add(pb);

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

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				// -- 3001
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

		private bool MDown = false;
		private Point MPoint = new Point(-1, -1);
		private int MDelta = 0;
		private int MDelta_Win7 = 0;

		private void MainPanel_MouseDown(object sender, MouseEventArgs e)
		{
			MDown = true;
			MRefresh();
		}

		private void MainPanel_MouseMove(object sender, MouseEventArgs e)
		{
			MPoint = e.Location;
			MRefresh();
		}

		private void MainPanel_MouseUp(object sender, MouseEventArgs e)
		{
			MDown = false;
			MRefresh();
		}

		private void MRefresh()
		{
			this.Text = MDown + ", " + MPoint + ", " + MDelta + ", " + MDelta_Win7;
		}
	}
}
