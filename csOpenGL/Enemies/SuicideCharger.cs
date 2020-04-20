using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class SuicideCharger : Enemy
    {

        public SuicideCharger(int x, int y) : base(Enemies.SUICIDE_CHARGER_ENEMY_HEALTH, Enemies.SUICIDE_CHARGER_ENEMY_MANA, x, y, 44, 44, 0, Globals.TileSize, Globals.TileSize, Enemies.SUICIDE_CHARGER_ENEMY_SPEED, Enemies.SUICIDE_CHARGER_ENEMY_ATTACKPOINT, Enemies.SUICIDE_CHARGER_ENEMY_ATTACKSPEED, Enemies.SUICIDE_CHARGER_ENEMY_DAMAGE, "a Yetee", Enemies.SUICIDE_CHARGER_BLOCK)
        {
            attackAni = new Animation(0, 9, 10);
            idleAni = new Animation(0, 3, 10);
        }

        public override void AIMove(double delta)
        {
            Charge(delta);
        }

        public void Charge(double delta)
        {
            Player p = Globals.l.p;
            if(Globals.checkCol((int)p.x, (int)p.y, p.w, p.h, (int)x, (int)y, w, h))
            {
                for (int i = 0; i < 150; i++)
                {
                    float px = (float)(x + (Globals.Rng.NextDouble() * 2 - 1) * (Globals.TileSize));
                    float py = (float)(y + (Globals.Rng.NextDouble() * 2 - 1) * (Globals.TileSize));
                    float xd = px - x;
                    float yd = py - y;
                    double distance = Math.Sqrt(xd * xd + yd * yd);
                    if (distance <= Globals.TileSize)
                    {
                        Globals.l.Current.particles.Add(new Particle(px, py, 0, 0, Globals.TileSize / 2, Globals.TileSize / 2, 17, 0, 30, 1, 1, 1, true, new Animation(0, 11, 2.5)));
                    }
                }
                p.DealPhysicalDamage(damage, name, "Explosion", this, 2);
                Health = 0;
                Globals.rootActionLog.Death(this.name);
                Globals.l.Current.removables.Add(this);
            }
            else if(attacking)
            {
                if(Move(xDir, yDir))
                {
                    attacking = false;
                    attackTimer = 0;
                }
            }
            else
            {
                attackTimer += delta;
                if(attackTimer > attackSpeed)
                {
                    attacking = true;
                    float xd = p.x + p.w / 2 - x - w / 2;
                    float yd = p.y + p.h / 2 - y - h / 2;
                    float dis = (float)Math.Sqrt(xd * xd + yd * yd);
                    xDir = (float)(xd * speed / dis);
                    yDir = (float)(yd * speed / dis);
                }
            }
        }

    }
}
