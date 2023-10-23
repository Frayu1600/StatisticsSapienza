using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attacks_on_systems
{
    internal class ResizeableRectangleManager
    {
        private PictureBox pictureBox;
        private List<ResizeableRectangle> rrs;
        private Random r;

        private List<bool> attacks;

        public ResizeableRectangleManager(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;

            rrs = new List<ResizeableRectangle>();
            r = new Random();

            attacks = new List<bool>();
        }

        public void DrawResizeableRectangles()
        {
            if (rrs.Count == 0) return;

            using (Graphics g = Graphics.FromImage(pictureBox.Image)) g.Clear(Color.White);

            foreach (ResizeableRectangle rr in rrs)
                rr.DrawChartOnBitmap();
        }

        public void SimulateAttacks(double p, int nattacks, Brush brush, Pen pen)
        {
            if (rrs.Count == 0) return;

            double generated;

            for (int i = 0; i < nattacks; i++)
            {
                generated = r.NextDouble();

                using (Graphics g = Graphics.FromImage(pictureBox.Image))
                {
                    g.Clear(Color.White);
                    foreach (ResizeableRectangle rr in rrs)
                    {
                        rr.DrawChartOnBitmap();
                        rr.ReSimulateAttacks(attacks, brush, pen);
                        rr.SimulateAttack(p > generated, brush, pen);
                    }
                }              

                this.attacks.Add(p > generated);
            }
        }

        public void CreateResizeableRectangle(int x, int y, int _CHART_WIDTH, int _CHART_HEIGHT, int _ROWS, int _COLUMNS, int _CORNER_SIZE, chartType ct)
        {
            rrs.Add(new ResizeableRectangle(x, y, _CHART_WIDTH, _CHART_HEIGHT, _ROWS, _COLUMNS, _CORNER_SIZE, ct, pictureBox));
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
                        rr.ReSimulateAttacks(attacks, Brushes.Red, Pens.Red);
                    }
                }
            }
        }
    }
}
