using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class PrayerCircleBossRoom : Room
    {
        public int CirclesToGo { get; set; }

        public PrayerCircleBossRoom(Theme theme) : base(25, 25, theme)
        {
            CirclesToGo = 2;
            enemies.Clear();
            Globals.Boss = theme.GetBoss();
            enemies.Add(Globals.Boss);
            Random rng = new Random();
            for (int i = 0; i < 2; i++)
            {
                int randX = rng.Next(1, 24);
                int randY = rng.Next(1, 24);
                Structures.Add(new PrayerCircle(randX, randY, tileGrid, theme));
            }
        }

        public void HasPrayed()
        {
            if(--CirclesToGo < 1)
            {
                Random rng = new Random();
                tileGrid[rng.Next(1, 24), rng.Next(1, 24)] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                Globals.rootActionLog.Add("You have finished this boss, stairs have appeared");
            }
            Globals.rootActionLog.Add("You have prayed");
        }
    }
}
