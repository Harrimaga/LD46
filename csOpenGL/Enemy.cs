using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Enemy : Entity
    {

        public double attackTimer = 0, attackSpeed = 0, attackPoint = 0, damage;
        public bool attacked = false, attacking = false;
        public string name;

        public Enemy(double Health, float x, float y, int texNum, int spriteNum, int w, int h, double speed, double attackPoint, double attackSpeed, double damage, string name, double PhysicalAmp = 1, double MagicalAmp = 1)
        {
            Init(Health, x, y, texNum, spriteNum, w, h, speed, PhysicalAmp, MagicalAmp);
            this.attackSpeed = attackSpeed;
            this.attackPoint = attackPoint;
            this.damage = damage;
            this.name = name;
        }

        public override void Update(double delta)
        {
            AIMove(delta);
            base.Update(delta);
        }

        public virtual void AIMove(double delta)
        {

        }

        public void StupidMovement(double delta)
        {
            Player p = Globals.l.p;
            float xd = p.x + p.w / 2 - x - w / 2;
            float yd = p.y + p.h / 2 - y - h / 2;
            float dis = (float)Math.Sqrt(xd * xd + yd * yd);
            if (dis < 1000 && dis > Globals.TileSize)
            {
                xd /= dis;
                yd /= dis;
                xDir = xd;
                yDir = yd;
                Move((float)(xd * delta * speed), (float)(yd * delta * speed));
            }
        }

        public void BasicMeleeAttack(double delta)
        {
            Player p = Globals.l.p;
            float xd = p.x + p.w / 2 - x - w / 2;
            float yd = p.y + p.h / 2 - y - h / 2;
            float dis = (float)Math.Sqrt(xd * xd + yd * yd);
            if(dis <= (p.w/2 + w/2)*1.2)
            {
                if(attacking)
                {
                    attackTimer += delta;
                    if(!attacked && attackTimer > attackSpeed*attackPoint)
                    {
                        p.DealPhysicalDamage(damage, name, "their Fist");
                        attacked = true;
                    }
                    else if(attackTimer > attackSpeed)
                    {
                        attacking = false;
                    }
                }
                else
                {
                    attackTimer = 0;
                    attacking = true;
                    attacked = false;
                }
            }
            else
            {
                attacking = false;
            }
        }


    }
}
