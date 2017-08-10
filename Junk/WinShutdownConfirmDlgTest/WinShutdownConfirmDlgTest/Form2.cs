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
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		private void Form2_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.checkBox1.Checked)
			{
				Common.WriteLog("Form2_FormClosing_BeforeConfirm");
				e.Cancel = MessageBox.Show("Form2_FormClosing", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel;
				Common.WriteLog("Form2_FormClosing_AfterConfirm e.Cancel: " + e.Cancel);
			}
			else
				Common.WriteLog("Form2_FormClosing");
		}

		private void Form2_FormClosed(object sender, FormClosedEventArgs e)
		{
			Common.WriteLog("Form2_FormClosed");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new Form3().ShowDialog();
			Common.WriteLog("Form2_button1_Click");
		}
	}
}
