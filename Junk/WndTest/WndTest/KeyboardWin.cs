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
	public partial class KeyboardWin : Form
	{
		public KeyboardWin()
		{
			InitializeComponent();
		}

		private void KeyboardWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void KeyboardWin_Shown(object sender, EventArgs e)
		{
			MT_Enabled = true;
		}

		private void KeyboardWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void KeyboardWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			MT_Enabled = false;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (MT_Enabled == false || MT_Busy)
				return;

			MT_Busy = true;

			try
			{
				UpdateKeyState();
			}
			finally
			{
				MT_Busy = false;
				MT_Count++;
			}
		}

		private void UpdateKeyState()
		{
			short[][] state = KeyboardTools.GetState();
			List<string> lines = new List<string>();

			for (int r = 0; r < 32; r++)
			{
				List<string> parts = new List<string>();

				for (int c = 0; c < 8; c++)
				{
					int index = r + c * 32;

					parts.Add(
						StringTools.ZPad("" + index, 3) + ": " +
						StringTools.ToHex((ushort)state[0][index], 4) + ", " +
						StringTools.ToHex((ushort)state[1][index], 4)
						);
				}
				lines.Add(string.Join(" ", parts));
			}
			string text = string.Join("\r\n", lines);

			if (this.MainText.Text != text)
				this.MainText.Text = text;
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
	}
}
