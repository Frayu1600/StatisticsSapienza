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
            double lambda;

            for (int i = 0; i < nattacks; i++)
            {
                // generate and simulate the attack
                generated = r.NextDouble();
                defended = p > generated;
                foreach (ResizeableRectangle rr in rrs)
                {
                    if(rr.ct == chartType.FreqSubintervals)
                    {
                        lambda = p;
                        defended = (lambda/rr._N) < generated; // normalize by T
                    }
                    rr.SimulateAttack(defended, color, _SYSTEMS_COUNT);
                }
                   

                this.attacks.Add(defended);
            }
        }
        public void CreateResizeableRectangle(int x, int y, int _ATTACKS, int _ROWS, int _COLUMNS, int _CORNER_SIZE, chartType ct, int _N = 1)
        {
            rrs.Add(new ResizeableRectangle(x, y, _ATTACKS, _ROWS, _COLUMNS, _CORNER_SIZE, ct, pictureBox, _N));
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
