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
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarP).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Bottom;
            pictureBox.Location = new Point(0, 67);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1301, 459);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // trackBarP
            // 
            trackBarP.Location = new Point(123, 5);
            trackBarP.Name = "trackBarP";
            trackBarP.Size = new Size(166, 56);
            trackBarP.TabIndex = 2;
            trackBarP.Value = 5;
            trackBarP.Scroll += trackBarP_Scroll;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // labelP
            // 
            labelP.AutoSize = true;
            labelP.Location = new Point(306, 18);
            labelP.Name = "labelP";
            labelP.Size = new Size(55, 20);
            labelP.TabIndex = 3;
            labelP.Text = "p = 0.5";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1301, 526);
            Controls.Add(labelP);
            Controls.Add(trackBarP);
            Controls.Add(pictureBox);
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
    }
}