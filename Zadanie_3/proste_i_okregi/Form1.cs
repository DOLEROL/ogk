using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace proste_i_okregi
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        bool exec = false;
        int option = 0;
        int xp, xk, yp, yk, x0, y0;
        float R;
        Stopwatch sw = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = panel1.Size.Width;
            numericUpDown2.Maximum = panel1.Size.Height - 1;
            numericUpDown3.Maximum = panel1.Size.Width;
            numericUpDown4.Maximum = panel1.Size.Height-1;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (exec)
            {
                bmp = new Bitmap(panel1.Size.Width, panel1.Size.Height);
                e.Graphics.DrawImage(bmp, new Point(0, 0));

                xp = (int)numericUpDown1.Value;
                yp = (int)numericUpDown2.Value;
                xk = (int)numericUpDown3.Value;
                yk = (int)numericUpDown4.Value;

                x0 = (int)numericUpDown1.Value;
                y0 = (int)numericUpDown2.Value;
                R = (float)numericUpDown3.Value;

                if (option == 1)
                {
                    float dx = xk - xp;
                    float dy = yk - yp;
                    float m = dy / dx;
                    float y = yp;

                    sw.Start();

                    for (int i = xp; i < xk; ++i)
                    {
                        bmp.SetPixel(i, (int)Math.Floor(y + 0.5), Color.Black);
                        y += m;
                    }

                    sw.Stop();
                    MessageBox.Show("Czas trwania: " + sw.Elapsed.ToString());
                }
                else if (option == 2)
                {
                    int dx, dy, incrE, incrNE, d, x, y;
                    dx = xk - xp;
                    dy = yk - yp;
                    d = 2 * dy - dx;
                    incrE = dy * 2;
                    incrNE = (dy - dx) * 2;
                    x = xp;
                    y = yp;

                    sw.Start();

                    bmp.SetPixel(x, y, Color.Black);
                    while (x < xk)
                    {
                        if (d < 0)
                        {
                            d += incrE;
                            x++;
                        }
                        else
                        {
                            d += incrNE;
                            x++;
                            y++;
                        }
                        bmp.SetPixel(x, y, Color.Black);
                    }

                    sw.Stop();
                    MessageBox.Show("Czas trwania: " + sw.Elapsed.ToString());
                }
                else if (option == 3)
                {
                    float dest = (float)(Math.PI / 2);
                    float step = 1 / R;

                    sw.Start();

                    for (float i = 0; i < dest; i += step)
                    {
                        float x = (float)(R * Math.Cos(i));
                        float y = (float)(R * Math.Sin(i));

                        bmp.SetPixel((int)(x0 + x), (int)(y0 + y), Color.Black);
                        bmp.SetPixel((int)(x0 - x), (int)(y0 + y), Color.Black);
                        bmp.SetPixel((int)(x0 + x), (int)(y0 - y), Color.Black);
                        bmp.SetPixel((int)(x0 - x), (int)(y0 - y), Color.Black);
                    }

                    sw.Stop();
                    MessageBox.Show("Czas trwania: " + sw.Elapsed.ToString());
                }
                else if (option == 4)
                {
                    float dest = (float)(Math.PI / 4);
                    float step = 1 / R;

                    sw.Start();

                    for (float i = 0; i < dest; i += step)
                    {
                        float x = (float)(R * Math.Cos(i));
                        float y = (float)(R * Math.Sin(i));

                        bmp.SetPixel((int)(x0 + x), (int)(y0 + y), Color.Black);
                        bmp.SetPixel((int)(x0 - x), (int)(y0 + y), Color.Black);
                        bmp.SetPixel((int)(x0 + x), (int)(y0 - y), Color.Black);
                        bmp.SetPixel((int)(x0 - x), (int)(y0 - y), Color.Black);

                        bmp.SetPixel((int)(x0 + y), (int)(y0 + x), Color.Black);
                        bmp.SetPixel((int)(x0 + y), (int)(y0 - x), Color.Black);
                        bmp.SetPixel((int)(x0 - y), (int)(y0 + x), Color.Black);
                        bmp.SetPixel((int)(x0 - y), (int)(y0 - x), Color.Black);
                    }

                    sw.Stop();
                    MessageBox.Show("Czas trwania: " + sw.Elapsed.ToString());
                }
                else if (option == 5)
                {
                    int x, y;
                    float d;
                    x = 0;
                    y = (int)R;
                    d = 5 / 4 - R;

                    sw.Start();

                    bmp.SetPixel((int)(x0 + x), (int)(y0 + y), Color.Black);
                    bmp.SetPixel((int)(x0 - x), (int)(y0 + y), Color.Black);
                    bmp.SetPixel((int)(x0 + x), (int)(y0 - y), Color.Black);
                    bmp.SetPixel((int)(x0 - x), (int)(y0 - y), Color.Black);

                    bmp.SetPixel((int)(x0 + y), (int)(y0 + x), Color.Black);
                    bmp.SetPixel((int)(x0 + y), (int)(y0 - x), Color.Black);
                    bmp.SetPixel((int)(x0 - y), (int)(y0 + x), Color.Black);
                    bmp.SetPixel((int)(x0 - y), (int)(y0 - x), Color.Black);
                    while (y > x)
                    {
                        if (d < 0)
                        {
                            d += x * 2 + 3;
                            x++;
                        }else
                        {
                            d += (x - y) * 2 + 5;
                            x++;
                            y--;
                        }
                        bmp.SetPixel((int)(x0 + x), (int)(y0 + y), Color.Black);
                        bmp.SetPixel((int)(x0 - x), (int)(y0 + y), Color.Black);
                        bmp.SetPixel((int)(x0 + x), (int)(y0 - y), Color.Black);
                        bmp.SetPixel((int)(x0 - x), (int)(y0 - y), Color.Black);

                        bmp.SetPixel((int)(x0 + y), (int)(y0 + x), Color.Black);
                        bmp.SetPixel((int)(x0 + y), (int)(y0 - x), Color.Black);
                        bmp.SetPixel((int)(x0 - y), (int)(y0 + x), Color.Black);
                        bmp.SetPixel((int)(x0 - y), (int)(y0 - x), Color.Black);
                    }

                    sw.Stop();
                    MessageBox.Show("Czas trwania: " + sw.Elapsed.ToString());

                }
                else if (option == 6)
                {
                    int x, y, d, deltaE, deltaSE;
                    x = 0;
                    y = (int)R;
                    d = 1 - (int)R;
                    deltaE = 3;
                    deltaSE = 5 - (int)R * 2;

                    sw.Start();

                    bmp.SetPixel((int)(x0 + x), (int)(y0 + y), Color.Black);
                    bmp.SetPixel((int)(x0 - x), (int)(y0 + y), Color.Black);
                    bmp.SetPixel((int)(x0 + x), (int)(y0 - y), Color.Black);
                    bmp.SetPixel((int)(x0 - x), (int)(y0 - y), Color.Black);

                    bmp.SetPixel((int)(x0 + y), (int)(y0 + x), Color.Black);
                    bmp.SetPixel((int)(x0 + y), (int)(y0 - x), Color.Black);
                    bmp.SetPixel((int)(x0 - y), (int)(y0 + x), Color.Black);
                    bmp.SetPixel((int)(x0 - y), (int)(y0 - x), Color.Black);

                    while (y > x)
                    {
                        if (d < 0)
                        {
                            d += deltaE;
                            deltaE += 2;
                            deltaSE += 2;
                            x++;
                        }
                        else
                        {
                            d += deltaSE;
                            deltaE += 2;
                            deltaSE += 4;
                            x++;
                            y--;
                        }

                        bmp.SetPixel((int)(x0 + x), (int)(y0 + y), Color.Black);
                        bmp.SetPixel((int)(x0 - x), (int)(y0 + y), Color.Black);
                        bmp.SetPixel((int)(x0 + x), (int)(y0 - y), Color.Black);
                        bmp.SetPixel((int)(x0 - x), (int)(y0 - y), Color.Black);

                        bmp.SetPixel((int)(x0 + y), (int)(y0 + x), Color.Black);
                        bmp.SetPixel((int)(x0 + y), (int)(y0 - x), Color.Black);
                        bmp.SetPixel((int)(x0 - y), (int)(y0 + x), Color.Black);
                        bmp.SetPixel((int)(x0 - y), (int)(y0 - x), Color.Black);
                    }

                    sw.Stop();
                    MessageBox.Show("Czas trwania: " + sw.Elapsed.ToString());

                }
                e.Graphics.DrawImage(bmp, new Point(0, 0));
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label1.Text = "xp=";
            label2.Text = "yp=";
            label3.Text = "xk=";
            label4.Text = "yk=";

            numericUpDown4.Visible = true;

            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = false;
            button4.Visible = false;
            button1.Text = "Inkrementacyjnie";
            button2.Text = "Punkt środkowy";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "x0=";
            label2.Text = "y0=";
            label3.Text = "R=";
            label4.Visible = false;

            numericUpDown4.Visible = false;

            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button1.Text = "Symetria 4";
            button2.Text = "Symetria 8";
            button3.Text = "Punkt środkowy";
            button4.Text = "Punkt środkowy drugiego rzędu";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                exec = true;
                option = 1;
                panel1.Invalidate();
            }
            else
            {
                exec = true;
                option = 3;
                panel1.Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                exec = true;
                option = 2;
                panel1.Invalidate();
            }
            else
            {
                exec = true;
                option = 4;
                panel1.Invalidate();
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            exec = true;
            option = 5;
            panel1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            exec = true;
            option = 6;
            panel1.Invalidate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
