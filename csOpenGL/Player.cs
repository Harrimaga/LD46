using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Player : Entity
    {

        public Player(int x, int y)
        {
            Init(100, x, y, 0, 1, 128, 128, 10);
            ani = new Animation(0, 3, 10);
        }

        public override void Update(double delta)
        {
            Move((float)(delta * xDir * speed), (float)(delta * yDir * speed));

            base.Update(delta);

            xDir = 0;
            yDir = 0;
        }

        public void SetDir(int x, int y)
        {
            if(x != 0)
            {
                xDir = x;
            }
            if(y != 0)
            {
                yDir = y;
            }
        }

    }
}
