using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Attacks_on_systems
{
    public partial class Form1 : Form
    {
        private const int _ATTACKS = 30;

        private const int _COLUMNS = _ATTACKS;
        private const int _ROWS = _COLUMNS;

        private const int _CHART_HEIGHT = 300;
        private const int _CHART_WIDTH = 500;
        private const int _CORNER_SIZE = 10;

        private float _UNIT = _CHART_HEIGHT / (float)_ROWS;
        enum chartType { PlusMinus, Freq, RelativeFreq, NormalizedFreq };

        private Bitmap bitmap;

        private Rectangle[] rectangles;

        private Point startPoint;
        private Point previousMouseLocation;
        private bool isResizing;
        private bool isMoving;
        private int rectSelectedIndex;

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
            rectangles[0] = new Rectangle(50, 50, _CHART_WIDTH, _CHART_HEIGHT);
            //rectangles[1] = new Rectangle(600, 50, _CHART_WIDTH, _CHART_HEIGHT);

            DrawRectangles();
        }

        private void DrawRectangles()
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                foreach (Rectangle rect in rectangles)
                {
                    g.FillRectangle(Brushes.WhiteSmoke, rect);

                    float x, y;

                    for (int i = 0; i <= _COLUMNS; i++)
                    {
                        x = rect.Left + i * (rect.Width / (float)_COLUMNS);
                        g.DrawLine(Pens.Gray, x, rect.Top, x, rect.Bottom);

                        g.DrawString(i.ToString(), Font, Brushes.Black, x - 5, rect.Bottom + 5);
                    }

                    for (int i = 0; i <= _ROWS; i++)
                    {
                        y = rect.Bottom - i * (rect.Height / (float)_ROWS);
                        g.DrawLine(Pens.Gray, rect.Right, y, rect.Left, y);
                    }

                    g.DrawRectangle(Pens.Gray, topLeftCornerDrag(rect));
                    g.DrawRectangle(Pens.Gray, topRightCornerDrag(rect));
                    g.DrawRectangle(Pens.Gray, bottomLeftCornerDrag(rect));
                    g.DrawRectangle(Pens.Gray, bottomRightCornerDrag(rect));
                }
            }

            pictureBox.Invalidate();
            timer1.Start();
        }

        private void simulateAttack(chartType ct)
        {
            Random r = new Random();
            double generated;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                foreach (Rectangle rect in rectangles)
                {
                    float x = rect.X;
                    float y;

                    if (ct == chartType.PlusMinus) y = (rect.Bottom + rect.Top) / 2;
                    else y = rect.Bottom;

                    float previousx = x;
                    float previousy = y;

                    Rectangle chartDot = new Rectangle((int)x - 2, (int)y - 2, 5, 5);
                    g.FillRectangle(Brushes.Red, chartDot);
                    g.DrawRectangle(Pens.Red, chartDot);

                    float attacks = 0;
                    // Plot graph
                    for (int i = 1; i <= _COLUMNS; i++)
                    {
                        x = rect.X + i * (rect.Width / (float)_COLUMNS);

                        generated = r.NextDouble();
                        if (generated < p)
                        {
                            switch (ct)
                            {
                                case chartType.PlusMinus: y += _UNIT/2; score--; break;
                                case chartType.Freq: y -= _UNIT; break;
                                case chartType.RelativeFreq: y -= (_UNIT*4 / attacks); break;
                                case chartType.NormalizedFreq: y -= (_UNIT*2.5f / (float)Math.Sqrt(attacks)); break;
                            }
                        }
                        else
                        {
                            switch (ct)
                            {
                                case chartType.PlusMinus: y -= _UNIT / 2; score++; break;
                                case chartType.Freq: y -= 0; break;
                                case chartType.RelativeFreq: y -= 0; break;
                                case chartType.NormalizedFreq: y -= 0; break;
                            }
                        }

                        g.DrawLine(Pens.Red, previousx, previousy, x, y);

                        chartDot = new Rectangle((int)x - 2, (int)y - 2, 5, 5);
                        g.FillRectangle(Brushes.Red, chartDot);
                        g.DrawRectangle(Pens.Red, chartDot);

                        previousx = x;
                        previousy = y;
                        attacks++;
                        pictureBox.Refresh();
                    }

                    // only works for PlusMinus
                    g.DrawString(score.ToString(), Font, Brushes.Black, rect.Right + 5, y - 7);
                    score = 0;
                }
            }
            pictureBox.Refresh();
        }

        private Rectangle topLeftCornerDrag(Rectangle rect)
        {
            return new Rectangle(rect.Left - _CORNER_SIZE, rect.Top - _CORNER_SIZE, _CORNER_SIZE * 2, _CORNER_SIZE * 2);
        }

        private Rectangle topRightCornerDrag(Rectangle rect)
        {
            return new Rectangle(rect.Right - _CORNER_SIZE, rect.Top - _CORNER_SIZE, _CORNER_SIZE * 2, _CORNER_SIZE * 2);
        }

        private Rectangle bottomLeftCornerDrag(Rectangle rect)
        {
            return new Rectangle(rect.Left - _CORNER_SIZE, rect.Bottom - _CORNER_SIZE, _CORNER_SIZE * 2, _CORNER_SIZE * 2);
        }

        private Rectangle bottomRightCornerDrag(Rectangle rect)
        {
            return new Rectangle(rect.Right - _CORNER_SIZE, rect.Bottom - _CORNER_SIZE, _CORNER_SIZE * 2, _CORNER_SIZE * 2);
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            if (IsMouseOnCorner(e.Location))
            {
                startPoint = e.Location;
                isResizing = true;
            }
            else if (rectangles[0].Contains(e.Location))
            {
                startPoint = e.Location;
                isMoving = true;
                previousMouseLocation = e.Location;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                timer1.Stop();
                int newWidth = rectangles[0].Width + e.X - startPoint.X;
                int newHeight = rectangles[0].Height + e.Y - startPoint.Y;
                rectangles[0] = new Rectangle(rectangles[0].X, rectangles[0].Y, newWidth, newHeight);
                startPoint = e.Location;
                DrawRectangles();
            }
            else if (isMoving)
            {
                timer1.Stop();
                int deltaX = e.X - previousMouseLocation.X;
                int deltaY = e.Y - previousMouseLocation.Y;
                rectangles[0] = new Rectangle(rectangles[0].X + deltaX, rectangles[0].Y + deltaY, rectangles[0].Width, rectangles[0].Height);
                previousMouseLocation = e.Location;
                DrawRectangles();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            //_UNIT = (rectangles[0].Top - rectangles[0].Bottom) / (float)_ROWS;
            timer1.Start();
            isResizing = false;
            isMoving = false;
        }

        private bool IsMouseOnCorner(Point mouseLocation)
        {
            Rectangle rect = rectangles[0];

            return topLeftCornerDrag(rect).Contains(mouseLocation) || topRightCornerDrag(rect).Contains(mouseLocation) || bottomLeftCornerDrag(rect).Contains(mouseLocation) || bottomRightCornerDrag(rect).Contains(mouseLocation);
        }

        private void trackBarP_Scroll(object sender, EventArgs e)
        {
            p = (double)trackBarP.Value / 10;
            labelP.Text = $"p = {p}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            simulateAttack(chartType.PlusMinus);
        }
    }
}