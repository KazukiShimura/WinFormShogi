namespace WinFormShogi
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.turnLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.countLabel = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.playerList = new System.Windows.Forms.ListBox();
            this.comList = new System.Windows.Forms.ListBox();
            this.emptyList = new System.Windows.Forms.ListBox();
            this.playerSubList = new System.Windows.Forms.ListBox();
            this.comSubList = new System.Windows.Forms.ListBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.Location = new System.Drawing.Point(425, 127);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(35, 12);
            this.turnLabel.TabIndex = 0;
            this.turnLabel.Text = "手番：";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(427, 85);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "対局開始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(425, 153);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(35, 12);
            this.countLabel.TabIndex = 2;
            this.countLabel.Text = "手数：";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(427, 30);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(41, 16);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "3分";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // playerList
            // 
            this.playerList.FormattingEnabled = true;
            this.playerList.ItemHeight = 12;
            this.playerList.Location = new System.Drawing.Point(519, 27);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(70, 280);
            this.playerList.TabIndex = 5;
            // 
            // comList
            // 
            this.comList.FormattingEnabled = true;
            this.comList.ItemHeight = 12;
            this.comList.Location = new System.Drawing.Point(595, 27);
            this.comList.Name = "comList";
            this.comList.Size = new System.Drawing.Size(70, 280);
            this.comList.TabIndex = 6;
            // 
            // emptyList
            // 
            this.emptyList.FormattingEnabled = true;
            this.emptyList.ItemHeight = 12;
            this.emptyList.Location = new System.Drawing.Point(671, 27);
            this.emptyList.Name = "emptyList";
            this.emptyList.Size = new System.Drawing.Size(70, 280);
            this.emptyList.TabIndex = 6;
            // 
            // playerSubList
            // 
            this.playerSubList.FormattingEnabled = true;
            this.playerSubList.ItemHeight = 12;
            this.playerSubList.Location = new System.Drawing.Point(747, 27);
            this.playerSubList.Name = "playerSubList";
            this.playerSubList.Size = new System.Drawing.Size(70, 112);
            this.playerSubList.TabIndex = 5;
            // 
            // comSubList
            // 
            this.comSubList.FormattingEnabled = true;
            this.comSubList.ItemHeight = 12;
            this.comSubList.Location = new System.Drawing.Point(747, 145);
            this.comSubList.Name = "comSubList";
            this.comSubList.Size = new System.Drawing.Size(70, 112);
            this.comSubList.TabIndex = 7;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(427, 52);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "10分";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 411);
            this.Controls.Add(this.comSubList);
            this.Controls.Add(this.emptyList);
            this.Controls.Add(this.comList);
            this.Controls.Add(this.playerSubList);
            this.Controls.Add(this.playerList);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.turnLabel);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ListBox playerList;
        private System.Windows.Forms.ListBox comList;
        private System.Windows.Forms.ListBox emptyList;
        private System.Windows.Forms.ListBox playerSubList;
        private System.Windows.Forms.ListBox comSubList;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}
