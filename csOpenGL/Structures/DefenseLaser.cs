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

        public double damageTimer;
        public double damageTimerBase;

        private List<Sprite> sprites;

        private Animation ani;

        public DefenseLaser(int x, int y, Tile[,] tileGrid, Theme theme, int width) : base(width, 1, x, y, tileGrid, theme)
        {
            Interval = 100;
            InternalTimer = 0;
            damageTimer = 0;
            damageTimerBase = 1 * 60;
            ani = new Animation(0, 15, 10);
            sprites = new List<Sprite>();

            Place(tileGrid);
        }

        public override void Place(Tile[,] tileGrid)
        {
            for(int i = X; i<X+Width;i++)
            {
                if (i == X)
                {
                    sprites.Add(new Sprite(Globals.TileSize, Globals.TileSize, 0, Window.texs[22]));
                }
                else if (i == X + Width - 1)
                {
                    sprites.Add(new Sprite(Globals.TileSize, Globals.TileSize, 0, Window.texs[24]));
                }
                else
                {
                    sprites.Add(new Sprite(Globals.TileSize, Globals.TileSize, 0, Window.texs[23]));
                }
                tileGrid[i, Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.TILE, 0); //Should be an animated laser
            }
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
            InternalTimer += deltaTime;
            damageTimer += deltaTime;
            if(InternalTimer > Interval)
            {
                active = !active;
                InternalTimer = 0;
            }

            if (damageTimer > damageTimerBase)
            {
                if (active)
                {
                    OnTrigger();
                    damageTimer = 0;
                }
            }

            if (active)
            {
                foreach (Sprite sprite in sprites)
                {
                    ani.Update(sprite, deltaTime);
                }
            }
        }

        public override void Draw()
        {
            if (active)
            {
                for (int i = 0; i < sprites.Count; i++)
                {
                    sprites[i].Draw(X * Globals.TileSize + i * Globals.TileSize, Y * Globals.TileSize);
                }
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
