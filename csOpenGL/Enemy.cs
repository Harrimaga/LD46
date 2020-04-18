using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Enemy : Entity
    {

        public Enemy(double Health, float x, float y, int texNum, int spriteNum, int w, int h, double speed, double PhysicalAmp = 1, double MagicalAmp = 1)
        {
            Init(Health, x, y, texNum, spriteNum, w, h, speed, PhysicalAmp, MagicalAmp);
        }

        public override void Update(double delta)
        {
            AIMove();
        }

        public virtual void AIMove()
        {

        }

    }
}
