namespace OEC_UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxMapLinks = new System.Windows.Forms.TextBox();
            this.textBoxMatchLinks = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAPIKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(669, 531);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxMapLinks
            // 
            this.textBoxMapLinks.Location = new System.Drawing.Point(12, 344);
            this.textBoxMapLinks.Multiline = true;
            this.textBoxMapLinks.Name = "textBoxMapLinks";
            this.textBoxMapLinks.Size = new System.Drawing.Size(542, 235);
            this.textBoxMapLinks.TabIndex = 1;
            // 
            // textBoxMatchLinks
            // 
            this.textBoxMatchLinks.Location = new System.Drawing.Point(12, 50);
            this.textBoxMatchLinks.Multiline = true;
            this.textBoxMatchLinks.Name = "textBoxMatchLinks";
            this.textBoxMatchLinks.Size = new System.Drawing.Size(542, 235);
            this.textBoxMatchLinks.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Match Links:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Map Links:";
            // 
            // textBoxAPIKey
            // 
            this.textBoxAPIKey.Location = new System.Drawing.Point(582, 265);
            this.textBoxAPIKey.Name = "textBoxAPIKey";
            this.textBoxAPIKey.Size = new System.Drawing.Size(310, 20);
            this.textBoxAPIKey.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(579, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "API Key:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 591);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxAPIKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxMatchLinks);
            this.Controls.Add(this.textBoxMapLinks);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxMapLinks;
        private System.Windows.Forms.TextBox textBoxMatchLinks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAPIKey;
        private System.Windows.Forms.Label label3;
    }
}

