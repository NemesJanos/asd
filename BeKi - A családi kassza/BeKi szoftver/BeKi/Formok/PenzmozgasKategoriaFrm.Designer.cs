namespace BeKi
{
    partial class PenzmozgasKategoriaFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PenzmozgasKategoriaFrm));
            this.bevetelRadioBtn = new System.Windows.Forms.RadioButton();
            this.kiadasRadioBtn = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.felvitelBtn = new System.Windows.Forms.Button();
            this.megsemBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ujBtn = new System.Windows.Forms.Button();
            this.modositBtn = new System.Windows.Forms.Button();
            this.torolBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bevetelRadioBtn
            // 
            this.bevetelRadioBtn.AutoSize = true;
            this.bevetelRadioBtn.Location = new System.Drawing.Point(6, 19);
            this.bevetelRadioBtn.Name = "bevetelRadioBtn";
            this.bevetelRadioBtn.Size = new System.Drawing.Size(61, 17);
            this.bevetelRadioBtn.TabIndex = 3;
            this.bevetelRadioBtn.TabStop = true;
            this.bevetelRadioBtn.Text = "Bevétel";
            this.bevetelRadioBtn.UseVisualStyleBackColor = true;
            // 
            // kiadasRadioBtn
            // 
            this.kiadasRadioBtn.AutoSize = true;
            this.kiadasRadioBtn.Location = new System.Drawing.Point(6, 42);
            this.kiadasRadioBtn.Name = "kiadasRadioBtn";
            this.kiadasRadioBtn.Size = new System.Drawing.Size(57, 17);
            this.kiadasRadioBtn.TabIndex = 2;
            this.kiadasRadioBtn.TabStop = true;
            this.kiadasRadioBtn.Text = "Kiadás";
            this.kiadasRadioBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bevetelRadioBtn);
            this.groupBox1.Controls.Add(this.kiadasRadioBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(114, 72);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Típus kiválasztása";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 222;
            this.label1.Text = "Megnevezés";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(206, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 20);
            this.textBox1.TabIndex = 1;
            // 
            // felvitelBtn
            // 
            this.felvitelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.felvitelBtn.Location = new System.Drawing.Point(235, 54);
            this.felvitelBtn.Name = "felvitelBtn";
            this.felvitelBtn.Size = new System.Drawing.Size(90, 30);
            this.felvitelBtn.TabIndex = 3;
            this.felvitelBtn.Text = "Felvitel";
            this.felvitelBtn.UseVisualStyleBackColor = true;
            this.felvitelBtn.Click += new System.EventHandler(this.felvitelBtn_Click);
            // 
            // megsemBtn
            // 
            this.megsemBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.megsemBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.megsemBtn.Location = new System.Drawing.Point(277, 367);
            this.megsemBtn.Name = "megsemBtn";
            this.megsemBtn.Size = new System.Drawing.Size(75, 30);
            this.megsemBtn.TabIndex = 12;
            this.megsemBtn.Text = "Kész";
            this.megsemBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Location = new System.Drawing.Point(13, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 253);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 223;
            this.label3.Text = "Bevétel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 224;
            this.label2.Text = "Kiadás";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(178, 32);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(157, 212);
            this.listBox2.TabIndex = 2;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(7, 33);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(157, 212);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // ujBtn
            // 
            this.ujBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ujBtn.Location = new System.Drawing.Point(13, 367);
            this.ujBtn.Name = "ujBtn";
            this.ujBtn.Size = new System.Drawing.Size(75, 30);
            this.ujBtn.TabIndex = 9;
            this.ujBtn.Text = "Új";
            this.ujBtn.UseVisualStyleBackColor = true;
            this.ujBtn.Click += new System.EventHandler(this.ujBtn_Click);
            // 
            // modositBtn
            // 
            this.modositBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.modositBtn.Location = new System.Drawing.Point(94, 367);
            this.modositBtn.Name = "modositBtn";
            this.modositBtn.Size = new System.Drawing.Size(75, 30);
            this.modositBtn.TabIndex = 10;
            this.modositBtn.Text = "Módosít";
            this.modositBtn.UseVisualStyleBackColor = true;
            this.modositBtn.Click += new System.EventHandler(this.modositBtn_Click);
            // 
            // torolBtn
            // 
            this.torolBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.torolBtn.Location = new System.Drawing.Point(175, 367);
            this.torolBtn.Name = "torolBtn";
            this.torolBtn.Size = new System.Drawing.Size(75, 30);
            this.torolBtn.TabIndex = 11;
            this.torolBtn.Text = "Töröl";
            this.torolBtn.UseVisualStyleBackColor = true;
            this.torolBtn.Click += new System.EventHandler(this.torolBtn_Click);
            // 
            // PenzmozgasKategoriaFrm
            // 
            this.AcceptButton = this.felvitelBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.megsemBtn;
            this.ClientSize = new System.Drawing.Size(366, 412);
            this.Controls.Add(this.torolBtn);
            this.Controls.Add(this.modositBtn);
            this.Controls.Add(this.ujBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.megsemBtn);
            this.Controls.Add(this.felvitelBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PenzmozgasKategoriaFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kategóriák";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton bevetelRadioBtn;
        private System.Windows.Forms.RadioButton kiadasRadioBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button felvitelBtn;
        private System.Windows.Forms.Button megsemBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button ujBtn;
        private System.Windows.Forms.Button modositBtn;
        private System.Windows.Forms.Button torolBtn;
    }
}