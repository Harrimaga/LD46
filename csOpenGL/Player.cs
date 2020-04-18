﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Player : Entity
    {

        protected int xDir, yDir;

        public Player(int x, int y)
        {
            Init(100, x, y, 0, 1, 128, 128, 10);
        }

        public override void Update(double delta)
        {
            x += (float)(delta * speed * xDir);
            y += (float)(delta * speed * yDir);
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