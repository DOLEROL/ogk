using System;
using System.Drawing;
using System.Windows.Forms;

namespace Wypelnienie_obszaru
{
    public partial class Form1 : Form
    {
        int c = 0;
        Point[] points = new Point[4];
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
        }

        void changePixel(int x, int y, string color)
        {
            if (radioButton1.Checked)
            {
                bmp.SetPixel(x, y, Color.Red);
                if (bmp.GetPixel(x + 1, y).Name == color) changePixel(x + 1, y, color);
                if (bmp.GetPixel(x - 1, y).Name == color) changePixel(x - 1, y, color);
                if (bmp.GetPixel(x, y + 1).Name == color) changePixel(x, y + 1, color);
                if (bmp.GetPixel(x, y - 1).Name == color) changePixel(x, y - 1, color);
            }
            else if (radioButton2.Checked)
            {
                bmp.SetPixel(x, y, Color.Red);

                if (bmp.GetPixel(x + 1, y).Name == color) changePixel(x + 1, y, color);
                if (bmp.GetPixel(x - 1, y).Name == color) changePixel(x - 1, y, color);
                if (bmp.GetPixel(x, y + 1).Name == color) changePixel(x, y + 1, color);
                if (bmp.GetPixel(x, y - 1).Name == color) changePixel(x, y - 1, color);

                if (
                    bmp.GetPixel(x + 1, y + 1).Name == color
                    && bmp.GetPixel(x, y + 1).Name == color
                    && bmp.GetPixel(x + 1, y).Name == color
                    ) changePixel(x + 1, y + 1, color);
                if (
                    bmp.GetPixel(x - 1, y - 1).Name == color
                    && bmp.GetPixel(x, y - 1).Name == color
                    && bmp.GetPixel(x - 1, y).Name == color
                    ) changePixel(x - 1, y - 1, color);
                if (
                    bmp.GetPixel(x + 1, y - 1).Name == color
                    && bmp.GetPixel(x, y - 1).Name == color
                    && bmp.GetPixel(x + 1, y).Name == color
                    ) changePixel(x + 1, y - 1, color);
                if (
                    bmp.GetPixel(x - 1, y + 1).Name == color
                    && bmp.GetPixel(x, y + 1).Name == color
                    && bmp.GetPixel(x - 1, y).Name == color
                    ) changePixel(x - 1, y + 1, color);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (c == 0)
            {
                points[0] = new Point(e.X, e.Y);
                points[3] = points[0];
                c++;
            }
            else if (c == 1)
            {
                points[1] = new Point(e.X, e.Y);
                c++;
            }
            else if (c == 2)
            {
                points[2] = new Point(e.X, e.Y);
                bmp = new Bitmap(pictureBox1.Size.Width - 1, pictureBox1.Size.Height - 1);
                pictureBox1.Image = bmp;
                Graphics g = Graphics.FromImage(bmp);
                Pen pen = new Pen(Color.Black, 1);
                g.DrawLines(pen, points);
                if (radioButton3.Checked)
                {
                    int w, h;
                    w = pictureBox1.Size.Width - 1;
                    h = pictureBox1.Size.Height - 1;

                    for (int i = 0; i < h; i++)
                    {
                        int n = 0;
                        for (int j = 0; j < w; j++)
                        {
                            if (bmp.GetPixel(j, i).Name != "0" && bmp.GetPixel(j, i).Name != bmp.GetPixel(j - 1, i).Name)
                            {
                                n++;
                            }
                            else
                            {
                                if (n % 2 == 1)
                                {
                                    bmp.SetPixel(j, i, Color.Red);
                                }
                            }
                        }
                    }
                }
                c++;
            }
            else if (c == 3)
            {
                changePixel(e.X, e.Y, bmp.GetPixel(e.X, e.Y).Name);
                pictureBox1.Image = bmp;
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            c = 0;
            bmp = new Bitmap(pictureBox1.Size.Width - 1, pictureBox1.Size.Height - 1);
            pictureBox1.Image = bmp;
        }
    }
}
