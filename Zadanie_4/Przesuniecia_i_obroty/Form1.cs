using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Przesuniecia_i_obroty
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        Pen pen;
        Point[] p1, p2, p3, p4, p5;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Size.Width - 1, pictureBox1.Size.Height - 1);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);

            pen = new Pen(Color.Black, 1);

            p1 = new Point[] {
                new Point(50, 50),
                new Point(50, 100),
                new Point(100, 100),
                new Point(100, 50),
            };
            p2 = new Point[] {
                new Point(200, 50),
                new Point(200, 100),
                new Point(250, 100),
                new Point(250, 50),
            };
            p3 = new Point[] {
                new Point(50, 200),
                new Point(50, 250),
                new Point(100, 250),
                new Point(100, 200),
            };
            p4 = new Point[] {
                new Point(200, 200),
                new Point(200, 250),
                new Point(250, 250),
                new Point(250, 200),
            };
            p5 = new Point[] {
                new Point(125, 125),
                new Point(125, 175),
                new Point(175, 175),
                new Point(175, 125),
            };

            g.DrawPolygon(pen, p1);
            g.DrawPolygon(pen, p2);
            g.DrawPolygon(pen, p3);
            g.DrawPolygon(pen, p4);
            g.FillPolygon(Brushes.Red, p5);
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            int scroll = hScrollBar1.Value;

            bmp = new Bitmap(pictureBox1.Size.Width - 1, pictureBox1.Size.Height - 1);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);

            Matrix matrDraw = new Matrix();
            matrDraw.Reset();
            matrDraw.Translate(scroll, 0);
            matrDraw.RotateAt(-scroll * 2, new Point(150, 150));

            g.Transform = matrDraw;
            g.DrawPolygon(pen, p1);
            g.DrawPolygon(pen, p2);
            g.DrawPolygon(pen, p3);
            g.DrawPolygon(pen, p4);

            g.Dispose();
            g = Graphics.FromImage(bmp);

            Matrix matrFill = new Matrix();
            matrFill.Reset();
            matrFill.Translate(scroll, 0);
            matrFill.RotateAt(scroll * 2, new Point(150, 150));

            g.Transform = matrFill;
            g.FillPolygon(Brushes.Red, p5);
        }
    }
}
