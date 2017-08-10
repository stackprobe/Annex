namespace WndTest
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
			this.MainText = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enumWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.keyboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainText
			// 
			this.MainText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainText.Location = new System.Drawing.Point(12, 29);
			this.MainText.Multiline = true;
			this.MainText.Name = "MainText";
			this.MainText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.MainText.Size = new System.Drawing.Size(557, 474);
			this.MainText.TabIndex = 1;
			this.MainText.TextChanged += new System.EventHandler(this.MainText_TextChanged);
			this.MainText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainText_KeyPress);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(581, 26);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mainToolStripMenuItem
			// 
			this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enumWindowsToolStripMenuItem,
            this.keyboardToolStripMenuItem});
			this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
			this.mainToolStripMenuItem.Size = new System.Drawing.Size(47, 22);
			this.mainToolStripMenuItem.Text = "Main";
			// 
			// enumWindowsToolStripMenuItem
			// 
			this.enumWindowsToolStripMenuItem.Name = "enumWindowsToolStripMenuItem";
			this.enumWindowsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.enumWindowsToolStripMenuItem.Text = "EnumWindows";
			this.enumWindowsToolStripMenuItem.Click += new System.EventHandler(this.enumWindowsToolStripMenuItem_Click);
			// 
			// keyboardToolStripMenuItem
			// 
			this.keyboardToolStripMenuItem.Name = "keyboardToolStripMenuItem";
			this.keyboardToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.keyboardToolStripMenuItem.Text = "Keyboard";
			this.keyboardToolStripMenuItem.Click += new System.EventHandler(this.keyboardToolStripMenuItem_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(581, 515);
			this.Controls.Add(this.MainText);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "WndTest";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox MainText;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enumWindowsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem keyboardToolStripMenuItem;

	}
}

