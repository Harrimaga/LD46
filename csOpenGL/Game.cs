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
        private Hotkey num7 = new Hotkey(false).AddKey(Key.Z);
        private Hotkey num8 = new Hotkey(false).AddKey(Key.X);
        private Hotkey num9 = new Hotkey(false).AddKey(Key.C);
        private Hotkey num0 = new Hotkey(false).AddKey(Key.V);
        private Hotkey numm = new Hotkey(false).AddKey(Key.B);
        private Hotkey nump = new Hotkey(false).AddKey(Key.N);
        private Hotkey pickUp = new Hotkey(false).AddKey(Key.R);
        private Hotkey interact = new Hotkey(false).AddKey(Key.R);
        private int Seed = 5;
        private GameState gameState = GameState.PLAYING;

        public List<DrawnButton> buttons = new List<DrawnButton>();
        private Player p;
        private int LevelsPlayed { get; set; }

        public Game(Window window)
        {
            Game.game = this;
            this.window = window;
            OnLoad();
        }

        public void OnLoad()
        {
            //p = new Fighter(Globals.TileSize, Globals.TileSize);
            //new Level(Globals.Themes[Globals.Rng.Next(Globals.Themes.Count)], p, Seed);
            ToMainMenu();
            LevelsPlayed = 0;
        }

        public void Update(double delta)
        {
            //Updating logic
            switch (gameState)
            {
                case GameState.NEXTLEVEL:
                    if (pickUp.IsDown()) Globals.l.Current.TryPickup();
                    break;
                case GameState.PLAYING:
                    if (Globals.Boss.Health == 0)
                    {
                        gameState = GameState.DEAD;
                        Globals.rootActionLog.BossDeath();
                        buttons.Add(new DrawnButton("Restart", 760, 600, 400, 75, () => { ToMainMenu(); }));
                        return;
                    };
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
                    if (num7.IsDown()) TryItemAttack(0);
                    if (num8.IsDown()) TryItemAttack(1);
                    if (num9.IsDown()) TryItemAttack(2);
                    if (num0.IsDown()) TryItemAttack(3);
                    if (numm.IsDown()) TryItemAttack(4);
                    if (nump.IsDown()) TryItemAttack(5);
                    if (interact.IsDown()) TryInteraction();

                    Globals.l.Update(delta);
                    if (p.Health <= 0)
                    {
                        gameState = GameState.DEAD;
                        buttons.Clear();
                        buttons.Add(new DrawnButton("Restart", 760, 600, 400, 75, () => { ToMainMenu(); }));
                    }
                    break;
            }
        }

        private void TrySpellAttack(int spellSlot)
        {
            if (spellSlot < Globals.l.p.Spells.Count)
            {
                Spell spell = Globals.l.p.Spells[spellSlot];
                spell.Cast(Window.window.mouseX + Window.camX, Window.window.mouseY + Window.camY, Globals.l.Current.enemies, Globals.l.p);
            }
        }

        private void TryItemAttack(int itemSlot)
        {
            if (itemSlot < Globals.l.p.items.Count)
            {
                Item item = Globals.l.p.items[itemSlot];
                if(item.UseItem(Window.window.mouseX + Window.camX, Window.window.mouseY + Window.camY, Globals.l.Current.enemies, Globals.l.p))
                {
                    Globals.l.p.DequipItem(item, false);
                }
            }
        }

        private void TryInteraction()
        {
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    Tile t = Globals.l.Current.getTile(i + (int)Globals.l.p.x / Globals.l.Current.tileSize, j + (int)Globals.l.p.y / Globals.l.Current.tileSize);
                    if (t.GetTileType() == TileType.BUTTON)
                    {
                        Globals.l.Current.PressButton(i*Globals.TileSize + Globals.l.p.x, j * Globals.TileSize + Globals.l.p.y);
                        return;
                    }
                    else if (t.GetTileType() == TileType.STAIRS)
                    {
                        if (++LevelsPlayed >= 4)
                        {
                            gameState = GameState.WON;
                            buttons.Clear();
                            buttons.Add(new DrawnButton("Restart", 760, 600, 400, 75, () => { ToMainMenu(); }));
                            Globals.rootActionLog.Add("You have won the game");
                        }
                        else
                        {
                            gameState = GameState.NEXTLEVEL;
                            buttons.Add(new DrawnButton("Full heal", 760, 400, 400, 75, () => { FullHeal(); }));
                            for(int k = 0; k < 4; k++)
                            {
                                int rn = Globals.Rng.Next(Globals.l.Current.chanceTotal);
                                Item it = null;
                                foreach (Itemchances item in Globals.l.Current.itemDrops)
                                {
                                    if (rn < item.chance)
                                    {
                                        it = item.make();
                                        break;
                                    }
                                }
                                if (it != null)
                                {
                                    buttons.Add(new DrawnButton("", 770 + k * 100, 500, 80, 80, () => { GiveItem(it); }, it.Sprite.texture));
                                }
                                
                            }
                        }
                        return;
                    }
                }
            }
        }

        private void FullHeal()
        {
            p.Health = p.MaxHealth;
            p.Mana = p.MaxMana;
            p.CurrentBlock = p.StandardBlock;
            GotoNextLevel();
        }

        private void GiveItem(Item i)
        {
            if(Globals.l.p.items.Count < 6)
            {
                Globals.l.p.EquipItem(i);
                GotoNextLevel();
            }
        }

        private void GotoNextLevel()
        {
            Globals.l = new Level(Globals.Themes[Globals.Rng.Next(Globals.Themes.Count)], Globals.l.p, Globals.Rng.Next());
            p.x = Globals.TileSize;
            p.y = Globals.TileSize;
            gameState = GameState.PLAYING;
            buttons.Clear();
            p.ReAddButtons();
            if (Globals.musicPlaying != MusicPlaying.NORMAL)
            {
                Globals.musicPlaying = MusicPlaying.NORMAL;
                SoundManager.PlayMusic("Sound/Track17.wav");
            }
        }

        public void Draw()
        {
            //Do all you draw calls here
            switch (gameState)
            {
                case GameState.PLAYING:
                    Globals.l.Draw();
                    Window.window.DrawText("Level: " + (int)(LevelsPlayed + 1), 1725, 830);
                    Globals.rootActionLog.Draw();
                    break;
                case GameState.DEAD:
                    Window.window.DrawTextCentered("You lost!", 960, 300);
                    Globals.rootActionLog.Draw();
                    break;
                case GameState.WON:
                    Window.window.DrawTextCentered("You won!", 960, 300);
                    Globals.rootActionLog.Draw();
                    break;
                case GameState.NEXTLEVEL:
                    Window.window.DrawTextCentered("Choose a bonus", 960, 300);
                    p.DrawUI();
                    Globals.rootActionLog.Draw();
                    break;
                case GameState.MAINMENU:
                    Window.window.DrawTextCentered("Choose your class:", 960, 300);
                    Window.window.DrawTextCentered("W,A,S,D = Move     SPACE = Attack     1-6 = Use Spell     R = Pick up item / Interact     Z-N = using items", 960, 800, Globals.buttonFont);
                    Window.window.DrawTextCentered("Complete the dungeon, but keep the Boss Alive! He is just a friendly guy. (He can attack you though)", 960, 900, Globals.buttonFont);
                    Window.window.DrawTextCentered("Tutorial by: Tim", 960, 1000, Globals.buttonFont);
                    break;
            }
            foreach (DrawnButton button in buttons)
            {
                button.Draw();
            }

        }

        public void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if (e.Button == MouseButton.Left)
            {
                for (int i = buttons.Count - 1; i >= 0; i--)
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

        public void ToMainMenu()
        {
            buttons.Clear();
            gameState = GameState.MAINMENU;
            buttons.Add(new DrawnButton("Fighter", 760, 400, 400, 75, () => { Restart(new Fighter(Globals.TileSize, Globals.TileSize)); }));
            buttons.Add(new DrawnButton("Mage", 760, 500, 400, 75, () => { Restart(new Mage(Globals.TileSize, Globals.TileSize)); }));
        }

        public void Restart(Player pp)
        {
            buttons.Clear();
            pp.ReAddButtons();
            p = pp;
            Globals.PossibleBosses = new List<Enemy> { new Hirrathak(), new Pylonius(), new Twaelf(), new Yureon() };
            new Level(Globals.Themes[Globals.Rng.Next(Globals.Themes.Count)], p, Globals.Rng.Next());
            gameState = GameState.PLAYING;
            LevelsPlayed = 0;
            if(Globals.musicPlaying != MusicPlaying.NORMAL)
            {
                Globals.musicPlaying = MusicPlaying.NORMAL;
                SoundManager.PlayMusic("Sound/Track17.wav");
            }
        }
    }
}