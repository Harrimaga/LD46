using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class TestEnemy : Enemy
    {

        public TestEnemy(int x, int y) : base(100, x, y, 0, 3, Globals.TileSize, Globals.TileSize, 5)
        {

        }

        public override void AIMove(double delta)
        {
            StupidMovement(delta);
        }

    }
}
