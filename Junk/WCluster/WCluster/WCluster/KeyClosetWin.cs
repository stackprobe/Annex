using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class KeyClosetWin : Form
	{
		public KeyClosetWin()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
			this.lblStatus.Text = "";

			keySheetInit();
		}

		private WinRect _winRect = Gnd.i.keyClosetWinRect;

		private void KeyClosetWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void KeyClosetWin_Shown(object sender, EventArgs e)
		{
			_winRect.apply(this);
			refreshSheet();
		}

		private void KeyClosetWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			_winRect.set(this);
		}

		private void KeyClosetWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		// 要 this.AllowDrop = true;

		private void keySheet_DragEnter(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
					e.Effect = DragDropEffects.Copy;
			}
			catch
			{ }
		}

		private void keySheet_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

				if (files.Length < 1)
					return;

				string file = files[0];
				file = FileTools.toFullPath(file); // 2bs

				// TODO

				MessageBox.Show("*Drop: " + file);
			}
			catch
			{ }
		}

		private long mtCounter;

		private void mainTimer_Tick(object sender, EventArgs e)
		{
			mtCounter++;
		}

		private void KeyClosetWin_Move(object sender, EventArgs e)
		{
			KeyClosetWin_ResizeEnd(null, null);
		}

		private void KeyClosetWin_ResizeEnd(object sender, EventArgs e)
		{
			if (mtCounter < Consts.FORM_INITED_TIMER_COUNT)
				return;

			_winRect.set(this);
		}

		private void keySheetInit()
		{
			this.keySheet.RowCount = 0;
			this.keySheet.ColumnCount = 0;
			this.keySheet.ColumnCount = 4;

			this.keySheet.RowHeadersVisible = false; // 行ヘッダ_非表示

			foreach (DataGridViewColumn column in this.keySheet.Columns) // 列ソート_禁止
			{
				column.SortMode = DataGridViewColumnSortMode.NotSortable;
			}

			int colidx = 0;

			{
				DataGridViewColumn column = this.keySheet.Columns[colidx++];

				column.HeaderText = "名前";
				column.Width = 100;
			}

			{
				DataGridViewColumn column = this.keySheet.Columns[colidx++];

				column.HeaderText = "Ident";
				column.Width = 100;
			}

			{
				DataGridViewColumn column = this.keySheet.Columns[colidx++];

				column.HeaderText = "鍵";
				column.Width = 100;
			}

			{
				DataGridViewColumn column = this.keySheet.Columns[colidx++];

				column.HeaderText = "Hash";
				column.Width = 100;
			}
		}

		private void keySheetSetRow(int rowidx, Key key)
		{
			DataGridViewRow row = this.keySheet.Rows[rowidx];

			row.Cells[0].Value = key.getName();
			row.Cells[1].Value = key.getIdent();
			row.Cells[2].Value = key.getRawKey();
			row.Cells[3].Value = key.getHash();
		}

		private Key keySheetGetRow(int rowidx)
		{
			DataGridViewRow row = this.keySheet.Rows[rowidx];

			return new Key(
				row.Cells[0].Value.ToString(),
				row.Cells[1].Value.ToString(),
				row.Cells[2].Value.ToString(),
				row.Cells[3].Value.ToString()
				);
		}

		private void refreshSheet()
		{
			// TODO

			// test
			{
				keySheet.RowCount = 20;

				for (int c = 0; c < 20; c++)
				{
					keySheetSetRow(c, Key.create());
				}
				Utils.adjustColumnsWidth(keySheet);
			}
		}

		private void keySheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			// noop
		}

		private void keySheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			// TODO

			MessageBox.Show("*DblClick: " + e.RowIndex); // test
		}

		private void 追加AToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void 編集EToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void 削除DToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void インポートIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void エクスポートEToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void desideToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}
	}
}
