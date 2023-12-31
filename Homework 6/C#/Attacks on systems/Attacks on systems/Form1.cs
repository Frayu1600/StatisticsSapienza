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
        private const int _ATTACKS = 100;

        private const int _CHART_HEIGHT = 300;
        private const int _CHART_WIDTH = 500;
        private const int _CORNER_SIZE = 7;

        private const int _SYSTEMS_COUNT = 250;

        private int x = 50;
        private int y = 100;

        private int systemsAttacked = 0;

        ResizeableRectangleManager rrm;

        Random r;

        private double p = 0.5;
        public Form1()
        {
            InitializeComponent();
            InitializeBitmap();
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

        private void InitializeBitmap()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
        }

        private void InitializeRectangles()
        {
            rrm = new ResizeableRectangleManager(pictureBox, _SYSTEMS_COUNT);

            rrm.CreateResizeableRectangle(x, y, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.SandP, 5, 10, 5, 20);
            rrm.CreateResizeableRectangle(x + 550, y, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.SandP, 5, 10, 5, 60);
            rrm.CreateResizeableRectangle(x + 300, y + 400, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.SandP, 5, 10, 5, 80);
            //rrm.CreateResizeableRectangle(x + 550, y, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.Freq);
            //rrm.CreateResizeableRectangle(x, y + 400, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.RelativeFreq);
            //rrm.CreateResizeableRectangle(x + 550, y + 400, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.NormalizedFreq);

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
            p = (double)trackBarP.Value / 10;
            labelP.Text = $"p_penetration = {p}";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Color randomColor = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
            rrm.SimulateAttacks(p, _ATTACKS, randomColor);

            if (systemsAttacked++ == _SYSTEMS_COUNT) timer1.Stop();

            labelAttacks.Text = $"Simulating system #{systemsAttacked - 1} of {_SYSTEMS_COUNT}";
        }
    }
}