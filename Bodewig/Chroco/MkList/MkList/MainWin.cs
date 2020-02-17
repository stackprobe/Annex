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
using Charlotte.Chocomint.Dialogs;

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
			this.MinimumSize = this.Size;

			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

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

					this.MTBusy.Enter(); // 終了確定

					// ----

					// -- 9900

					// ----
				}
				catch (Exception e)
				{
					MessageBox.Show("" + e, "Error @ CloseWindow()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
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
				if (this.XPressed)
				{
					this.XPressed = false;
					this.CloseWindow();
					return;
				}
				if (this.MTCount % this.TVRefreshPeriod == 0)
				{
					if (this.TVRefreshPeriod < 20)
						this.TVRefreshPeriod++;

					using (this.TVEditSection())
					{
						foreach (TreeNode node in this.RecentlyCheckedTVNodes)
							node.Checked = node.Checked; // リフレッシュ
					}
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

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CloseWindow();
			return;
		}

		private void フォルダを開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					string dir = Ground.RootDir;

					if (SaveLoadDialogs.SelectFolder(ref dir, "ルートフォルダを開いてください"))
					{
						dir = FileTools.MakeFullPath(dir);

						if (Directory.Exists(dir) == false)
							throw new Exception("フォルダは存在しません。" + dir);

						this.TVClear();

						using (new Utils.UISuspend(this.TV))
						{
							this.AddTo(this.TV.Nodes, dir);
						}
						Ground.RootDir = dir;
					}
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("失敗：フォルダを開く", ex);
					this.TVClear();
				}
			}
		}

		private void AddTo(TreeNodeCollection dest, string targDir)
		{
			string[] dirs = Directory.GetDirectories(targDir);
			string[] files = Directory.GetFiles(targDir);

			Array.Sort(dirs, StringTools.CompIgnoreCase);
			Array.Sort(files, StringTools.CompIgnoreCase);

			foreach (string dir in dirs)
			{
				TreeNode node = new TreeNode(Path.GetFileName(dir));

				this.AddTo(node.Nodes, dir);

				node.Tag = new NodeTag()
				{
					DirFlag = true,
				};

				dest.Add(node);
			}
			foreach (string file in files)
			{
				TreeNode node = new TreeNode(Path.GetFileName(file));

				node.Tag = new NodeTag()
				{
					DirFlag = false,
				};

				dest.Add(node);
			}
		}

		private void ファイルに保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					string wFile = SaveLoadDialogs.SaveFile("保存先のファイルを入力して下さい", "txt", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "output.txt"));

					if (wFile != null)
					{
						using (StreamWriter writer = new StreamWriter(wFile, false, StringTools.ENCODING_SJIS))
						{
							this.WriteTV(writer, "", this.TV.Nodes);
						}
					}
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("失敗：ファイルに保存", ex);
				}
			}
		}

		private void WriteTV(StreamWriter writer, string parentDir, TreeNodeCollection nodes)
		{
			foreach (TreeNode node in nodes)
			{
				string path = Path.Combine(parentDir, node.Text);

				if (((NodeTag)node.Tag).DirFlag)
				{
					this.WriteTV(writer, path, node.Nodes);

					//writer.WriteLine(path); // test
				}
				else if (node.Checked)
				{
					writer.WriteLine(path);
				}
			}
		}

		private void ファイルから読み込みToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					string rFile = SaveLoadDialogs.LoadFile("読み込むファイルを選択して下さい", "テキスト:txt", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "target.txt"));

					if (rFile != null)
					{
						throw null; // TODO
					}
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("失敗：ファイルから読み込み", ex);
				}
			}
		}

		private void リフレッシュToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.TVEditSection())
			{
				foreach (TreeNode node in this.GetAllNode())
				{
					node.Checked = node.Checked;
				}
			}
		}

		private void 全選択ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.TVEditSection())
			{
				foreach (TreeNode node in this.GetAllNode())
				{
					node.Checked = true;
				}
			}
		}

		private void 全選択解除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.TVEditSection())
			{
				foreach (TreeNode node in this.GetAllNode())
				{
					node.Checked = false;
				}
			}
		}

		private IEnumerable<TreeNode> GetAllNode()
		{
			return this.GetAllNode(this.TV.Nodes);
		}

		private IEnumerable<TreeNode> GetAllNode(TreeNodeCollection rootNodes)
		{
			Queue<TreeNodeCollection> q = new Queue<TreeNodeCollection>();

			q.Enqueue(rootNodes);

			while (1 <= q.Count)
			{
				TreeNodeCollection nodes = q.Dequeue();

				foreach (TreeNode node in nodes)
				{
					yield return node;
					q.Enqueue(node.Nodes);
				}
			}
		}

		// このへんから TV 用

		private void TV_AfterSelect(object sender, TreeViewEventArgs e)
		{
			// noop
		}

		private void TVClear()
		{
			this.RecentlyCheckedTVNodes.Clear();
			this.TV.Nodes.Clear();
		}

		private List<TreeNode> RecentlyCheckedTVNodes = new List<TreeNode>();
		private int TVRefreshPeriod = 1;

		private void TV_AfterCheck(object sender, TreeViewEventArgs e)
		{
			using (this.TVEditSection())
			{
				foreach (TreeNode node in this.GetAllNode(e.Node.Nodes))
					node.Checked = e.Node.Checked;

				TreeNode parent = e.Node.Parent;

				while (parent != null)
				{
					parent.Checked = this.GetNodes(parent.Nodes).Any(node => node.Checked);
					parent = parent.Parent;
				}
				this.AddCheckedTVNode(e.Node);
				this.TVRefreshPeriod = 1;
			}
		}

		private void AddCheckedTVNode(TreeNode node)
		{
			if (this.RecentlyCheckedTVNodes.Any(n => n == node))
				return;

			while (5 <= this.RecentlyCheckedTVNodes.Count)
				this.RecentlyCheckedTVNodes.RemoveAt(0);

			this.RecentlyCheckedTVNodes.Add(node);
		}

		private IEnumerable<TreeNode> GetNodes(TreeNodeCollection nodes)
		{
			foreach (TreeNode node in nodes)
				yield return node;
		}

		private AnonyDisposable TVEditSection()
		{
			this.TV.AfterCheck -= this.TV_AfterCheck;

			return new AnonyDisposable(() =>
				this.TV.AfterCheck += this.TV_AfterCheck
				);
		}
	}
}
