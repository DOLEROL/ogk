using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace Fraktale
{
    class Okno : GameWindow
    {
        int obrx = 0;
        int obry = 0;
        int obrz = 0;
        float wheel = 0;
        int VertexBufferObject;
        public Okno(int width, int height)
            : base(width, height, GraphicsMode.Default, "Fraktale")
        {

        }
        protected override void OnLoad(EventArgs e)
        {
            VertexBufferObject = GL.GenBuffer();
            base.OnLoad(e);
        }
        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            base.OnUnload(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            wheel = mouse.Wheel * 0.1f;

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            else if (input.IsKeyDown(Key.X))
            {
                obrx += 5;
            }
            else if (input.IsKeyDown(Key.Y))
            {
                obry += 5;
            }
            else if (input.IsKeyDown(Key.Z))
            {
                obrz += 5;
            }

            base.OnUpdateFrame(e);
        }

        void RysProstop(float dx, float dy, float dz, float w, float s, float d, float rot, int depth)
        {
            w /= 2.0f;
            s /= 2.0f;
            d /= 2.0f;

            float[][] p4 = new float[4][]
            {
                new float[3]{dx - s, dy + w, dz + d}, // lewy górny punkt
                new float[3]{dx + s, dy + w, dz + d}, // prawy górny punkt
                new float[3]{dx + s, dy - w, dz + d}, // prawy dolny punkt
                new float[3]{dx - s, dy - w, dz + d}, // lewy dolny punkt
            };

            float[] ps = { (p4[2][0] + p4[3][0]) / 2, (p4[2][1] + p4[3][1]) / 2 };

            for (int i = 0; i < 4; i++)
            {
                p4[i][0] -= ps[0];
                p4[i][1] -= ps[1];
            }

            for (int i = 0; i < 4; i++)
            {
                float x1 = p4[i][0], y1 = p4[i][1];
                p4[i][0] = x1 * (float)Math.Cos(Math.PI * rot / 180.0f) - y1 * (float)Math.Sin(Math.PI * rot / 180.0f);
                p4[i][1] = x1 * (float)Math.Sin(Math.PI * rot / 180.0f) + y1 * (float)Math.Cos(Math.PI * rot / 180.0f);
            }

            for (int i = 0; i < 4; i++)
            {
                p4[i][0] += ps[0];
                p4[i][1] += ps[1];
            }

            float[][] p = new float[8][];

            for (int i = 0; i < p4.Length * 2; i += 2)
            {
                p[i] = p4[i / 2];
                p[i + 1] = new float[3] { p4[i / 2][0], p4[i / 2][1], p4[i / 2][2] - 2 * d };

            }

            float[][][] bryla = new float[6][][];

            bryla[0] = new float[4][] { p[0], p[2], p[4], p[6] }; // przód
            bryla[1] = new float[4][] { p[1], p[3], p[5], p[7] }; // tył
            bryla[2] = new float[4][] { bryla[0][0], bryla[0][1], bryla[1][1], bryla[1][0] }; // góra
            bryla[3] = new float[4][] { bryla[0][3], bryla[0][2], bryla[1][2], bryla[1][3] }; // dół
            bryla[4] = new float[4][] { bryla[0][0], bryla[0][3], bryla[1][3], bryla[1][0] }; // lewo
            bryla[5] = new float[4][] { bryla[0][1], bryla[0][2], bryla[1][2], bryla[1][1] }; // prawo

            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < 6; i++)
            {
                if (i % 3 == 0)
                {
                    GL.Color3(0.0f, 0.0f, 1.0f);
                }
                else if (i % 3 == 1)
                {
                    GL.Color3(0.0f, 1.0f, 0.0f);
                }
                else if (i % 3 == 2)
                {
                    GL.Color3(1.0f, 0.0f, 0.0f);
                }
                for (int j = 0; j < 4; j++)
                {
                    GL.Vertex3(bryla[i][j][0], bryla[i][j][1], bryla[i][j][2]);
                }
            }

            GL.End();

            if (depth == 12)
            {
                return;
            }

            w *= 2.0f;
            s *= 2.0f;
            d *= 2.0f;

            dx = (p4[0][0] + p4[1][0]) / 2;

            float dy1 = dy + (p4[1][1] - p4[3][1]);
            float dy2 = dy + (p4[0][1] - p4[2][1]);

            RysProstop(dx, dy2, dz, w, s, d, rot + 10, depth + 1);
            RysProstop(dx, dy1, dz, w, s, d, rot - 10, depth + 1);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.CornflowerBlue);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0, 0, wheel - 3.0f);
            GL.Rotate(obrx, 1, 0, 0);
            GL.Rotate(obry, 0, 1, 0);
            GL.Rotate(obrz, 0, 0, 1);

            // figury
            RysProstop(0, 0.25f, 0, 0.5f, 0.1f, 0.1f, 0, 0);

            GL.Flush();

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
        protected override void OnResize(EventArgs e)
        {
            // obszar renderingu - całe okno
            GL.Viewport(ClientRectangle);
            // wybór macierzy rzutowania
            GL.MatrixMode(MatrixMode.Projection);
            // macierz rzutowania = macierz jednostkowa
            GL.LoadIdentity();
            GL.Frustum(-5.0, 5.0, -5.0, 5.0, 1.0, 10.0);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
