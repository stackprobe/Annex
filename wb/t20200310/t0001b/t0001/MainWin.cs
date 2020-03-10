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

			this.MainSheet.DataSource = _mrgList.GetDataSource();

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

			this.MS_AddColumn("MSC-Selected", "", false);
			this.MS_AddColumn("MSC-ImageL", "旧画像", true);
			this.MS_AddColumn("MSC-ImageR", "新画像", true);

			this.MainSheet.RowTemplate.Height = 110;
		}

		private void MS_AddColumn(string name, string title, bool imgFlg)
		{
			DataGridViewColumn column;

			if (imgFlg)
				column = new DataGridViewImageColumn();
			else
				column = new DataGridViewTextBoxColumn();

			column.DataPropertyName = name;
			column.HeaderText = title;
			column.Width = 110;
			column.SortMode = DataGridViewColumnSortMode.NotSortable;

			this.MainSheet.Columns.Add(column);
		}

		public const string MS_S_SELECTED = "SELECTED";
		public const string MS_S_NOT_SELECTED = "-";

		private void MainSheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			// noop
		}

		private void selectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO ???
		}

		private void unselectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO ???
		}

		// TODO ???
		// TODO ???
		// TODO ???
	}
}
