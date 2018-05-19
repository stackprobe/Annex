namespace Charlotte
{
	partial class EncryptionSelectDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncryptionSelectDlg));
			this.btnPlain = new System.Windows.Forms.Button();
			this.btnPassphrase = new System.Windows.Forms.Button();
			this.btnUseKey = new System.Windows.Forms.Button();
			this.btnUseKeyBundle = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnPlain
			// 
			this.btnPlain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPlain.Location = new System.Drawing.Point(6, 26);
			this.btnPlain.Name = "btnPlain";
			this.btnPlain.Size = new System.Drawing.Size(358, 50);
			this.btnPlain.TabIndex = 0;
			this.btnPlain.Text = "暗号化しない";
			this.btnPlain.UseVisualStyleBackColor = true;
			this.btnPlain.Click += new System.EventHandler(this.btnPlain_Click);
			// 
			// btnPassphrase
			// 
			this.btnPassphrase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPassphrase.Location = new System.Drawing.Point(6, 82);
			this.btnPassphrase.Name = "btnPassphrase";
			this.btnPassphrase.Size = new System.Drawing.Size(358, 50);
			this.btnPassphrase.TabIndex = 1;
			this.btnPassphrase.Text = "パスフレーズを入力して暗号化";
			this.btnPassphrase.UseVisualStyleBackColor = true;
			this.btnPassphrase.Click += new System.EventHandler(this.btnPassphrase_Click);
			// 
			// btnUseKey
			// 
			this.btnUseKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUseKey.Location = new System.Drawing.Point(6, 138);
			this.btnUseKey.Name = "btnUseKey";
			this.btnUseKey.Size = new System.Drawing.Size(358, 50);
			this.btnUseKey.TabIndex = 2;
			this.btnUseKey.Text = "Key を使って暗号化";
			this.btnUseKey.UseVisualStyleBackColor = true;
			this.btnUseKey.Click += new System.EventHandler(this.btnUseKey_Click);
			// 
			// btnUseKeyBundle
			// 
			this.btnUseKeyBundle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUseKeyBundle.Location = new System.Drawing.Point(6, 194);
			this.btnUseKeyBundle.Name = "btnUseKeyBundle";
			this.btnUseKeyBundle.Size = new System.Drawing.Size(358, 50);
			this.btnUseKeyBundle.TabIndex = 3;
			this.btnUseKeyBundle.Text = "Key Bundle を使って暗号化";
			this.btnUseKeyBundle.UseVisualStyleBackColor = true;
			this.btnUseKeyBundle.Click += new System.EventHandler(this.btnUseKeyBundle_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.btnPlain);
			this.groupBox1.Controls.Add(this.btnUseKeyBundle);
			this.groupBox1.Controls.Add(this.btnPassphrase);
			this.groupBox1.Controls.Add(this.btnUseKey);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(370, 250);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "暗号化オプション";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(271, 268);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(111, 50);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "キャンセル";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// EncryptionSelectDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 330);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EncryptionSelectDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WCluster / 圧縮 / 暗号化オプション";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EncryptionSelectDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EncryptionSelectDlg_FormClosed);
			this.Load += new System.EventHandler(this.EncryptionSelectDlg_Load);
			this.Shown += new System.EventHandler(this.EncryptionSelectDlg_Shown);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnPlain;
		private System.Windows.Forms.Button btnPassphrase;
		private System.Windows.Forms.Button btnUseKey;
		private System.Windows.Forms.Button btnUseKeyBundle;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnCancel;
	}
}
