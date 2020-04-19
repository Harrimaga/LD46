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
        public double minSpeed, chargeTime, chargeCooldown;
        public Pylonius() : base(Enemies.PYLONIUS_HEALTH, Enemies.PYLONIUS_MANA, 12 * Globals.TileSize, 12 * Globals.TileSize, 6, 3, 3, Globals.TileSize, Globals.TileSize, Enemies.PYLONIUS_SPEED, Enemies.RANGED_ENEMY_ATTACKPOINT, Enemies.PYLONIUS_ATTACKSPEED, Enemies.PYLONIUS_DAMAGE, "Pylonius, the Bull", Enemies.PYLONIUS_BLOCK, Enemies.PYLONIUS_PHYSICAL_AMP, Enemies.PYLONIUS_MAGICAL_AMP)
        {
            charging = false;
            hasHit = false;
            minSpeed = speed;
            chargeTime = 100 * 60;
            chargeCooldown = 15 * 60;
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
                hasHit = false;
            }
            if (!charging)
            {
                if (!hasHit) speed += 0.1;
                StupidMovement(delta);
                hasHit = BasicMeleeAttack(delta);
                if (hasHit)
                {
                    speed = minSpeed;
                }
            }
            if (charging)
            {
                chargeTime -= delta;
                if (chargeTime <= 0)
                {
                    chargeTime = 3 * 60;
                    hasHit = false;
                    charging = false;
                }
                speed += 0.1;
            }

            chargeCooldown -= delta;
        }
    }
}
