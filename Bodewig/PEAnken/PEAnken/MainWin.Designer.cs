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
			this.MainSheet = new System.Windows.Forms.DataGridView();
			this.MainSheetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.選択解除SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.アプリケーションToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uRL読み込みToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.絞り込みMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.絞り込みクリアMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.South = new System.Windows.Forms.ToolStripStatusLabel();
			this.FreeWord = new System.Windows.Forms.TextBox();
			this.FreeWordMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.絞り込みクリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.列の幅リセットToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.MainSheet)).BeginInit();
			this.MainSheetMenu.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.FreeWordMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// MainSheet
			// 
			this.MainSheet.AllowUserToAddRows = false;
			this.MainSheet.AllowUserToDeleteRows = false;
			this.MainSheet.AllowUserToResizeRows = false;
			this.MainSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.MainSheet.ContextMenuStrip = this.MainSheetMenu;
			this.MainSheet.Location = new System.Drawing.Point(12, 60);
			this.MainSheet.Name = "MainSheet";
			this.MainSheet.ReadOnly = true;
			this.MainSheet.RowHeadersVisible = false;
			this.MainSheet.RowTemplate.Height = 21;
			this.MainSheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.MainSheet.Size = new System.Drawing.Size(560, 376);
			this.MainSheet.TabIndex = 0;
			this.MainSheet.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MainSheet_ColumnHeaderMouseClick);
			this.MainSheet.SelectionChanged += new System.EventHandler(this.MainSheet_SelectionChanged);
			// 
			// MainSheetMenu
			// 
			this.MainSheetMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.選択解除SToolStripMenuItem,
            this.toolStripMenuItem2,
            this.列の幅リセットToolStripMenuItem});
			this.MainSheetMenu.Name = "MainSheetMenu";
			this.MainSheetMenu.Size = new System.Drawing.Size(153, 76);
			// 
			// 選択解除SToolStripMenuItem
			// 
			this.選択解除SToolStripMenuItem.Name = "選択解除SToolStripMenuItem";
			this.選択解除SToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.選択解除SToolStripMenuItem.Text = "選択解除";
			this.選択解除SToolStripMenuItem.Click += new System.EventHandler(this.選択解除SToolStripMenuItem_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリケーションToolStripMenuItem,
            this.編集ToolStripMenuItem,
            this.絞り込みMenu});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(584, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// アプリケーションToolStripMenuItem
			// 
			this.アプリケーションToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了ToolStripMenuItem});
			this.アプリケーションToolStripMenuItem.Name = "アプリケーションToolStripMenuItem";
			this.アプリケーションToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
			this.アプリケーションToolStripMenuItem.Text = "アプリケーション";
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
			this.終了ToolStripMenuItem.Text = "終了";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
			// 
			// 編集ToolStripMenuItem
			// 
			this.編集ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uRL読み込みToolStripMenuItem});
			this.編集ToolStripMenuItem.Name = "編集ToolStripMenuItem";
			this.編集ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.編集ToolStripMenuItem.Text = "編集";
			// 
			// uRL読み込みToolStripMenuItem
			// 
			this.uRL読み込みToolStripMenuItem.Name = "uRL読み込みToolStripMenuItem";
			this.uRL読み込みToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			this.uRL読み込みToolStripMenuItem.Text = "URL読み込み";
			this.uRL読み込みToolStripMenuItem.Click += new System.EventHandler(this.URL読み込みToolStripMenuItem_Click);
			// 
			// 絞り込みMenu
			// 
			this.絞り込みMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.絞り込みクリアMenuItem,
            this.toolStripMenuItem1});
			this.絞り込みMenu.Name = "絞り込みMenu";
			this.絞り込みMenu.Size = new System.Drawing.Size(62, 20);
			this.絞り込みMenu.Text = "絞り込み";
			// 
			// 絞り込みクリアMenuItem
			// 
			this.絞り込みクリアMenuItem.Name = "絞り込みクリアMenuItem";
			this.絞り込みクリアMenuItem.Size = new System.Drawing.Size(100, 22);
			this.絞り込みクリアMenuItem.Text = "クリア";
			this.絞り込みクリアMenuItem.Click += new System.EventHandler(this.絞り込みクリアMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(97, 6);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.South});
			this.statusStrip1.Location = new System.Drawing.Point(0, 439);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(584, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// South
			// 
			this.South.Name = "South";
			this.South.Size = new System.Drawing.Size(86, 17);
			this.South.Text = "準備しています...";
			// 
			// FreeWord
			// 
			this.FreeWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FreeWord.ContextMenuStrip = this.FreeWordMenu;
			this.FreeWord.Location = new System.Drawing.Point(12, 27);
			this.FreeWord.MaxLength = 300;
			this.FreeWord.Name = "FreeWord";
			this.FreeWord.Size = new System.Drawing.Size(560, 27);
			this.FreeWord.TabIndex = 3;
			this.FreeWord.TextChanged += new System.EventHandler(this.FreeWord_TextChanged);
			// 
			// FreeWordMenu
			// 
			this.FreeWordMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.絞り込みクリアToolStripMenuItem});
			this.FreeWordMenu.Name = "FreeWordMenu";
			this.FreeWordMenu.Size = new System.Drawing.Size(101, 26);
			// 
			// 絞り込みクリアToolStripMenuItem
			// 
			this.絞り込みクリアToolStripMenuItem.Name = "絞り込みクリアToolStripMenuItem";
			this.絞り込みクリアToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.絞り込みクリアToolStripMenuItem.Text = "クリア";
			this.絞り込みクリアToolStripMenuItem.Click += new System.EventHandler(this.絞り込みクリアToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
			// 
			// 列の幅リセットToolStripMenuItem
			// 
			this.列の幅リセットToolStripMenuItem.Name = "列の幅リセットToolStripMenuItem";
			this.列の幅リセットToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.列の幅リセットToolStripMenuItem.Text = "列の幅リセット";
			this.列の幅リセットToolStripMenuItem.Click += new System.EventHandler(this.列の幅リセットToolStripMenuItem_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 461);
			this.Controls.Add(this.FreeWord);
			this.Controls.Add(this.MainSheet);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "PEAnken";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			((System.ComponentModel.ISupportInitialize)(this.MainSheet)).EndInit();
			this.MainSheetMenu.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.FreeWordMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.DataGridView MainSheet;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem アプリケーションToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 編集ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uRL読み込みToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel South;
		private System.Windows.Forms.TextBox FreeWord;
		private System.Windows.Forms.ContextMenuStrip MainSheetMenu;
		private System.Windows.Forms.ToolStripMenuItem 選択解除SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 絞り込みMenu;
		private System.Windows.Forms.ContextMenuStrip FreeWordMenu;
		private System.Windows.Forms.ToolStripMenuItem 絞り込みクリアToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 絞り込みクリアMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem 列の幅リセットToolStripMenuItem;
	}
}

