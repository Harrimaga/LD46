﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Turret : Enemy
    {
        public Turret(int x, int y) : base (Enemies.TURRET_HEALTH, Enemies.TURRET_MANA, x, y, 0, 3, 0, Globals.TileSize, Globals.TileSize, Enemies.TURRET_SPEED, Enemies.TURRET_ATTACKPOINT, Enemies.TURRET_ATTACKSPEED, Enemies.TURRET_DAMAGE, "Turret (Twaelf)", Enemies.TURRET_BLOCK)
        {
            range = Enemies.TURRET_RANGE;
            minDistance = range;
            projectileSpeed = Enemies.TURRET_PROJECTILE_SPEED;
        }

        public override void AIMove(double delta)
        {
            // I CAN beat the shit out of you without getting closer!
            BasicRangedAttack(delta);
            base.AIMove(delta);
        }
    }
}