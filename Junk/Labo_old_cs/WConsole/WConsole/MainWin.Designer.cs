namespace WConsole
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
			this.OutputText = new System.Windows.Forms.TextBox();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.InputText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// OutputText
			// 
			this.OutputText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OutputText.Location = new System.Drawing.Point(12, 12);
			this.OutputText.Multiline = true;
			this.OutputText.Name = "OutputText";
			this.OutputText.ReadOnly = true;
			this.OutputText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.OutputText.Size = new System.Drawing.Size(600, 385);
			this.OutputText.TabIndex = 0;
			this.OutputText.TabStop = false;
			this.OutputText.TextChanged += new System.EventHandler(this.OutputText_TextChanged);
			this.OutputText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OutputText_KeyDown);
			this.OutputText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OutputText_KeyPress);
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// InputText
			// 
			this.InputText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InputText.Location = new System.Drawing.Point(12, 403);
			this.InputText.Name = "InputText";
			this.InputText.Size = new System.Drawing.Size(600, 27);
			this.InputText.TabIndex = 1;
			this.InputText.Text = "echo ここにコマンドを入力+ENTER";
			this.InputText.TextChanged += new System.EventHandler(this.InputText_TextChanged);
			this.InputText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputText_KeyDown);
			this.InputText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputText_KeyPress);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 442);
			this.Controls.Add(this.InputText);
			this.Controls.Add(this.OutputText);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "Console";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox OutputText;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.TextBox InputText;
	}
}

