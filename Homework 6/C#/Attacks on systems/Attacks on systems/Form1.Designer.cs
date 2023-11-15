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
            trackBarP = new TrackBar();
            timer1 = new System.Windows.Forms.Timer(components);
            labelP = new Label();
            labelAttacks = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarP).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Margin = new Padding(3, 2, 3, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1138, 880);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // trackBarP
            // 
            trackBarP.Location = new Point(108, 4);
            trackBarP.Margin = new Padding(3, 2, 3, 2);
            trackBarP.Name = "trackBarP";
            trackBarP.Size = new Size(145, 45);
            trackBarP.TabIndex = 2;
            trackBarP.Value = 5;
            trackBarP.Scroll += trackBarP_Scroll;
            // 
            // timer1
            // 
            timer1.Interval = 250;
            timer1.Tick += timer1_Tick;
            // 
            // labelP
            // 
            labelP.AutoSize = true;
            labelP.Location = new Point(268, 14);
            labelP.Name = "labelP";
            labelP.Size = new Size(109, 15);
            labelP.TabIndex = 3;
            labelP.Text = "p_penetration = 0.5";
            // 
            // labelAttacks
            // 
            labelAttacks.AutoSize = true;
            labelAttacks.Location = new Point(268, 34);
            labelAttacks.Name = "labelAttacks";
            labelAttacks.Size = new Size(131, 15);
            labelAttacks.TabIndex = 4;
            labelAttacks.Text = "Simulating system # of ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1138, 880);
            Controls.Add(labelAttacks);
            Controls.Add(labelP);
            Controls.Add(trackBarP);
            Controls.Add(pictureBox);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarP).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private TrackBar trackBarP;
        private System.Windows.Forms.Timer timer1;
        private Label labelP;
        private Label labelAttacks;
    }
}