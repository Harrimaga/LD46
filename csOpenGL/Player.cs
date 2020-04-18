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
            Init(100, x, y, 0, 3, 1, 128, 128, 10);
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

        public override bool DealPhysicalDamage(double damage, string name, string with)
        {
            Globals.rootActionLog.TakeDamage(name, damage, with);
            return base.DealPhysicalDamage(damage, name, with);
        }

        public override bool DealMagicDamage(double damage, string name, string with)
        {
            Globals.rootActionLog.TakeDamage(name, damage, with);
            return base.DealMagicDamage(damage, name, with);
        }

    }
}
