namespace playing_with_uniform
{
    partial class Form1
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
            textBoxN = new TextBox();
            textBoxK = new TextBox();
            labelN = new Label();
            label1 = new Label();
            buttonCalc = new Button();
            textBoxResult = new TextBox();
            SuspendLayout();
            // 
            // textBoxN
            // 
            textBoxN.Location = new Point(60, 12);
            textBoxN.Name = "textBoxN";
            textBoxN.Size = new Size(125, 27);
            textBoxN.TabIndex = 0;
            // 
            // textBoxK
            // 
            textBoxK.Location = new Point(270, 12);
            textBoxK.Name = "textBoxK";
            textBoxK.Size = new Size(125, 27);
            textBoxK.TabIndex = 1;
            // 
            // labelN
            // 
            labelN.AutoSize = true;
            labelN.Location = new Point(31, 15);
            labelN.Name = "labelN";
            labelN.Size = new Size(23, 20);
            labelN.TabIndex = 2;
            labelN.Text = "N:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(245, 15);
            label1.Name = "label1";
            label1.Size = new Size(19, 20);
            label1.TabIndex = 3;
            label1.Text = "k:";
            // 
            // buttonCalc
            // 
            buttonCalc.Location = new Point(163, 57);
            buttonCalc.Name = "buttonCalc";
            buttonCalc.Size = new Size(94, 29);
            buttonCalc.TabIndex = 4;
            buttonCalc.Text = "Calculate";
            buttonCalc.UseVisualStyleBackColor = true;
            buttonCalc.Click += buttonCalc_Click;
            // 
            // textBoxResult
            // 
            textBoxResult.Location = new Point(19, 101);
            textBoxResult.Multiline = true;
            textBoxResult.Name = "textBoxResult";
            textBoxResult.ReadOnly = true;
            textBoxResult.ScrollBars = ScrollBars.Vertical;
            textBoxResult.Size = new Size(412, 337);
            textBoxResult.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(451, 450);
            Controls.Add(textBoxResult);
            Controls.Add(buttonCalc);
            Controls.Add(label1);
            Controls.Add(labelN);
            Controls.Add(textBoxK);
            Controls.Add(textBoxN);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxN;
        private TextBox textBoxK;
        private Label labelN;
        private Label label1;
        private Button buttonCalc;
        private TextBox textBoxResult;
    }
}