﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class ButtonClickBoss : Room
    {
        public int ButtonCLicksNeeded { get; set; }

        public ButtonClickBoss(Theme theme) : base(25, 25, theme)
        {
            ButtonCLicksNeeded = 4;
            enemies.Clear();
            enemies.Add(theme.GetBoss());
            Random rng = new Random();
            for (int i = 0; i < 4; i++)
            {
                int randX = rng.Next(1, 25);
                int randY = rng.Next(1, 25);
                tileGrid[randX, randY] = new Tile(new Sprite(tileSize, tileSize, 0, theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.BUTTON);
            }
        }

        public override void PressButton(float px, float py)
        {
            tileGrid[(int)px / tileSize, (int)py / tileSize] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.TILE);
            if (--ButtonCLicksNeeded < 1)
            {
                Random rng = new Random();
                tileGrid[rng.Next(1, 25), rng.Next(1, 25)] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS);
                Globals.rootActionLog.Add("You have finished this boss, stairs have appeared");
            }
        }
    }
}
