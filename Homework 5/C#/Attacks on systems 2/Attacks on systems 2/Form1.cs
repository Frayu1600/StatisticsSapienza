using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System;

namespace Attacks_on_systems
{
    public partial class Form1 : Form
    {
        private const int _ATTACKS = 5000;
        private const int _T = _ATTACKS;
        private const int _N = 3000;

        private const int _CHART_HEIGHT = 500;
        private const int _CHART_WIDTH = 1000;
        private const int _CORNER_SIZE = 7;

        private const int _SYSTEMS_COUNT = 3000;

        private int x = 50;
        private int y = 100;

        private int systemsAttacked;

        ResizeableRectangleManager rrm;

        Random r;

        private double lambda;
        public Form1()
        {
            InitializeComponent();
            InitializeBitmap();
            InitializePLabel();
            InitializeRectangles();
            InitializeTimer();

            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
        }

        private void InitializeTimer()
        {
            r = new Random();
            timer1.Interval = 10;
            timer1.Start();
        }

        private void InitializePLabel()
        {
            lambda = trackBarP.Value * ((double)_N / 10);
            labelP.Text = $"λ = {lambda}";
        }

        private void InitializeBitmap()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
        }

        private void InitializeRectangles()
        {
            rrm = new ResizeableRectangleManager(pictureBox, _SYSTEMS_COUNT);

            //rrm.CreateResizeableRectangle(x, y, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.PlusMinus);
            //rrm.CreateResizeableRectangle(x + 550, y, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.Freq);
            //rrm.CreateResizeableRectangle(x, y + 400, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.RelativeFreq);
            //rrm.CreateResizeableRectangle(x + 550, y + 400, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.NormalizedFreq);
            rrm.CreateResizeableRectangle(x, y, _CHART_WIDTH, _CHART_HEIGHT, _T, _CORNER_SIZE, chartType.FreqSubintervals, _N);

            rrm.DrawResizeableRectangles();
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
            lambda = (double)trackBarP.Value * ((double)_N / 10);
            labelP.Text = $"λ = {lambda}";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Color randomColor = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
            rrm.SimulateAttacks(lambda, _ATTACKS, randomColor);

            if (systemsAttacked++ == _SYSTEMS_COUNT) timer1.Stop();

            labelAttacks.Text = $"Simulating system #{systemsAttacked - 1} of {_SYSTEMS_COUNT}";
        }
    }
}