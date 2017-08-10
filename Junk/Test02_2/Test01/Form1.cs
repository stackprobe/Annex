using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Test01
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
	}

	public class DBDataGridView : DataGridView
	{
		public DBDataGridView() : base()
		{
			this.DoubleBuffered = true;
		}
	}
}
