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
        private Hotkey attack = new Hotkey(false).AddKey(Key.Space);

        private Player p = new Player(128, 128, Balance.FIGHTER_BASE_ATTACK_POINT, Balance.FIGHTER_BASE_ATTACK_SPEED, Balance.FIGHTER_BASE_DAMAGE, Balance.FIGHTER_NAME);
        private Theme theme = new Theme("Basic");
        private List<DrawnButton> buttons = new List<DrawnButton>();

        public Game(Window window)
        {
            this.window = window;
            OnLoad();
        }

        public void OnLoad()
        {
            buttons.Add(new DrawnButton("Red Velvet", 500, 500, 200, 50, () => { Console.WriteLine("Button 1"); }));
            Globals.l = new Level(theme, p, 0);
        }

        public void Update(double delta)
        {
            //Updating logic
            if (left.IsDown()) p.SetDir(-1, 0);
            if (right.IsDown()) p.SetDir(1, 0);
            if (up.IsDown()) p.SetDir(0, -1);
            if (down.IsDown()) p.SetDir(0, 1);
            if (attack.IsDown()) p.a = true;

            Globals.l.Update(delta);
        }

        public void Draw()
        {
            //Do all you draw calls here
            Globals.l.Draw();
            Globals.rootActionLog.Draw();
            foreach (DrawnButton button in buttons)
            {
                button.Draw();
            }
            
        }

        public void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if(e.Button == MouseButton.Left)
            {
                foreach (DrawnButton button in buttons)
                {
                    if (button.IsInButton(mx, my))
                    {
                        button.OnClick();
                    }
                }
            }
        }

        public void MouseUp(MouseButtonEventArgs e, int mx, int my)
        {

        }
        
    }
}
