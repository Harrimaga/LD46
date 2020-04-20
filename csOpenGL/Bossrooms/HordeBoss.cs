using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class HordeBoss : Room
    {
        bool Finished { get; set; }
        public HordeBoss(Theme theme) : base(25, 25, theme)
        {
            isBossRoom = true;

            for (int i = 0; i < 10; i++)
            {
                Random rng = new Random();
                enemies.Add(theme.GetEnemy(rng.Next(2, 23)*Globals.TileSize, rng.Next(2, 23) * Globals.TileSize));
            }
            for (int i = 0; i < 5; i++)
            {
                int odds = Globals.Rng.Next(0, 1000);
                if (odds < 150)
                {
                    if (width >= 7 && height >= 5)
                    {
                        int structW = 3;
                        int structH = 1;
                        Structures.Add(new Wall(Globals.Rng.Next(2, width - 1 - structW), Globals.Rng.Next(2, height - 1 - structH), tileGrid, theme, true));
                    }
                }
                else if (odds < 300)
                {
                    if (width >= 5 && height >= 7)
                    {
                        int structW = 1;
                        int structH = 3;
                        Structures.Add(new Wall(Globals.Rng.Next(2, width - 1 - structW), Globals.Rng.Next(2, height - 1 - structH), tileGrid, theme, false));
                    }
                }
                else if (odds < 580)
                {
                    if (width >= 5 && height >= 5)
                    {
                        int structH = 1;
                        Structures.Add(new DefenseLaser(1, Globals.Rng.Next(2, height - 1 - structH), tileGrid, theme, width - 2));
                    }
                }
            }
            Globals.Boss = theme.GetBoss();
            enemies.Add(Globals.Boss);
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            if(enemies.Count<=1 && !Finished)
            {
                Finished = true;
                Random rng = new Random();
                tileGrid[rng.Next(1, 24), rng.Next(1, 24)] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                Globals.rootActionLog.Add("You have finished this boss, stairs have appeared");
            }
        }

        public override void Draw(float x, float y)
        {
            base.Draw(x, y);
            Window.window.DrawTextCentered("Defeat all enemies except for the boss to advance to the next floor!", 860, 0, Globals.buttonFont);
        }
    }
}
