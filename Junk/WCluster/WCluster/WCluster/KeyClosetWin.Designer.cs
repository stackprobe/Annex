namespace Charlotte
{
	partial class KeyClosetWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyClosetWin));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.keySheet = new System.Windows.Forms.DataGridView();
			this.keySheetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.追加AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.削除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.インポートIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.エクスポートEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.desideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainTimer = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.keySheet)).BeginInit();
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
			// keySheet
			// 
			this.keySheet.AllowDrop = true;
			this.keySheet.AllowUserToAddRows = false;
			this.keySheet.AllowUserToDeleteRows = false;
			this.keySheet.AllowUserToResizeRows = false;
			this.keySheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.keySheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.keySheet.ContextMenuStrip = this.keySheetMenu;
			this.keySheet.Location = new System.Drawing.Point(12, 12);
			this.keySheet.MultiSelect = false;
			this.keySheet.Name = "keySheet";
			this.keySheet.ReadOnly = true;
			this.keySheet.RowTemplate.Height = 21;
			this.keySheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.keySheet.Size = new System.Drawing.Size(460, 324);
			this.keySheet.TabIndex = 2;
			this.keySheet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.keySheet_CellContentClick);
			this.keySheet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.keySheet_CellDoubleClick);
			this.keySheet.DragDrop += new System.Windows.Forms.DragEventHandler(this.keySheet_DragDrop);
			this.keySheet.DragEnter += new System.Windows.Forms.DragEventHandler(this.keySheet_DragEnter);
			// 
			// keySheetMenu
			// 
			this.keySheetMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.追加AToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.削除DToolStripMenuItem,
            this.toolStripMenuItem1,
            this.インポートIToolStripMenuItem,
            this.エクスポートEToolStripMenuItem,
            this.toolStripMenuItem2,
            this.desideToolStripMenuItem});
			this.keySheetMenu.Name = "keySheetMenu";
			this.keySheetMenu.Size = new System.Drawing.Size(203, 148);
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
			// インポートIToolStripMenuItem
			// 
			this.インポートIToolStripMenuItem.Name = "インポートIToolStripMenuItem";
			this.インポートIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.インポートIToolStripMenuItem.Text = "インポート(&I)";
			this.インポートIToolStripMenuItem.Click += new System.EventHandler(this.インポートIToolStripMenuItem_Click);
			// 
			// エクスポートEToolStripMenuItem
			// 
			this.エクスポートEToolStripMenuItem.Name = "エクスポートEToolStripMenuItem";
			this.エクスポートEToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.エクスポートEToolStripMenuItem.Text = "エクスポート(&E)";
			this.エクスポートEToolStripMenuItem.Click += new System.EventHandler(this.エクスポートEToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(199, 6);
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
			// KeyClosetWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 362);
			this.Controls.Add(this.keySheet);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimizeBox = false;
			this.Name = "KeyClosetWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "WCluster / Key Closet";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyClosetWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeyClosetWin_FormClosed);
			this.Load += new System.EventHandler(this.KeyClosetWin_Load);
			this.Shown += new System.EventHandler(this.KeyClosetWin_Shown);
			this.ResizeEnd += new System.EventHandler(this.KeyClosetWin_ResizeEnd);
			this.Move += new System.EventHandler(this.KeyClosetWin_Move);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.keySheet)).EndInit();
			this.keySheetMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.DataGridView keySheet;
		private System.Windows.Forms.ContextMenuStrip keySheetMenu;
		private System.Windows.Forms.ToolStripMenuItem 追加AToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 編集EToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 削除DToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem desideToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem インポートIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem エクスポートEToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.Timer mainTimer;
	}
}
