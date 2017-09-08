using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WndTest
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
			// noop
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void enumWindowsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<WindowTools.Info> infos = WindowTools.FindWindows();
			List<string> lines = new List<string>();

			foreach (WindowTools.Info info in infos)
			{
				StringBuilder buff = new StringBuilder();

				buff.Append(info.Title);
				buff.Append(", ");
				buff.Append(info.ClassName);
				buff.Append(", ");
				buff.Append(info.Text);
				buff.Append(", ");

				if (info.Rect != null)
				{
					buff.Append("[");
					buff.Append(info.Rect.L);
					buff.Append(", ");
					buff.Append(info.Rect.T);
					buff.Append(", ");
					buff.Append(info.Rect.W);
					buff.Append(", ");
					buff.Append(info.Rect.H);
					buff.Append("]");
				}
				buff.Append(", ");
				buff.Append(info.HWnd);
				buff.Append(", ");

				if (info.Parent != null)
					buff.Append(info.Parent.HWnd);
				else
					buff.Append("TOP");

				lines.Add(buff.ToString());
			}
			this.MainText.Text = string.Join("\r\n", lines);
		}

		private void MainText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void MainText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // ctrl a
			{
				this.MainText.SelectAll();
				e.Handled = true;
			}
		}

		private void keyboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (Form f = new KeyboardWin())
			{
				f.ShowDialog();
			}
		}
	}
}
