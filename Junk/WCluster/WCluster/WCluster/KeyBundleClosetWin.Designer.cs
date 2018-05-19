namespace Charlotte
{
	partial class KeyBundleClosetWin
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyBundleClosetWin));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.kbSheet = new System.Windows.Forms.DataGridView();
			this.keySheetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.追加AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.削除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.desideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainTimer = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.kbSheet)).BeginInit();
			this.keySheetMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 339);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(484, 23);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(104, 18);
			this.lblStatus.Text = "準備しています...";
			// 
			// kbSheet
			// 
			this.kbSheet.AllowUserToAddRows = false;
			this.kbSheet.AllowUserToDeleteRows = false;
			this.kbSheet.AllowUserToResizeRows = false;
			this.kbSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.kbSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.kbSheet.ContextMenuStrip = this.keySheetMenu;
			this.kbSheet.Location = new System.Drawing.Point(12, 12);
			this.kbSheet.MultiSelect = false;
			this.kbSheet.Name = "kbSheet";
			this.kbSheet.ReadOnly = true;
			this.kbSheet.RowTemplate.Height = 21;
			this.kbSheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.kbSheet.Size = new System.Drawing.Size(460, 324);
			this.kbSheet.TabIndex = 2;
			this.kbSheet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.kbSheet_CellContentClick);
			this.kbSheet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.kbSheet_CellDoubleClick);
			// 
			// keySheetMenu
			// 
			this.keySheetMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.追加AToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.削除DToolStripMenuItem,
            this.toolStripMenuItem1,
            this.desideToolStripMenuItem});
			this.keySheetMenu.Name = "keySheetMenu";
			this.keySheetMenu.Size = new System.Drawing.Size(203, 98);
			// 
			// 追加AToolStripMenuItem
			// 
			this.追加AToolStripMenuItem.Name = "追加AToolStripMenuItem";
			this.追加AToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.追加AToolStripMenuItem.Text = "新しい鍵を生成する(&A)";
			this.追加AToolStripMenuItem.Click += new System.EventHandler(this.追加AToolStripMenuItem_Click);
			// 
			// 編集EToolStripMenuItem
			// 
			this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
			this.編集EToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.編集EToolStripMenuItem.Text = "名前を変更する(&R)";
			this.編集EToolStripMenuItem.Click += new System.EventHandler(this.編集EToolStripMenuItem_Click);
			// 
			// 削除DToolStripMenuItem
			// 
			this.削除DToolStripMenuItem.Name = "削除DToolStripMenuItem";
			this.削除DToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.削除DToolStripMenuItem.Text = "削除(&D)";
			this.削除DToolStripMenuItem.Click += new System.EventHandler(this.削除DToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(199, 6);
			// 
			// desideToolStripMenuItem
			// 
			this.desideToolStripMenuItem.Name = "desideToolStripMenuItem";
			this.desideToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.desideToolStripMenuItem.Text = "決定(&S)";
			this.desideToolStripMenuItem.Click += new System.EventHandler(this.desideToolStripMenuItem_Click);
			// 
			// mainTimer
			// 
			this.mainTimer.Enabled = true;
			this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
			// 
			// KeyBundleClosetWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 362);
			this.Controls.Add(this.kbSheet);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimizeBox = false;
			this.Name = "KeyBundleClosetWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "WCluster / Key Bundle Closet";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyBundleClosetWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeyBundleClosetWin_FormClosed);
			this.Load += new System.EventHandler(this.KeyBundleClosetWin_Load);
			this.Shown += new System.EventHandler(this.KeyBundleClosetWin_Shown);
			this.ResizeEnd += new System.EventHandler(this.KeyBundleClosetWin_ResizeEnd);
			this.Move += new System.EventHandler(this.KeyBundleClosetWin_Move);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.kbSheet)).EndInit();
			this.keySheetMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.DataGridView kbSheet;
		private System.Windows.Forms.ContextMenuStrip keySheetMenu;
		private System.Windows.Forms.ToolStripMenuItem 追加AToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 編集EToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 削除DToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem desideToolStripMenuItem;
		private System.Windows.Forms.Timer mainTimer;
	}
}
