using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using QuickFont;
using SharpFont;

namespace LD46
{

    public class DrawList
    {

        private SData[] data;
        private int size = 0, max = 10;

        public DrawList()
        {
            data = new SData[max];
        }

        public void Add(SData s)
        {
            if (size == max)
            {
                SData[] n = new SData[max * 2];
                for (int i = 0; i < max; i++)
                {
                    n[i] = data[i];
                }
                data = n;
                max *= 2;
            }
            data[size] = s;
            size++;
        }

        public SData[] getData()
        {
            return data;
        }

        public void Clear()
        {
            size = 0;
        }

        public int Count()
        {
            return size;
        }

    }

    public class Window : GameWindow
    {
        public static DrawList sd;
        public static List<Texture> texs = new List<Texture>();
        public static float screenScaleX, screenScaleY, camX = 0, camY = 0;
        public static Window window;

        private Shader shader;
        private int vao, vbo, ssbo;
        QFont font;
        QFontDrawing textDrawing;
        float[] data = {
                0.0f, 1.0f, 0,
                1.0f, 0.0f, 0,
                0.0f, 0.0f, 0,
                1.0f, 1.0f, 0,
                0.0f, 1.0f, 0,
                1.0f, 0.0f, 0
            };
        private double delta;
        private Game game;
        public int mouseX, mouseY;

        public Window(int w, int h, string title) : base(w, h, GraphicsMode.Default, title)
        {
            Window.sd = new DrawList();
            Window.window = this;
        }

        protected override void OnLoad(EventArgs e)
        {
            WindowBorder = WindowBorder.Hidden;
            WindowState = WindowState.Fullscreen;

            GL.ClearColor(0.05f, 0.05f, 0.05f, 1.0f);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            string s = GL.GetString(StringName.Vendor);
            if (GL.GetString(StringName.Vendor).Equals("ATI Technologies Inc."))
            {
                shader = new Shader("Shaders/vsAMD.glsl", "Shaders/fsAMD.glsl");
            }
            else
            {
                shader = new Shader("Shaders/vs.glsl", "Shaders/fs.glsl");
            }

            vbo = GL.GenBuffer();
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            ssbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, ssbo);
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, ssbo);

            //Font
            font = new QFont("Fonts/arial.ttf", 36 * Height / 1080.0f, new QuickFont.Configuration.QFontBuilderConfiguration(true));
            textDrawing = new QFontDrawing();
            Matrix4 m = Matrix4.Identity;
            m.M11 /= (float)(1920.0 / 2);
            m.M22 /= (float)(1080.0 / 2);
            textDrawing.ProjectionMatrix = m;

            //Textures
            Window.texs.Add(new Texture("Textures/Test.png", 16, 16, 8, 8));
            

            game = new Game(this);
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            Window.screenScaleX = Width / 1920.0f;
            Window.screenScaleY = Height / 1080.0f;
            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            delta = e.Time * 60;
            if(delta > 4)
            {
                delta = 4;
            }
            Update();
            foreach (Texture t in Window.texs)
            {
                GL.Arb.MakeImageHandleResident(t.Handle, All.ReadOnly);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Viewport(0, 0, Width, Height);
            shader.Use();
            SData[] sdd = Window.sd.getData();
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, ssbo);
            GL.BufferData<SData>(BufferTarget.ShaderStorageBuffer, (sizeof(int) * 4 + 2 * sizeof(long) + 9 * sizeof(float)) * Window.sd.Count(), sdd, BufferUsageHint.DynamicDraw);
            GL.Uniform2(GL.GetUniformLocation(shader.Handle, "screenSize"), Width, Height);
            GL.BindVertexArray(vao);
            GL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);
            GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, 6, Window.sd.Count());
            GL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);
            GL.UseProgram(0);

            textDrawing.RefreshBuffers();
            textDrawing.Draw();

            Context.SwapBuffers();

            Window.sd.Clear();
            foreach (Texture t in Window.texs)
            {
                GL.Arb.MakeImageHandleNonResident(t.Handle);
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            game.MouseDown(e, mouseX, mouseY);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            game.MouseUp(e, mouseX, mouseY);
            base.OnMouseDown(e);
        }

        private void Update()
        {
            //input
            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            Hotkey.Update(input);
            //other shit
            textDrawing.DrawingPrimitives.Clear();
            game.Update(delta);
            game.Draw();
            SoundManager.Update();
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            mouseX = (int)(e.X / screenScaleX);
            mouseY = (int)(e.Y / screenScaleX);
            base.OnMouseMove(e);
        }

        public void DrawText(string text, int x, int y, QFont f = null)
        {
            if (f == null)
            {
                f = font;
            }
            textDrawing.Print(f, text, new Vector3(x - 960, 540 - y, 0), QFontAlignment.Left);
        }

        public void DrawText(string text, int x, int y, float r, float g, float b, float a, QFont f = null)
        {
            if (f == null)
            {
                f = font;
            }
            QFontRenderOptions opt = new QFontRenderOptions() { Colour = Color.FromArgb(new Color4(r, g, b, a).ToArgb()) };
            textDrawing.Print(f, text, new Vector3(x - 960, 540 - y, 0), QFontAlignment.Left, opt);
        }

        public void DrawTextCentered(string text, int x, int y, QFont f = null)
        {
            if (f == null)
            {
                f = font;
            }
            textDrawing.Print(f, text, new Vector3(x - 960, 540 - y, 0), QFontAlignment.Centre);
        }

        public void DrawTextCentered(string text, int x, int y, float r, float g, float b, float a, QFont f = null)
        {
            if (f == null)
            {
                f = font;
            }
            QFontRenderOptions opt = new QFontRenderOptions() { Colour = Color.FromArgb(new Color4(r, g, b, a).ToArgb()) };
            textDrawing.Print(f, text, new Vector3(x - 960, 540 - y, 0), QFontAlignment.Centre, opt);
        }

    }
}
