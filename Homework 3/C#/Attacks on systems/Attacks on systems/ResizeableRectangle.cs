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

    // used to redraw every attack when a chart is dragged or resized
    internal class Result
    {
        public readonly Color color;
        public readonly float result;
        public Result(float result, Color color) {
            this.color = color;
            this.result = result;
        }
    }

    internal class ResizeableRectangle : PictureBox
    {
        // the rectangle actually drawn in the picturebox
        private Rectangle rect;

        private PictureBox pictureBox;

        // scale factors
        private float yStep;
        private float xStep;
        private float relativeAndNormalizedScaleFactor;
        private int chartNumberDistanceFactor;

        public readonly chartType ct;

        // constants
        private int _ROWS;
        private int _COLUMNS;
        private int _CORNER_SIZE;
        private int _ATTACKS;

        // variables regarding the attack
        private int attacksPerformed;
        private int penetrations;
        private float currentScore;

        // used to redraw every attack when a chart is dragged or resized 
        private List<Result> results;

        // coordinates for the drawing of lines for the attacks
        private float x;
        private float y;
        private float previousx;
        private float previousy;

        // variables for the resizing/dragging of the rectangle
        private bool isResizing;
        private bool isMoving;
        private Point startPoint;
        private Point previousMouseLocation;

        public ResizeableRectangle(int x, int y, int width, int height, int attacks, int cornerSize, chartType ct, PictureBox pictureBox)
        {
            this.rect = new Rectangle(x, y, width, height);
            this.ct = ct;
            this.pictureBox = pictureBox;

            this._ATTACKS = attacks;
            this._ROWS = attacks;
            this._COLUMNS = _ROWS;
            this._CORNER_SIZE = cornerSize;

            this.yStep = height / (float)_ROWS;
            this.xStep = width / (float)_COLUMNS;

            this.currentScore = 0;
            this.penetrations = 0;
            this.attacksPerformed = 0;

            this.chartNumberDistanceFactor = CalcChartNumberDistanceFactor();
            this.relativeAndNormalizedScaleFactor = CalcRelativeAndNormalizedScaleFactor();

            this.isResizing = false;
            this.isMoving = false;

            this.x = rect.X;
            this.y = (ct == chartType.PlusMinus) ? HalfwayYPoint : rect.Bottom;

            this.results = new List<Result>();

            this.previousx = x;
            this.previousy = y;
        }

        private float CalcRelativeAndNormalizedScaleFactor()
        {
            switch (_ATTACKS)
            {
                case int n when n <= 10: return 1.5f;
                case int n when n <= 20: return 2;
                case int n when n <= 100: return 5;
                default: return 10;
            }
        }

        private int CalcChartNumberDistanceFactor()
        {
            switch (_ATTACKS)
            {
                case int n when n <= 10: return 1;
                case int n when n <= 20: return 2;
                case int n when n <= 100: return 10;
                case int n when n <= 500: return 50;
                default: return 100;
            }
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
                float x, y;

                // draw the columns
                for (int i = 0; i <= _COLUMNS; i += chartNumberDistanceFactor)
                {
                    x = rect.Left + i * xStep;
                    g.DrawString(i.ToString(), Control.DefaultFont, Brushes.Black, x - 5, rect.Bottom + 5);
                    g.DrawLine(Pens.LightGray, x, rect.Top, x, rect.Bottom);
                }

                // draw the rows
                for (int i = 0; i <= _ROWS * 2; i += chartNumberDistanceFactor)
                {
                    y = rect.Bottom - i * yStep / 2;
                    g.DrawLine(Pens.LightGray, rect.Right, y, rect.Left, y);
                }

                // draw the zero line if we are using PlusMinus graph
                if (ct == chartType.PlusMinus) g.DrawLine(Pens.Red, rect.Left, HalfwayYPoint, rect.Right, HalfwayYPoint);

                // draw the resize squares
                g.DrawRectangle(Pens.LightGray, this.topLeftCorner);
                g.DrawRectangle(Pens.LightGray, this.topRightCorner);
                g.DrawRectangle(Pens.LightGray, this.bottomLeftCorner);
                g.DrawRectangle(Pens.LightGray, this.bottomRightCorner);

                // draw labels
                g.DrawString("Attacks",
                    Control.DefaultFont,
                    Brushes.Black,
                    this.HalfwayXPoint - (xStep / 2),
                    rect.Bottom + (40*yStep)
                );
                g.DrawString(ct.ToString(),
                    Control.DefaultFont,
                    Brushes.Black,
                    this.HalfwayXPoint - (2*xStep),
                    rect.Top - (40*yStep)
                 );      
            }
            pictureBox.Invalidate();
        }

        // resimulates every attack given a list of booleans (outcomes) and the systems count
        public void ReSimulateAttacks(List<bool> attacks, int _SYSTEMS_COUNT)
        {
            ResetAllSimulationValues();

            // resimulate every attack
            for (int i = 0; i < attacks.Count; i++)
                SimulateAttack(attacks[i], results[i/_ROWS].color, _SYSTEMS_COUNT);
        }

        // reset every simulation parameter
        private void ResetAllSimulationValues()
        {  
            this.x = rect.X;
            this.y = (ct == chartType.PlusMinus) ? HalfwayYPoint : rect.Bottom;

            this.attacksPerformed = 0;
            this.currentScore = 0;
            this.penetrations = 0;

            this.previousx = x;
            this.previousy = y;
        }

        // create the histogram
        public void CreateHistogram(int _SYSTEMS_COUNT)
        {
            // draws histograms at the end of the simulation
            if (results.Count <= _SYSTEMS_COUNT) return;

            // save min and max score as references
            float minScore = results.Min(r => r.result);
            float maxScore = results.Max(r => r.result);

            // calculate the number of rectangles to draw
            int boxes = Math.Max(5, results.Count/10);

            // scale factor
            float UIRectHeight = rect.Height / boxes;

            // to calculate the height of every rectangle
            int[] intervals = new int[boxes];

            // width of every rectangle
            float intervalWidth = (maxScore - minScore) / intervals.Length;

            // add values to every interval accordingly
            for (int i = 0; i < results.Count; i++)
            {
                double intervalIndex = Math.Floor((results[i].result - minScore) / intervalWidth);

                if (intervalIndex >= intervals.Length) intervals[intervals.Length - 1]++;
                else intervals[(int)intervalIndex]++;
            }

            // prepare the rectangles
            float histogramRectY;
            int UIRectWidthUnit;
            for (int i = 0; i < intervals.Length; i++)
            {
                // adjust the max height to avoid overflow
                UIRectWidthUnit = rect.Width / Math.Abs(intervals.AsQueryable().Max()) / 2;
                histogramRectY = rect.Y + i * UIRectHeight;
                Rectangle histogramRect = new Rectangle
                (
                    rect.Right - intervals[i] * UIRectWidthUnit,
                    (int)histogramRectY,    
                    intervals[i] * UIRectWidthUnit,
                    (int)UIRectHeight
                );

                // actually draw the rectangles
                using (Graphics g = Graphics.FromImage(pictureBox.Image))
                {
                    Color semiTransparentColor = Color.FromArgb(128, Color.Red);
                    Brush semiTransparentBrush = new SolidBrush(semiTransparentColor);

                    g.FillRectangle(semiTransparentBrush, histogramRect);

                    semiTransparentBrush.Dispose();
                }
            }
        }

        // simulate a single attack given the outcome of the attack itself
        public void SimulateAttack(bool defended, Color color, int _SYSTEMS_COUNT)
        {
            Brush brush = new SolidBrush(color);
            Pen pen = new Pen(color, 2);

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                x = rect.X + ++attacksPerformed * (rect.Width / (float)_COLUMNS);

                if (!defended)
                {
                    penetrations++;
                    switch (ct)
                    {
                        case chartType.PlusMinus: 
                            y -= yStep / 2; 
                            currentScore++; 
                        break;
                        case chartType.Freq: 
                            y -= yStep; 
                            currentScore++; 
                        break;
                        case chartType.RelativeFreq: 
                            y -= (yStep * relativeAndNormalizedScaleFactor / attacksPerformed); 
                            currentScore += ((float)penetrations / (float)attacksPerformed); 
                        break;
                        case chartType.NormalizedFreq: 
                            y -= (yStep * relativeAndNormalizedScaleFactor / (float)Math.Sqrt(attacksPerformed)); 
                            currentScore += (penetrations / (float)Math.Sqrt(attacksPerformed)); 
                        break;
                    }
                }
                else
                {
                    switch (ct)
                    {
                        case chartType.PlusMinus: y += yStep / 2; currentScore--; break;
                        case chartType.Freq: y += 0; break;
                        case chartType.RelativeFreq: y += 0; break;
                        case chartType.NormalizedFreq: y += 0; break;
                     }
                }

                // only on the first attack, draw the line to the first coordinate
                if(attacksPerformed == 1) g.DrawLine(pen, previousx, (ct == chartType.PlusMinus) ? HalfwayYPoint : rect.Bottom, x, y);
                else g.DrawLine(pen, previousx, previousy, x, y);

                // keep values for the next attack
                previousx = x;
                previousy = y;

                // this check is needed to avoid going past the last column of the graph
                if (attacksPerformed == _COLUMNS)
                {
                    this.results.Add(new Result(currentScore, color));

                    ResetAllSimulationValues();

                    // draw the histogram
                    CreateHistogram(_SYSTEMS_COUNT);
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

        // updates the position of the rectangle following a resize/drag
        private void UpdatePosition(int X, int Y, int newWidth, int newHeight, float newYStep, float newXStep)
        {
            rect.X = X;
            rect.Y = Y; 
            rect.Width = newWidth;
            rect.Height = newHeight;
            yStep = newYStep;  
            xStep = newXStep;
        }
    }
}