namespace ZipDetails
{
    partial class ZipDetailsForm
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.SelectFİleButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ShowContentCbx = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SelectFİleButton
            // 
            this.SelectFİleButton.Location = new System.Drawing.Point(7, 8);
            this.SelectFİleButton.Margin = new System.Windows.Forms.Padding(4);
            this.SelectFİleButton.Name = "SelectFİleButton";
            this.SelectFİleButton.Size = new System.Drawing.Size(155, 38);
            this.SelectFİleButton.TabIndex = 0;
            this.SelectFİleButton.Text = "Select Zip File";
            this.SelectFİleButton.UseVisualStyleBackColor = true;
            this.SelectFİleButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(174, 8);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(638, 59);
            this.textBox1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(7, 74);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(805, 465);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // ShowContentCbx
            // 
            this.ShowContentCbx.AutoSize = true;
            this.ShowContentCbx.Location = new System.Drawing.Point(12, 46);
            this.ShowContentCbx.Name = "ShowContentCbx";
            this.ShowContentCbx.Size = new System.Drawing.Size(115, 22);
            this.ShowContentCbx.TabIndex = 4;
            this.ShowContentCbx.Text = "İçeriği Göster";
            this.ShowContentCbx.UseVisualStyleBackColor = true;
            // 
            // ZipDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 543);
            this.Controls.Add(this.ShowContentCbx);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SelectFİleButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ZipDetailsForm";
            this.Text = "ZipDetails";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectFİleButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox ShowContentCbx;
    }
}

