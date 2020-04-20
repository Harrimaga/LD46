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
        public HordeBoss(Theme theme):base(25,25,theme)
        {
            for (int i = 0; i < 10;i++)
            {
                Random rng = new Random();
                enemies.Add(theme.GetEnemy(rng.Next(1, 24), rng.Next(1, 24)));
            }
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
    }
}
