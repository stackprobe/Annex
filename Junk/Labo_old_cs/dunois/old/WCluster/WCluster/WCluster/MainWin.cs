using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Threading;
using System.IO;

namespace WCluster
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				Clusterizer.CancelFlag.SetFlag(true);
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

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.MinimumSize = this.Size;

			this.BackColor = Color.Black;
			this.MainPanel.BackColor = Color.Black;

			this.ProgressImg.Width = this.ProgressImg.Image.Width;
			this.ProgressImg.Height = this.ProgressImg.Image.Height;

			this.Status.ForeColor = Color.White;
			this.Status2.ForeColor = Color.Gray;

			this.MT_Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				if (this.MT_Count == 0)
				{
					this.MainProcTh = new Thread(MainProc);
					this.MainProcTh.Start();
				}
				if (5 < this.MT_Count)
				{
					if (this.MainProcTh.IsAlive == false)
					{
						this.MT_Enabled = false;
						this.Close();
						return;
					}
				}

				lock (Confirm_SYNCROOT)
				{
					if (Confirm_Message != null)
					{
						DialogResult ret = MessageBox.Show(
							Confirm_Message,
							"WCluster / 確認",
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Warning
							);

						Confirm_Message = null;
						Confirm_Ret = ret == DialogResult.OK;
					}
				}

				if (this.MT_Count % 3 == 0)
				{
					int l = (this.MainPanel.Width - this.ProgressImg.Width) / 2;
					int t = (this.MainPanel.Height - this.ProgressImg.Height) / 2;
					t -= 10;

					if (this.ProgressImg.Left != l)
						this.ProgressImg.Left = l;

					if (this.ProgressImg.Top != t)
						this.ProgressImg.Top = t;

					{
						string status = Clusterizer.CancelFlag.GetFlag() ?
							"Cancelling..." :
							Clusterizer.Status.GetString();

						long dirCount = Clusterizer.DirCounter.GetCount();
						long fileCount = Clusterizer.FileCounter.GetCount();
						string status2;

						if (dirCount == 0 && fileCount == 0)
						{
							status2 = "";
						}
						else
						{
							status2 = Clusterizer.DirCounter.GetCount() +
								" directories, " +
								Clusterizer.FileCounter.GetCount() +
								" files processed";
						}
						if (this.Status.Text != status)
							this.Status.Text = status;

						if (this.Status2.Text != status2)
							this.Status2.Text = status2;
					}

					l += (this.ProgressImg.Width - this.Status.Width) / 2;
					t += this.ProgressImg.Height + 10;

					if (this.Status.Left != l)
						this.Status.Left = l;

					if (this.Status.Top != t)
						this.Status.Top = t;

					l = this.ProgressImg.Left + (this.ProgressImg.Width - this.Status2.Width) / 2;
					t += 20;

					if (this.Status2.Left != l)
						this.Status2.Left = l;

					if (this.Status2.Top != t)
						this.Status2.Top = t;
				}

				Image img = this.ProgressImg.Image;
				img.RotateFlip(RotateFlipType.Rotate90FlipNone);
				this.ProgressImg.Image = img;
			}
			catch (Exception ex)
			{
				this.MT_Enabled = false;
				throw ex;
			}
			finally
			{
				this.MT_Count++;
				this.MT_Busy = false;
			}
		}

		private Thread MainProcTh;
		public static Exception MainProcEx;

		private const string CLUSTER_EXT = ".wclu";
		private const string DEF_OUT_EXT = ".out";

		private void MainProc()
		{
			try
			{
				string[] args = Environment.GetCommandLineArgs();
				args = ShiftArray(args);

				Clusterizer clusterizer = new Clusterizer();
				bool forceMode = false;
				string autoWPath = null;

				for (; ; )
				{
					if (args[0].ToUpper() == "/F")
					{
						forceMode = true;
						args = ShiftArray(args);
						continue;
					}
					if (args[0].ToUpper() == "/P")
					{
						clusterizer = new CipherClusterizer(args[1]);
						args = ShiftArray(args);
						args = ShiftArray(args);
						continue;
					}
					if (args[0].ToUpper() == "/S")
					{
						Clusterizer.ShowConsoleFlag = true;
						args = ShiftArray(args);
						continue;
					}
					if (args[0].ToUpper() == "/O")
					{
						autoWPath = args[1];
						args = ShiftArray(args);
						args = ShiftArray(args);
						continue;
					}
					if (args[0].ToUpper() == "/-")
					{
						args = ShiftArray(args);
						break;
					}
					break;
				}
				if (args.Length == 1 && autoWPath != null)
				{
					args = PushArray(args, autoWPath);
				}
				if (args.Length == 1)
				{
					string rPath = args[0];

					rPath = Path.GetFullPath(rPath);

					if (File.Exists(rPath))
					{
						string wPath =
							Path.GetExtension(rPath).ToLower() == CLUSTER_EXT ?
								EraseExtension(rPath) :
								rPath + DEF_OUT_EXT;

						if (
							forceMode == false &&
							ExistsPath(wPath) &&
							ConfirmOverwrite("[1F]") == false
							)
							return;

						clusterizer.FileToDirectory(rPath, wPath);
						Completed();
						return;
					}
					if (Directory.Exists(rPath))
					{
						string wPath = rPath + CLUSTER_EXT;

						if (
							forceMode == false &&
							ExistsPath(wPath) &&
							ConfirmOverwrite("[1D]") == false
							)
							return;

						clusterizer.DirectoryToFile(rPath, wPath);
						Completed();
						return;
					}
					throw new Exception("入力パスは存在しません。[1]");
				}
				if (args.Length == 2)
				{
					string rPath = args[0];
					string wPath = args[1];

					rPath = Path.GetFullPath(rPath);
					wPath = Path.GetFullPath(wPath);

					if (File.Exists(rPath))
					{
						if (Directory.Exists(wPath))
						{
							wPath = Path.Combine(
								wPath,
								Path.GetExtension(rPath).ToLower() == CLUSTER_EXT ?
									Path.GetFileNameWithoutExtension(rPath) :
									Path.GetFileName(rPath) + DEF_OUT_EXT
								);
						}
						if (
							forceMode == false &&
							ExistsPath(wPath) &&
							ConfirmOverwrite("[2F]") == false
							)
							return;

						clusterizer.FileToDirectory(rPath, wPath);
						Completed();
						return;
					}
					if (Directory.Exists(rPath))
					{
						if (Directory.Exists(wPath))
							wPath = Path.Combine(wPath, Path.GetFileName(rPath) + CLUSTER_EXT);

						if (
							forceMode == false &&
							ExistsPath(wPath) &&
							ConfirmOverwrite("[2D]") == false
							)
							return;

						clusterizer.DirectoryToFile(rPath, wPath);
						Completed();
						return;
					}
					throw new Exception("入力パスは存在しません。[2]");
				}
				throw new Exception("コマンド引数エラー");
			}
			catch (Exception e)
			{
				MainProcEx = e;
			}
		}

		public static string[] ShiftArray(string[] src)
		{
			string[] dest = new string[src.Length - 1];

			for (int index = 1; index < src.Length; index++)
				dest[index - 1] = src[index];

			return dest;
		}

		public static string[] PushArray(string[] src, string appendix)
		{
			string[] dest = new string[src.Length + 1];

			for (int index = 0; index < src.Length; index++)
				dest[index] = src[index];

			dest[src.Length] = appendix;
			return dest;
		}

		public static string EraseExtension(string path)
		{
			return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
		}

		public static bool ExistsPath(string path)
		{
			return File.Exists(path) || Directory.Exists(path);
		}

		private object Confirm_SYNCROOT = new object();
		private string Confirm_Message;
		private bool Confirm_Ret;

		private bool ConfirmOverwrite(string trailer)
		{
			return Confirm("出力パスは既に存在します。上書きしますか？ " + trailer);
		}

		private bool Confirm(string message)
		{
			lock (Confirm_SYNCROOT)
			{
				Confirm_Message = message;
			}
			for (; ; )
			{
				Thread.Sleep(100); // MainTimer_Tick でロックされるまで、、ロックされたら回らなくなるので注意！

				lock (Confirm_SYNCROOT)
				{
					if (Confirm_Message == null)
					{
						return Confirm_Ret;
					}
				}
			}
		}

		private void Completed()
		{
			Clusterizer.Status.SetString("Completed successfully");
			Thread.Sleep(500);
		}
	}
}
