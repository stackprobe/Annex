using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	public partial class SettingWin : Form
	{
		public SettingWin()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void SettingWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void SettingWin_Shown(object sender, EventArgs e)
		{
			Gnd.i.settingWinRect.apply(this);
		}

		private void SettingWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			Gnd.i.settingWinRect.set(this);
		}

		private void SettingWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			// TODO save
			this.Close();
		}

		private long mtCounter;

		private void mainTimer_Tick(object sender, EventArgs e)
		{
			mtCounter++;
		}

		private void SettingWin_Move(object sender, EventArgs e)
		{
			SettingWin_ResizeEnd(null, null);
		}

		private void SettingWin_ResizeEnd(object sender, EventArgs e)
		{
			if (this.mtCounter < Consts.FORM_INITED_TIMER_COUNT)
				return;

			Gnd.i.settingWinRect.set(this);
		}
	}
}
