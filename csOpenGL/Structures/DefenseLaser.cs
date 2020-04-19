using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class DefenseLaser : Structure
    {
        public double Interval { get; set; }
        private double InternalTimer { get; set; }
        public DefenseLaser(int x, int y, Tile[,] tileGrid, Theme theme, int width) : base(width, 1, x, y, tileGrid, theme)
        {
            Interval = 100;
            InternalTimer = 0;

            Place(tileGrid);
        }

        public override void Place(Tile[,] tileGrid)
        {
            for(int i = X; i<X+Width;i++)
            {
                tileGrid[i, Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.TILE, 0); //Should be an animated laser
            }
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
            InternalTimer += deltaTime;
            if(InternalTimer > Interval)
            {
                OnTrigger();
                InternalTimer -= Interval;
            }
        }

        public override void OnTrigger()
        {
            base.OnTrigger();
            if ((int)(Globals.l.p.y / Globals.TileSize) == Y && (int)(Globals.l.p.x / Globals.TileSize) >= X && (int)(Globals.l.p.y / Globals.TileSize) <= X + Width)
            {
                Globals.l.p.DealMagicDamage(25, "Defense system", "a laser");
            }
        }
    }
}
