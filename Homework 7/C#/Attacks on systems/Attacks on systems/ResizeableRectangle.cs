using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Attacks_on_systems
{
    public enum chartType { PlusMinus, Freq, RelativeFreq, NormalizedFreq, Poisson, GamblersRuin, GeometricBrownian, StandardBrownian, GeneralBrownian };

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
        private float generalBrownianMotionScaleFactor;

        public readonly chartType ct;

        // constants
        private int _ROWS;
        private int _COLUMNS;
        private int _CORNER_SIZE;
        public int _ATTACKS;

        // variables regarding the attack
        private int attacksPerformed;
        private int totalAttacksPerformed;
        private int penetrations;
        private float currentScore;

        // variables regarding hw6
        private int S;
        private int sStep;
        private int P;
        private int[] markedSystems;
        private bool[] marked;

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

        // variables for SDEs
        private float dt;
        private Random r;
        private float mu;
        private float sigma;

        public ResizeableRectangle(int x, int y, int width, int height, int attacks, int cornerSize, chartType ct, PictureBox pictureBox, float mu = 0, float sigma = 0, int startS = 0, int sStep = 0, int sLevels = 10, int P = 0)
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
            this.totalAttacksPerformed = 0;

            this.S = startS;
            this.sStep = sStep;
            this.P = P;
            this.marked = new bool[sLevels];
            this.markedSystems = new int[sLevels];

            this.dt = 1f / _ATTACKS;
            this.r = new Random();
            this.mu = ct == chartType.GeneralBrownian ? (this._ATTACKS * 0.5f) : mu;
            this.sigma = ct == chartType.GeneralBrownian ? (float)Math.Sqrt(this._ATTACKS * 0.5f * (1 - 0.5f)) : sigma;

            this.chartNumberDistanceFactor = CalcChartNumberDistanceFactor();
            this.relativeAndNormalizedScaleFactor = CalcRelativeAndNormalizedScaleFactor();
            this.generalBrownianMotionScaleFactor = CalcGeneralBrownianMotionScaleFactor();

            this.isResizing = false;
            this.isMoving = false;

            this.x = rect.X;
            this.y = (ct == chartType.PlusMinus || ct == chartType.GamblersRuin || ct == chartType.StandardBrownian || ct == chartType.GeneralBrownian) ? HalfwayYPoint : rect.Bottom;

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
                case int n when n <= 100: return 3;
                case int n when n <= 500: return 2;
                default: return 10;
            }
        }

        private float CalcGeneralBrownianMotionScaleFactor()
        {
            switch (_ATTACKS)
            {
                case int n when n <= 10: return 15;
                case int n when n <= 20: return 5;
                case int n when n <= 100: return 1;
                case int n when n <= 200: return 0.5f;
                case int n when n <= 300: return 0.2f;
                case int n when n <= 500: return 0.1f;
                default: return 0.05f;
            }
        }
        private int CalcChartNumberDistanceFactor()
        {
            return _ATTACKS / 10;
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

                // draw the levels
                if(ct == chartType.GamblersRuin)
                {
                    // draw S levels
                    for (int i = this.S; i <= this.markedSystems.Length * sStep; i += sStep)
                    {
                        g.DrawLine(Pens.Green, rect.Left, HalfwayYPoint - (i * yStep), rect.Right, HalfwayYPoint - (i * yStep));
                        g.DrawString(i.ToString(), Control.DefaultFont, Brushes.Green, rect.Left - 20, HalfwayYPoint - (i * yStep) - 10);
                    }
                        

                    // draw P level
                    g.DrawLine(Pens.Brown, rect.Left, HalfwayYPoint + P, rect.Right, HalfwayYPoint + P);
                    g.DrawString((-P).ToString(), Control.DefaultFont, Brushes.Brown, rect.Left - 25, HalfwayYPoint + P - 10);
                }

                // draw the zero line if we are using PlusMinus graph
                if (ct == chartType.PlusMinus || ct == chartType.GamblersRuin || ct == chartType.StandardBrownian || ct == chartType.GeneralBrownian) g.DrawLine(Pens.Red, rect.Left, HalfwayYPoint, rect.Right, HalfwayYPoint);

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
                    rect.Bottom + yStep
                );
                g.DrawString(ct.ToString(),
                    Control.DefaultFont,
                    Brushes.Black,
                    this.HalfwayXPoint - (2*xStep),
                    rect.Top - yStep
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
        public void ResetAllSimulationValues()
        {  
            this.x = rect.X;
            this.y = (ct == chartType.PlusMinus || ct == chartType.GamblersRuin || ct == chartType.StandardBrownian || ct == chartType.GeneralBrownian) ? HalfwayYPoint : rect.Bottom;

            this.attacksPerformed = 0;
            this.currentScore = 0;
            this.penetrations = 0;

            this.previousx = x;
            this.previousy = y;

            this.marked.AsSpan().Fill(false);
        }

        // create the histogram
        public void CreateHistogram()
        {
            // save min and max score as references
            float minScore = results.Min(r => r.result);
            float maxScore = results.Max(r => r.result);

            // calculate the number of rectangles to draw
            int boxes = Math.Max(5, results.Count / 10);

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

        private double GaussianRandomValue()
        {
            return Math.Sqrt(-2 * Math.Log(r.NextDouble())) * Math.Cos((2 * Math.PI) * r.NextDouble());
        }

        // simulate a single attack given the outcome of the attack itself
        public void SimulateAttack(bool defended, Color color, int _SYSTEMS_COUNT)
        {
            Brush brush = new SolidBrush(color);
            Pen pen = new Pen(color, 2);
            float dX;

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                x = rect.X + ++attacksPerformed * (rect.Width / (float)_COLUMNS);

                if (!defended)
                {
                    penetrations++;
                    switch (ct)
                    {
                        case chartType.PlusMinus:
                        case chartType.GamblersRuin:
                            y -= yStep / 2; 
                            currentScore++; 
                        break;
                        case chartType.Freq:
                        case chartType.Poisson:
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
                        case chartType.GeometricBrownian:
                            dX = (float)Math.Exp(this.mu + this.sigma * this.GaussianRandomValue());
                            y -= yStep * dX;
                            currentScore += dX;
                        break;
                        case chartType.StandardBrownian:
                            dX = (float)this.GaussianRandomValue();
                            y -= dX;
                            currentScore += dX;
                        break;
                        case chartType.GeneralBrownian:
                            dX = (this.mu + this.sigma * (float)this.GaussianRandomValue()) * (float)Math.Sqrt(this.dt);
                            y -= dX * generalBrownianMotionScaleFactor;
                            currentScore += dX;
                        break;
                    }
                }
                else
                {
                    switch (ct)
                    {
                        case chartType.PlusMinus:
                        case chartType.GamblersRuin:
                            y += yStep / 2; 
                            currentScore--; 
                        break;
                        case chartType.Freq:
                        case chartType.RelativeFreq:
                        case chartType.NormalizedFreq:
                        case chartType.Poisson:
                        case chartType.GeometricBrownian:
                            y += 0; 
                        break;
                        case chartType.StandardBrownian:
                            dX = (float)this.GaussianRandomValue();
                            y += dX;
                            currentScore -= dX;
                        break;
                        case chartType.GeneralBrownian:
                            dX = (this.mu + this.sigma * (float)this.GaussianRandomValue()) * (float)Math.Sqrt(this.dt);
                            y += dX * generalBrownianMotionScaleFactor;
                            currentScore -= dX;
                        break;
                    }
                }

                // only on the first attack, draw the line to the first coordinate
                if(attacksPerformed == 1) g.DrawLine(pen, previousx, (ct == chartType.PlusMinus || ct == chartType.GamblersRuin || ct == chartType.StandardBrownian || ct == chartType.GeneralBrownian) ? HalfwayYPoint : rect.Bottom, x, y);
                else g.DrawLine(pen, previousx, previousy, x, y);

                // keep values for the next attack
                previousx = x;
                previousy = y;

                // count if a line was crossed
                totalAttacksPerformed++;
                if(ct == chartType.GamblersRuin)
                {
                    // check if it touches P
                    if (currentScore == -P) this.marked.AsSpan().Fill(true);
                    else
                    {
                        // check if it touches any of the S
                        for (int i = this.S; i < this.markedSystems.Length * sStep; i += sStep)
                        {
                            // if yes, count the system
                            if (currentScore >= i && !marked[i / sStep])
                            {
                                markedSystems[i / sStep]++;
                                marked[i / sStep] = true;
                                break;
                            }
                        }
                    }
                }

                // this check is needed to avoid going past the last column of the graph
                if (attacksPerformed == _COLUMNS) 
                {
                    this.results.Add(new Result(currentScore, color));
                    ResetAllSimulationValues();
                }

                // when every attack was performed, print the final result
                if (_SYSTEMS_COUNT * _COLUMNS == totalAttacksPerformed) CreateHistogram();
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