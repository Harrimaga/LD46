using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Pylonius : Enemy
    {

        public bool charging, hasHit;
        public double minSpeed, chargeTime, chargeCooldownMax, chargeCooldown;
        public Pylonius() : base(Enemies.PYLONIUS_HEALTH, Enemies.PYLONIUS_MANA, 12 * Globals.TileSize, 12 * Globals.TileSize, 18, 19, 3, Globals.TileSize * 4, Globals.TileSize * 4, Enemies.PYLONIUS_SPEED, Enemies.RANGED_ENEMY_ATTACKPOINT, Enemies.PYLONIUS_ATTACKSPEED, Enemies.PYLONIUS_DAMAGE, "Pylonius, the Bull", Enemies.PYLONIUS_BLOCK, Enemies.PYLONIUS_PHYSICAL_AMP, Enemies.PYLONIUS_MAGICAL_AMP)
        {
            charging = false;
            hasHit = false;
            minSpeed = speed;
            chargeTime = 10 * 60;
            chargeCooldownMax = 8 * 60;
            chargeCooldown = 0;
            attackAni = new Animation(0, 9, 10);
            idleAni = new Animation(0, 3, 10);
        }

        public override void Update(double delta)
        {
            base.Update(delta);
        }

        public override void AIMove(double delta)
        {
            if (!charging && speed == minSpeed && chargeCooldown <= 0)
            {
                charging = true;
                chargeCooldown = chargeCooldownMax;
                hasHit = false;
            }
            if (!charging)
            {
                if (!hasHit) speed += 0.04;
                StupidMovement(delta);
                hasHit = BasicMeleeAttack(delta);
                if (hasHit)
                {
                    speed = minSpeed;
                    charging = false;
                    hasHit = false;
                }
            }
            if (charging)
            {
                chargeTime -= delta;
                if (chargeTime <= 0)
                {
                    chargeTime = 10 * 60;
                    charging = false;
                }
                speed += 0.06;
            }

            chargeCooldown -= delta;
        }
    }
}
