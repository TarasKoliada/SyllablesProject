namespace Sklady
{
    partial class CharactersBase
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.btnAddConsonant = new System.Windows.Forms.Button();
            this.btnRemoveConsonant = new System.Windows.Forms.Button();
            this.tbConsValue = new System.Windows.Forms.TextBox();
            this.tbConsPower = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbVowel = new System.Windows.Forms.TextBox();
            this.btnRemoveVowel = new System.Windows.Forms.Button();
            this.btnAddVowel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbChToRemove = new System.Windows.Forms.TextBox();
            this.btnRemoveChr = new System.Windows.Forms.Button();
            this.btnAddChrToRemove = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbCharToSkip = new System.Windows.Forms.TextBox();
            this.btnRemoveCharToSkip = new System.Windows.Forms.Button();
            this.btnAddCharToSkip = new System.Windows.Forms.Button();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 89);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(204, 264);
            this.listBox1.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(231, 89);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(204, 264);
            this.listBox2.TabIndex = 1;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // btnAddConsonant
            // 
            this.btnAddConsonant.Location = new System.Drawing.Point(154, 56);
            this.btnAddConsonant.Name = "btnAddConsonant";
            this.btnAddConsonant.Size = new System.Drawing.Size(27, 25);
            this.btnAddConsonant.TabIndex = 2;
            this.btnAddConsonant.Text = "+";
            this.btnAddConsonant.UseVisualStyleBackColor = true;
            this.btnAddConsonant.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRemoveConsonant
            // 
            this.btnRemoveConsonant.Location = new System.Drawing.Point(188, 56);
            this.btnRemoveConsonant.Name = "btnRemoveConsonant";
            this.btnRemoveConsonant.Size = new System.Drawing.Size(27, 25);
            this.btnRemoveConsonant.TabIndex = 3;
            this.btnRemoveConsonant.Text = "-";
            this.btnRemoveConsonant.UseVisualStyleBackColor = true;
            this.btnRemoveConsonant.Click += new System.EventHandler(this.btnRemoveConsonant_Click);
            // 
            // tbConsValue
            // 
            this.tbConsValue.Location = new System.Drawing.Point(12, 61);
            this.tbConsValue.Name = "tbConsValue";
            this.tbConsValue.Size = new System.Drawing.Size(56, 20);
            this.tbConsValue.TabIndex = 4;
            // 
            // tbConsPower
            // 
            this.tbConsPower.Location = new System.Drawing.Point(80, 61);
            this.tbConsPower.Name = "tbConsPower";
            this.tbConsPower.Size = new System.Drawing.Size(58, 20);
            this.tbConsPower.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(386, 377);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 32);
            this.button3.TabIndex = 6;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Character:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Power:";
            // 
            // tbVowel
            // 
            this.tbVowel.Location = new System.Drawing.Point(231, 61);
            this.tbVowel.Name = "tbVowel";
            this.tbVowel.Size = new System.Drawing.Size(111, 20);
            this.tbVowel.TabIndex = 12;
            // 
            // btnRemoveVowel
            // 
            this.btnRemoveVowel.Location = new System.Drawing.Point(404, 58);
            this.btnRemoveVowel.Name = "btnRemoveVowel";
            this.btnRemoveVowel.Size = new System.Drawing.Size(27, 25);
            this.btnRemoveVowel.TabIndex = 11;
            this.btnRemoveVowel.Text = "-";
            this.btnRemoveVowel.UseVisualStyleBackColor = true;
            this.btnRemoveVowel.Click += new System.EventHandler(this.btnRemoveVowel_Click);
            // 
            // btnAddVowel
            // 
            this.btnAddVowel.Location = new System.Drawing.Point(370, 58);
            this.btnAddVowel.Name = "btnAddVowel";
            this.btnAddVowel.Size = new System.Drawing.Size(27, 25);
            this.btnAddVowel.TabIndex = 10;
            this.btnAddVowel.Text = "+";
            this.btnAddVowel.UseVisualStyleBackColor = true;
            this.btnAddVowel.Click += new System.EventHandler(this.btnAddVowel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Character:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(32, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 31);
            this.label4.TabIndex = 14;
            this.label4.Text = "Consonants";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(283, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 31);
            this.label5.TabIndex = 15;
            this.label5.Text = "Vowels";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(452, 89);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(204, 264);
            this.listBox3.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(446, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 31);
            this.label6.TabIndex = 21;
            this.label6.Text = "Chars to remove";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(449, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Character:";
            // 
            // tbChToRemove
            // 
            this.tbChToRemove.Location = new System.Drawing.Point(452, 65);
            this.tbChToRemove.Name = "tbChToRemove";
            this.tbChToRemove.Size = new System.Drawing.Size(111, 20);
            this.tbChToRemove.TabIndex = 19;
            // 
            // btnRemoveChr
            // 
            this.btnRemoveChr.Location = new System.Drawing.Point(615, 58);
            this.btnRemoveChr.Name = "btnRemoveChr";
            this.btnRemoveChr.Size = new System.Drawing.Size(27, 25);
            this.btnRemoveChr.TabIndex = 18;
            this.btnRemoveChr.Text = "-";
            this.btnRemoveChr.UseVisualStyleBackColor = true;
            this.btnRemoveChr.Click += new System.EventHandler(this.btnRemoveChr_Click);
            // 
            // btnAddChrToRemove
            // 
            this.btnAddChrToRemove.Location = new System.Drawing.Point(582, 58);
            this.btnAddChrToRemove.Name = "btnAddChrToRemove";
            this.btnAddChrToRemove.Size = new System.Drawing.Size(27, 25);
            this.btnAddChrToRemove.TabIndex = 17;
            this.btnAddChrToRemove.Text = "+";
            this.btnAddChrToRemove.UseVisualStyleBackColor = true;
            this.btnAddChrToRemove.Click += new System.EventHandler(this.btnAddChrToRemove_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(682, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 31);
            this.label8.TabIndex = 27;
            this.label8.Text = "Letters to skip";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(672, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Character:";
            // 
            // tbCharToSkip
            // 
            this.tbCharToSkip.Location = new System.Drawing.Point(674, 64);
            this.tbCharToSkip.Name = "tbCharToSkip";
            this.tbCharToSkip.Size = new System.Drawing.Size(111, 20);
            this.tbCharToSkip.TabIndex = 25;
            // 
            // btnRemoveCharToSkip
            // 
            this.btnRemoveCharToSkip.Location = new System.Drawing.Point(846, 58);
            this.btnRemoveCharToSkip.Name = "btnRemoveCharToSkip";
            this.btnRemoveCharToSkip.Size = new System.Drawing.Size(27, 25);
            this.btnRemoveCharToSkip.TabIndex = 24;
            this.btnRemoveCharToSkip.Text = "-";
            this.btnRemoveCharToSkip.UseVisualStyleBackColor = true;
            this.btnRemoveCharToSkip.Click += new System.EventHandler(this.btnRemoveCharToSkip_Click);
            // 
            // btnAddCharToSkip
            // 
            this.btnAddCharToSkip.Location = new System.Drawing.Point(813, 58);
            this.btnAddCharToSkip.Name = "btnAddCharToSkip";
            this.btnAddCharToSkip.Size = new System.Drawing.Size(27, 25);
            this.btnAddCharToSkip.TabIndex = 23;
            this.btnAddCharToSkip.Text = "+";
            this.btnAddCharToSkip.UseVisualStyleBackColor = true;
            this.btnAddCharToSkip.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(674, 89);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(204, 264);
            this.listBox4.TabIndex = 22;
            // 
            // CharactersBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 419);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbCharToSkip);
            this.Controls.Add(this.btnRemoveCharToSkip);
            this.Controls.Add(this.btnAddCharToSkip);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbChToRemove);
            this.Controls.Add(this.btnRemoveChr);
            this.Controls.Add(this.btnAddChrToRemove);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbVowel);
            this.Controls.Add(this.btnRemoveVowel);
            this.Controls.Add(this.btnAddVowel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tbConsPower);
            this.Controls.Add(this.tbConsValue);
            this.Controls.Add(this.btnRemoveConsonant);
            this.Controls.Add(this.btnAddConsonant);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Name = "CharactersBase";
            this.Text = "CharactersBase";
            this.Load += new System.EventHandler(this.CharactersBase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button btnAddConsonant;
        private System.Windows.Forms.Button btnRemoveConsonant;
        private System.Windows.Forms.TextBox tbConsValue;
        private System.Windows.Forms.TextBox tbConsPower;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbVowel;
        private System.Windows.Forms.Button btnRemoveVowel;
        private System.Windows.Forms.Button btnAddVowel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbChToRemove;
        private System.Windows.Forms.Button btnRemoveChr;
        private System.Windows.Forms.Button btnAddChrToRemove;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbCharToSkip;
        private System.Windows.Forms.Button btnRemoveCharToSkip;
        private System.Windows.Forms.Button btnAddCharToSkip;
        private System.Windows.Forms.ListBox listBox4;
    }
}