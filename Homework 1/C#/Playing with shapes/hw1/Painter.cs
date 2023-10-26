using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    internal class Painter
    {
        private PictureBox pictureBox;

        // simple painter class
        public Painter(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
        }

        // draws a point
        public void DrawPoint(Color color, int thickness, int x, int y, int width, int height)
        {
            Pen pen = new Pen(color, thickness);
            Brush brush = new SolidBrush(color);

            using(Graphics g = Graphics.FromImage(pictureBox.Image)) 
            {
                g.FillEllipse(brush, x, y, width, height);
                g.DrawEllipse(pen, x, y, width, height);
            }
        }

        // draws a line
        public void DrawLine(Color color, int thickness, float x1, float y1, float x2, float y2)
        {
            Pen pen = new Pen(color, thickness);

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
                g.DrawLine(pen, x1, y1, x2, y2);
        }

        // draws a circle
        public void DrawCircle(Color color, int thickness, int x, int y, int width, int height)
        {
            Pen pen = new Pen(color, thickness);
            Brush brush = new SolidBrush(color);

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                g.FillEllipse(brush, x, y, width, height);
                g.DrawEllipse(pen, x, y, width, height);
            }
        }

        // draws a rectangle
        public void DrawRectangle(Color color, int thickness, int x, int y, int width, int height)
        {
            Pen pen = new Pen(color, thickness);
            Brush brush = new SolidBrush(color);

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                g.FillRectangle(brush, x, y, width, height);
                g.DrawRectangle(pen, x, y, width, height);
            }
        }

    }
}
