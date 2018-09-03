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
				throw null;
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
			// TODO
		}

		private void MapPanel_MouseUp(object sender, MouseEventArgs e)
		{
			// TODO
		}

		private void MapPanel_MouseMove(object sender, MouseEventArgs e)
		{
			// TODO
		}

		private void MapPanelMouseWheel(object sender, MouseEventArgs e)
		{
			// TODO
		}
	}
}
