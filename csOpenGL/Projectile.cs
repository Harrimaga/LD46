using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Projectile
    {

        public float x, y, xSpeed, ySpeed;
        public double damage;
        public bool isMagic;
        public Entity Shooter;
        public Sprite s;
        public int w, h, pierce;
        public string pname;

        public Projectile(float x, float y, float xSpeed, float ySpeed, double damage, bool isMagic, Entity shooter, int tNum, int sNum, int w, int h, string pname, int pierce = 1, Animation ani = null)
        {
            this.x = x;
            this.y = y;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.damage = damage;
            this.isMagic = isMagic;
            this.pname = pname;
            this.w = w;
            this.h = h;
            this.pierce = pierce;
            Shooter = shooter;
            s = new Sprite(w, h, sNum, Window.texs[tNum]);
        }

        public void Draw()
        {
            s.Draw(x, y, true, (float)Math.Atan2(xSpeed, -ySpeed));
        }

        public bool Update(double delta)
        {
            x += (float)(xSpeed * delta);
            y += (float)(ySpeed * delta);
            for (int i = (int)(x / Globals.TileSize); i < (int)(x / Globals.TileSize) + 2 + w / Globals.TileSize && i < Globals.l.Current.width && i > -1; i++)
            {
                for (int j = (int)(y / Globals.TileSize); j < (int)(y / Globals.TileSize) + 2 + h / Globals.TileSize && j < Globals.l.Current.height && j > -1; j++)
                {
                    Tile t = Globals.l.Current.getTile(i, j);
                    if (t.GetWalkable() == Walkable.SOLID)
                    {
                        if (Globals.checkCol((int)x, (int)y, w, h, i * Globals.TileSize, j * Globals.TileSize, Globals.TileSize, Globals.TileSize))
                        {
                            return true;
                        }
                    }
                }
            }
            if (Globals.l.p != Shooter && Globals.checkCol((int)x, (int)y, w, h, (int)Globals.l.p.x, (int)Globals.l.p.y, Globals.l.p.w, Globals.l.p.h))
            {
                if(hit(Globals.l.p))
                {
                    return true;
                }
            }
            foreach(Enemy e in Globals.l.Current.enemies)
            {
                if (Shooter != e && Globals.checkCol((int)x, (int)y, w, h, (int)e.x, (int)e.y, e.w, e.h))
                {
                    if (hit(e))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool hit(Entity e)
        {
            if (isMagic)
            {
                e.DealMagicDamage(damage * Shooter.MagicalAmp, Shooter.name, pname);
            }
            else
            {
                e.DealPhysicalDamage(damage * Shooter.PhysicalAmp, Shooter.name, pname);
            }
            pierce--;
            return pierce <= 0;
        }

    }
}
