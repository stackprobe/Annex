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
	public partial class KeyEditDlg : Form
	{
		public bool okPressed = false;

		private Key _key;

		public KeyEditDlg(Key key)
		{
			_key = key;

			InitializeComponent();

			this.MinimumSize = this.Size;
			this.MaximumSize = new Size(this.Size.Width * 2, this.Size.Height);

			// load
			{
				this.txtName.Text = _key.getName();
				this.txtIdent.Text = _key.getIdent();
				this.txtRawKey.Text = _key.getRawKey();
				this.txtHash.Text = _key.getHash();
			}
		}

		private void KeyEditDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void KeyEditDlg_Shown(object sender, EventArgs e)
		{
			// noop
		}

		private void KeyEditDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void KeyEditDlg_FormClosed(object sender, FormClosedEventArgs e)
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
					if (isFairName(this.txtName.Text) == false)
						throw new FaultOperation(
							"名前に問題があります。\n" +
							"** Shift_JIS に変換出来ない文字は使用出来ません。\n" +
							"** 前後に空白を入れることは出来ません。\n" +
							"** " + Consts.KEY_NAME_LEN_MIN + " 文字以上でなければなりません。\n" +
							"** " + Consts.KEY_NAME_LEN_MAX + " 文字以下でなければなりません。"
							);
				}
				// save
				{
					_key.setName(this.txtName.Text);
				}
				this.okPressed = true;
				this.Close();
			}
			catch (Exception ex)
			{
				refreshUI();
				FaultOperation.caught(ex);
			}
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			refreshUI();
		}

		private void refreshUI()
		{
			// 名前の色
			{
				Color fore_color = Color.Black;
				Color back_color = Color.White;

				if (isFairName(this.txtName.Text) == false)
				{
					fore_color = Color.Red;
					back_color = Color.FromArgb(255, 255, 200);
				}

				this.txtName.ForeColor = fore_color;
				this.txtName.BackColor = back_color;
			}

			this.txtHash.Text = Key.getHash(
				this.txtName.Text,
				this.txtIdent.Text,
				this.txtRawKey.Text
				);
		}

		private bool isFairName(string src)
		{
			return toFiarName(src) == src;
		}

		private string toFiarName(string src)
		{
			src = JString.toJString(src, true, false, false, true).Trim();

			if (IntTools.isRange(src.Length, Consts.KEY_NAME_LEN_MIN, Consts.KEY_NAME_LEN_MAX) == false)
				return "too short or too long"; // dummy

			return src;
		}

		private void txtRawKey_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // ctrl_a
			{
				txtRawKey.SelectAll();
				e.Handled = true;
			}
		}

		private void txtName_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				this.btnOk_Click(null, null);
				e.Handled = true;
			}
		}
	}
}
