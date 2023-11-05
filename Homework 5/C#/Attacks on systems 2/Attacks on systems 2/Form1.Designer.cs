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
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1301, 1055);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
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
            timer1.Interval = 250;
            timer1.Tick += timer1_Tick;
            // 
            // labelP
            // 
            labelP.AutoSize = true;
            labelP.Location = new Point(306, 19);
            labelP.Name = "labelP";
            labelP.Size = new Size(78, 20);
            labelP.TabIndex = 3;
            labelP.Text = "lambda = ";
            // 
            // labelAttacks
            // 
            labelAttacks.AutoSize = true;
            labelAttacks.Location = new Point(306, 45);
            labelAttacks.Name = "labelAttacks";
            labelAttacks.Size = new Size(164, 20);
            labelAttacks.TabIndex = 4;
            labelAttacks.Text = "Simulating system # of ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1301, 1055);
            Controls.Add(labelAttacks);
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
        private Label labelAttacks;
    }
}