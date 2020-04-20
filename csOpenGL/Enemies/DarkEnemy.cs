using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class DarkEnemy : Enemy
    {

        public DarkEnemy(int x, int y) : base(Enemies.DARK_ENEMY_HEALTH, Enemies.DARK_ENEMY_MANA, x, y, 0, 3, 3, Globals.TileSize, Globals.TileSize, Enemies.DARK_ENEMY_SPEED, Enemies.DARK_ENEMY_ATTACKPOINT, Enemies.DARK_ENEMY_ATTACKSPEED, Enemies.DARK_ENEMY_DAMAGE, "a Dario", Enemies.DARK_BLOCK)
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
