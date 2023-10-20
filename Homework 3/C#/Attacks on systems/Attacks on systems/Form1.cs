using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Attacks_on_systems
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Rectangle[] rectangles;
        private Rectangle chartGraph1;
        private Rectangle chartGraph2; 
        private int selectedRectangleIndex = -1;
        private Point lastMousePos;

        private const int _ROWS = 20;
        private const int _COLUMNS = 20;

        private const int _CHART_HEIGHT = 200;
        private const int _CHART_WIDTH = 400;

        enum chartType { a, b, c }

        private float _UNIT = 0;

        private int score = 0;
        private double p = 0.5;
        public Form1()
        {
            InitializeComponent();
            InitializeBitmap();
            InitializeRectangles();
        }

        private void InitializeBitmap()
        {
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bitmap;
        }

        private void InitializeRectangles()
        {
            rectangles = new Rectangle[4];
            
            chartGraph1 = new Rectangle(50, 50, _CHART_WIDTH, _CHART_HEIGHT);
            chartGraph2 = new Rectangle(600, 50, _CHART_WIDTH, _CHART_HEIGHT);
            rectangles[0] = chartGraph1;
            rectangles[1] = chartGraph2;    

            _UNIT = _CHART_HEIGHT / (float)_ROWS / 2;

            DrawRectangles();
        }

        private void DrawRectangles()
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                
                foreach(Rectangle rect in rectangles)
                {
                    g.FillRectangle(Brushes.WhiteSmoke, rect);

                    float x, y;

                    for (int i = 0; i <= _COLUMNS; i++)
                    {
                        x = rect.Left + i * (rect.Width / (float)_COLUMNS);
                        g.DrawLine(Pens.Gray, x, rect.Top, x, rect.Bottom);
                    }

                    for (int i = 0; i <= _ROWS; i++)
                    {
                        y = rect.Bottom - i * (rect.Height / (float)_ROWS);
                        g.DrawLine(Pens.Gray, rect.Right, y, rect.Left, y);
                    }
                }
            }

            pictureBox.Invalidate();
            timer1.Start();
        }


        private void simulateAttack()
        {
            Random r = new Random();
            double generated;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                foreach (Rectangle rect in rectangles)
                {
                    float x = rect.Left;
                    float y = rect.Bottom / 2;

                    float previousx = x;
                    float previousy = y;
                    // Plot graph
                    for (int i = 1; i <= _COLUMNS; i++)
                    {
                        x = rect.Left + i * (rect.Width / (float)_COLUMNS);

                        generated = r.NextDouble();

                        if (generated < p) y += _UNIT;
                        else y -= _UNIT;

                        g.DrawLine(Pens.Red, previousx, previousy, x, y);
                        previousx = x;
                        previousy = y;
                    }
                }
            }
            pictureBox.Invalidate();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < rectangles.Length; i++)
            {
                if (rectangles[i].Contains(e.Location))
                {
                    selectedRectangleIndex = i;
                    lastMousePos = e.Location;
                    break;
                }
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedRectangleIndex >= 0)
            {
                timer1.Stop();
                Rectangle rect = rectangles[selectedRectangleIndex];
                int dx = e.X - lastMousePos.X;
                int dy = e.Y - lastMousePos.Y;
                rect.X += dx;
                rect.Y += dy;
                rectangles[selectedRectangleIndex] = rect;
                lastMousePos = e.Location;
                DrawRectangles();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            selectedRectangleIndex = -1;
            timer1.Start();
        }

        private void trackBarP_Scroll(object sender, EventArgs e)
        {
            p = (double)trackBarP.Value / 10;
            labelP.Text = $"p = {p}"; 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            simulateAttack();
        }
    }
}