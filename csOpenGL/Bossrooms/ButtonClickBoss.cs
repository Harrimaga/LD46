using System;
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
            ButtonCLicksNeeded = 6;
            enemies.Clear();
            Globals.Boss = theme.GetBoss();
            enemies.Add(Globals.Boss);
            Random rng = new Random();
            for (int i = 0; i < 6; i++)
            {
                int randX = rng.Next(1, 24);
                int randY = rng.Next(1, 24);
                if (tileGrid[randX, randY].GetWalkable() == Walkable.WALKABLE)
                {
                    tileGrid[randX, randY] = new Tile(new Sprite(tileSize, tileSize, 0, theme.GetTextureByType(TileType.BUTTON)), Walkable.WALKABLE, TileType.BUTTON, 0);
                }
                else
                {
                    i--;
                }
            }
        }

        public override void PressButton(float px, float py)
        {
            tileGrid[(int)px / tileSize, (int)py / tileSize] = new Tile(new Sprite(tileSize, tileSize, 1, Theme.GetTextureByType(TileType.BUTTON)), Walkable.WALKABLE, TileType.TILE, 0);
            if (--ButtonCLicksNeeded < 1)
            {
                Random rng = new Random();
                tileGrid[rng.Next(1, 24), rng.Next(1, 24)] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                Globals.rootActionLog.Add("You have finished this boss, stairs have appeared");
            }
        }

        public override void DrawOnMinimap(int x, int y, float cc)
        {
            base.DrawOnMinimap(x, y, 0.5f, 0.5f, 0);
        }
    }
}
