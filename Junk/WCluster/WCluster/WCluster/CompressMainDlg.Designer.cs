namespace Charlotte
{
	partial class CompressMainDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompressMainDlg));
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
			this.toolTip1.SetToolTip(this.pbDrop, "ここへ圧縮したいフォルダ・ファイルをドラッグ・アンド・ドロップして下さい。\r\nここをクリックして、圧縮したいフォルダ・ファイルを選択して下さい。");
			this.pbDrop.Click += new System.EventHandler(this.pbDrop_Click);
			// 
			// mainTimer
			// 
			this.mainTimer.Enabled = true;
			this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
			// 
			// CompressMainDlg
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(294, 272);
			this.Controls.Add(this.pbDrop);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CompressMainDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "WCluster / 圧縮";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CompressMainDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CompressMainDlg_FormClosed);
			this.Load += new System.EventHandler(this.CompressMainDlg_Load);
			this.Shown += new System.EventHandler(this.CompressMainDlg_Shown);
			this.ResizeEnd += new System.EventHandler(this.CompressMainDlg_ResizeEnd);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CompressMainDlg_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CompressMainDlg_DragEnter);
			this.Move += new System.EventHandler(this.CompressMainDlg_Move);
			((System.ComponentModel.ISupportInitialize)(this.pbDrop)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbDrop;
		private System.Windows.Forms.Timer mainTimer;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
