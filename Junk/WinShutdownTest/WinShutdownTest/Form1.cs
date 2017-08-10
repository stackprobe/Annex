using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFrmApp1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new Form2().ShowDialog();

			Tools.WriteLog("Form1.button1_Click.end");
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			Tools.WriteLog("Form1.closing");

			e.Cancel = this.checkBox1.Checked;
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Tools.WriteLog("Form1.closed");
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (G.I.CloseForm1Flag)
			{
				G.I.CloseForm1Flag = false;
				this.Close();
			}
		}
	}
}
