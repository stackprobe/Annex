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
			this.MSMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.unselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.MainSheet)).BeginInit();
			this.MSMenu.SuspendLayout();
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
			this.MainSheet.ContextMenuStrip = this.MSMenu;
			this.MainSheet.Location = new System.Drawing.Point(12, 12);
			this.MainSheet.Name = "MainSheet";
			this.MainSheet.ReadOnly = true;
			this.MainSheet.RowHeadersVisible = false;
			this.MainSheet.RowTemplate.Height = 21;
			this.MainSheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.MainSheet.Size = new System.Drawing.Size(502, 689);
			this.MainSheet.TabIndex = 0;
			this.MainSheet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainSheet_CellContentClick);
			// 
			// MSMenu
			// 
			this.MSMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.unselectToolStripMenuItem});
			this.MSMenu.Name = "MSMenu";
			this.MSMenu.Size = new System.Drawing.Size(153, 70);
			// 
			// selectToolStripMenuItem
			// 
			this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
			this.selectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.selectToolStripMenuItem.Text = "Select";
			this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
			// 
			// unselectToolStripMenuItem
			// 
			this.unselectToolStripMenuItem.Name = "unselectToolStripMenuItem";
			this.unselectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.unselectToolStripMenuItem.Text = "Unselect";
			this.unselectToolStripMenuItem.Click += new System.EventHandler(this.unselectToolStripMenuItem_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526, 713);
			this.Controls.Add(this.MainSheet);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "t0001";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			((System.ComponentModel.ISupportInitialize)(this.MainSheet)).EndInit();
			this.MSMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.DataGridView MainSheet;
		private System.Windows.Forms.ContextMenuStrip MSMenu;
		private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem unselectToolStripMenuItem;
	}
}

