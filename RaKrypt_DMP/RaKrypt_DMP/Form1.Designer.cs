namespace RaKrypt_DMP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.keyLbl = new System.Windows.Forms.Label();
            this.cyphersComboBox = new System.Windows.Forms.ComboBox();
            this.encryptCheckBox = new System.Windows.Forms.RadioButton();
            this.decryptCheckBox = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.keyLengthLabel = new System.Windows.Forms.Label();
            this.key2TextBox = new System.Windows.Forms.TextBox();
            this.key2Lbl = new System.Windows.Forms.Label();
            this.testModeCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusLbl = new System.Windows.Forms.Label();
            this.encExtensionTextBox = new System.Windows.Forms.TextBox();
            this.decExtensionTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cestaLbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.zmenCestuBtn = new System.Windows.Forms.Button();
            this.customPathCheckBox = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(222, 47);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(59, 20);
            this.keyTextBox.TabIndex = 1;
            this.keyTextBox.TextChanged += new System.EventHandler(this.keyTextBox_TextChanged);
            // 
            // keyLbl
            // 
            this.keyLbl.AutoSize = true;
            this.keyLbl.Location = new System.Drawing.Point(197, 50);
            this.keyLbl.Name = "keyLbl";
            this.keyLbl.Size = new System.Drawing.Size(29, 13);
            this.keyLbl.TabIndex = 2;
            this.keyLbl.Text = "Klíč:";
            // 
            // cyphersComboBox
            // 
            this.cyphersComboBox.FormattingEnabled = true;
            this.cyphersComboBox.Items.AddRange(new object[] {
            "Caesar",
            "Vigenèr",
            "Vigenèr (autokláv)",
            "Vernam",
            "Reverse",
            "Affine"});
            this.cyphersComboBox.Location = new System.Drawing.Point(12, 46);
            this.cyphersComboBox.Name = "cyphersComboBox";
            this.cyphersComboBox.Size = new System.Drawing.Size(179, 21);
            this.cyphersComboBox.TabIndex = 5;
            this.cyphersComboBox.Text = "Prosím vyberte typ šifry";
            this.cyphersComboBox.SelectedIndexChanged += new System.EventHandler(this.cyphersComboBox_SelectedIndexChanged);
            // 
            // encryptCheckBox
            // 
            this.encryptCheckBox.AutoSize = true;
            this.encryptCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.encryptCheckBox.Checked = true;
            this.encryptCheckBox.Location = new System.Drawing.Point(3, 5);
            this.encryptCheckBox.Name = "encryptCheckBox";
            this.encryptCheckBox.Size = new System.Drawing.Size(82, 17);
            this.encryptCheckBox.TabIndex = 6;
            this.encryptCheckBox.TabStop = true;
            this.encryptCheckBox.Text = "Zakryptovat";
            this.encryptCheckBox.UseVisualStyleBackColor = true;
            // 
            // decryptCheckBox
            // 
            this.decryptCheckBox.AutoSize = true;
            this.decryptCheckBox.Location = new System.Drawing.Point(88, 5);
            this.decryptCheckBox.Name = "decryptCheckBox";
            this.decryptCheckBox.Size = new System.Drawing.Size(83, 17);
            this.decryptCheckBox.TabIndex = 7;
            this.decryptCheckBox.TabStop = true;
            this.decryptCheckBox.Text = "Dekryptovat";
            this.decryptCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.decryptCheckBox);
            this.panel1.Controls.Add(this.encryptCheckBox);
            this.panel1.Location = new System.Drawing.Point(55, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(171, 28);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // keyLengthLabel
            // 
            this.keyLengthLabel.AutoSize = true;
            this.keyLengthLabel.Location = new System.Drawing.Point(245, 28);
            this.keyLengthLabel.Name = "keyLengthLabel";
            this.keyLengthLabel.Size = new System.Drawing.Size(0, 13);
            this.keyLengthLabel.TabIndex = 9;
            // 
            // key2TextBox
            // 
            this.key2TextBox.Location = new System.Drawing.Point(222, 67);
            this.key2TextBox.Name = "key2TextBox";
            this.key2TextBox.Size = new System.Drawing.Size(59, 20);
            this.key2TextBox.TabIndex = 10;
            // 
            // key2Lbl
            // 
            this.key2Lbl.AutoSize = true;
            this.key2Lbl.Location = new System.Drawing.Point(197, 67);
            this.key2Lbl.Name = "key2Lbl";
            this.key2Lbl.Size = new System.Drawing.Size(16, 13);
            this.key2Lbl.TabIndex = 11;
            this.key2Lbl.Text = "b:";
            // 
            // testModeCheckBox
            // 
            this.testModeCheckBox.AutoSize = true;
            this.testModeCheckBox.Checked = true;
            this.testModeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.testModeCheckBox.Location = new System.Drawing.Point(12, 419);
            this.testModeCheckBox.Name = "testModeCheckBox";
            this.testModeCheckBox.Size = new System.Drawing.Size(256, 17);
            this.testModeCheckBox.TabIndex = 12;
            this.testModeCheckBox.Text = "Testovací Režim (maximálně 1 soubor najednou)";
            this.testModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 439);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(265, 39);
            this.label2.TabIndex = 13;
            this.label2.Text = "Testovací režim provede zašifrování textu a poté\r\nautomaticky zašifrovaný text de" +
    "šifruje.\r\nVytvořeno hlavně pro efektivitu a komfort při testování";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 363);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 39);
            this.label3.TabIndex = 14;
            this.label3.Text = "Každá šifra má svoje omezení pro způsob zápisu klíče.\r\nPři špatném zápisu a násle" +
    "dném pokusu o šifraci\r\nse tyto požadavky zobrazí v novém okně.\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(12, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(272, 2);
            this.label4.TabIndex = 15;
            this.label4.Text = "label4";
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(2, 2);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(52, 13);
            this.statusLbl.TabIndex = 16;
            this.statusLbl.Text = "Initializing";
            // 
            // encExtensionTextBox
            // 
            this.encExtensionTextBox.Location = new System.Drawing.Point(181, 102);
            this.encExtensionTextBox.Name = "encExtensionTextBox";
            this.encExtensionTextBox.Size = new System.Drawing.Size(100, 20);
            this.encExtensionTextBox.TabIndex = 17;
            this.encExtensionTextBox.Text = "_RaKrypted";
            // 
            // decExtensionTextBox
            // 
            this.decExtensionTextBox.Location = new System.Drawing.Point(181, 128);
            this.decExtensionTextBox.Name = "decExtensionTextBox";
            this.decExtensionTextBox.Size = new System.Drawing.Size(100, 20);
            this.decExtensionTextBox.TabIndex = 18;
            this.decExtensionTextBox.Text = "_DeRaKrypted";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(234, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Cesta, kam budou uloženy zpracované soubory:";
            // 
            // cestaLbl
            // 
            this.cestaLbl.AutoSize = true;
            this.cestaLbl.Location = new System.Drawing.Point(9, 194);
            this.cestaLbl.MaximumSize = new System.Drawing.Size(250, 0);
            this.cestaLbl.Name = "cestaLbl";
            this.cestaLbl.Size = new System.Drawing.Size(167, 13);
            this.cestaLbl.TabIndex = 20;
            this.cestaLbl.Text = "Tam, odkud byly soubory vloženy.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Koncovka zašifrovaného souboru:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Koncovka dešifrovaného souboru:";
            // 
            // zmenCestuBtn
            // 
            this.zmenCestuBtn.Location = new System.Drawing.Point(12, 226);
            this.zmenCestuBtn.Name = "zmenCestuBtn";
            this.zmenCestuBtn.Size = new System.Drawing.Size(269, 23);
            this.zmenCestuBtn.TabIndex = 23;
            this.zmenCestuBtn.Text = "Změnit cestu";
            this.zmenCestuBtn.UseVisualStyleBackColor = true;
            this.zmenCestuBtn.Click += new System.EventHandler(this.zmenCestuBtn_Click);
            // 
            // customPathCheckBox
            // 
            this.customPathCheckBox.AutoSize = true;
            this.customPathCheckBox.Location = new System.Drawing.Point(15, 255);
            this.customPathCheckBox.Name = "customPathCheckBox";
            this.customPathCheckBox.Size = new System.Drawing.Size(203, 17);
            this.customPathCheckBox.TabIndex = 24;
            this.customPathCheckBox.Text = "Chci použít mnou definovanou cestu.";
            this.customPathCheckBox.UseVisualStyleBackColor = true;
            this.customPathCheckBox.CheckedChanged += new System.EventHandler(this.customPathCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 487);
            this.Controls.Add(this.customPathCheckBox);
            this.Controls.Add(this.zmenCestuBtn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cestaLbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.decExtensionTextBox);
            this.Controls.Add(this.encExtensionTextBox);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.testModeCheckBox);
            this.Controls.Add(this.key2Lbl);
            this.Controls.Add(this.key2TextBox);
            this.Controls.Add(this.keyLengthLabel);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.cyphersComboBox);
            this.Controls.Add(this.keyLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "RaKrypt";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.Label keyLbl;
        private System.Windows.Forms.ComboBox cyphersComboBox;
        private System.Windows.Forms.RadioButton encryptCheckBox;
        private System.Windows.Forms.RadioButton decryptCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label keyLengthLabel;
        private System.Windows.Forms.TextBox key2TextBox;
        private System.Windows.Forms.Label key2Lbl;
        private System.Windows.Forms.CheckBox testModeCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.TextBox encExtensionTextBox;
        private System.Windows.Forms.TextBox decExtensionTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label cestaLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button zmenCestuBtn;
        private System.Windows.Forms.CheckBox customPathCheckBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

