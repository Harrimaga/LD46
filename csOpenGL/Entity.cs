using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public abstract class Entity
    {

        protected double MaxHealth, Health, PhysicalAmp, MagicalAmp, speed;
        protected int w, h;
        protected float x, y, rotation = 0, r = 1, g = 1, b = 1, a = 1;
        protected Sprite s;

        public void Init(double Health, float x, float y, int texNum, int spriteNum, int w, int h, double speed, double PhysicalAmp = 1, double MagicalAmp = 1)
        {
            this.MaxHealth = Health;
            this.Health = Health;
            this.PhysicalAmp = PhysicalAmp;
            this.MagicalAmp = MagicalAmp;
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.w = w;
            this.h = h;
            s = new Sprite(w, h, spriteNum, Window.texs[texNum]);
        }

        public virtual void Draw()
        {
            s.Draw(x, y, rotation, r, g, b, a);
        }

        public abstract void Update(double delta);

        public virtual bool DealPhysicalDamage(double damage)
        {
            Health -= damage;
            return Health < 0;
        }

        public virtual bool DealMagicDamage(double damage)
        {
            Health -= damage;
            return Health < 0;
        }

        public bool CheckCollision()
        {
            for(int i = (int)(x/Globals.TileSize); i < (int)(x / Globals.TileSize) + 2 + w/Globals.TileSize && i < Globals.l.Current.width; i++)
            {
                for(int j = (int)(y / Globals.TileSize); j < (int)(y / Globals.TileSize) + 2 + h / Globals.TileSize && j < Globals.l.Current.height; j++)
                {
                    //check bool
                }
            }
            return false;
        }

    }
}
