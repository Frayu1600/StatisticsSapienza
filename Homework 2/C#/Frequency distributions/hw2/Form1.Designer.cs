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
            buttonEval = new Button();
            comboBoxVariable = new ComboBox();
            buttonAddVariable = new Button();
            textBoxSelected = new TextBox();
            label1 = new Label();
            label2 = new Label();
            buttonRemove = new Button();
            label3 = new Label();
            textBoxIntervals = new TextBox();
            dataGridViewResult = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).BeginInit();
            SuspendLayout();
            // 
            // buttonGetTsvFile
            // 
            buttonGetTsvFile.Location = new Point(40, 41);
            buttonGetTsvFile.Margin = new Padding(3, 2, 3, 2);
            buttonGetTsvFile.Name = "buttonGetTsvFile";
            buttonGetTsvFile.Size = new Size(140, 45);
            buttonGetTsvFile.TabIndex = 0;
            buttonGetTsvFile.Text = "Get .tsv file";
            buttonGetTsvFile.UseVisualStyleBackColor = true;
            buttonGetTsvFile.Click += buttonGetTsvFile_Click;
            // 
            // buttonEval
            // 
            buttonEval.Enabled = false;
            buttonEval.Location = new Point(721, 23);
            buttonEval.Margin = new Padding(3, 2, 3, 2);
            buttonEval.Name = "buttonEval";
            buttonEval.Size = new Size(135, 23);
            buttonEval.TabIndex = 3;
            buttonEval.Text = "Eval distribution";
            buttonEval.UseVisualStyleBackColor = true;
            buttonEval.Click += buttonEval_Click;
            // 
            // comboBoxVariable
            // 
            comboBoxVariable.Cursor = Cursors.Hand;
            comboBoxVariable.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxVariable.Enabled = false;
            comboBoxVariable.FormattingEnabled = true;
            comboBoxVariable.Location = new Point(285, 23);
            comboBoxVariable.Margin = new Padding(3, 2, 3, 2);
            comboBoxVariable.Name = "comboBoxVariable";
            comboBoxVariable.Size = new Size(183, 23);
            comboBoxVariable.TabIndex = 2;
            // 
            // buttonAddVariable
            // 
            buttonAddVariable.Location = new Point(474, 23);
            buttonAddVariable.Name = "buttonAddVariable";
            buttonAddVariable.Size = new Size(121, 24);
            buttonAddVariable.TabIndex = 2;
            buttonAddVariable.Text = "Add variable";
            buttonAddVariable.UseVisualStyleBackColor = true;
            buttonAddVariable.Click += buttonAddVariable_Click;
            // 
            // textBoxSelected
            // 
            textBoxSelected.Location = new Point(285, 53);
            textBoxSelected.Name = "textBoxSelected";
            textBoxSelected.ReadOnly = true;
            textBoxSelected.Size = new Size(571, 23);
            textBoxSelected.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(196, 26);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 4;
            label1.Text = "Select variable";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(196, 56);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 5;
            label2.Text = "Selected";
            // 
            // buttonRemove
            // 
            buttonRemove.Enabled = false;
            buttonRemove.Location = new Point(601, 23);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(114, 24);
            buttonRemove.TabIndex = 6;
            buttonRemove.Text = "Remove latest";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(196, 88);
            label3.Name = "label3";
            label3.Size = new Size(85, 15);
            label3.TabIndex = 7;
            label3.Text = "Select intervals";
            // 
            // textBoxIntervals
            // 
            textBoxIntervals.BackColor = SystemColors.Window;
            textBoxIntervals.ForeColor = Color.Gray;
            textBoxIntervals.Location = new Point(285, 85);
            textBoxIntervals.Name = "textBoxIntervals";
            textBoxIntervals.Size = new Size(571, 23);
            textBoxIntervals.TabIndex = 8;
            textBoxIntervals.GotFocus += textBoxIntervals_GotFocus;
            textBoxIntervals.LostFocus += textBoxIntervals_LostFocus;
            // 
            // dataGridViewResult
            // 
            dataGridViewResult.AllowUserToAddRows = false;
            dataGridViewResult.AllowUserToDeleteRows = false;
            dataGridViewResult.AllowUserToOrderColumns = true;
            dataGridViewResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResult.Location = new Point(12, 123);
            dataGridViewResult.Name = "dataGridViewResult";
            dataGridViewResult.ReadOnly = true;
            dataGridViewResult.RowTemplate.Height = 25;
            dataGridViewResult.Size = new Size(844, 335);
            dataGridViewResult.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 470);
            Controls.Add(dataGridViewResult);
            Controls.Add(textBoxIntervals);
            Controls.Add(label3);
            Controls.Add(buttonRemove);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonAddVariable);
            Controls.Add(buttonEval);
            Controls.Add(comboBoxVariable);
            Controls.Add(textBoxSelected);
            Controls.Add(buttonGetTsvFile);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonGetTsvFile;
        private Button buttonEval;
        private ComboBox comboBoxVariable;
        private Button buttonAddVariable;
        private TextBox textBoxSelected;
        private Label label1;
        private Label label2;
        private Button buttonRemove;
        private Label label3;
        private TextBox textBoxIntervals;
        private DataGridView dataGridViewResult;
    }
}