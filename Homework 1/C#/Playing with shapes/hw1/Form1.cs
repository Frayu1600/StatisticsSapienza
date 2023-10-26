using System.Windows.Forms;

namespace hw1
{
    public partial class Form1 : Form
    {
        Painter p;
        public Form1()
        {
            InitializeComponent();
            InitializeBitmap();
            InitializePainter();

            DrawShapes();
        }

        private void InitializePainter()
        {
            p = new Painter(pictureBox1);
        }

        private void InitializeBitmap()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void DrawShapes()
        {
            p.DrawPoint(Color.Red, 2, 20, 80, 10, 10);
            p.DrawLine(Color.Red, 2, 40, 85, 100, 85);
            p.DrawCircle(Color.Red, 2, 120, 75, 40, 40);
            p.DrawRectangle(Color.Red, 2, 180, 70, 80, 50);
        }
    }
}