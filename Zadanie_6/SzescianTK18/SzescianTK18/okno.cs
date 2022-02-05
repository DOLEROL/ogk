using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace SzescianTK18
{
    class Okno : GameWindow
    {
        int obrx = 0;
        int obry = 0;
        int obrz = 0;
        int VertexBufferObject;
        public Okno(int width, int height)
            : base(width, height, GraphicsMode.Default, "Przykład 3")
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

        void RysProstop(float dx, float dy, float dz, float w, float s, float d)
        {
            w /= 2.0f;
            s /= 2.0f;
            d /= 2.0f;

            float[] ws = { 
                dx - s, dy + w, dz - d,
                dx - s, dy + w, dz + d,
                dx + s, dy + w, dz - d,
                dx + s, dy + w, dz + d,
                dx - s, dy - w, dz - d,
                dx - s, dy - w, dz + d,
                dx + s, dy - w, dz - d,
                dx + s, dy - w, dz + d,
            };

            float[][] p = new float[8][];

            for (int i = 0; i < ws.Length / 3; i++)
            {
                p[i] = new float[3] { ws[i * 3], ws[i * 3 + 1], ws[i * 3 + 2] };
            }

            float[][][] bryla = new float[6][][];

            bryla[0] = new float[4][] { p[1], p[0], p[2], p[3] };
            bryla[1] = new float[4][] { p[1], p[3], p[7], p[5] };
            bryla[2] = new float[4][] { p[2], p[6], p[7], p[3] };
            bryla[3] = new float[4][] { p[5], p[4], p[6], p[7] };
            bryla[4] = new float[4][] { p[0], p[2], p[6], p[4] }; 
            bryla[5] = new float[4][] { p[0], p[4], p[5], p[1] };

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
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(Color.CornflowerBlue);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0, 0, -4.5);
            GL.Rotate(obrx, 1, 0, 0);
            GL.Rotate(obry, 0, 1, 0);
            GL.Rotate(obrz, 0, 0, 1);

            RysProstop(1, 1, 1, 2, 3, 4);
            RysProstop(-1, -1, -1, 2, 3, 4);
            RysProstop(-2, 0, 1, 1, 1, 1);

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
