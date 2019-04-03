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
	public partial class InputTextDlg : Form
	{
		public InputTextDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void InputText_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void 入力テキスト_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void 入力テキスト_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 10) // ctrl_enter
			{
				this.読み込みボタン_Click(null, null);
				e.Handled = true;
			}
		}

		public string RetText = null;

		private void 読み込みボタン_Click(object sender, EventArgs e)
		{
			this.RetText = this.入力テキスト.Text;
			this.Close();
		}
	}
}
