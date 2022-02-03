using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wykres_funkcji
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        Pen pen, redPen;

        int F(int k)
        {
            return ((pictureBox1.Size.Height / 2) - k - 50 * (-1));
        }

        void Points(int xk, int xp, int k)
        {
            int x, y;
            for (int i = 0; i < (xk - xp) / k; i++)
            {
                x = i * k + xp + pictureBox1.Size.Width / 2;
                y = F(i * k + xp);
                bmp.SetPixel(x, y + 1, Color.Blue);
                bmp.SetPixel(x, y, Color.Blue);
                bmp.SetPixel(x, y - 1, Color.Blue);
                bmp.SetPixel(x + 1, y + 1, Color.Blue);
                bmp.SetPixel(x + 1, y, Color.Blue);
                bmp.SetPixel(x + 1, y - 1, Color.Blue);
                bmp.SetPixel(x - 1, y + 1, Color.Blue);
                bmp.SetPixel(x - 1, y, Color.Blue);
                bmp.SetPixel(x - 1, y - 1, Color.Blue);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int xk, xp, k ;

            xp = Int32.Parse(textBox1.Text);
            xk = Int32.Parse(textBox2.Text);
            k = (int)numericUpDown1.Value;

            bmp = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

            Points(xk, xp, k);

            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);
            pen = new Pen(Color.Black, 1);
            redPen = new Pen(Color.Red, 1);

            Point[] points = new Point[(xk - xp) / k];

            for (int i = 0; i < (xk - xp) / k; i++)
            {
                points[i] = new Point(i * k + xp + pictureBox1.Size.Width / 2, F(i * k + xp));
            }
            g.DrawLines(pen, points);
            g.DrawLine(redPen, 0, pictureBox1.Size.Height/2, pictureBox1.Size.Width, pictureBox1.Size.Height / 2);
            g.DrawLine(redPen, pictureBox1.Size.Width/2, 0, pictureBox1.Size.Width / 2, pictureBox1.Size.Height);
        }
    }
}
