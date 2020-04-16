using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Game
    {

        public Window window;
        private Hotkey left = new Hotkey(true).AddKey(Key.A).AddKey(Key.Left);
        private Hotkey right = new Hotkey(true).AddKey(Key.D).AddKey(Key.Right);
        private Hotkey up = new Hotkey(true).AddKey(Key.W).AddKey(Key.Up);
        private Hotkey down = new Hotkey(true).AddKey(Key.S).AddKey(Key.Down);

        private Sprite s = new Sprite(64, 64, 1, Window.texs[0]);

        public Game(Window window)
        {
            this.window = window;
            OnLoad();
        }

        public void OnLoad()
        {
            
        }

        public void Update(double delta)
        {
            //Updating logic
            if (left.IsDown()) Window.camX -= (float)(10 * delta);
            if (right.IsDown()) Window.camX += (float)(10 * delta);
            if (up.IsDown()) Window.camY -= (float)(10 * delta);
            if (down.IsDown()) Window.camY += (float)(10 * delta);

        }

        public void Draw()
        {
            //Do all you draw calls here
            s.Draw(0, 0);
            window.DrawText("test", 0, 0);
        }

        public void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {

        }

        public void MouseUp(MouseButtonEventArgs e, int mx, int my)
        {

        }
        
    }
}
