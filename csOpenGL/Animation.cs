using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Animation
    {

        private int start, last, current;
        private double time, timer = 0;

        public Animation(int start, int last, double time)
        {
            this.start = start;
            this.last = last;
            this.time = time;
        }

        public void Update(Sprite s, double delta)
        {
            timer += delta;
            if(timer > time)
            {
                timer -= time;
                current++;
                if(current > last)
                {
                    current = start;
                }
            }
            s.num = current;
        }

    }
}
