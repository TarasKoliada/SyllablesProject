namespace Sklady
{
    partial class TestView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox2 = new System.Windows.Forms.RichTextBox();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            button1 = new System.Windows.Forms.Button();
            richTextBox3 = new System.Windows.Forms.RichTextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            richTextBox4 = new System.Windows.Forms.RichTextBox();
            label4 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new System.Drawing.Point(655, 65);
            richTextBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new System.Drawing.Size(264, 261);
            richTextBox2.TabIndex = 5;
            richTextBox2.Text = "";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new System.Drawing.Point(29, 65);
            richTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(264, 543);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(25, 618);
            button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(100, 35);
            button1.TabIndex = 3;
            button1.Text = "Go";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox3
            // 
            richTextBox3.Location = new System.Drawing.Point(655, 356);
            richTextBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.Size = new System.Drawing.Size(264, 252);
            richTextBox3.TabIndex = 6;
            richTextBox3.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(29, 40);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(39, 20);
            label1.TabIndex = 7;
            label1.Text = "Text:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(655, 40);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 20);
            label2.TabIndex = 8;
            label2.Text = "Syllables:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(655, 331);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(39, 20);
            label3.TabIndex = 9;
            label3.Text = "CVV:";
            // 
            // richTextBox4
            // 
            richTextBox4.Location = new System.Drawing.Point(343, 65);
            richTextBox4.Name = "richTextBox4";
            richTextBox4.Size = new System.Drawing.Size(264, 543);
            richTextBox4.TabIndex = 10;
            richTextBox4.Text = "";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(343, 42);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(101, 20);
            label4.TabIndex = 11;
            label4.Text = "Transcription: ";
            // 
            // TestView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(label4);
            Controls.Add(richTextBox4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(richTextBox3);
            Controls.Add(richTextBox2);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "TestView";
            Size = new System.Drawing.Size(937, 674);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.Label label4;
    }
}
