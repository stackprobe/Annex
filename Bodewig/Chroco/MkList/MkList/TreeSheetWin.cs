﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Charlotte.Tools;
using System.Collections;

namespace Charlotte
{
	public partial class TreeSheetWin : Form
	{
		private TreeView TV;

		public TreeSheetWin(TreeView tv)
		{
			this.TV = tv;

			InitializeComponent();

			this.MinimumSize = this.Size;
			this.South.Text = Ground.OpenedRootDir;
			this.SouthEast.Text = "";

			ExtraTools.SetEnabledDoubleBuffer(this.MainSheet);
		}

		private void TreeSheetWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void TreeSheetWin_Shown(object sender, EventArgs e)
		{
			this.MS_Init();
			this.MS_Load(this.TV);
		}

		private void TreeSheetWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void TreeSheetWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MS_Save(this.TV);
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

			this.MS_AddColumn("", 50, true);
			this.MS_AddColumn("パス", 300);
			this.MS_AddColumn("ローカル名", 200);
			this.MS_AddColumn("拡張子", 100);
			this.MS_AddColumn("サイズ", 100, false, true);
		}

		private void MS_AddColumn(string title, int width, bool checkBox = false, bool rightAlign = false)
		{
			DataGridViewColumn column;

			if (checkBox)
				column = new DataGridViewCheckBoxColumn();
			else
				column = new DataGridViewTextBoxColumn();

			column.HeaderText = title;
			column.Width = width;
			column.SortMode = DataGridViewColumnSortMode.Programmatic;

			if (rightAlign)
				column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

			this.MainSheet.Columns.Add(column);
		}

		private void MS_Load(TreeView tv)
		{
			using (new Utils.UISuspend(this.MainSheet))
			{
				IEnumerable<MS_Record> records = this.TVToRecords(Utils.GetNodes(tv.Nodes));

				this.MainSheet.RowCount = 0;
				this.MainSheet.RowCount = records.Count();

				int rowidx = 0;
				foreach (MS_Record record in records)
				{
					this.MS_SetRecord(this.MainSheet.Rows[rowidx], record);
					rowidx++;
				}

				this.SouthEast.Text = "" + this.MainSheet.RowCount;
			}
			this.MainSheet.ClearSelection();
		}

		private void MS_Save(TreeView tv)
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
			row.Cells[c++].Value = Utils.TryGetFileSize(Path.Combine(Ground.RootDir, record.FilePath), 0L);
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

		private void MainSheet_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			int colidx = e.ColumnIndex;

			if (colidx < 0 || this.MainSheet.ColumnCount <= colidx)
				return;

			SortOrder order = this.MainSheet.Columns[colidx].HeaderCell.SortGlyphDirection;

			if (order == SortOrder.Ascending)
				order = SortOrder.Descending;
			else
				order = SortOrder.Ascending;

			this.MS_Sort(colidx, order);

			for (int ci = 0; ci < this.MainSheet.ColumnCount; ci++)
				this.MainSheet.Columns[ci].HeaderCell.SortGlyphDirection = ci == colidx ? order : SortOrder.None;
		}

		private void MS_Sort(int colidx, SortOrder order)
		{
			this.MS_Sort((a, b) => this.MS_CellComp(
				"" + a.Cells[colidx].Value,
				"" + b.Cells[colidx].Value,
				this.MainSheet.Columns[colidx]
				) * (order == SortOrder.Ascending ? 1 : -1));
		}

		private int MS_CellComp(string a, string b, DataGridViewColumn column)
		{
			if (column.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
				return LongTools.Comp(long.Parse(a), long.Parse(b));

			return StringTools.CompIgnoreCase(a, b);
		}

		private void MS_Sort(Comparison<DataGridViewRow> comp)
		{
			this.MainSheet.Sort(new MS_Comp()
			{
				Comp = comp,
			});

			//this.MainSheet.ClearSelection();
		}

		private class MS_Comp : IComparer
		{
			public Comparison<DataGridViewRow> Comp;

			public int Compare(object a, object b)
			{
				return Comp((DataGridViewRow)a, (DataGridViewRow)b);
			}
		}

		private void MainSheet_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1 && e.ColumnIndex == 0)
			{
				bool value = (bool)this.MainSheet.Rows[e.RowIndex].Cells[0].Value;
				value = value == false;
				this.MainSheet.Rows[e.RowIndex].Cells[0].Value = value;
			}
		}

		//
		// このへんまで MainSheet 用
		//

		private IEnumerable<MS_Record> TVToRecords(IEnumerable<TreeNode> nodes, string prefix = "")
		{
			foreach (TreeNode node in nodes)
			{
				if (((NodeTag)node.Tag).DirFlag)
				{
					foreach (MS_Record record in this.TVToRecords(Utils.GetNodes(node.Nodes), prefix + node.Text + "\\"))
						yield return record;
				}
				else
				{
					yield return new MS_Record()
					{
						Checked = node.Checked,
						FilePath = prefix + node.Text,
					};
				}
			}
		}

		private void 選択解除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MainSheet.ClearSelection();
		}
	}
}
