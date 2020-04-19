﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class RangedEnemy : Enemy
    {
        public RangedEnemy(int x, int y) : base(Enemies.RANGED_ENEMY_HEALTH, Enemies.RANGED_ENEMY_MANA, x, y, 0, 3, 3, Globals.TileSize, Globals.TileSize, Enemies.RANGED_ENEMY_SPEED, Enemies.RANGED_ENEMY_ATTACKPOINT, Enemies.RANGED_ENEMY_ATTACKSPEED, Enemies.RANGED_ENEMY_DAMAGE, "Kees")
        {
            range = Enemies.RANGED_ENEMY_RANGE;
            minDistance = range;
            projectileSpeed = Enemies.RANGED_PROJECTILE_SPEED;
        }

        public override void AIMove(double delta)
        {
            StupidMovement(delta);
            BasicRangedAttack(delta);
        }
    }
}