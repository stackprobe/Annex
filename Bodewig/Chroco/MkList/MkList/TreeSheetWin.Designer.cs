namespace Charlotte
{
	partial class TreeSheetWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeSheetWin));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.アプリToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.MainSheet = new System.Windows.Forms.DataGridView();
			this.MSMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.選択解除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.South = new System.Windows.Forms.ToolStripStatusLabel();
			this.SouthEast = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainSheet)).BeginInit();
			this.MSMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(684, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// アプリToolStripMenuItem
			// 
			this.アプリToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.閉じるToolStripMenuItem});
			this.アプリToolStripMenuItem.Name = "アプリToolStripMenuItem";
			this.アプリToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.アプリToolStripMenuItem.Text = "アプリ";
			// 
			// 閉じるToolStripMenuItem
			// 
			this.閉じるToolStripMenuItem.Name = "閉じるToolStripMenuItem";
			this.閉じるToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
			this.閉じるToolStripMenuItem.Text = "閉じる";
			this.閉じるToolStripMenuItem.Click += new System.EventHandler(this.閉じるToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.South,
            this.SouthEast});
			this.statusStrip1.Location = new System.Drawing.Point(0, 439);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(684, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
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
			this.MainSheet.Location = new System.Drawing.Point(12, 27);
			this.MainSheet.Name = "MainSheet";
			this.MainSheet.ReadOnly = true;
			this.MainSheet.RowHeadersVisible = false;
			this.MainSheet.RowTemplate.Height = 21;
			this.MainSheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.MainSheet.Size = new System.Drawing.Size(660, 409);
			this.MainSheet.TabIndex = 3;
			this.MainSheet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainSheet_CellClick);
			this.MainSheet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainSheet_CellContentClick);
			this.MainSheet.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MainSheet_ColumnHeaderMouseClick);
			// 
			// MSMenu
			// 
			this.MSMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.選択解除ToolStripMenuItem});
			this.MSMenu.Name = "MSMenu";
			this.MSMenu.Size = new System.Drawing.Size(123, 26);
			// 
			// 選択解除ToolStripMenuItem
			// 
			this.選択解除ToolStripMenuItem.Name = "選択解除ToolStripMenuItem";
			this.選択解除ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.選択解除ToolStripMenuItem.Text = "選択解除";
			this.選択解除ToolStripMenuItem.Click += new System.EventHandler(this.選択解除ToolStripMenuItem_Click);
			// 
			// South
			// 
			this.South.Name = "South";
			this.South.Size = new System.Drawing.Size(610, 17);
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
			// TreeSheetWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 461);
			this.Controls.Add(this.MainSheet);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "TreeSheetWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "MkList - Sheet";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TreeSheetWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TreeSheetWin_FormClosed);
			this.Load += new System.EventHandler(this.TreeSheetWin_Load);
			this.Shown += new System.EventHandler(this.TreeSheetWin_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainSheet)).EndInit();
			this.MSMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.DataGridView MainSheet;
		private System.Windows.Forms.ToolStripMenuItem アプリToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 閉じるToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip MSMenu;
		private System.Windows.Forms.ToolStripMenuItem 選択解除ToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel South;
		private System.Windows.Forms.ToolStripStatusLabel SouthEast;
	}
}
