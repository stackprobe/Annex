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
	public partial class InputPassphraseDlg : Form
	{
		public bool okPressed = false;
		public string retPassphrase = "";

		public InputPassphraseDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
			this.MaximumSize = new Size(this.Size.Width * 2, this.Size.Height);

			this.lblStatus.Text = "";
		}

		private void InputPassphraseDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputPassphraseDlg_Shown(object sender, EventArgs e)
		{
			// noop
		}

		private void InputPassphraseDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputPassphraseDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				// check
				{
					if (isFairPassphrase(this.txtPassphrase.Text) == false)
						throw new FaultOperation(
							"パスフレーズに問題があります。\n" +
							"** Shift_JIS に変換出来ない文字は使用出来ません。\n" +
							"** 前後に空白を入れることは出来ません。\n" +
							"** " + Consts.PASSPHRASE_LEN_MIN + " 文字以上でなければなりません。\n" +
							"** " + Consts.PASSPHRASE_LEN_MAX + " 文字以下でなければなりません。"
							);
				}
				// ret
				{
					this.okPressed = true;
					this.retPassphrase = this.txtPassphrase.Text;
				}
				this.Close();
			}
			catch (Exception ex)
			{
				refreshUI();
				FaultOperation.caught(ex);
			}
		}

		private void txtPassphrase_TextChanged(object sender, EventArgs e)
		{
			refreshUI();
		}

		private void refreshUI()
		{
			// パスフレーズの色
			{
				Color fore_color = Color.Black;
				Color back_color = Color.White;

				{
					string pp = this.txtPassphrase.Text;

					if (isFairPassphrase(pp) == false)
					{
						fore_color = Color.Red;
						back_color = Color.FromArgb(255, 255, 200);
					}

					{
						int xExp = get拡張Exp(pp);

						if (xExp != -1)
						{
							fore_color = Color.FromArgb(0, xExp * 4, 200);
							//fore_color = Color.FromArgb(0, 100 + xExp * 2, 200); // old
						}
					}
				}

				this.txtPassphrase.ForeColor = fore_color;
				this.txtPassphrase.BackColor = back_color;
			}
		}

		private Color getPassphraseForeColor(string src)
		{
			if (isFairPassphrase(src) == false)
				return Color.Red;

			{
				int xExp = get拡張Exp(src);

				if (xExp != -1)
					return Color.FromArgb(0, 100 + xExp * 2, 200);
			}

			return Color.Black;
		}

		private bool isFairPassphrase(string src)
		{
			return toFairPassphrase(src) == src;
		}

		private string toFairPassphrase(string src)
		{
			src = JString.toJString(src, true, false, false, true).Trim();

			if (IntTools.isRange(src.Length, Consts.PASSPHRASE_LEN_MIN, Consts.PASSPHRASE_LEN_MAX) == false)
				return "too short or too long"; // dummy

			return src;
		}

		private int get拡張Exp(string src)
		{
			if (
				5 <= src.Length &&
				src[src.Length - 1] == ']' &&
				StringTools.DIGIT.Contains(src[src.Length - 2]) &&
				StringTools.DIGIT.Contains(src[src.Length - 3]) &&
				(StringTools.ASCII + StringTools.HAN_KANA).Contains(src[src.Length - 4]) &&
				src[src.Length - 5] == '['
				)
			{
				int xExp = int.Parse(src.Substring(src.Length - 3, 2));

				if (IntTools.isRange(xExp, 20, 50))
					return xExp;
			}
			return -1; // no_拡張
		}

		private void txtPassphrase_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				this.btnOk_Click(null, null);
				e.Handled = true;
			}
		}
	}
}
