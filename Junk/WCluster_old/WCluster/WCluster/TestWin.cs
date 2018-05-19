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
	public partial class TestWin : Form
	{
		public TestWin()
		{
			InitializeComponent();
		}

		private void DebugWin_Load(object sender, EventArgs e)
		{
			//
		}

		private void doShowDialog(Form f)
		{
			this.Visible = false;

			f.ShowDialog();

			f.Dispose();
			f = null;

			this.Visible = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			doShowDialog(new CompressMainDlg());
		}

		private void button2_Click(object sender, EventArgs e)
		{
			doShowDialog(new DecompressMainDlg());
		}

		private void button3_Click(object sender, EventArgs e)
		{
			doShowDialog(new SettingWin());
		}

		private void button4_Click(object sender, EventArgs e)
		{
			doShowDialog(new EncryptionSelectDlg());
		}

		private void button5_Click(object sender, EventArgs e)
		{
			doShowDialog(new InputPassphraseDlg());
		}

		private void button6_Click(object sender, EventArgs e)
		{
			doShowDialog(new KeyClosetWin());
		}

		private void button7_Click(object sender, EventArgs e)
		{
			doShowDialog(new KeyBundleClosetWin());
		}

		private void button8_Click(object sender, EventArgs e)
		{
			doShowDialog(new KeyEditDlg(Key.getDummyKey()));
		}

		private void button9_Click(object sender, EventArgs e)
		{
			doShowDialog(new KeyBundleEditDlg(KeyBundle.create()));
		}
	}
}
