using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Utils;
using System.Collections;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

			this.MS_Init();
			this.South.Text = "";

			// ----

			this.MTEnabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;

			// ----

			// -- 9999
		}

		private void BeforeDialog()
		{
			this.MTEnabled = false;
		}

		private void AfterDialog()
		{
			this.MTEnabled = true;
		}

		private void CloseWindow()
		{
			this.MTEnabled = false;

			// ----

			// -- 9000

			// ----

			this.Close();
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				// -- 3001
			}
			catch (Exception ex)
			{
				ProcMain.WriteLog(ex);
			}
			finally
			{
				this.MTBusy = false;
				this.MTCount++;
			}
		}

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CloseWindow();
		}

		private void URL読み込みToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (InputTextDlg f = new InputTextDlg())
			{
				f.ShowDialog();

				if (f.RetText != null)
				{
					try
					{
						AnkenTable at = new AnkenTable();

						at.AddText(f.RetText);

						MS_Load(at.GetTable());
					}
					catch (Exception ex)
					{
						MessageBox.Show(
							"" + ex,
							"失敗",
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning
							);

						MS_Clear();
					}
				}
			}
		}

		private void MS_Init()
		{
			ExtraTools.SetEnabledDoubleBuffer(this.MainSheet);

			MS_Clear();
		}

		private void MS_Clear()
		{
			this.MainSheet.RowCount = 0;
			this.MainSheet.ColumnCount = 0;
		}

		private void MS_Load(HeaderTable table)
		{
			this.MainSheet.Visible = false;

			this.MainSheet.RowCount = 0;
			this.MainSheet.ColumnCount = 0;

			this.MainSheet.ColumnCount = table.Header.Length;

			for (int colidx = 0; colidx < table.Header.Length; colidx++)
			{
				this.MainSheet.Columns[colidx].HeaderText = table.Header[colidx];
				this.MainSheet.Columns[colidx].SortMode = DataGridViewColumnSortMode.Programmatic;
			}

			this.MainSheet.RowCount = table.Rows.Length;

			for (int rowidx = 0; rowidx < table.Rows.Length; rowidx++)
			{
				DataGridViewRow msRow = this.MainSheet.Rows[rowidx];

				for (int colidx = 0; colidx < table.Header.Length; colidx++)
				{
					msRow.Cells[colidx].Value = table.Rows[rowidx][colidx];
				}
			}

			this.MainSheet.Visible = true;

			this.MS_AutoResize();
			this.MainSheet.ClearSelection();

			this.South.Text = "" + this.MainSheet.RowCount;
		}

		private void MS_AutoResize()
		{
			for (int colidx = 0; colidx < this.MainSheet.ColumnCount; colidx++)
				this.MainSheet.Columns[colidx].Width = 1000;

			this.MainSheet.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

			for (int colidx = 0; colidx < this.MainSheet.ColumnCount; colidx++)
			{
				int w = this.MainSheet.Columns[colidx].Width;
				w = Math.Min(w + 30, 300);
				this.MainSheet.Columns[colidx].Width = w;
			}
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

			MS_Sort(colidx, order);

			for (int ci = 0; ci < this.MainSheet.ColumnCount; ci++)
				this.MainSheet.Columns[ci].HeaderCell.SortGlyphDirection = ci == colidx ? order : SortOrder.None;
		}

		private void MS_Sort(int colidx, SortOrder order)
		{
			MS_Sort((a, b) =>
				StringTools.CompIgnoreCase(
					"" + a.Cells[colidx].Value,
					"" + b.Cells[colidx].Value
					) * (order == SortOrder.Ascending ? 1 : -1)
				);
		}

		private void MS_Sort(Comparison<DataGridViewRow> comp)
		{
			this.MainSheet.Sort(new MS_Comp()
			{
				Comp = comp,
			});

			this.MainSheet.ClearSelection();
		}

		private class MS_Comp : IComparer
		{
			public Comparison<DataGridViewRow> Comp;

			public int Compare(object a, object b)
			{
				return Comp((DataGridViewRow)a, (DataGridViewRow)b);
			}
		}
	}
}
