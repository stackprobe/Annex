namespace Charlotte
{
	partial class ProgressDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressDlg));
			this.PDMessage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// PDMessage
			// 
			this.PDMessage.AutoSize = true;
			this.PDMessage.ForeColor = System.Drawing.Color.White;
			this.PDMessage.Location = new System.Drawing.Point(12, 9);
			this.PDMessage.Name = "PDMessage";
			this.PDMessage.Size = new System.Drawing.Size(82, 20);
			this.PDMessage.TabIndex = 0;
			this.PDMessage.Text = "PDMessage";
			// 
			// ProgressDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Teal;
			this.ClientSize = new System.Drawing.Size(200, 40);
			this.Controls.Add(this.PDMessage);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "ProgressDlg";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MkList - Progress";
			this.Load += new System.EventHandler(this.ProgressDlg_Load);
			this.Shown += new System.EventHandler(this.ProgressDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label PDMessage;

	}
}
