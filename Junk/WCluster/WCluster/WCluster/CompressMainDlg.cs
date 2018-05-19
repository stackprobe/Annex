using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class CompressMainDlg : Form
	{
		public CompressMainDlg()
		{
			InitializeComponent();
		}

		private WinRect _winRect = Gnd.i.compressMainDlgRect;
		private int _dropImageSize = (int)Gnd.i.compressDropImageSize;

		private void CompressMainDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void CompressMainDlg_Shown(object sender, EventArgs e)
		{
			_winRect.apply(this);

			this.pbDrop.Image = Bitmap.FromFile(dropImageFile);

			{
				int dw = this.pbDrop.Width - _dropImageSize;
				int dh = this.pbDrop.Height - _dropImageSize;

				this.Width -= dw;
				this.Height -= dh;
			}

			this.mtEnabled = true;
		}

		private void CompressMainDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			_winRect.set(this);
		}

		private void CompressMainDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.mtEnabled = false;
		}

		private string dropImageFile
		{
			get
			{
				string file = "compress" + _dropImageSize + ".dat";

				if (File.Exists(file) == false)
					file = @"..\..\..\..\doc\compress" + _dropImageSize + ".png";

				file = FileTools.makeFullPath(file);
				return file;
			}
		}

		private void pbDrop_Click(object sender, EventArgs e)
		{
			// TODO

			MessageBox.Show("*Click"); // test
		}

		// 要 this.AllowDrop = true;

		private void CompressMainDlg_DragEnter(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
					e.Effect = DragDropEffects.Copy;
			}
			catch
			{ }
		}

		private void CompressMainDlg_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

				if (files.Length < 1)
					return;

				string file = files[0];
				file = FileTools.toFullPath(file); // 2bs

				mtInvokers.Enqueue(delegate
				{
					// TODO

					MessageBox.Show("*Drop: " + file); // test
				});
			}
			catch
			{ }
		}

		private bool mtEnabled;
		private bool mtBusy;
		private long mtCount;

		private Queue<Utils.operation_d> mtInvokers = new Queue<Utils.operation_d>();

		private void mainTimer_Tick(object sender, EventArgs e)
		{
			if (this.mtEnabled == false || this.mtBusy)
				return;

			this.mtBusy = true;

			try
			{
				if (1 <= mtInvokers.Count)
				{
					mtInvokers.Dequeue()();
					return; // 2bs
				}
			}
			finally
			{
				this.mtBusy = false;
				this.mtCount++;
			}
		}

		private void CompressMainDlg_Move(object sender, EventArgs e)
		{
			CompressMainDlg_ResizeEnd(null, null);
		}

		private void CompressMainDlg_ResizeEnd(object sender, EventArgs e)
		{
			if (mtCount < Consts.FORM_INITED_TIMER_COUNT)
				return;

			_winRect.set(this);
		}
	}
}
