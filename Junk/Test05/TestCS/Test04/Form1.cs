using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// noop
		}

		private Process Proc;

		private void button1_Click(object sender, EventArgs e)
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = "..\\..\\..\\..\\TestCPP\\Release\\ModuleTest.exe";

			this.Proc = Process.Start(psi);
		}

		private void CheckProcEnd()
		{
			// シャットダウンした場合 -> this.Proc は CTRL+C と同じエラーコード -1073741510 で終わっている。
			// CUI が GUI より常に先に終わるかどうかは不明...

			if (this.Proc != null)
			{
				if (this.Proc.HasExited)
				{
					File.AppendAllLines("C:\\tmp\\Test05_Log.txt", new string[]
					{
						"ExitCode: " + this.Proc.ExitCode,
					}
					);
					this.Proc = null;
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.CheckProcEnd();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			new Form2().ShowDialog();

			this.CheckProcEnd();
		}
	}
}
