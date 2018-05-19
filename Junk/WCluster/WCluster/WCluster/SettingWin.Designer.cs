namespace Charlotte
{
	partial class SettingWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingWin));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tp圧縮 = new System.Windows.Forms.TabPage();
			this.tp展開 = new System.Windows.Forms.TabPage();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.アプリAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.鍵の管理KToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.keyClosetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.keyBundleClosetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.mainTimer = new System.Windows.Forms.Timer(this.components);
			this.tabControl1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tp圧縮);
			this.tabControl1.Controls.Add(this.tp展開);
			this.tabControl1.Location = new System.Drawing.Point(12, 27);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(609, 445);
			this.tabControl1.TabIndex = 1;
			// 
			// tp圧縮
			// 
			this.tp圧縮.Location = new System.Drawing.Point(4, 29);
			this.tp圧縮.Name = "tp圧縮";
			this.tp圧縮.Padding = new System.Windows.Forms.Padding(3);
			this.tp圧縮.Size = new System.Drawing.Size(601, 412);
			this.tp圧縮.TabIndex = 0;
			this.tp圧縮.Text = "圧縮";
			this.tp圧縮.UseVisualStyleBackColor = true;
			// 
			// tp展開
			// 
			this.tp展開.Location = new System.Drawing.Point(4, 29);
			this.tp展開.Name = "tp展開";
			this.tp展開.Padding = new System.Windows.Forms.Padding(3);
			this.tp展開.Size = new System.Drawing.Size(601, 412);
			this.tp展開.TabIndex = 1;
			this.tp展開.Text = "展開";
			this.tp展開.UseVisualStyleBackColor = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリAToolStripMenuItem,
            this.鍵の管理KToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(633, 26);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// アプリAToolStripMenuItem
			// 
			this.アプリAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem});
			this.アプリAToolStripMenuItem.Name = "アプリAToolStripMenuItem";
			this.アプリAToolStripMenuItem.Size = new System.Drawing.Size(74, 22);
			this.アプリAToolStripMenuItem.Text = "アプリ(&A)";
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
			// 
			// 鍵の管理KToolStripMenuItem
			// 
			this.鍵の管理KToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyClosetToolStripMenuItem,
            this.keyBundleClosetToolStripMenuItem});
			this.鍵の管理KToolStripMenuItem.Name = "鍵の管理KToolStripMenuItem";
			this.鍵の管理KToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
			this.鍵の管理KToolStripMenuItem.Text = "鍵の管理(&K)";
			// 
			// keyClosetToolStripMenuItem
			// 
			this.keyClosetToolStripMenuItem.Name = "keyClosetToolStripMenuItem";
			this.keyClosetToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.keyClosetToolStripMenuItem.Text = "&Key Closet";
			// 
			// keyBundleClosetToolStripMenuItem
			// 
			this.keyBundleClosetToolStripMenuItem.Name = "keyBundleClosetToolStripMenuItem";
			this.keyBundleClosetToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.keyBundleClosetToolStripMenuItem.Text = "Key &Bundle Closet";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(407, 478);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(102, 42);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(515, 478);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(102, 42);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "キャンセル";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// mainTimer
			// 
			this.mainTimer.Enabled = true;
			this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
			// 
			// SettingWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(633, 532);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimizeBox = false;
			this.Name = "SettingWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WCluster";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingWin_FormClosed);
			this.Load += new System.EventHandler(this.SettingWin_Load);
			this.Shown += new System.EventHandler(this.SettingWin_Shown);
			this.ResizeEnd += new System.EventHandler(this.SettingWin_ResizeEnd);
			this.Move += new System.EventHandler(this.SettingWin_Move);
			this.tabControl1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tp圧縮;
		private System.Windows.Forms.TabPage tp展開;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ToolStripMenuItem アプリAToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 鍵の管理KToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem keyClosetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem keyBundleClosetToolStripMenuItem;
		private System.Windows.Forms.Timer mainTimer;
	}
}
