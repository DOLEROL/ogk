using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_obraz
{
    public partial class Form1 : Form
    {
        String filePath;
        bool read = false;
        bool CMY = false;
        bool GRAY = false;
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    panel1.Invalidate();
                    read = true;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (read)
            {
                bmp = new Bitmap(filePath);
                e.Graphics.DrawImage(bmp, new Point(0, 0));

                for (int i = 0; i < bmp.Width; i++) {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        if (CMY)
                        {
                            Color c = bmp.GetPixel(i, j);
                            int r, g, b;
                            if (checkBox1.Checked)
                            {
                                r = c.R;
                            }
                            else
                            {
                                r = 255 - c.R;
                            }
                            if (checkBox2.Checked)
                            {
                                g = c.G;
                            }
                            else
                            {
                                g = 255 - c.G;
                            }
                            if (checkBox3.Checked)
                            {
                                b = c.B;
                            }
                            else
                            {
                                b = 255 - c.B;
                            }
                            
                            Color pixelColor = Color.FromArgb(r, g, b);
                            bmp.SetPixel(i, j, pixelColor);
                        } else if (GRAY)
                        {
                            Color c = bmp.GetPixel(i, j);
                            int gray = (c.R + c.G + c.B) / 3;
                            Color pixelColor = Color.FromArgb(gray, gray, gray);
                            bmp.SetPixel(i, j, pixelColor);
                        }
                    }
                }
                panel2.Invalidate();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (read)
            {
                e.Graphics.DrawImage(bmp, new Point(0, 0));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CMY = true;
            GRAY = false;
            panel1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CMY = false;
            GRAY = true;
            panel1.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
