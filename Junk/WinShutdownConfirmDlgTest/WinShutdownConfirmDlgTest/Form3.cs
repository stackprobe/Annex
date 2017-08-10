using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinShutdownConfirmDlgTest
{
	public partial class Form3 : Form
	{
		public Form3()
		{
			InitializeComponent();
		}

		private void Form3_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.checkBox1.Checked)
			{
				Common.WriteLog("Form3_FormClosing_BeforeConfirm");
				e.Cancel = MessageBox.Show("Form3_FormClosing", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel;
				Common.WriteLog("Form3_FormClosing_AfterConfirm e.Cancel: " + e.Cancel);
			}
			else
				Common.WriteLog("Form3_FormClosing");
		}

		private void Form3_FormClosed(object sender, FormClosedEventArgs e)
		{
			Common.WriteLog("Form3_FormClosed");
		}
	}
}
