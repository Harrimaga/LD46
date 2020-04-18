﻿using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Game
    {

        public Window window;
        private Hotkey left = new Hotkey(true).AddKey(Key.A).AddKey(Key.Left);
        private Hotkey right = new Hotkey(true).AddKey(Key.D).AddKey(Key.Right);
        private Hotkey up = new Hotkey(true).AddKey(Key.W).AddKey(Key.Up);
        private Hotkey down = new Hotkey(true).AddKey(Key.S).AddKey(Key.Down);

        private Player p = new Player(128, 128);
        private Enemy e = new Enemy(1, 1000, 50, 0, 2, 128, 128, 5);
        private Theme theme = new Theme("Basic");

        public Game(Window window)
        {
            this.window = window;
            OnLoad();
        }

        public void OnLoad()
        {
            Globals.l = new Level(theme);
        }

        public void Update(double delta)
        {
            //Updating logic
            if (left.IsDown()) p.SetDir(-1, 0);
            if (right.IsDown()) p.SetDir(1, 0);
            if (up.IsDown()) p.SetDir(0, -1);
            if (down.IsDown()) p.SetDir(0, 1);

            p.Update(delta);

        }

        public void Draw()
        {
            //Do all you draw calls here
            Globals.l.Current.Draw(0, 0);
            p.Draw();
            e.Draw();
        }

        public void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            DrawnButton drawnButton = new DrawnButton(0,0,800,800, () => { Console.WriteLine(mx + "-" + my); });
            if(drawnButton.IsInButton(mx, my))
            {
                drawnButton.OnClick();
            }
        }

        public void MouseUp(MouseButtonEventArgs e, int mx, int my)
        {

        }
        
    }
}
