using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Security.Permissions;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		private bool XPressed = false;

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				this.XPressed = true;
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private MergedList _mrgList;

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

			this.MS_Init();

			_mrgList = new MergedList();

			foreach (MergedList.RowInfo row in _mrgList.Rows)
			{
				this.MS_AddRow(new MS_Row()
				{
					Selected = false,
					ImageL = row.ThumbL,
					ImageR = row.ThumbR,
				});
			}

			// ----

			this.MTBusy.Leave();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTBusy.Enter(); // 2bs

			// ----

			// -- 9999
		}

		private void CloseWindow()
		{
			using (this.MTBusy.Section())
			{
				try
				{
					// -- 9000

					// ----
				}
				catch (Exception e)
				{
					MessageBox.Show("" + e, "Error @ CloseWindow()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				this.MTBusy.Enter();
				this.Close();
			}
		}

		private VisitorCounter MTBusy = new VisitorCounter(1);
		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTBusy.HasVisitor())
				return;

			this.MTBusy.Enter();
			try
			{
				// -- 3001

				if (this.XPressed)
				{
					this.XPressed = false;
					this.CloseWindow();
					return;
				}
			}
			catch (Exception ex)
			{
				ProcMain.WriteLog(ex);
			}
			finally
			{
				this.MTBusy.Leave();
				this.MTCount++;
			}
		}

		private void MS_Init()
		{
			this.MainSheet.RowCount = 0;
			this.MainSheet.ColumnCount = 0;

			this.MS_AddColumn("", false);
			this.MS_AddColumn("旧画像", true);
			this.MS_AddColumn("新画像", true);
		}

		private void MS_AddColumn(string title, bool imgFlg)
		{
			DataGridViewColumn column;

			if (imgFlg)
				column = new DataGridViewImageColumn();
			else
				column = new DataGridViewTextBoxColumn();

			column.HeaderText = title;
			//column.Width = 101;
			column.Width = 110;
			column.SortMode = DataGridViewColumnSortMode.NotSortable;

			this.MainSheet.Columns.Add(column);
		}

		private const string MS_S_SELECTED = "SELECTED";
		private const string MS_S_NOT_SELECTED = "-";

		private class MS_Row
		{
			public bool Selected;
			public Bitmap ImageL;
			public Bitmap ImageR;
		}

		private void MS_AddRow(MS_Row row)
		{
			this.MainSheet.RowCount++;
			//this.MainSheet.Rows[this.MainSheet.RowCount - 1].Height = 101;
			this.MainSheet.Rows[this.MainSheet.RowCount - 1].Height = 110;
			this.MS_SetRow(this.MainSheet.RowCount - 1, row);
		}

		private void MS_SetRow(int rowidx, MS_Row row)
		{
			DataGridViewRow msRow = this.MainSheet.Rows[rowidx];

			msRow.Cells[0].Value = row.Selected ? MS_S_SELECTED : MS_S_NOT_SELECTED;
			msRow.Cells[1].Value = row.ImageL;
			msRow.Cells[2].Value = row.ImageR;
		}

		private MS_Row MS_GetRow(int rowidx)
		{
			DataGridViewRow msRow = this.MainSheet.Rows[rowidx];

			return new MS_Row()
			{
				Selected = (string)msRow.Cells[0].Value == MS_S_SELECTED,
				ImageL = (Bitmap)msRow.Cells[1].Value,
				ImageR = (Bitmap)msRow.Cells[2].Value,
			};
		}

		private void AllSelectedRow(Action<MS_Row> reaction)
		{
			for (int rowidx = 0; rowidx < this.MainSheet.RowCount; rowidx++)
			{
				if (this.MainSheet.Rows[rowidx].Selected)
				{
					MS_Row row = this.MS_GetRow(rowidx);

					reaction(row);

					this.MS_SetRow(rowidx, row);
				}
			}
		}

		private void MainSheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			// noop
		}

		private void selectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AllSelectedRow(row => row.Selected = true);
		}

		private void unselectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AllSelectedRow(row => row.Selected = false);
		}

		// TODO ???
		// TODO ???
		// TODO ???
	}
}
