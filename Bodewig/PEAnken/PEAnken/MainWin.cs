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
using System.IO;

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

		private DetailDlg DetailDlg = null;

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

			this.MS_Init();
			//this.South.Text = "";

			this.DetailDlg = new DetailDlg();
			this.DetailDlg.Show();

			ProcMain.WriteLog = message => this.DetailDlg.SetMainText("" + message);

			if (File.Exists(Consts.ResourceTextFile_絞り込み))
			{
				foreach (string line in File.ReadAllLines(Consts.ResourceTextFile_絞り込み, StringTools.ENCODING_SJIS))
				{
					Add絞り込みItem(line);
				}
			}
			else
			{
				// サンプル
				Add絞り込みItem("Java C#");
				Add絞り込みItem("Java Web");
				Add絞り込みItem("Java Web Spring");
			}

			// ----

			this.MTEnabled = true;
		}

		private void Add絞り込みItem(string word)
		{
			ToolStripMenuItem item = new ToolStripMenuItem();

			item.Text = word;
			item.Click += new EventHandler((sender, e) => this.FreeWord.Text = word);

			this.絞り込みMenu.DropDownItems.Add(item);
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

			ProcMain.WriteLog = message => { };

			this.DetailDlg.Close();
			this.DetailDlg = null;

			// ----

			this.Close();
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private Queue<Action> MTLiteEvents = new Queue<Action>();

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				NarrowDownEachTimer();

				for (int count = MTLiteEvents.Count; 0 < count; count -= 10) // 少なくとも1/10を処理する。
					MTLiteEvents.Dequeue()();

				this.South.Text = NarrowDownHitCount + " (" + this.MainSheet.SelectedRows.Count + ") / " + this.MainSheet.RowCount +
					", W=" + this.NarrowDownWords.Length +
					", R=" + this.NarrowDownRowIndex +
					", Q=" + this.MTLiteEvents.Count;
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

		private string[] NarrowDownWords = new string[0];
		private int NarrowDownRowIndex = 0;
		private int NarrowDownHitCount = 0;

		private void NarrowDown_Reset()
		{
			this.NarrowDownWords = StringTools.Tokenize(this.FreeWord.Text, " ", false, true);
			this.NarrowDownRowIndex = 0;
			this.NarrowDownHitCount = 0;
		}

		private void NarrowDownEachTimer()
		{
			for (int c = 0; c < Consts.NARROW_DOWN_ROW_COUNT_EACH_TIMER; c++)
			{
				if (this.MainSheet.RowCount <= NarrowDownRowIndex)
					break;

				bool match = NarrowDown_IsRowMatch();

				this.MainSheet.Rows[NarrowDownRowIndex].Visible = match;

				if (match)
					NarrowDownHitCount++;

				NarrowDownRowIndex++;
			}
		}

		private bool NarrowDown_IsRowMatch()
		{
			string[] words = NarrowDownWords;

			if (words.Length == 0)
				return true;

			foreach (string word in words)
				if (NarrowDown_IsRowMatchWord(word) == false)
					return false;

			return true;
		}

		private bool NarrowDown_IsRowMatchWord(string word)
		{
			int rowidx = NarrowDownRowIndex;

			for (int colidx = 0; colidx < this.MainSheet.ColumnCount; colidx++)
			{
				string cell = "" + this.MainSheet.Rows[rowidx].Cells[colidx].Value;

				if (StringTools.ContainsIgnoreCase(cell, word))
					return true;
			}
			return false;
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

						//if (this.FreeWord.Text == "")
						//this.FreeWord.Text = "800,000 C#";
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

			//this.South.Text = "" + this.MainSheet.RowCount;

			this.NarrowDown_Reset();
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
			MS_Sort((a, b) => MS_CellComp(
				"" + a.Cells[colidx].Value,
				"" + b.Cells[colidx].Value
				) * (order == SortOrder.Ascending ? 1 : -1));
		}

		private int MS_CellComp(string a, string b)
		{
			int ret = VariantTools.Comp(a, b, value =>
			{
				if (StringTools.LiteValidate(value, StringTools.DECIMAL + ",円"))
				{
					value = new string(value.Where(chr => StringTools.DECIMAL.Contains(chr)).ToArray());

					if (1 <= value.Length && value.Length <= 9) // ? 0 ～ 999,999,999
					{
						return int.Parse(value);
					}
				}
				return IntTools.IMAX;
			});

			if (ret == 0)
				ret = StringTools.CompIgnoreCase(a, b);

			return ret;
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

		private int MS_GetSelectedRowIndex()
		{
			for (int rowidx = 0; rowidx < this.MainSheet.RowCount; rowidx++)
				if (this.MainSheet.Rows[rowidx].Selected)
					return rowidx;

			return -1;
		}

		private void FreeWord_TextChanged(object sender, EventArgs e)
		{
			this.NarrowDown_Reset();
		}

		private void MainSheet_SelectionChanged(object sender, EventArgs e)
		{
			MTLiteEvents.Enqueue(MS_SelectionChangedMain);
		}

		private void MS_SelectionChangedMain()
		{
			int rowidx = MS_GetSelectedRowIndex();
			string text;

			if (rowidx != -1)
				text = this.GetRowText(rowidx);
			else
				text = "";

			if (this.DetailDlg.GetMainText() != text)
				this.DetailDlg.SetMainText(text);
		}

		private string GetRowText(int rowidx)
		{
			List<string> lines = new List<string>();

			for (int colidx = 0; colidx < this.MainSheet.ColumnCount; colidx++)
			{
				string cell = "" + this.MainSheet.Rows[rowidx].Cells[colidx].Value;

				if (cell != "")
				{
					lines.Add("【" + this.MainSheet.Columns[colidx].HeaderText + "】");
					lines.Add("　" + cell);
				}
			}
			return string.Join("\r\n", lines);
		}

		private void 選択解除SToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MainSheet.ClearSelection();
		}
	}
}
