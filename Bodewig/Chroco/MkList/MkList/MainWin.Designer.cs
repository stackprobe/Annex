namespace Charlotte
{
	partial class MainWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.TVDummy = new System.Windows.Forms.TextBox();
			this.TVMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.リフレッシュToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.全チェックToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.全チェック解除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.ツリーを開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ツリーを閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.選択されているノードの配下を開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.選択されているノードを閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.アプリToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.フォルダを開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.ファイルに保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ファイルから読み込むToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ツールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.シートToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.South = new System.Windows.Forms.ToolStripStatusLabel();
			this.SouthEast = new System.Windows.Forms.ToolStripStatusLabel();
			this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ファイルサイズも読み込むToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高速終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TVMenu.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// TVDummy
			// 
			this.TVDummy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TVDummy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.TVDummy.Location = new System.Drawing.Point(12, 27);
			this.TVDummy.Multiline = true;
			this.TVDummy.Name = "TVDummy";
			this.TVDummy.Size = new System.Drawing.Size(260, 209);
			this.TVDummy.TabIndex = 0;
			this.TVDummy.Text = "TVDummy";
			this.TVDummy.TextChanged += new System.EventHandler(this.TVDummy_TextChanged);
			// 
			// TVMenu
			// 
			this.TVMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.リフレッシュToolStripMenuItem,
            this.toolStripMenuItem3,
            this.全チェックToolStripMenuItem,
            this.全チェック解除ToolStripMenuItem,
            this.toolStripMenuItem4,
            this.ツリーを開くToolStripMenuItem,
            this.ツリーを閉じるToolStripMenuItem,
            this.選択されているノードの配下を開くToolStripMenuItem,
            this.選択されているノードを閉じるToolStripMenuItem});
			this.TVMenu.Name = "TVMenu";
			this.TVMenu.Size = new System.Drawing.Size(232, 192);
			// 
			// リフレッシュToolStripMenuItem
			// 
			this.リフレッシュToolStripMenuItem.Name = "リフレッシュToolStripMenuItem";
			this.リフレッシュToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			this.リフレッシュToolStripMenuItem.Text = "リフレッシュ";
			this.リフレッシュToolStripMenuItem.Click += new System.EventHandler(this.リフレッシュToolStripMenuItem_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(228, 6);
			// 
			// 全チェックToolStripMenuItem
			// 
			this.全チェックToolStripMenuItem.Name = "全チェックToolStripMenuItem";
			this.全チェックToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			this.全チェックToolStripMenuItem.Text = "全てチェックする";
			this.全チェックToolStripMenuItem.Click += new System.EventHandler(this.全チェックToolStripMenuItem_Click);
			// 
			// 全チェック解除ToolStripMenuItem
			// 
			this.全チェック解除ToolStripMenuItem.Name = "全チェック解除ToolStripMenuItem";
			this.全チェック解除ToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			this.全チェック解除ToolStripMenuItem.Text = "全てのチェックを外す";
			this.全チェック解除ToolStripMenuItem.Click += new System.EventHandler(this.全チェック解除ToolStripMenuItem_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(228, 6);
			// 
			// ツリーを開くToolStripMenuItem
			// 
			this.ツリーを開くToolStripMenuItem.Name = "ツリーを開くToolStripMenuItem";
			this.ツリーを開くToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			this.ツリーを開くToolStripMenuItem.Text = "ツリーを開く";
			this.ツリーを開くToolStripMenuItem.Click += new System.EventHandler(this.ツリーを開くToolStripMenuItem_Click);
			// 
			// ツリーを閉じるToolStripMenuItem
			// 
			this.ツリーを閉じるToolStripMenuItem.Name = "ツリーを閉じるToolStripMenuItem";
			this.ツリーを閉じるToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			this.ツリーを閉じるToolStripMenuItem.Text = "ツリーを閉じる";
			this.ツリーを閉じるToolStripMenuItem.Click += new System.EventHandler(this.ツリーを閉じるToolStripMenuItem_Click);
			// 
			// 選択されているノードの配下を開くToolStripMenuItem
			// 
			this.選択されているノードの配下を開くToolStripMenuItem.Name = "選択されているノードの配下を開くToolStripMenuItem";
			this.選択されているノードの配下を開くToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			this.選択されているノードの配下を開くToolStripMenuItem.Text = "選択されているノードの配下を開く";
			this.選択されているノードの配下を開くToolStripMenuItem.Click += new System.EventHandler(this.選択されているノードの配下を開くToolStripMenuItem_Click);
			// 
			// 選択されているノードを閉じるToolStripMenuItem
			// 
			this.選択されているノードを閉じるToolStripMenuItem.Name = "選択されているノードを閉じるToolStripMenuItem";
			this.選択されているノードを閉じるToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			this.選択されているノードを閉じるToolStripMenuItem.Text = "選択されているノードを閉じる";
			this.選択されているノードを閉じるToolStripMenuItem.Click += new System.EventHandler(this.選択されているノードを閉じるToolStripMenuItem_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリToolStripMenuItem,
            this.設定ToolStripMenuItem,
            this.ツールToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(284, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// アプリToolStripMenuItem
			// 
			this.アプリToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.フォルダを開くToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ファイルに保存ToolStripMenuItem,
            this.ファイルから読み込むToolStripMenuItem,
            this.toolStripMenuItem2,
            this.終了ToolStripMenuItem});
			this.アプリToolStripMenuItem.Name = "アプリToolStripMenuItem";
			this.アプリToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.アプリToolStripMenuItem.Text = "アプリ";
			// 
			// フォルダを開くToolStripMenuItem
			// 
			this.フォルダを開くToolStripMenuItem.Name = "フォルダを開くToolStripMenuItem";
			this.フォルダを開くToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.フォルダを開くToolStripMenuItem.Text = "フォルダを開く";
			this.フォルダを開くToolStripMenuItem.Click += new System.EventHandler(this.フォルダを開くToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 6);
			// 
			// ファイルに保存ToolStripMenuItem
			// 
			this.ファイルに保存ToolStripMenuItem.Name = "ファイルに保存ToolStripMenuItem";
			this.ファイルに保存ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.ファイルに保存ToolStripMenuItem.Text = "ファイルに保存";
			this.ファイルに保存ToolStripMenuItem.Click += new System.EventHandler(this.ファイルに保存ToolStripMenuItem_Click);
			// 
			// ファイルから読み込むToolStripMenuItem
			// 
			this.ファイルから読み込むToolStripMenuItem.Name = "ファイルから読み込むToolStripMenuItem";
			this.ファイルから読み込むToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.ファイルから読み込むToolStripMenuItem.Text = "ファイルから読み込む";
			this.ファイルから読み込むToolStripMenuItem.Click += new System.EventHandler(this.ファイルから読み込むToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 6);
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.終了ToolStripMenuItem.Text = "終了";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
			// 
			// ツールToolStripMenuItem
			// 
			this.ツールToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.シートToolStripMenuItem});
			this.ツールToolStripMenuItem.Name = "ツールToolStripMenuItem";
			this.ツールToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.ツールToolStripMenuItem.Text = "ツール";
			// 
			// シートToolStripMenuItem
			// 
			this.シートToolStripMenuItem.Name = "シートToolStripMenuItem";
			this.シートToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.シートToolStripMenuItem.Text = "シート";
			this.シートToolStripMenuItem.Click += new System.EventHandler(this.シートToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.South,
            this.SouthEast});
			this.statusStrip1.Location = new System.Drawing.Point(0, 239);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(284, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// South
			// 
			this.South.Name = "South";
			this.South.Size = new System.Drawing.Size(210, 17);
			this.South.Spring = true;
			this.South.Text = "South";
			this.South.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SouthEast
			// 
			this.SouthEast.Name = "SouthEast";
			this.SouthEast.Size = new System.Drawing.Size(59, 17);
			this.SouthEast.Text = "SouthEast";
			// 
			// 設定ToolStripMenuItem
			// 
			this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルサイズも読み込むToolStripMenuItem,
            this.高速終了ToolStripMenuItem});
			this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
			this.設定ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.設定ToolStripMenuItem.Text = "設定";
			// 
			// ファイルサイズも読み込むToolStripMenuItem
			// 
			this.ファイルサイズも読み込むToolStripMenuItem.Name = "ファイルサイズも読み込むToolStripMenuItem";
			this.ファイルサイズも読み込むToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.ファイルサイズも読み込むToolStripMenuItem.Text = "ファイルサイズも読み込む";
			this.ファイルサイズも読み込むToolStripMenuItem.Click += new System.EventHandler(this.ファイルサイズも読み込むToolStripMenuItem_Click);
			// 
			// 高速終了ToolStripMenuItem
			// 
			this.高速終了ToolStripMenuItem.Name = "高速終了ToolStripMenuItem";
			this.高速終了ToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.高速終了ToolStripMenuItem.Text = "高速終了";
			this.高速終了ToolStripMenuItem.Click += new System.EventHandler(this.高速終了ToolStripMenuItem_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.TVDummy);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "MkList";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.TVMenu.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.TextBox TVDummy;
		private System.Windows.Forms.ContextMenuStrip TVMenu;
		private System.Windows.Forms.ToolStripMenuItem リフレッシュToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem アプリToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem フォルダを開くToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ファイルに保存ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ファイルから読み込むToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem 全チェックToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 全チェック解除ToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel South;
		private System.Windows.Forms.ToolStripStatusLabel SouthEast;
		private System.Windows.Forms.ToolStripMenuItem ツールToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem シートToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem ツリーを開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ツリーを閉じるToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 選択されているノードの配下を開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 選択されているノードを閉じるToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ファイルサイズも読み込むToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高速終了ToolStripMenuItem;
	}
}

