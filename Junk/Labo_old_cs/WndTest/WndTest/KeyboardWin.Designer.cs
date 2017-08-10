namespace WndTest
{
	partial class KeyboardWin
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
			this.MainText = new System.Windows.Forms.TextBox();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// MainText
			// 
			this.MainText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainText.Font = new System.Drawing.Font("ＭＳ ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MainText.Location = new System.Drawing.Point(14, 14);
			this.MainText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MainText.Multiline = true;
			this.MainText.Name = "MainText";
			this.MainText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.MainText.Size = new System.Drawing.Size(796, 367);
			this.MainText.TabIndex = 0;
			this.MainText.TextChanged += new System.EventHandler(this.MainText_TextChanged);
			this.MainText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainText_KeyPress);
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Interval = 500;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// KeyboardWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(824, 396);
			this.Controls.Add(this.MainText);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "KeyboardWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "WndTest - Keyboard";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyboardWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeyboardWin_FormClosed);
			this.Load += new System.EventHandler(this.KeyboardWin_Load);
			this.Shown += new System.EventHandler(this.KeyboardWin_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox MainText;
		private System.Windows.Forms.Timer MainTimer;
	}
}
