using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attacks_on_systems
{
    // the purpose of this class is to handle the interaction between ResizeableRectangle and the Form itself
    internal class ResizeableRectangleManager
    {
        private PictureBox pictureBox;
        private List<ResizeableRectangle> rrs;
        private Random r;

        private int _SYSTEMS_COUNT;

        private List<bool> attacks;

        public ResizeableRectangleManager(PictureBox pictureBox, int SYSTEMS_COUNT)
        {
            this.pictureBox = pictureBox;

            this.rrs = new List<ResizeableRectangle>();
            this.r = new Random();

            this.attacks = new List<bool>();
            this._SYSTEMS_COUNT = SYSTEMS_COUNT;
        }

        public void DrawResizeableRectangles()
        {
            // safety check
            if (rrs.Count == 0) return;

            // clean picturebox
            using (Graphics g = Graphics.FromImage(pictureBox.Image)) g.Clear(Color.White);

            // draw every ResizeableRectangle
            foreach (ResizeableRectangle rr in rrs)
                rr.DrawChartOnBitmap();
        }

        // lets us simulate an arbitrary number of attacks using a specified color
        public void SimulateAttacks(double p, int nattacks, Color color)
        {
            // safety check
            if (rrs.Count == 0) return;

            double generated;
            bool defended;

            for (int i = 0; i < nattacks; i++)
            {
                // generate and simulate the attack
                generated = r.NextDouble();
                defended = p > generated;

                foreach (ResizeableRectangle rr in rrs) 
                    rr.SimulateAttack(rr.ct == chartType.Poisson ? (p / rr._ATTACKS) < generated: defended, color, _SYSTEMS_COUNT);
                

                this.attacks.Add(defended);
            }
        }
        public void CreateResizeableRectangle(int x, int y, int _ATTACKS, int _ROWS, int _COLUMNS, int _CORNER_SIZE, chartType ct, float mu = 0, float sigma = 0, int startS = 0, int sStep = 0, int sLevels = 10, int P = 0)
        {
            rrs.Add(new ResizeableRectangle(x, y, _ATTACKS, _ROWS, _COLUMNS, _CORNER_SIZE, ct, pictureBox, mu, sigma, startS, sStep, sLevels, P));
        }

        // resets all
        public void ResetAllRectangles()
        {
            // clean picturebox
            using (Graphics g = Graphics.FromImage(pictureBox.Image)) g.Clear(Color.White);

            // draw every ResizeableRectangle
            foreach (ResizeableRectangle rr in rrs)
            {   
                rr.ResetAllSimulationValues();
                rr.DrawChartOnBitmap();
            }
        }

        // removes every rectangle
        public void Flush()
        {
            // clean picturebox
            using (Graphics g = Graphics.FromImage(pictureBox.Image)) g.Clear(Color.White);
            pictureBox.Invalidate();

            this.rrs.Clear();
        }

        public void ResizeableRectangleManager_MouseUp(object? sender, MouseEventArgs e)
        {
            foreach (ResizeableRectangle rr in rrs)
                rr.ResizeableRectangle_MouseUp(sender, e);
        }

        public void ResizeableRectangleManager_MouseDown(object? sender, MouseEventArgs e)
        {
            foreach (ResizeableRectangle rr in rrs)
                rr.ResizeableRectangle_MouseDown(sender, e);
        }

        public void ResizeableRectangleManager_MouseMove(object? sender, MouseEventArgs e)
        {
            foreach (ResizeableRectangle rr in rrs)
                rr.ResizeableRectangle_MouseMove(sender, e);

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                if (e.Button == MouseButtons.Left)
                {
                    g.Clear(Color.White);
                    foreach (ResizeableRectangle rr in rrs)
                    {
                        rr.DrawChartOnBitmap();
                        rr.ReSimulateAttacks(attacks, _SYSTEMS_COUNT);
                    }
                }
            }
        }
    }
}
