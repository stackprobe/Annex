using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Charlotte
{
	public partial class TreeSheetWin : Form
	{
		public TreeSheetWin()
		{
			InitializeComponent();
		}

		private void TreeTableWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void TreeSheetWin_Shown(object sender, EventArgs e)
		{
			// noop
		}

		private void TreeSheetWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void TreeSheetWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void 閉じるToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void MainSheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			// noop
		}

		//
		// このへんから MainSheet 用
		//

		private void MS_Init()
		{
			this.MainSheet.RowCount = 0;
			this.MainSheet.ColumnCount = 0;

			this.MS_AddColumn("", true);
			this.MS_AddColumn("パス");
			this.MS_AddColumn("ローカル名");
			this.MS_AddColumn("拡張子");
			this.MS_AddColumn("サイズ");
		}

		private void MS_AddColumn(string title, bool checkBox = false)
		{
			DataGridViewColumn column;

			if (checkBox)
				column = new DataGridViewCheckBoxColumn();
			else
				column = new DataGridViewColumn();

			column.SortMode = DataGridViewColumnSortMode.Programmatic;

			// TODO ?

			this.MainSheet.Columns.Add(column);
		}

		private void MS_Load(TreeView tv)
		{
			throw null; // TODO tv --> MainSheet
		}

		private void MS_Feedback(TreeView tv)
		{
			throw null; // TODO MainSheet --> tv
		}

		private void MS_SetRecord(DataGridViewRow row, MS_Record record)
		{
			int c = 0;

			row.Cells[c++].Value = record.Checked;
			row.Cells[c++].Value = record.FilePath;
			row.Cells[c++].Value = Path.GetFileName(record.FilePath);
			row.Cells[c++].Value = Path.GetExtension(record.FilePath);
			row.Cells[c++].Value = new FileInfo(record.FilePath).Length;
		}

		private MS_Record MS_GetRecord(DataGridViewRow row)
		{
			return new MS_Record()
			{
				Checked = (bool)row.Cells[0].Value,
				FilePath = (string)row.Cells[1].Value,
			};
		}

		private class MS_Record
		{
			public bool Checked = false;
			public string FilePath = @"C:\temp\Dummy.tmp"; // Dummy value
		}

		//
		// このへんまで MainSheet 用
		//

		private void 選択解除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MainSheet.ClearSelection();
		}
	}
}
