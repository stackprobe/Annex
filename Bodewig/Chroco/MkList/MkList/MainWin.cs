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
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private TreeView TV;

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

			{
				TreeView tv = new TreeViewWP();

				tv.Location = this.TVDummy.Location;
				tv.Size = this.TVDummy.Size;
				tv.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
				tv.CheckBoxes = true;
				tv.ContextMenuStrip = this.TVMenu;
				tv.AfterCheck += this.TV_AfterCheck;

				this.TV = tv;
			}

			this.Controls.Remove(this.TVDummy);
			this.Controls.Add(this.TV);

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

		private void TVDummy_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void リフレッシュToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (TreeNode node in this.GetAllNode())
			{
				node.Checked = node.Checked;
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

		private void フォルダを開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

					if (SaveLoadDialogs.SelectFolder(ref dir, "ルートフォルダを開いてください"))
					{
						dir = FileTools.MakeFullPath(dir);

						if (Directory.Exists(dir) == false)
							throw new Exception("フォルダは存在しません。" + dir);

						this.TV.Nodes.Clear();

						using (new Utils.UISuspend(this.TV))
						{
							this.AddTo(this.TV.Nodes, dir);
						}
					}
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("失敗：フォルダを開く", ex);
					this.TV.Nodes.Clear();
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
			}
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
