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

			typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this.dataGridView1, true, null);

			for (int c = 0; c < 120; c++)
			{
				DataGridViewColumn column = new DataGridViewColumn();

				column.FillWeight = 10;
				column.Width = 16;
				column.CellTemplate = new DataGridViewTextBoxCell();

				this.dataGridView1.Columns.Add(column);
			}
			for (int r = 0; r < 30; r++)
			{
				DataGridViewRow row = new DataGridViewRow();

				row.Height = 16;

				this.dataGridView1.Rows.Add(row);
			}
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
