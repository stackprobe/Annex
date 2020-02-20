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

			this.MinimumSize = this.Size;
			this.South.Text = "";
			this.SouthEast.Text = "";
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			Ground.LoadDatFile();

			this.LoadLTWH();
		}

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

		private void LoadLTWH()
		{
			if (Ground.MainWin_W == -1) // ? 未保存
				return;

			this.Left = Ground.MainWin_L;
			this.Top = Ground.MainWin_T;
			this.Width = Ground.MainWin_W;
			this.Height = Ground.MainWin_H;
		}

		private void SaveLTWH()
		{
			if (this.WindowState != FormWindowState.Normal)
				return;

			Ground.MainWin_L = this.Left;
			Ground.MainWin_T = this.Top;
			Ground.MainWin_W = this.Width;
			Ground.MainWin_H = this.Height;
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

					this.SaveLTWH();

					Ground.SaveDatFile();

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

		private void TVDummy_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void リフレッシュToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.TVEditSection())
			{
				foreach (TreeNode node in this.GetAllNode())
				{
					node.Checked = node.Checked;
				}
				this.TV.SelectedNode = null; // 選択解除
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

		private IEnumerable<TreeNode> GetNodes(TreeNodeCollection nodes)
		{
			foreach (TreeNode node in nodes)
				yield return node;
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

						this.TV.Nodes.Clear();

						using (new Utils.UISuspend(this.TV))
						{
							this.AddTo(this.TV.Nodes, dir);
						}
						Ground.RootDir = dir;

						this.South.Text = dir; // 暫定？
						this.SouthEast.Text = "" + this.GetAllNode().Where(node => ((NodeTag)node.Tag).DirFlag == false).Count(); // 暫定
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

		private void ファイルに保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					string wFile = SaveLoadDialogs.SaveFile("保存先のファイルを入力して下さい", "txt", Ground.LastFile);

					if (wFile != null)
					{
						using (StreamWriter writer = new StreamWriter(wFile, false, StringTools.ENCODING_SJIS))
						{
							this.WriteTV(writer, "", this.TV.Nodes);
						}
						Ground.LastFile = wFile;
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

		private void ファイルから読み込むToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					string rFile = SaveLoadDialogs.LoadFile("読み込むファイルを選択して下さい", "テキスト:txt", Ground.LastFile);

					if (rFile != null)
					{
						string[] lines = File.ReadAllLines(rFile, StringTools.ENCODING_SJIS);

						using (this.TVEditSection())
						{
							foreach (TreeNode node in this.GetAllNode())
								node.Checked = false;

							foreach (string line in lines)
							{
								if (line == "")
									throw new Exception("空行を読み込みました。");

								if (line[1] == ':')
									throw new Exception("フルパスは処理出来ません。\r\n" + line);

								string[] tokens = line.Split('\\');
								TreeNode currNode = null;

								foreach (string token in tokens)
								{
									TreeNodeCollection nodeCollection = currNode == null ? this.TV.Nodes : currNode.Nodes;
									TreeNode[] nodes = this.GetNodes(nodeCollection).ToArray();
									int nodeIndex = ArrayTools.IndexOf(nodes, node => StringTools.EqualsIgnoreCase(node.Text, token));

									if (nodeIndex == -1)
										throw new Exception("ツリーに存在しないファイルパスを読み込みました。\r\n" + line);

									currNode = nodes[nodeIndex];
								}
								currNode.Checked = true;
							}
							this.TVRecorrect();
						}
						Ground.LastFile = rFile;
					}
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("失敗：ファイルから読み込み", ex);
				}
			}
		}

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CloseWindow();
		}

		//
		// このへんから TV 用
		//

		private TreeView TV;

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

		private AnonyDisposable TVEditSection()
		{
			this.TV.AfterCheck -= this.TV_AfterCheck;

			return new AnonyDisposable(() =>
				this.TV.AfterCheck += this.TV_AfterCheck
				);
		}

		private void TVRecorrect()
		{
			this.TVRecorrect(this.GetNodes(this.TV.Nodes).ToArray());
		}

		private void TVRecorrect(TreeNode[] nodes)
		{
			foreach (TreeNode node in nodes)
			{
				this.TVRecorrect(node);
			}
		}

		private void TVRecorrect(TreeNode parent)
		{
			TreeNode[] children = this.GetNodes(parent.Nodes).ToArray();

			if (1 <= children.Length)
			{
				this.TVRecorrect(children);
				parent.Checked = this.GetNodes(parent.Nodes).Any(node => node.Checked);
			}
		}
	}
}
