﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class TestEnemy : Enemy
    {

        public TestEnemy(int x, int y) : base(Enemies.TEST_ENEMY_HEALTH, x, y, 0, 3, Globals.TileSize, Globals.TileSize, Enemies.TEST_ENEMY_SPEED, Enemies.TEST_ENEMY_ATTACKPOINT, Enemies.TEST_ENEMY_ATTACKSPEED, Enemies.TEST_ENEMY_DAMAGE, "Kees")
        {

        }

        public override void AIMove(double delta)
        {
            StupidMovement(delta);
            BasicMeleeAttack(delta);
        }

    }
}
