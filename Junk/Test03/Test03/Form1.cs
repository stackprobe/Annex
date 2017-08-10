using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test03
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();

			for (int c = 0; c < 80; c++)
			{
				this.GMap.Columns.Add("", "");
				this.GMap.Columns[this.GMap.ColumnCount - 1].Width = 16;
				this.GMap.Columns[this.GMap.ColumnCount - 1].SortMode = DataGridViewColumnSortMode.NotSortable;
			}
			for (int r = 0; r < 30; r++)
			{
				this.GMap.Rows.Add();
				this.GMap.Rows[this.GMap.RowCount - 1].Height = 16;
			}

			this.GMap.AllowUserToResizeRows = false;
			this.GMap.AllowUserToResizeColumns = false;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// todo
		}
	}
}
