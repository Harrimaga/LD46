using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class PrayerCircle : Structure
    {
        public bool Prayed { get; set; }
        public double TimePrayed { get; set; }
        public double TimeToPray { get; set; }

        public PrayerCircle(int x, int y, Tile[,] tileGrid, Theme theme):base(1,1,x,y,tileGrid,theme)
        {
            Place(tileGrid);
            Prayed = false;
            TimePrayed = 0;
            TimeToPray = 240;
        }
        public override void Place(Tile[,] tileGrid)
        {
            tileGrid[X,Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.PRAYERCIRLE, 0);
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
            if ((int)(Globals.l.p.y / Globals.TileSize) == Y && (int)(Globals.l.p.x / Globals.TileSize) >= X && (int)(Globals.l.p.y / Globals.TileSize) <= X + Width && !Prayed)
            {
                TimePrayed += deltaTime;
                if(TimePrayed>TimeToPray)
                {
                    Prayed = true;
                    PrayerCircleBossRoom room = (PrayerCircleBossRoom)Globals.l.Current;
                    room.HasPrayed();
                }
            }
        }
    }
}
