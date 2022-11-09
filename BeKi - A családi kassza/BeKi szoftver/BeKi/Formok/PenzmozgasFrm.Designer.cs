namespace BeKi
{
    partial class PenzmozgasFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PenzmozgasFrm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rendszeressegGropupBox = new System.Windows.Forms.GroupBox();
            this.nemRendszeresRadio = new System.Windows.Forms.RadioButton();
            this.eventeRadio = new System.Windows.Forms.RadioButton();
            this.feleventeRadio = new System.Windows.Forms.RadioButton();
            this.negyedeventeRadio = new System.Windows.Forms.RadioButton();
            this.havontaRadio = new System.Windows.Forms.RadioButton();
            this.esedekessegMCalendar = new System.Windows.Forms.MonthCalendar();
            this.esedekessegGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tipusGroupBox = new System.Windows.Forms.GroupBox();
            this.bevetelRadio = new System.Windows.Forms.RadioButton();
            this.kiadasRadio = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.teljesitveGroupBox = new System.Windows.Forms.GroupBox();
            this.teljesitesMCalendar = new System.Windows.Forms.MonthCalendar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.esedekessegTeljesitesUA = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.teljesitveCheckBox = new System.Windows.Forms.CheckBox();
            this.rendszeressegGropupBox.SuspendLayout();
            this.esedekessegGroupBox.SuspendLayout();
            this.tipusGroupBox.SuspendLayout();
            this.teljesitveGroupBox.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(427, 290);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OK_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(513, 290);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 30);
            this.button2.TabIndex = 11;
            this.button2.Text = "Mégsem";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(114, 20);
            this.textBox1.TabIndex = 1;
            // 
            // rendszeressegGropupBox
            // 
            this.rendszeressegGropupBox.Controls.Add(this.nemRendszeresRadio);
            this.rendszeressegGropupBox.Controls.Add(this.eventeRadio);
            this.rendszeressegGropupBox.Controls.Add(this.feleventeRadio);
            this.rendszeressegGropupBox.Controls.Add(this.negyedeventeRadio);
            this.rendszeressegGropupBox.Controls.Add(this.havontaRadio);
            this.rendszeressegGropupBox.Location = new System.Drawing.Point(428, 131);
            this.rendszeressegGropupBox.Name = "rendszeressegGropupBox";
            this.rendszeressegGropupBox.Size = new System.Drawing.Size(165, 137);
            this.rendszeressegGropupBox.TabIndex = 9;
            this.rendszeressegGropupBox.TabStop = false;
            this.rendszeressegGropupBox.Text = "Rendszeresség";
            // 
            // nemRendszeresRadio
            // 
            this.nemRendszeresRadio.AutoSize = true;
            this.nemRendszeresRadio.Checked = true;
            this.nemRendszeresRadio.Location = new System.Drawing.Point(6, 19);
            this.nemRendszeresRadio.Name = "nemRendszeresRadio";
            this.nemRendszeresRadio.Size = new System.Drawing.Size(101, 17);
            this.nemRendszeresRadio.TabIndex = 4;
            this.nemRendszeresRadio.TabStop = true;
            this.nemRendszeresRadio.Text = "Nem rendszeres";
            this.nemRendszeresRadio.UseVisualStyleBackColor = true;
            // 
            // eventeRadio
            // 
            this.eventeRadio.AutoSize = true;
            this.eventeRadio.Location = new System.Drawing.Point(6, 109);
            this.eventeRadio.Name = "eventeRadio";
            this.eventeRadio.Size = new System.Drawing.Size(59, 17);
            this.eventeRadio.TabIndex = 3;
            this.eventeRadio.Text = "Évente";
            this.eventeRadio.UseVisualStyleBackColor = true;
            // 
            // feleventeRadio
            // 
            this.feleventeRadio.AutoSize = true;
            this.feleventeRadio.Location = new System.Drawing.Point(6, 86);
            this.feleventeRadio.Name = "feleventeRadio";
            this.feleventeRadio.Size = new System.Drawing.Size(72, 17);
            this.feleventeRadio.TabIndex = 2;
            this.feleventeRadio.Text = "Félévente";
            this.feleventeRadio.UseVisualStyleBackColor = true;
            // 
            // negyedeventeRadio
            // 
            this.negyedeventeRadio.AutoSize = true;
            this.negyedeventeRadio.Location = new System.Drawing.Point(6, 63);
            this.negyedeventeRadio.Name = "negyedeventeRadio";
            this.negyedeventeRadio.Size = new System.Drawing.Size(95, 17);
            this.negyedeventeRadio.TabIndex = 1;
            this.negyedeventeRadio.Text = "Negyedévente";
            this.negyedeventeRadio.UseVisualStyleBackColor = true;
            // 
            // havontaRadio
            // 
            this.havontaRadio.AutoSize = true;
            this.havontaRadio.Location = new System.Drawing.Point(6, 40);
            this.havontaRadio.Name = "havontaRadio";
            this.havontaRadio.Size = new System.Drawing.Size(66, 17);
            this.havontaRadio.TabIndex = 0;
            this.havontaRadio.Text = "Havonta";
            this.havontaRadio.UseVisualStyleBackColor = true;
            // 
            // esedekessegMCalendar
            // 
            this.esedekessegMCalendar.Location = new System.Drawing.Point(8, 19);
            this.esedekessegMCalendar.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.esedekessegMCalendar.MaxSelectionCount = 1;
            this.esedekessegMCalendar.MinDate = new System.DateTime(2019, 1, 1, 0, 0, 0, 0);
            this.esedekessegMCalendar.Name = "esedekessegMCalendar";
            this.esedekessegMCalendar.TabIndex = 8;
            this.esedekessegMCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.MCalendar_DateChanged);
            // 
            // esedekessegGroupBox
            // 
            this.esedekessegGroupBox.Controls.Add(this.esedekessegMCalendar);
            this.esedekessegGroupBox.Location = new System.Drawing.Point(12, 131);
            this.esedekessegGroupBox.Name = "esedekessegGroupBox";
            this.esedekessegGroupBox.Size = new System.Drawing.Size(194, 191);
            this.esedekessegGroupBox.TabIndex = 6;
            this.esedekessegGroupBox.TabStop = false;
            this.esedekessegGroupBox.Text = "Esedékesség (első) napja";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Összeg";
            // 
            // tipusGroupBox
            // 
            this.tipusGroupBox.Controls.Add(this.bevetelRadio);
            this.tipusGroupBox.Controls.Add(this.kiadasRadio);
            this.tipusGroupBox.Location = new System.Drawing.Point(250, 12);
            this.tipusGroupBox.Name = "tipusGroupBox";
            this.tipusGroupBox.Size = new System.Drawing.Size(123, 69);
            this.tipusGroupBox.TabIndex = 4;
            this.tipusGroupBox.TabStop = false;
            this.tipusGroupBox.Text = "Típus";
            // 
            // bevetelRadio
            // 
            this.bevetelRadio.AutoSize = true;
            this.bevetelRadio.Location = new System.Drawing.Point(6, 19);
            this.bevetelRadio.Name = "bevetelRadio";
            this.bevetelRadio.Size = new System.Drawing.Size(61, 17);
            this.bevetelRadio.TabIndex = 1;
            this.bevetelRadio.TabStop = true;
            this.bevetelRadio.Text = "Bevétel";
            this.bevetelRadio.UseVisualStyleBackColor = true;
            this.bevetelRadio.CheckedChanged += new System.EventHandler(this.TipusRadio_CheckedChanged);
            // 
            // kiadasRadio
            // 
            this.kiadasRadio.AutoSize = true;
            this.kiadasRadio.Checked = true;
            this.kiadasRadio.Location = new System.Drawing.Point(6, 42);
            this.kiadasRadio.Name = "kiadasRadio";
            this.kiadasRadio.Size = new System.Drawing.Size(57, 17);
            this.kiadasRadio.TabIndex = 3;
            this.kiadasRadio.TabStop = true;
            this.kiadasRadio.Text = "Kiadás";
            this.kiadasRadio.UseVisualStyleBackColor = true;
            this.kiadasRadio.CheckedChanged += new System.EventHandler(this.TipusRadio_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(153, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(61, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Bevétel";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(57, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Kiadás";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Megnevezés";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.Location = new System.Drawing.Point(75, 46);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 30);
            this.button3.TabIndex = 13;
            this.button3.Text = "Kategóriák";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.kategoriakBtn);
            // 
            // teljesitveGroupBox
            // 
            this.teljesitveGroupBox.Controls.Add(this.teljesitesMCalendar);
            this.teljesitveGroupBox.Location = new System.Drawing.Point(221, 131);
            this.teljesitveGroupBox.Name = "teljesitveGroupBox";
            this.teljesitveGroupBox.Size = new System.Drawing.Size(194, 191);
            this.teljesitveGroupBox.TabIndex = 8;
            this.teljesitveGroupBox.TabStop = false;
            this.teljesitveGroupBox.Text = "Teljesítési dátum";
            // 
            // teljesitesMCalendar
            // 
            this.teljesitesMCalendar.Location = new System.Drawing.Point(8, 19);
            this.teljesitesMCalendar.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.teljesitesMCalendar.MaxSelectionCount = 1;
            this.teljesitesMCalendar.MinDate = new System.DateTime(2019, 1, 1, 0, 0, 0, 0);
            this.teljesitesMCalendar.Name = "teljesitesMCalendar";
            this.teljesitesMCalendar.TabIndex = 8;
            this.teljesitesMCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.MCalendar_DateChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Location = new System.Drawing.Point(428, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(165, 83);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Kategória";
            // 
            // esedekessegTeljesitesUA
            // 
            this.esedekessegTeljesitesUA.AutoSize = true;
            this.esedekessegTeljesitesUA.Location = new System.Drawing.Point(18, 76);
            this.esedekessegTeljesitesUA.Name = "esedekessegTeljesitesUA";
            this.esedekessegTeljesitesUA.Size = new System.Drawing.Size(90, 17);
            this.esedekessegTeljesitesUA.TabIndex = 3;
            this.esedekessegTeljesitesUA.Text = "Csak könyvel";
            this.esedekessegTeljesitesUA.UseVisualStyleBackColor = true;
            this.esedekessegTeljesitesUA.CheckedChanged += new System.EventHandler(this.esedekessegTeljesitesUA_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(92, 50);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(114, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.ThousandsSeparator = true;
            this.numericUpDown1.Enter += new System.EventHandler(this.numericUpDown1_Enter);
            this.numericUpDown1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown1_MouseClick);
            // 
            // teljesitveCheckBox
            // 
            this.teljesitveCheckBox.AutoSize = true;
            this.teljesitveCheckBox.Location = new System.Drawing.Point(221, 108);
            this.teljesitveCheckBox.Name = "teljesitveCheckBox";
            this.teljesitveCheckBox.Size = new System.Drawing.Size(73, 17);
            this.teljesitveCheckBox.TabIndex = 7;
            this.teljesitveCheckBox.Text = "Teljesítve";
            this.teljesitveCheckBox.UseVisualStyleBackColor = true;
            this.teljesitveCheckBox.CheckedChanged += new System.EventHandler(this.teljesitveCheckBox_CheckedChanged);
            // 
            // PenzmozgasFrm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(606, 333);
            this.Controls.Add(this.teljesitveCheckBox);
            this.Controls.Add(this.esedekessegTeljesitesUA);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.teljesitveGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.esedekessegGroupBox);
            this.Controls.Add(this.rendszeressegGropupBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tipusGroupBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PenzmozgasFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pénzmozgás";
            this.rendszeressegGropupBox.ResumeLayout(false);
            this.rendszeressegGropupBox.PerformLayout();
            this.esedekessegGroupBox.ResumeLayout(false);
            this.tipusGroupBox.ResumeLayout(false);
            this.tipusGroupBox.PerformLayout();
            this.teljesitveGroupBox.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox rendszeressegGropupBox;
        private System.Windows.Forms.RadioButton nemRendszeresRadio;
        private System.Windows.Forms.RadioButton eventeRadio;
        private System.Windows.Forms.RadioButton feleventeRadio;
        private System.Windows.Forms.RadioButton negyedeventeRadio;
        private System.Windows.Forms.RadioButton havontaRadio;
        private System.Windows.Forms.MonthCalendar esedekessegMCalendar;
        private System.Windows.Forms.GroupBox esedekessegGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox tipusGroupBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton bevetelRadio;
        private System.Windows.Forms.RadioButton kiadasRadio;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox teljesitveGroupBox;
        private System.Windows.Forms.MonthCalendar teljesitesMCalendar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox esedekessegTeljesitesUA;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox teljesitveCheckBox;
    }
}