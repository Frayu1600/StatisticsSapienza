namespace hw2
{
    partial class Form1 : Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonGetTsvFile = new Button();
            textBox1 = new TextBox();
            comboBoxUnivaried = new ComboBox();
            buttonuUnivaried = new Button();
            groupBox1 = new GroupBox();
            buttonMultivaried = new Button();
            groupBox2 = new GroupBox();
            textBoxMultivaried = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonGetTsvFile
            // 
            buttonGetTsvFile.Location = new Point(57, 25);
            buttonGetTsvFile.Name = "buttonGetTsvFile";
            buttonGetTsvFile.Size = new Size(94, 29);
            buttonGetTsvFile.TabIndex = 0;
            buttonGetTsvFile.Text = "Get .tsv file";
            buttonGetTsvFile.UseVisualStyleBackColor = true;
            buttonGetTsvFile.Click += buttonGetTsvFile_Click;
            // 
            // textBox1
            // 
            textBox1.AcceptsReturn = true;
            textBox1.Location = new Point(57, 176);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(722, 248);
            textBox1.TabIndex = 1;
            // 
            // comboBoxUnivaried
            // 
            comboBoxUnivaried.Enabled = false;
            comboBoxUnivaried.FormattingEnabled = true;
            comboBoxUnivaried.Location = new Point(11, 26);
            comboBoxUnivaried.Name = "comboBoxUnivaried";
            comboBoxUnivaried.Size = new Size(151, 28);
            comboBoxUnivaried.TabIndex = 2;
            // 
            // buttonuUnivaried
            // 
            buttonuUnivaried.Enabled = false;
            buttonuUnivaried.Location = new Point(574, 34);
            buttonuUnivaried.Name = "buttonuUnivaried";
            buttonuUnivaried.Size = new Size(193, 29);
            buttonuUnivaried.TabIndex = 3;
            buttonuUnivaried.Text = "Get variable distribution";
            buttonuUnivaried.UseVisualStyleBackColor = true;
            buttonuUnivaried.Click += buttonUnivaried_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBoxUnivaried);
            groupBox1.Location = new Point(399, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(380, 70);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Univaried distribution";
            // 
            // buttonMultivaried
            // 
            buttonMultivaried.Enabled = false;
            buttonMultivaried.Location = new Point(574, 110);
            buttonMultivaried.Name = "buttonMultivaried";
            buttonMultivaried.Size = new Size(193, 29);
            buttonMultivaried.TabIndex = 5;
            buttonMultivaried.Text = "Get variable distribution";
            buttonMultivaried.UseVisualStyleBackColor = true;
            buttonMultivaried.Click += buttonMultivaried_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxMultivaried);
            groupBox2.Location = new Point(399, 84);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(380, 70);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Multivaried distribution";
            // 
            // textBoxMultivaried
            // 
            textBoxMultivaried.Location = new Point(11, 28);
            textBoxMultivaried.Name = "textBoxMultivaried";
            textBoxMultivaried.Size = new Size(151, 27);
            textBoxMultivaried.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonMultivaried);
            Controls.Add(groupBox2);
            Controls.Add(buttonuUnivaried);
            Controls.Add(textBox1);
            Controls.Add(buttonGetTsvFile);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonGetTsvFile;
        private TextBox textBox1;
        private ComboBox comboBoxUnivaried;
        private Button buttonuUnivaried;
        private GroupBox groupBox1;
        private Button buttonMultivaried;
        private GroupBox groupBox2;
        private TextBox textBoxMultivaried;
    }
}