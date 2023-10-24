using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Attacks_on_systems
{
    public enum chartType { PlusMinus, Freq, RelativeFreq, NormalizedFreq };
    internal class ResizeableRectangle : PictureBox
    {
        private Rectangle rect;

        private PictureBox pictureBox;

        public float yStep;
        public float xStep;

        public readonly chartType ct;

        public readonly int _ROWS;
        public readonly int _COLUMNS;
        public readonly int _CORNER_SIZE;
        private float score;
        private int attacks;

        private List<float> results;

        private float x;
        private float y;
        private float previousx;
        private float previousy;

        private bool isResizing;
        private bool isMoving;
        private Point startPoint;

        private Point previousMouseLocation;

        public ResizeableRectangle(int x, int y, int width, int height, int rows, int columns, int cornerSize, chartType ct, PictureBox pictureBox)
        {
            this.rect = new Rectangle(x, y, width, height);
            this.ct = ct;
            this.pictureBox = pictureBox;

            this._ROWS = rows;
            this._COLUMNS = columns;
            this._CORNER_SIZE = cornerSize;

            this.yStep = height / (float)_ROWS;
            this.xStep = width / (float)_COLUMNS;

            this.score = 0;
            this.attacks = 0;

            this.isResizing = false;
            this.isMoving = false;

            this.x = rect.X;
            this.y = (ct == chartType.PlusMinus) ? HalfwayYPoint : rect.Bottom;

            this.results = new List<float>();

            this.previousx = x;
            this.previousy = y;
        }

        public void ResizeableRectangle_MouseUp(object? sender, MouseEventArgs e)
        {
            isResizing = false;
            isMoving = false;
        }

        public void ResizeableRectangle_MouseDown(object? sender, MouseEventArgs e)
        {
            if (IsMouseOnCorner(e.Location))
            {
                startPoint = e.Location;
                isResizing = true;
            }
            else if (rect.Contains(e.Location))
            {
                startPoint = e.Location;
                isMoving = true;
                previousMouseLocation = e.Location;
            }
        }

        public void ResizeableRectangle_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                int newWidth = rect.Width + e.X - startPoint.X;
                int newHeight = rect.Height + e.Y - startPoint.Y;
                float newYStep = newHeight / _ROWS;
                float newXStep = newWidth / _COLUMNS;
                startPoint = e.Location;

                UpdatePosition(rect.X, rect.Y, newWidth, newHeight, newYStep, newXStep);
            }
            else if (isMoving)
            {
                int deltaX = e.X - previousMouseLocation.X;
                int deltaY = e.Y - previousMouseLocation.Y;
                previousMouseLocation = e.Location;

                UpdatePosition(rect.X + deltaX, rect.Y + deltaY, rect.Width, rect.Height, yStep, xStep);
            }
        }

        public bool IsMouseOnCorner(Point mouseLocation)
        {
            return topLeftCorner.Contains(mouseLocation) || topRightCorner.Contains(mouseLocation) || bottomLeftCorner.Contains(mouseLocation) || bottomRightCorner.Contains(mouseLocation);
        }

        public void DrawChartOnBitmap()
        {
            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {

                g.FillRectangle(Brushes.Transparent, rect);

                float x, y;

                for (int i = 0; i <= _COLUMNS; i++)
                {
                    x = rect.Left + i * xStep;
                    g.DrawLine(Pens.LightGray, x, rect.Top, x, rect.Bottom);

                    g.DrawString(i.ToString(), Control.DefaultFont, Brushes.Black, x - 5, rect.Bottom + 5);
                }

                for (int i = 0; i <= _ROWS*2; i++)
                {
                    y = rect.Bottom - i * yStep/2;
                    g.DrawLine(Pens.LightGray, rect.Right, y, rect.Left, y);
                }

                if (ct == chartType.PlusMinus) g.DrawLine(Pens.Red, rect.Left, HalfwayYPoint, rect.Right, HalfwayYPoint);

                g.DrawRectangle(Pens.Gray, this.topLeftCorner);
                g.DrawRectangle(Pens.Gray, this.topRightCorner);
                g.DrawRectangle(Pens.Gray, this.bottomLeftCorner);
                g.DrawRectangle(Pens.Gray, this.bottomRightCorner);

                /*
                g.DrawString("Attacks",
                    Control.DefaultFont,
                    Brushes.Black,
                    this.HalfwayXPoint - (xStep / 2),
                    rect.Bottom + (yStep / 1.5f)
                );

                */
                g.DrawString(ct.ToString(),
                    Control.DefaultFont,
                    Brushes.Black,
                    this.HalfwayXPoint - (xStep / 2),
                    rect.Top - (yStep / 1.5f)
                 );
               
            }
            pictureBox.Invalidate();
        }

        public void ReSimulateAttacks(List<bool> attacks, Brush brush, Pen pen, int _SYSTEMS_COUNT)
        {
            this.x = rect.X;
            this.y = (ct == chartType.PlusMinus) ? HalfwayYPoint : rect.Bottom;

            this.attacks = 0;
            this.score = 0;

            previousx = x;
            previousy = y;

            foreach (bool attack in attacks)
                SimulateAttack(attack, brush, pen, _SYSTEMS_COUNT);
        }

        public void CreateHistogram(int _SYSTEMS_COUNT)
        {
            if (results.Count <= _SYSTEMS_COUNT) return;

            float minScore = results.AsQueryable().Min();
            float maxScore = results.AsQueryable().Max();

            int boxes = Math.Max(5, results.Count/10);

            float rect_width = rect.Width / boxes;

            int[] intervals = new int[boxes];
            float intervalWidth = (maxScore - minScore) / intervals.Length;
            for (int i = 0; i < results.Count; i++)
            {
                double intervalIndex = Math.Floor((results[i] - minScore) / intervalWidth);
                if (intervalIndex >= intervals.Length)
                {
                    intervals[intervals.Length - 1]++;
                }
                else
                {
                    intervals[(int)intervalIndex]++;
                }
            }

            float basicx;
            for (int i = 0; i < intervals.Length; i++)
            {
                int basicy = rect.Height / Math.Abs(intervals.AsQueryable().Max());
                basicx = rect.X + i * rect_width;
                Rectangle histogramRect = new Rectangle((int)basicx, rect.Bottom - intervals[i] * basicy, (int)rect_width, intervals[i] * basicy);

                using (Graphics g = Graphics.FromImage(pictureBox.Image))
                {
                    Color semiTransparentColor = Color.FromArgb(128, Color.Red);
                    Brush semiTransparentBrush = new SolidBrush(semiTransparentColor);

                    g.FillRectangle(semiTransparentBrush, histogramRect);

                    semiTransparentBrush.Dispose();
                }
            }

            pictureBox.Invalidate();
        }

        // returns the score at every step
        public void SimulateAttack(bool penetrated, Brush brush, Pen pen, int _SYSTEMS_COUNT)
        {
            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                Rectangle chartDot = new Rectangle((int)x - 1, (int)y - 1, 2, 2);
                g.FillRectangle(brush, chartDot);
                g.DrawRectangle(pen, chartDot);

                x = rect.X + ++attacks * (rect.Width / (float)_COLUMNS);

                if (!penetrated)
                {
                    switch (ct)
                    {
                        case chartType.PlusMinus: y -= yStep / 2; score++; break;
                        case chartType.Freq: y -= yStep; score++; break;
                        case chartType.RelativeFreq: y -= (yStep * 4 / attacks); score = (++score / (int)attacks); break;
                        case chartType.NormalizedFreq: y -= (yStep * 2.5f / (float)Math.Sqrt(attacks)); score = (++score / (int)Math.Sqrt(attacks)); break;
                    }
                }
                else
                {
                    switch (ct)
                    {
                        case chartType.PlusMinus: y += yStep / 2; score--; break;
                        case chartType.Freq: y += 0; break;
                        case chartType.RelativeFreq: y += 0; break;
                        case chartType.NormalizedFreq: y += 0; break;
                     }
                }

                if(attacks == 1) g.DrawLine(pen, previousx, (ct == chartType.PlusMinus) ? HalfwayYPoint : rect.Bottom, x, y);
                else g.DrawLine(pen, previousx, previousy, x, y);

                chartDot = new Rectangle((int)x - 1, (int)y - 1, 2, 2);
                g.FillRectangle(brush, chartDot);
                g.DrawRectangle(pen, chartDot);

                previousx = x;
                previousy = y;

                if (attacks == _COLUMNS)
                {
                    //g.DrawString(score.ToString(), Control.DefaultFont, Brushes.Black, rect.Right + 5, y - 7);

                    this.x = rect.X;
                    this.y = (ct == chartType.PlusMinus) ? HalfwayYPoint : rect.Bottom;

                    this.results.Add(score);

                    CreateHistogram(_SYSTEMS_COUNT);

                    this.attacks = 0;
                    this.score = 0;

                    previousx = x;
                    previousy = y;
                }
            }
            pictureBox.Invalidate();
        }
        

        private Rectangle topLeftCorner
        {
            get { return new Rectangle(rect.Left - _CORNER_SIZE, rect.Top - _CORNER_SIZE, _CORNER_SIZE * 2, _CORNER_SIZE * 2); }
        }

        private Rectangle topRightCorner
        {
            get { return new Rectangle(rect.Right - _CORNER_SIZE, rect.Top - _CORNER_SIZE, _CORNER_SIZE * 2, _CORNER_SIZE * 2); }
        }

        private Rectangle bottomLeftCorner
        {
            get { return new Rectangle(rect.Left - _CORNER_SIZE, rect.Bottom - _CORNER_SIZE, _CORNER_SIZE * 2, _CORNER_SIZE * 2); }
        }

        private Rectangle bottomRightCorner
        {
            get { return new Rectangle(rect.Right - _CORNER_SIZE, rect.Bottom - _CORNER_SIZE, _CORNER_SIZE * 2, _CORNER_SIZE * 2); }
        }

        private int HalfwayXPoint
        {
            get { return (rect.Left + rect.Right) / 2; }
            set { }
        }

        private int HalfwayYPoint
        {
            get { return (rect.Bottom + rect.Top) / 2; }
            set { }
        }

        private void UpdatePosition(int X, int Y, int newWidth, int newHeight, float newYStep, float newXStep)
        {
            rect.X = X;
            rect.Y = Y; 
            rect.Width = newWidth;
            rect.Height = newHeight;
            yStep = newYStep;  
            xStep = newXStep;

            DrawChartOnBitmap();
        }
    }
}