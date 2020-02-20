using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
			// TODO
		}

		private void MS_Load()
		{
			// TODO
		}

		// TODO

		//
		// このへんまで MainSheet 用
		//

		private void 選択解除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MainSheet.ClearSelection();
		}
	}
}
