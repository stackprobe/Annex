using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test01
{
	public partial class Form2 : Form
	{
		public static bool Death;

		public Form2()
		{
			Death = false;
			InitializeComponent();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (Death)
			{
				this.Close();
			}
		}
	}
}
