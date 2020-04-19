using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Game
    {

        public static Game game;

        public Window window;
        private Hotkey left = new Hotkey(true).AddKey(Key.A).AddKey(Key.Left);
        private Hotkey right = new Hotkey(true).AddKey(Key.D).AddKey(Key.Right);
        private Hotkey up = new Hotkey(true).AddKey(Key.W).AddKey(Key.Up);
        private Hotkey down = new Hotkey(true).AddKey(Key.S).AddKey(Key.Down);
        private Hotkey attack = new Hotkey(false).AddKey(Key.Space);
        private Hotkey num1 = new Hotkey(false).AddKey(Key.Number1);
        private Hotkey num2 = new Hotkey(false).AddKey(Key.Number2);
        private Hotkey num3 = new Hotkey(false).AddKey(Key.Number3);
        private Hotkey num4 = new Hotkey(false).AddKey(Key.Number4);
        private Hotkey num5 = new Hotkey(false).AddKey(Key.Number5);
        private Hotkey num6 = new Hotkey(false).AddKey(Key.Number6);
        private Hotkey pickUp = new Hotkey(false).AddKey(Key.Q);
        private Hotkey interact = new Hotkey(false).AddKey(Key.C);

        public List<DrawnButton> buttons = new List<DrawnButton>();
        private Player p;
        private Theme theme = new Theme("SpaceDark");

        public Game(Window window)
        {
            Game.game = this;
            this.window = window;
            OnLoad();
        }

        public void OnLoad()
        {
            //buttons.Add(new DrawnButton("Red Velvet", 500, 500, 200, 50, () => { Console.WriteLine("Button 1"); }));
            p = new Fighter(Globals.TileSize, Globals.TileSize);
            new Level(theme, p, 0);
        }

        public void Update(double delta)
        {
            //Updating logic
            if (p.Health > 0)
            {
                if (left.IsDown()) p.SetDir(-1, 0);
                if (right.IsDown()) p.SetDir(1, 0);
                if (up.IsDown()) p.SetDir(0, -1);
                if (down.IsDown()) p.SetDir(0, 1);
                if (attack.IsDown()) p.a = true;
                if (pickUp.IsDown()) Globals.l.Current.TryPickup();
                //Spell hotkeys
                if (num1.IsDown()) TrySpellAttack(0);
                if (num2.IsDown()) TrySpellAttack(1);
                if (num3.IsDown()) TrySpellAttack(2);
                if (num4.IsDown()) TrySpellAttack(3);
                if (num5.IsDown()) TrySpellAttack(4);
                if (num6.IsDown()) TrySpellAttack(5);
                if (interact.IsDown()) TryInteraction();

                Globals.l.Update(delta);
                if(p.Health <= 0)
                {
                    buttons.Clear();
                    buttons.Add(new DrawnButton("Restart", 760, 600, 400, 75, () => { Restart(); }));
                }
            }

        }

        private void TrySpellAttack(int spellSlot)
        {
            if(spellSlot < Globals.l.p.Spells.Count )
            {
                Spell spell = Globals.l.p.Spells[spellSlot];
                spell.Cast(Window.window.mouseX + Window.camX, Window.window.mouseY + Window.camY, Globals.l.Current.enemies, Globals.l.p);
            }
        }

        private void TryInteraction()
        {
            Tile t = Globals.l.Current.getTile((int)Globals.l.p.x/Globals.l.Current.tileSize, (int)Globals.l.p.y / Globals.l.Current.tileSize);
            if(t.GetTileType() == TileType.BUTTON)
            {
                Globals.l.Current.PressButton(Globals.l.p.x, Globals.l.p.y);
            }
        }

        public void Draw()
        {
            //Do all you draw calls here
            if (p.Health > 0)
            {
                Globals.l.Draw();
            }
            else
            {
                Window.window.DrawTextCentered("You died!", 960, 300);
            }
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
                for(int i = buttons.Count-1; i >= 0; i--)
                {
                    DrawnButton button = buttons[i];
                    if (button.IsInButton(mx, my))
                    {
                        button.OnClick();
                        break;
                    }
                }
            }
        }

        public void MouseUp(MouseButtonEventArgs e, int mx, int my)
        {

        }
        
        public void Restart()
        {
            buttons.Clear();
            p = new Fighter(Globals.TileSize, Globals.TileSize);
            new Level(theme, p, Globals.l.Rng.Next());
        }

    }
}
