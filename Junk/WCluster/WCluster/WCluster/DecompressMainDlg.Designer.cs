namespace Charlotte
{
	partial class DecompressMainDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecompressMainDlg));
			this.pbDrop = new System.Windows.Forms.PictureBox();
			this.mainTimer = new System.Windows.Forms.Timer(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pbDrop)).BeginInit();
			this.SuspendLayout();
			// 
			// pbDrop
			// 
			this.pbDrop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbDrop.Location = new System.Drawing.Point(0, 0);
			this.pbDrop.Name = "pbDrop";
			this.pbDrop.Size = new System.Drawing.Size(294, 272);
			this.pbDrop.TabIndex = 0;
			this.pbDrop.TabStop = false;
			this.toolTip1.SetToolTip(this.pbDrop, "ここへアーカイブファイルをドラッグ・アンド・ドロップして下さい。\r\nここをクリックして、アーカイブファイルを選択して下さい。");
			this.pbDrop.Click += new System.EventHandler(this.pbDrop_Click);
			// 
			// mainTimer
			// 
			this.mainTimer.Enabled = true;
			this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
			// 
			// DecompressMainDlg
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(294, 272);
			this.Controls.Add(this.pbDrop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DecompressMainDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "WCluster / 展開";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DecompressMainDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DecompressMainDlg_FormClosed);
			this.Load += new System.EventHandler(this.DecompressMainDlg_Load);
			this.Shown += new System.EventHandler(this.DecompressMainDlg_Shown);
			this.ResizeEnd += new System.EventHandler(this.DecompressMainDlg_ResizeEnd);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DecompressMainDlg_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DecompressMainDlg_DragEnter);
			this.Move += new System.EventHandler(this.DecompressMainDlg_Move);
			((System.ComponentModel.ISupportInitialize)(this.pbDrop)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbDrop;
		private System.Windows.Forms.Timer mainTimer;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
