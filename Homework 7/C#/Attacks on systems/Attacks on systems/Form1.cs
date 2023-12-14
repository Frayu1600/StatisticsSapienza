using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Attacks_on_systems
{
    public partial class Form1 : Form
    {
        private int _ATTACKS;

        private const int _CHART_HEIGHT = 600;  //300;
        private const int _CHART_WIDTH = 1185; //500;
        private const int _CORNER_SIZE = 7;

        private int _SYSTEMS_COUNT;

        private int x = 50;
        private int y = 100;

        private int systemsAttacked = 0;
        private bool simulationFinished = false;

        private Dictionary<string, chartType> stringToChart;

        ResizeableRectangleManager rrm;
        Random r;

        private double p = 0.5;
        public Form1()
        {
            InitializeComponent();
            InitializeBitmap();
            InitializeComboBox();

            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
        }

        private void InitializeComboBox()
        {
            stringToChart = new Dictionary<string, chartType>
            {
                { "Plus and Minus", chartType.PlusMinus },
                { "Absolute Frequency", chartType.Freq },
                { "Relative Frequency", chartType.RelativeFreq },
                { "Normalized Frequency", chartType.NormalizedFreq },
                // white space
                { "Poisson", chartType.Poisson },
                { "Gambler's Ruin", chartType.GamblersRuin },
                // white space
                { "Standard Brownian Motion", chartType.StandardBrownian },
                { "Geometric Brownian Motion", chartType.GeometricBrownian },
                { "General Brownian Motion", chartType.GeneralBrownian }
            };

            comboBoxSimulation.SelectedIndex = 10;
        }

        private void InitializeProgressBar()
        {
            progressBar.Step = 1;
            progressBar.Maximum = _SYSTEMS_COUNT;
        }

        private void InitializeTimer()
        {
            timer1.Interval = (int)numericUpDownTimerSpeed.Value;
        }

        private void InitializeBitmap()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
        }

        private void InitializeRectangles()
        {
            rrm = new ResizeableRectangleManager(pictureBox, _SYSTEMS_COUNT);

            rrm.CreateResizeableRectangle(x, y, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, stringToChart[comboBoxSimulation.SelectedItem.ToString()], (float)numericUpDownMu.Value, (float)numericUpDownSigma.Value, (int)numericUpDownStartS.Value, (int)numericUpDownSStep.Value, (int)numericUpDownSLevels.Value, (int)numericUpDownP.Value);
            //rrm.CreateResizeableRectangle(x + 550, y, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.StandardBrownian);
            //rrm.CreateResizeableRectangle(x, y + 400, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.GeneralBrownian, 0.1f, 0.6f);
            //rrm.CreateResizeableRectangle(x + 550, y + 400, _CHART_WIDTH, _CHART_HEIGHT, _ATTACKS, _CORNER_SIZE, chartType.NormalizedFreq);

            rrm.DrawResizeableRectangles();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (rrm != null) rrm.ResizeableRectangleManager_MouseUp(sender, e);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (rrm != null) rrm.ResizeableRectangleManager_MouseDown(sender, e);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (rrm != null) rrm.ResizeableRectangleManager_MouseMove(sender, e);
        }

        private void trackBarP_Scroll(object sender, EventArgs e)
        {
            if (comboBoxSimulation.SelectedItem.ToString() == "Poisson")
            {
                p = trackBarP.Value * ((double)_ATTACKS / 10);
                labelP.Text = $"λ = {p}";
            }
            else
            {
                p = (double)trackBarP.Value / 10;
                labelP.Text = $"p = {p}";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Color randomColor = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
            rrm.SimulateAttacks(p, _ATTACKS, randomColor);
            progressBar.PerformStep();

            if (systemsAttacked++ == _SYSTEMS_COUNT)
            {
                timer1.Stop();
                simulationFinished = true;
                buttonPauseResume.Text = "Continue";
            }

            if (simulationFinished) labelAttacks.Text = $"Simulating system #{systemsAttacked - 1} of {systemsAttacked - 1}";
            else labelAttacks.Text = $"Simulating system #{systemsAttacked - 1} of {_SYSTEMS_COUNT}";
        }

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            if (comboBoxSimulation.SelectedItem.ToString() == string.Empty) return;

            _SYSTEMS_COUNT = (int)numericUpDownSystems.Value;
            _ATTACKS = (int)numericUpDownAttacks.Value;

            InitializeProgressBar();
            InitializeRectangles();
            InitializeTimer();

            r = new Random();

            timer1.Start();
            buttonSimulate.Enabled = false;

            buttonPauseResume.Enabled = true;
            buttonReset.Enabled = true;
        }

        private void buttonPauseResume_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                buttonPauseResume.Text = "Resume";
            }
            else
            {
                timer1.Start();
                buttonPauseResume.Text = "Pause";
            }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            systemsAttacked = 0;
            labelAttacks.Text = $"Simulating system # of ?";
            buttonPauseResume.Text = "Pause";
            progressBar.Value = 0;
            simulationFinished = false;

            rrm.ResetAllRectangles();
            buttonReset.Enabled = false;

            buttonPauseResume.Enabled = false;
            buttonSimulate.Enabled = true;
        }

        private void numericUpDownTimerSpeed_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDownTimerSpeed.Value;
        }

        private void comboBoxSimulation_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxSimulation.SelectedItem.ToString())
            {
                case "Geometric Brownian Motion":
                    {
                        numericUpDownMu.Enabled = true;
                        numericUpDownSigma.Enabled = true;

                        numericUpDownSStep.Enabled = false;
                        numericUpDownP.Enabled = false;
                        numericUpDownStartS.Enabled = false;
                        numericUpDownSLevels.Enabled = false;
                    }
                    break;
                case "Poisson":
                    {
                        _ATTACKS = (int)numericUpDownAttacks.Value;

                        p = trackBarP.Value * ((double)_ATTACKS / 10);
                        labelP.Text = $"λ = {p}";

                        numericUpDownSStep.Enabled = false;
                        numericUpDownP.Enabled = false;
                        numericUpDownStartS.Enabled = false;
                        numericUpDownSLevels.Enabled = false;
                    }
                    break;
                case "Gambler's Ruin":
                    {
                        numericUpDownSStep.Enabled = true;
                        numericUpDownP.Enabled = true;
                        numericUpDownStartS.Enabled = true;
                        numericUpDownSLevels.Enabled = true;
                    }
                    break;
                default:
                    {
                        numericUpDownMu.Enabled = false;
                        numericUpDownSigma.Enabled = false;
                        numericUpDownSStep.Enabled = false;
                        numericUpDownP.Enabled = false;
                        numericUpDownStartS.Enabled = false;
                        numericUpDownSLevels.Enabled = false;
                        p = (double)trackBarP.Value / 10;
                        labelP.Text = $"p = {p}";
                    }
                    break;
            }
        }
    }
}