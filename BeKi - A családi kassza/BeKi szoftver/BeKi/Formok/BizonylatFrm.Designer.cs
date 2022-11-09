namespace BeKi
{
    partial class BizonylatFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BizonylatFrm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.torlesBtn = new System.Windows.Forms.Button();
            this.MegsemBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "JPG képfájl|*.jpg|PNG képfájl|*.png|PDF dokumentum|*.pdf";
            // 
            // torlesBtn
            // 
            this.torlesBtn.Location = new System.Drawing.Point(14, 215);
            this.torlesBtn.Name = "torlesBtn";
            this.torlesBtn.Size = new System.Drawing.Size(80, 30);
            this.torlesBtn.TabIndex = 0;
            this.torlesBtn.Text = "Törlés";
            this.torlesBtn.UseVisualStyleBackColor = true;
            this.torlesBtn.Click += new System.EventHandler(this.TorlesBtn_Click);
            // 
            // MegsemBtn
            // 
            this.MegsemBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.MegsemBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MegsemBtn.Location = new System.Drawing.Point(223, 214);
            this.MegsemBtn.Name = "MegsemBtn";
            this.MegsemBtn.Size = new System.Drawing.Size(80, 30);
            this.MegsemBtn.TabIndex = 1;
            this.MegsemBtn.Text = "Kész";
            this.MegsemBtn.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(17, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(286, 160);
            this.listBox1.TabIndex = 2;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 179);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 30);
            this.button3.TabIndex = 3;
            this.button3.Text = "Felvitel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.bizonylatFajlbolBtn);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Kimentés fájlba";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.bizonylatfilebaMenteseBtn);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(99, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "A megtekintéshez kattintson duplán a listaelemre!";
            // 
            // BizonylatFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.MegsemBtn;
            this.ClientSize = new System.Drawing.Size(316, 258);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.MegsemBtn);
            this.Controls.Add(this.torlesBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BizonylatFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bizonylatok";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BizonylatFrm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button torlesBtn;
        private System.Windows.Forms.Button MegsemBtn;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}