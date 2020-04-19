using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class DarkEnemy : Enemy
    {

        public DarkEnemy(int x, int y) : base(Enemies.DARK_ENEMY_HEALTH, Enemies.DARK_ENEMY_MANA, x, y, 0, 3, 3, Globals.TileSize, Globals.TileSize, Enemies.DARK_ENEMY_SPEED, Enemies.DARK_ENEMY_ATTACKPOINT, Enemies.DARK_ENEMY_ATTACKSPEED, Enemies.DARK_ENEMY_DAMAGE, "Kees", Enemies.DARK_BLOCK)
        {

        }

        public override void AIMove(double delta)
        {
            StupidMovement(delta);
            BasicMeleeAttack(delta);
        }

    }
}
