using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class TestEnemy : Enemy
    {

        public TestEnemy(int x, int y) : base(Enemies.TEST_ENEMY_HEALTH, Enemies.TEST_ENEMY_MANA, x, y, 0, 3, 3, Globals.TileSize, Globals.TileSize, Enemies.TEST_ENEMY_SPEED, Enemies.TEST_ENEMY_ATTACKPOINT, Enemies.TEST_ENEMY_ATTACKSPEED, Enemies.TEST_ENEMY_DAMAGE, "a Iiuoo", Enemies.TEST_BLOCK)
        {
            attackAni = new Animation(0, 9, 10);
            idleAni = new Animation(0, 3, 10);
        }

        public override void AIMove(double delta)
        {
            StupidMovement(delta);
            BasicMeleeAttack(delta);
        }

    }
}
