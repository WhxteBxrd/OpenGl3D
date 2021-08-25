using System;
using SFML.Window;
using SFML.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using System.Runtime.InteropServices;

namespace Project
{
    class Program 
    {
        static RenderWindow window;

        const int SW_HIDE = 0;
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private static void window_Closed(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
        static void Main(string[] args) 
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 10;

            GameWindowSettings gameWindowSettings = new GameWindowSettings();
            NativeWindowSettings nativeWindow = new NativeWindowSettings();
            nativeWindow.StartVisible = false;

            var GameWindow = new GameWindow(gameWindowSettings, nativeWindow);

            window = new RenderWindow(new VideoMode(480, 320), "Title", Styles.Default, settings);

            window.SetActive();
            window.Closed += new EventHandler(window_Closed);

            //
            GL.Viewport(0, 0, (int)480, (int)320);

            //
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);
                window_Draw();
                window.Display();
            }
        }

        static public void window_Draw()
        {
            GL.Rotate(+0.1f, 0f, 1f, 0);
            GL.Rotate(+0.1f, +1f, 0, +1f);

            float[] vertices =
            {
                //Front
                -0.3f,0.4f,0,
                0.3f,0.4f,0,

                0.3f,0.4f,0,
                0.3f,-0.4f,0,

                0.3f,-0.4f,0,
                -0.3f,-0.4f,0,

                -0.3f,0.4f,0,
                -0.3f,-0.4f,0,

                //Left
                -0.3f,0.4f,0,
                -0.3f,0.4f,-0.6f,

                 -0.3f,-0.4f,0,
                 -0.3f,-0.4f,-0.6f,

                //Right
                0.3f,-0.4f,0,
                0.3f,-0.4f,-0.6f,

                0.3f,0.4f,0,
                0.3f,0.4f,-0.6f,

                //Rear
                -0.3f,0.4f,-0.6f,
                0.3f,0.4f,-0.6f,

                0.3f,0.4f,-0.6f,
                0.3f,-0.4f,-0.6f,

                0.3f,-0.4f,-0.6f,
                -0.3f,-0.4f,-0.6f,

                -0.3f,0.4f,-0.6f,
                -0.3f,-0.4f,-0.6f,

                //Center intersection
                -0.3f,0.4f,0,
                0.3f,-0.4f,-0.6f,

                0.3f,0.4f,0,
                -0.3f,-0.4f,-0.6f,

                -0.3f,-0.4f,0,
                0.3f,0.4f,-0.6f,

                0.3f,-0.4f,0,
                -0.3f,0.4f,-0.6f,

                //Lateral intersection
                //Up
                -0.3f,0.4f,0,
                0.3f,0.4f,-0.6f,

                0.3f,0.4f,0,
                -0.3f,0.4f,-0.6f,
                //Dn
                -0.3f,-0.4f,0,
                0.3f,-0.4f,-0.6f,

                0.3f,-0.4f,0,
                -0.3f,-0.4f,-0.6f,
                //front
                -0.3f,0.4f,0,
                0.3f,-0.4f,0,

                -0.3f,-0.4f,0,
                 0.3f,0.4f,0,

                 //rear
                 -0.3f,0.4f,-0.6f,
                0.3f,-0.4f,-0.6f,

                -0.3f,-0.4f,-0.6f,
                 0.3f,0.4f,-0.6f,
                //left
                -0.3f,0.4f,0,
                -0.3f,-0.4f,-0.6f,

                -0.3f,-0.4f,0,
                -0.3f,0.4f,-0.6f,
                //right
                0.3f,0.4f,0,
                0.3f,-0.4f,-0.6f,

                0.3f,-0.4f,0,
                0.3f,0.4f,-0.6f
            };

            int BufferDate = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferDate);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferDate);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.DrawArrays(OpenTK.Graphics.OpenGL.PrimitiveType.Lines, 0, 100);
            GL.DisableVertexAttribArray(0);
        }
    }
}
