using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class RappidFireRanged : Enemy
    {
        public RappidFireRanged(int x, int y) : base(Enemies.RAPPID_FIRE_ENEMY_HEALTH, Enemies.RAPPID_FIRE_ENEMY_MANA, x, y, 0, 3, 3, Globals.TileSize, Globals.TileSize, Enemies.RAPPID_FIRE_ENEMY_SPEED, Enemies.RAPPID_FIRE_ENEMY_ATTACKPOINT, Enemies.RAPPID_FIRE_ENEMY_ATTACKSPEED, Enemies.RAPPID_FIRE_ENEMY_DAMAGE, "Ranged", Enemies.RANGED_BLOCK)
        {
            range = Enemies.RAPPID_FIRE_ENEMY_RANGE;
            minDistance = range;
            projectileSpeed = Enemies.RAPPID_FIRE_ENEMY_PROJECTILE_SPEED;
            accuracy = Enemies.RAPPID_FIRE_ENEMY_ACCURACY;
        }

        public override void AIMove(double delta)
        {
            StupidMovement(delta);
            BasicRangedAttack(delta);
        }
    }
}
