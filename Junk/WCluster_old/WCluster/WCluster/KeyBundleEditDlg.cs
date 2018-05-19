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
	public partial class KeyBundleEditDlg : Form
	{
		public bool okPressed = false;

		private KeyBundle _kb;

		public KeyBundleEditDlg(KeyBundle kb)
		{
			_kb = kb;

			InitializeComponent();

			this.MinimumSize = this.Size;

			// load
			{
				this.txtName.Text = kb.getName();
				kbTreeLoad(kb.getRoot());
			}
		}

		private void KeyBundleEditDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void KeyBundleEditDlg_Shown(object sender, EventArgs e)
		{
			// noop
		}

		private void KeyBundleEditDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void KeyBundleEditDlg_FormClosed(object sender, FormClosedEventArgs e)
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
							"** " + Consts.KEY_BUNDLE_NAME_LEN_MIN + " 文字以上でなければなりません。\n" +
							"** " + Consts.KEY_BUNDLE_NAME_LEN_MAX + " 文字以下でなければなりません。"
							);

					kbTreeCheck();
				}
				// save
				{
					_kb.setName(this.txtName.Text);
					_kb.setRoot(kbTreeSave());
				}
				this.okPressed = true;
				this.Close();
			}
			catch (Exception ex)
			{
				FaultOperation.caught(ex);
			}
		}

		private bool isFairName(string src)
		{
			return toFiarName(src) == src;
		}

		private string toFiarName(string src)
		{
			src = JString.toJString(src, true, false, false, true).Trim();

			if (IntTools.isRange(src.Length, Consts.KEY_BUNDLE_NAME_LEN_MIN, Consts.KEY_BUNDLE_NAME_LEN_MAX) == false)
				return "too short or too long"; // dummy

			return src;
		}

		private void txtName_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				btnOk_Click(null, null);
				e.Handled = true;
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
		}

		private void kbTreeLoad(KeyBundle.Node root)
		{
			// TODO

			// test
			{
			}
		}

		private void kbTreeCheck()
		{
			// TODO
		}

		private KeyBundle.Node kbTreeSave()
		{
			// TODO

			return new KeyBundle.KBKey()
			{
				ident = Consts.DUMMY_IDENT,
			};
		}
	}
}
