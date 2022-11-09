namespace BeKi
{
    partial class SzervizFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SzervizFrm));
            this.okBtn = new System.Windows.Forms.Button();
            this.megsemBtn = new System.Windows.Forms.Button();
            this.autoIndulasCheckBox = new System.Windows.Forms.CheckBox();
            this.talcaertesitesCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.mentesUtvonalValasztoDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.mentesVisszaallitasDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.infoButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(226, 104);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(80, 30);
            this.okBtn.TabIndex = 5;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // megsemBtn
            // 
            this.megsemBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.megsemBtn.Location = new System.Drawing.Point(312, 104);
            this.megsemBtn.Name = "megsemBtn";
            this.megsemBtn.Size = new System.Drawing.Size(80, 30);
            this.megsemBtn.TabIndex = 6;
            this.megsemBtn.Text = "Mégsem";
            this.megsemBtn.UseVisualStyleBackColor = true;
            // 
            // autoIndulasCheckBox
            // 
            this.autoIndulasCheckBox.AutoSize = true;
            this.autoIndulasCheckBox.Location = new System.Drawing.Point(12, 12);
            this.autoIndulasCheckBox.Name = "autoIndulasCheckBox";
            this.autoIndulasCheckBox.Size = new System.Drawing.Size(289, 17);
            this.autoIndulasCheckBox.TabIndex = 1;
            this.autoIndulasCheckBox.Text = "A program automatikusan induljon kis méretben a tálcán";
            this.autoIndulasCheckBox.UseVisualStyleBackColor = true;
            // 
            // talcaertesitesCheckBox
            // 
            this.talcaertesitesCheckBox.AutoSize = true;
            this.talcaertesitesCheckBox.Location = new System.Drawing.Point(12, 35);
            this.talcaertesitesCheckBox.Name = "talcaertesitesCheckBox";
            this.talcaertesitesCheckBox.Size = new System.Drawing.Size(323, 17);
            this.talcaertesitesCheckBox.TabIndex = 2;
            this.talcaertesitesCheckBox.Text = "Tálcaértesítés a nem rendezett, aktuális havi pénzmozgásokról";
            this.talcaertesitesCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 30);
            this.button1.TabIndex = 7;
            this.button1.Text = "Biztonsági mentés készítése";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.biztonsagiMentesBtn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 30);
            this.button2.TabIndex = 8;
            this.button2.Text = "Biztonsági mentés visszaállítása";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.mentesVisszaallitasBtn_Click);
            // 
            // mentesVisszaallitasDialog
            // 
            this.mentesVisszaallitasDialog.Filter = "MSSQL Adatbázis mentés|*.bak";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(410, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 121);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Infó";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // infoButton
            // 
            this.infoButton.Image = global::BeKi.Properties.Resources.piggy_jobbra_atlatszo_info_FELEAKKORA;
            this.infoButton.Location = new System.Drawing.Point(350, 12);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(39, 40);
            this.infoButton.TabIndex = 10;
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler(this.infoButton_Click);
            // 
            // SzervizFrm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.megsemBtn;
            this.ClientSize = new System.Drawing.Size(401, 144);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.talcaertesitesCheckBox);
            this.Controls.Add(this.autoIndulasCheckBox);
            this.Controls.Add(this.megsemBtn);
            this.Controls.Add(this.okBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SzervizFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Szervíz";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button megsemBtn;
        private System.Windows.Forms.CheckBox autoIndulasCheckBox;
        private System.Windows.Forms.CheckBox talcaertesitesCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog mentesUtvonalValasztoDialog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog mentesVisszaallitasDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button infoButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}