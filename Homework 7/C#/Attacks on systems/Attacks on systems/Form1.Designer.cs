namespace Attacks_on_systems
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
            components = new System.ComponentModel.Container();
            pictureBox = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            labelAttacks = new Label();
            buttonSimulate = new Button();
            trackBarP = new TrackBar();
            labelP = new Label();
            buttonPauseResume = new Button();
            buttonReset = new Button();
            label1 = new Label();
            comboBoxSimulation = new ComboBox();
            progressBar = new ProgressBar();
            label2 = new Label();
            numericUpDownSystems = new NumericUpDown();
            numericUpDownAttacks = new NumericUpDown();
            label3 = new Label();
            numericUpDownTimerSpeed = new NumericUpDown();
            label4 = new Label();
            numericUpDownMu = new NumericUpDown();
            numericUpDownSigma = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            numericUpDownStartS = new NumericUpDown();
            label7 = new Label();
            numericUpDownSStep = new NumericUpDown();
            label8 = new Label();
            labelPGamblersRuin = new Label();
            label9 = new Label();
            numericUpDownSLevels = new NumericUpDown();
            numericUpDownP = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSystems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAttacks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTimerSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSigma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStartS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSStep).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSLevels).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownP).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Margin = new Padding(3, 2, 3, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1281, 880);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // timer1
            // 
            timer1.Interval = 250;
            timer1.Tick += timer1_Tick;
            // 
            // labelAttacks
            // 
            labelAttacks.AutoSize = true;
            labelAttacks.BackColor = Color.White;
            labelAttacks.Location = new Point(892, 14);
            labelAttacks.Name = "labelAttacks";
            labelAttacks.Size = new Size(139, 15);
            labelAttacks.TabIndex = 4;
            labelAttacks.Text = "Simulating system # of ? ";
            // 
            // buttonSimulate
            // 
            buttonSimulate.Location = new Point(788, 6);
            buttonSimulate.Name = "buttonSimulate";
            buttonSimulate.Size = new Size(98, 51);
            buttonSimulate.TabIndex = 5;
            buttonSimulate.Text = "Simulate!";
            buttonSimulate.UseVisualStyleBackColor = true;
            buttonSimulate.Click += buttonSimulate_Click;
            // 
            // trackBarP
            // 
            trackBarP.BackColor = Color.White;
            trackBarP.Location = new Point(553, 6);
            trackBarP.Margin = new Padding(3, 2, 3, 2);
            trackBarP.Name = "trackBarP";
            trackBarP.Size = new Size(71, 45);
            trackBarP.TabIndex = 2;
            trackBarP.Value = 5;
            trackBarP.Scroll += trackBarP_Scroll;
            // 
            // labelP
            // 
            labelP.AutoSize = true;
            labelP.BackColor = Color.White;
            labelP.Location = new Point(566, 37);
            labelP.Name = "labelP";
            labelP.Size = new Size(43, 15);
            labelP.TabIndex = 3;
            labelP.Text = "p = 0.5";
            // 
            // buttonPauseResume
            // 
            buttonPauseResume.Enabled = false;
            buttonPauseResume.Location = new Point(1076, 6);
            buttonPauseResume.Name = "buttonPauseResume";
            buttonPauseResume.Size = new Size(95, 52);
            buttonPauseResume.TabIndex = 6;
            buttonPauseResume.Text = "Pause";
            buttonPauseResume.UseVisualStyleBackColor = true;
            buttonPauseResume.Click += buttonPauseResume_Click;
            // 
            // buttonReset
            // 
            buttonReset.Enabled = false;
            buttonReset.Location = new Point(1177, 6);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(95, 52);
            buttonReset.TabIndex = 7;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Location = new Point(32, 11);
            label1.Name = "label1";
            label1.Size = new Size(145, 15);
            label1.TabIndex = 8;
            label1.Text = "Select mode of simulation";
            // 
            // comboBoxSimulation
            // 
            comboBoxSimulation.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSimulation.FormattingEnabled = true;
            comboBoxSimulation.Items.AddRange(new object[] { "Plus and Minus", "Absolute Frequency", "Relative Frequency", "Normalized Frequency", "", "Poisson", "Gambler's Ruin", "", "Standard Brownian Motion", "Geometric Brownian Motion", "General Brownian Motion" });
            comboBoxSimulation.Location = new Point(12, 33);
            comboBoxSimulation.Name = "comboBoxSimulation";
            comboBoxSimulation.Size = new Size(188, 23);
            comboBoxSimulation.TabIndex = 9;
            comboBoxSimulation.SelectedIndexChanged += comboBoxSimulation_SelectedIndexChanged;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(892, 37);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(178, 20);
            progressBar.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Location = new Point(422, 11);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 12;
            label2.Text = "nSystems";
            // 
            // numericUpDownSystems
            // 
            numericUpDownSystems.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownSystems.Location = new Point(485, 6);
            numericUpDownSystems.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericUpDownSystems.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownSystems.Name = "numericUpDownSystems";
            numericUpDownSystems.Size = new Size(62, 23);
            numericUpDownSystems.TabIndex = 14;
            numericUpDownSystems.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // numericUpDownAttacks
            // 
            numericUpDownAttacks.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownAttacks.Location = new Point(485, 34);
            numericUpDownAttacks.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numericUpDownAttacks.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownAttacks.Name = "numericUpDownAttacks";
            numericUpDownAttacks.Size = new Size(62, 23);
            numericUpDownAttacks.TabIndex = 15;
            numericUpDownAttacks.Value = new decimal(new int[] { 3000, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Location = new Point(426, 36);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 16;
            label3.Text = "nAttacks";
            // 
            // numericUpDownTimerSpeed
            // 
            numericUpDownTimerSpeed.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownTimerSpeed.Location = new Point(734, 6);
            numericUpDownTimerSpeed.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDownTimerSpeed.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownTimerSpeed.Name = "numericUpDownTimerSpeed";
            numericUpDownTimerSpeed.Size = new Size(48, 23);
            numericUpDownTimerSpeed.TabIndex = 17;
            numericUpDownTimerSpeed.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownTimerSpeed.ValueChanged += numericUpDownTimerSpeed_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Location = new Point(630, 11);
            label4.Name = "label4";
            label4.Size = new Size(98, 15);
            label4.TabIndex = 18;
            label4.Text = "Timer speed (ms)";
            // 
            // numericUpDownMu
            // 
            numericUpDownMu.DecimalPlaces = 2;
            numericUpDownMu.Enabled = false;
            numericUpDownMu.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDownMu.Location = new Point(650, 34);
            numericUpDownMu.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownMu.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDownMu.Name = "numericUpDownMu";
            numericUpDownMu.Size = new Size(44, 23);
            numericUpDownMu.TabIndex = 19;
            numericUpDownMu.Value = new decimal(new int[] { 5, 0, 0, 131072 });
            // 
            // numericUpDownSigma
            // 
            numericUpDownSigma.DecimalPlaces = 2;
            numericUpDownSigma.Enabled = false;
            numericUpDownSigma.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDownSigma.Location = new Point(734, 34);
            numericUpDownSigma.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownSigma.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDownSigma.Name = "numericUpDownSigma";
            numericUpDownSigma.Size = new Size(48, 23);
            numericUpDownSigma.TabIndex = 20;
            numericUpDownSigma.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Location = new Point(630, 37);
            label5.Name = "label5";
            label5.Size = new Size(14, 15);
            label5.TabIndex = 21;
            label5.Text = "μ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.White;
            label6.Location = new Point(714, 37);
            label6.Name = "label6";
            label6.Size = new Size(14, 15);
            label6.TabIndex = 22;
            label6.Text = "σ";
            // 
            // numericUpDownStartS
            // 
            numericUpDownStartS.Enabled = false;
            numericUpDownStartS.Location = new Point(367, 7);
            numericUpDownStartS.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numericUpDownStartS.Name = "numericUpDownStartS";
            numericUpDownStartS.Size = new Size(49, 23);
            numericUpDownStartS.TabIndex = 23;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(325, 11);
            label7.Name = "label7";
            label7.Size = new Size(36, 15);
            label7.TabIndex = 24;
            label7.Text = "startS";
            // 
            // numericUpDownSStep
            // 
            numericUpDownSStep.Enabled = false;
            numericUpDownSStep.Location = new Point(367, 34);
            numericUpDownSStep.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numericUpDownSStep.Name = "numericUpDownSStep";
            numericUpDownSStep.Size = new Size(49, 23);
            numericUpDownSStep.TabIndex = 25;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(325, 37);
            label8.Name = "label8";
            label8.Size = new Size(35, 15);
            label8.TabIndex = 26;
            label8.Text = "sStep";
            // 
            // labelPGamblersRuin
            // 
            labelPGamblersRuin.AutoSize = true;
            labelPGamblersRuin.Location = new Point(250, 11);
            labelPGamblersRuin.Name = "labelPGamblersRuin";
            labelPGamblersRuin.Size = new Size(14, 15);
            labelPGamblersRuin.TabIndex = 27;
            labelPGamblersRuin.Text = "P";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(220, 36);
            label9.Name = "label9";
            label9.Size = new Size(44, 15);
            label9.TabIndex = 28;
            label9.Text = "sLevels";
            // 
            // numericUpDownSLevels
            // 
            numericUpDownSLevels.Enabled = false;
            numericUpDownSLevels.Location = new Point(270, 34);
            numericUpDownSLevels.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numericUpDownSLevels.Name = "numericUpDownSLevels";
            numericUpDownSLevels.Size = new Size(49, 23);
            numericUpDownSLevels.TabIndex = 29;
            // 
            // numericUpDownP
            // 
            numericUpDownP.Enabled = false;
            numericUpDownP.Location = new Point(270, 7);
            numericUpDownP.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numericUpDownP.Name = "numericUpDownP";
            numericUpDownP.Size = new Size(49, 23);
            numericUpDownP.TabIndex = 30;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1281, 880);
            Controls.Add(numericUpDownP);
            Controls.Add(numericUpDownSLevels);
            Controls.Add(label9);
            Controls.Add(labelPGamblersRuin);
            Controls.Add(label8);
            Controls.Add(numericUpDownSStep);
            Controls.Add(label7);
            Controls.Add(numericUpDownStartS);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(numericUpDownSigma);
            Controls.Add(numericUpDownMu);
            Controls.Add(label4);
            Controls.Add(numericUpDownTimerSpeed);
            Controls.Add(label3);
            Controls.Add(numericUpDownAttacks);
            Controls.Add(numericUpDownSystems);
            Controls.Add(label2);
            Controls.Add(progressBar);
            Controls.Add(comboBoxSimulation);
            Controls.Add(label1);
            Controls.Add(buttonReset);
            Controls.Add(buttonPauseResume);
            Controls.Add(buttonSimulate);
            Controls.Add(labelAttacks);
            Controls.Add(labelP);
            Controls.Add(trackBarP);
            Controls.Add(pictureBox);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarP).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSystems).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAttacks).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTimerSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMu).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSigma).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStartS).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSStep).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSLevels).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownP).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private System.Windows.Forms.Timer timer1;
        private Label labelAttacks;
        private Button buttonSimulate;
        private TrackBar trackBarP;
        private Label labelP;
        private Button buttonPauseResume;
        private Button buttonReset;
        private Label label1;
        private ComboBox comboBoxSimulation;
        private ProgressBar progressBar;
        private Label label2;
        private NumericUpDown numericUpDownSystems;
        private NumericUpDown numericUpDownAttacks;
        private Label label3;
        private NumericUpDown numericUpDownTimerSpeed;
        private Label label4;
        private NumericUpDown numericUpDownMu;
        private NumericUpDown numericUpDownSigma;
        private Label label5;
        private Label label6;
        private NumericUpDown numericUpDownStartS;
        private Label label7;
        private NumericUpDown numericUpDownSStep;
        private Label label8;
        private Label labelPGamblersRuin;
        private Label label9;
        private NumericUpDown numericUpDownSLevels;
        private NumericUpDown numericUpDownP;
    }
}