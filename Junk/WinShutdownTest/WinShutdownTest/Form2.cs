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
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			// noop
		}

		private bool OpenForm3Flag;

		private void button1_Click(object sender, EventArgs e)
		{
			this.OpenForm3Flag = true;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.OpenForm3Flag)
			{
				this.OpenForm3Flag = false;
				new Form3().ShowDialog();
				Tools.WriteLog("Form2.timer1_Tick.end");
			}
		}

		private void Form2_FormClosing(object sender, FormClosingEventArgs e)
		{
			Tools.WriteLog("Form2.closing");

			e.Cancel = this.checkBox1.Checked;
		}

		private void Form2_FormClosed(object sender, FormClosedEventArgs e)
		{
			Tools.WriteLog("Form2.closed");
		}
	}
}
