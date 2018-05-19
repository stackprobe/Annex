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
	public partial class KeyBundleClosetWin : Form
	{
		public KeyBundleClosetWin()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
			this.lblStatus.Text = "";

			kbSheetInit();
		}

		private WinRect _winRect = Gnd.i.keyBundleClosetWinRect;

		private void KeyBundleClosetWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void KeyBundleClosetWin_Shown(object sender, EventArgs e)
		{
			_winRect.apply(this);
			refreshSheet();
		}

		private void KeyBundleClosetWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			_winRect.set(this);
		}

		private void KeyBundleClosetWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private long mtCounter;

		private void mainTimer_Tick(object sender, EventArgs e)
		{
			mtCounter++;
		}

		private void KeyBundleClosetWin_Move(object sender, EventArgs e)
		{
			KeyBundleClosetWin_ResizeEnd(null, null);
		}

		private void KeyBundleClosetWin_ResizeEnd(object sender, EventArgs e)
		{
			if (mtCounter < Consts.FORM_INITED_TIMER_COUNT)
				return;

			_winRect.set(this);
		}

		private void kbSheetInit()
		{
			this.kbSheet.RowCount = 0;
			this.kbSheet.ColumnCount = 0;
			this.kbSheet.ColumnCount = 3;

			this.kbSheet.RowHeadersVisible = false; // 行ヘッダ_非表示

			foreach (DataGridViewColumn column in this.kbSheet.Columns) // 列ソート_禁止
			{
				column.SortMode = DataGridViewColumnSortMode.NotSortable;
			}

			int colidx = 0;

			{
				DataGridViewColumn column = this.kbSheet.Columns[colidx++];

				column.HeaderText = "名前";
				column.Width = 200;
			}

			{
				DataGridViewColumn column = this.kbSheet.Columns[colidx++];

				column.HeaderText = "Tree";
				column.Width = 200;
			}

			{
				DataGridViewColumn column = this.kbSheet.Columns[colidx++];

				column.HeaderText = "Ident";
				column.Visible = false;
			}
		}

		private void kbSheetSetRow(int rowidx, KeyBundle kb)
		{
			DataGridViewRow row = this.kbSheet.Rows[rowidx];

			row.Cells[0].Value = kb.getName();
			row.Cells[1].Value = Utils.toEllipsis(kb.getRoot().ToString());
			row.Cells[2].Value = kb.getIdent();
		}

		private KeyBundle kbSheetGetRow(int rowidx)
		{
			DataGridViewRow row = this.kbSheet.Rows[rowidx];

			return KeyBundleCloset.load(row.Cells[2].Value.ToString());
		}

		private void refreshSheet()
		{
			List<KeyBundle> keyBundles = KeyBundleCloset.getAll();

			kbSheet.RowCount = keyBundles.Count;

			for (int index = 0; index < keyBundles.Count; index++)
				kbSheetSetRow(index, keyBundles[index]);

			Utils.adjustColumnsWidth(kbSheet);
		}

		private void kbSheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			// noop
		}

		private void kbSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

		private void desideToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}
	}
}
