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
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.checkBox1.Checked)
			{
				Common.WriteLog("Form1_FormClosing_BeforeConfirm");
				e.Cancel = MessageBox.Show("Form1_FormClosing", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel;
				Common.WriteLog("Form1_FormClosing_AfterConfirm e.Cancel: " + e.Cancel);
			}
			else
				Common.WriteLog("Form1_FormClosing");
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Common.WriteLog("Form1_FormClosed");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new Form2().ShowDialog();
			Common.WriteLog("Form1_button1_Click");
		}
	}
}
