using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class SuicideCharger : Enemy
    {

        public SuicideCharger(int x, int y) : base(Enemies.SUICIDE_CHARGER_ENEMY_HEALTH, Enemies.SUICIDE_CHARGER_ENEMY_MANA, x, y, 0, 3, 3, Globals.TileSize, Globals.TileSize, Enemies.SUICIDE_CHARGER_ENEMY_SPEED, Enemies.SUICIDE_CHARGER_ENEMY_ATTACKPOINT, Enemies.SUICIDE_CHARGER_ENEMY_ATTACKSPEED, Enemies.SUICIDE_CHARGER_ENEMY_DAMAGE, "Peter", Enemies.SUICIDE_CHARGER_BLOCK)
        {

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
