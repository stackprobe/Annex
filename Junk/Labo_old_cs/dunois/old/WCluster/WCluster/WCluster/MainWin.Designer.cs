namespace WCluster
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
			this.ProgressImg = new System.Windows.Forms.PictureBox();
			this.MainPanel = new System.Windows.Forms.Panel();
			this.Status = new System.Windows.Forms.Label();
			this.Status2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.ProgressImg)).BeginInit();
			this.MainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// ProgressImg
			// 
			this.ProgressImg.Image = global::WCluster.Properties.Resources.Progress;
			this.ProgressImg.Location = new System.Drawing.Point(3, 3);
			this.ProgressImg.Name = "ProgressImg";
			this.ProgressImg.Size = new System.Drawing.Size(129, 125);
			this.ProgressImg.TabIndex = 0;
			this.ProgressImg.TabStop = false;
			// 
			// MainPanel
			// 
			this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainPanel.Controls.Add(this.Status);
			this.MainPanel.Controls.Add(this.Status2);
			this.MainPanel.Controls.Add(this.ProgressImg);
			this.MainPanel.Location = new System.Drawing.Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(260, 238);
			this.MainPanel.TabIndex = 1;
			// 
			// Status
			// 
			this.Status.AutoSize = true;
			this.Status.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Status.Location = new System.Drawing.Point(21, 151);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(55, 13);
			this.Status.TabIndex = 1;
			this.Status.Text = "Ready...";
			this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Status2
			// 
			this.Status2.AutoSize = true;
			this.Status2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Status2.Location = new System.Drawing.Point(21, 177);
			this.Status2.Name = "Status2";
			this.Status2.Size = new System.Drawing.Size(55, 13);
			this.Status2.TabIndex = 2;
			this.Status2.Text = "Ready...";
			this.Status2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.MainPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WCluster";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			((System.ComponentModel.ISupportInitialize)(this.ProgressImg)).EndInit();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.PictureBox ProgressImg;
		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.Label Status;
		private System.Windows.Forms.Label Status2;
	}
}

