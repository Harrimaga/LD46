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
        public int maxDistance = 1000, minDistance = Globals.TileSize, range = 600;

        public Enemy(double Health, double Mana, float x, float y, int texNum, int attackTexNum, int spriteNum, int w, int h, double speed, double attackPoint, double attackSpeed, double damage, string name, double PhysicalAmp = 1, double MagicalAmp = 1)
        {
            Init(Health, Mana, x, y, texNum, attackTexNum, spriteNum, w, h, speed, 1, PhysicalAmp, MagicalAmp);
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
            if (dis < maxDistance && dis > minDistance)
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
            if (dis <= (p.w / 2 + w / 2) * 1.2)
            {
                if (attacking)
                {
                    attackTimer += delta;
                    if (!attacked && attackTimer > attackSpeed * attackPoint)
                    {
                        p.DealPhysicalDamage(damage, name, "their Fist");
                        attacked = true;
                        s = baseAnimation;
                        ani = new Animation(0, 3, 10);
                    }
                    else if (attackTimer > attackSpeed)
                    {
                        attacking = false;
                        s = baseAnimation;
                        ani = new Animation(0, 3, 10);
                    }
                }
                else
                {
                    s = attack;
                    ani = new Animation(0, 9, attackSpeed / 10);
                    attackTimer = 0;
                    attacking = true;
                    attacked = false;
                }
            }
            else
            {
                attacking = false;
                s = baseAnimation;
                ani = new Animation(0, 3, 10);
            }
        }
        public void BasicRangedAttack(double delta)
        {
            Player p = Globals.l.p;

            float xd = p.x + p.w / 2 - x - w / 2;
            float yd = p.y + p.h / 2 - y - h / 2;
            float dis = (float)Math.Sqrt(xd * xd + yd * yd);
            if (attacking)
            {
                attackTimer += delta;
                if (!attacked && attackTimer > attackSpeed * attackPoint)
                {
                    xd = p.x + p.w / 2 - x - w / 2 + (float)(100 * accuracy * (Globals.l.Rng.NextDouble() - 0.5));
                    yd = p.y + p.h / 2 - y - h / 2 + (float)(100 * accuracy * (Globals.l.Rng.NextDouble() - 0.5));
                    dis = (float)Math.Sqrt(xd * xd + yd * yd);
                    xd /= dis;
                    yd /= dis;
                    Globals.l.Current.projectiles.Add(new Projectile(x + w / 2 - Globals.TileSize / 4, y + h / 2 - Globals.TileSize / 4, xd * 12.5f / 100, yd * 12.5f / 100, damage, false, this, 5, 0, Globals.TileSize / 2, Globals.TileSize / 2, "Projectile", 1, new Animation(0, 2, 5)));
                    attacked = true;
                    s = baseAnimation;
                    ani = new Animation(0, 3, 10);
                }
                else if (attackTimer > attackSpeed)
                {
                    attacking = false;
                    s = baseAnimation;
                    ani = new Animation(0, 3, 10);
                }
            }
            else if (dis < range)
            {
                s = attack;
                ani = new Animation(0, 9, attackSpeed / 10);
                attackTimer = 0;
                attacking = true;
                attacked = false;
            }
        }

        public override void DealPhysicalDamage(double damage, string name, string with)
        {
            base.DealPhysicalDamage(damage, name, with);

            if (Health < 1)
            {
                Health = 0;
                Globals.rootActionLog.Death(this.name);
                Globals.l.Current.removables.Add(this);
            }
        }

        public override void DealMagicDamage(double damage, string name, string with)
        {
            base.DealMagicDamage(damage, name, with);

            if (Health < 1)
            {
                Health = 0;
                Globals.rootActionLog.Death(this.name);
                Globals.l.Current.removables.Add(this);
            }
        }
    }
}
