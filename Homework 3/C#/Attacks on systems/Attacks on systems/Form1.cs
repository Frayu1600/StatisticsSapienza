using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Attacks_on_systems
{
    public partial class Form1 : Form
    {
        private const int _ATTACKS = 150;

        private const int _COLUMNS = _ATTACKS;
        private const int _ROWS = _COLUMNS;

        private const int _CHART_HEIGHT = 300;
        private const int _CHART_WIDTH = 500;
        private const int _CORNER_SIZE = 10;

        private int x = 50;
        private int y = 100;

        ResizeableRectangleManager rrm;

        private double p = 0.5;
        public Form1()
        {
            InitializeComponent();
            InitializeBitmap();
            InitializeRectangles();

            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
        }

        private void InitializeBitmap()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
        }

        private void InitializeRectangles()
        {
            rrm = new ResizeableRectangleManager(pictureBox);

            rrm.CreateResizeableRectangle(x, y, _CHART_WIDTH, _CHART_HEIGHT, _ROWS, _COLUMNS, _CORNER_SIZE, chartType.PlusMinus);
            rrm.CreateResizeableRectangle(x + 550, y, _CHART_WIDTH, _CHART_HEIGHT, _ROWS, _COLUMNS, _CORNER_SIZE, chartType.Freq);
            rrm.CreateResizeableRectangle(x, y + 350, _CHART_WIDTH, _CHART_HEIGHT, _ROWS, _COLUMNS, _CORNER_SIZE, chartType.RelativeFreq);
            rrm.CreateResizeableRectangle(x + 550, y + 350, _CHART_WIDTH, _CHART_HEIGHT, _ROWS, _COLUMNS, _CORNER_SIZE, chartType.NormalizedFreq);

            rrm.DrawResizeableRectangles();

            rrm.SimulateAttacks(p, _ATTACKS, Brushes.Red, Pens.Red);

            timer1.Start();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            rrm.ResizeableRectangleManager_MouseUp(sender, e);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            rrm.ResizeableRectangleManager_MouseDown(sender, e);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            rrm.ResizeableRectangleManager_MouseMove(sender, e);
        }

        private void trackBarP_Scroll(object sender, EventArgs e)
        {
            p = (double)trackBarP.Value / 10;
            labelP.Text = $"p = {p}";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            rrm.SimulateAttacks(p, _ATTACKS, Brushes.Red, Pens.Red);
        }
    }
}