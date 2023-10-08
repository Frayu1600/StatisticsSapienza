namespace hw1
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

        private void DrawShapes(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen redPen = new Pen(Color.Red, 2);
            Brush redBrush = new SolidBrush(Color.Red);

            // Draw a point
            g.FillEllipse(redBrush, 20, 80, 10, 10);
            g.DrawEllipse(redPen, 20, 80, 10, 10);

            // Draw line
            g.DrawLine(redPen, 40, 85, 100, 85);

            // Draw circle
            g.FillEllipse(redBrush, 120, 75, 40, 40);
            g.DrawEllipse(redPen, 120, 75, 40, 40);

            // Draw rectangle
            g.FillRectangle(redBrush, 180, 70, 80, 50);
            g.DrawRectangle(redPen, 180, 70, 80, 50);


            redPen.Dispose();
            redBrush.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = "Fun Shapes";
            this.Size = new Size(400, 200);
            CenterToScreen();
            this.Paint += DrawShapes;
        }

        #endregion
    }
}