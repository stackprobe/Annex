namespace Charlotte
{
	partial class InputTextDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputTextDlg));
			this.入力テキスト = new System.Windows.Forms.TextBox();
			this.読み込みボタン = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// 入力テキスト
			// 
			this.入力テキスト.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.入力テキスト.Location = new System.Drawing.Point(12, 12);
			this.入力テキスト.Multiline = true;
			this.入力テキスト.Name = "入力テキスト";
			this.入力テキスト.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.入力テキスト.Size = new System.Drawing.Size(454, 290);
			this.入力テキスト.TabIndex = 0;
			this.入力テキスト.WordWrap = false;
			this.入力テキスト.TextChanged += new System.EventHandler(this.入力テキスト_TextChanged);
			this.入力テキスト.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.入力テキスト_KeyPress);
			// 
			// 読み込みボタン
			// 
			this.読み込みボタン.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.読み込みボタン.Location = new System.Drawing.Point(12, 308);
			this.読み込みボタン.Name = "読み込みボタン";
			this.読み込みボタン.Size = new System.Drawing.Size(860, 40);
			this.読み込みボタン.TabIndex = 1;
			this.読み込みボタン.Text = "読み込み";
			this.読み込みボタン.UseVisualStyleBackColor = true;
			this.読み込みボタン.Click += new System.EventHandler(this.読み込みボタン_Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBox1.Location = new System.Drawing.Point(472, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(400, 290);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			this.textBox1.WordWrap = false;
			// 
			// InputTextDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 360);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.読み込みボタン);
			this.Controls.Add(this.入力テキスト);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "InputTextDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "InputText";
			this.Load += new System.EventHandler(this.InputText_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox 入力テキスト;
		private System.Windows.Forms.Button 読み込みボタン;
		private System.Windows.Forms.TextBox textBox1;

	}
}
