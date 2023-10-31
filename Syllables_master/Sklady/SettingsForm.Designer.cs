namespace Sklady
{
    partial class SettingsForm
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
            this.tbSeparator = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbSeparationMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCharactersTable = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbLanguage = new System.Windows.Forms.ComboBox();
            this.cbPhoneticsMode = new System.Windows.Forms.CheckBox();
            this.cbAbsoluteMeasures = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbSeparator
            // 
            this.tbSeparator.Location = new System.Drawing.Point(12, 39);
            this.tbSeparator.Name = "tbSeparator";
            this.tbSeparator.Size = new System.Drawing.Size(155, 20);
            this.tbSeparator.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Separator:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(91, 257);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbSeparationMode
            // 
            this.cbSeparationMode.FormattingEnabled = true;
            this.cbSeparationMode.Items.AddRange(new object[] {
            "c-cc",
            "cc-c"});
            this.cbSeparationMode.Location = new System.Drawing.Point(12, 84);
            this.cbSeparationMode.Name = "cbSeparationMode";
            this.cbSeparationMode.Size = new System.Drawing.Size(155, 21);
            this.cbSeparationMode.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Separation Mode:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Characters Table:";
            // 
            // cbCharactersTable
            // 
            this.cbCharactersTable.FormattingEnabled = true;
            this.cbCharactersTable.Items.AddRange(new object[] {
            "Table 1",
            "Table 2"});
            this.cbCharactersTable.Location = new System.Drawing.Point(12, 132);
            this.cbCharactersTable.Name = "cbCharactersTable";
            this.cbCharactersTable.Size = new System.Drawing.Size(155, 21);
            this.cbCharactersTable.TabIndex = 6;
            this.cbCharactersTable.SelectedIndexChanged += new System.EventHandler(this.cbCharactersTable_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Language:";
            // 
            // cbbLanguage
            // 
            this.cbbLanguage.FormattingEnabled = true;
            this.cbbLanguage.Items.AddRange(new object[] {
            "c-cc",
            "cc-c"});
            this.cbbLanguage.Location = new System.Drawing.Point(10, 180);
            this.cbbLanguage.Name = "cbbLanguage";
            this.cbbLanguage.Size = new System.Drawing.Size(155, 21);
            this.cbbLanguage.TabIndex = 10;
            // 
            // cbPhoneticsMode
            // 
            this.cbPhoneticsMode.AutoSize = true;
            this.cbPhoneticsMode.Checked = true;
            this.cbPhoneticsMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPhoneticsMode.Location = new System.Drawing.Point(11, 212);
            this.cbPhoneticsMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPhoneticsMode.Name = "cbPhoneticsMode";
            this.cbPhoneticsMode.Size = new System.Drawing.Size(102, 17);
            this.cbPhoneticsMode.TabIndex = 12;
            this.cbPhoneticsMode.Text = "Phonetics mode";
            this.cbPhoneticsMode.UseVisualStyleBackColor = true;
            // 
            // cbAbsoluteMeasures
            // 
            this.cbAbsoluteMeasures.AutoSize = true;
            this.cbAbsoluteMeasures.Location = new System.Drawing.Point(11, 234);
            this.cbAbsoluteMeasures.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbAbsoluteMeasures.Name = "cbAbsoluteMeasures";
            this.cbAbsoluteMeasures.Size = new System.Drawing.Size(121, 17);
            this.cbAbsoluteMeasures.TabIndex = 13;
            this.cbAbsoluteMeasures.Text = "Absolute measures?";
            this.cbAbsoluteMeasures.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(179, 287);
            this.Controls.Add(this.cbAbsoluteMeasures);
            this.Controls.Add(this.cbPhoneticsMode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbbLanguage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbCharactersTable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSeparationMode);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSeparator);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSeparator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbSeparationMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCharactersTable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbLanguage;
        private System.Windows.Forms.CheckBox cbPhoneticsMode;
        private System.Windows.Forms.CheckBox cbAbsoluteMeasures;
    }
}