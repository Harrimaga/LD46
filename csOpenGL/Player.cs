using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Player : Entity
    {

        protected int xDir, yDir;

        public Player(int x, int y)
        {
            Init(100, x, y, 0, 1, 128, 128, 10);
        }

        public override void Update(double delta)
        {
            x += (float)(delta * speed * xDir);
            int[] coll = CheckCollision();
            if (coll != null)
            {
                if(xDir > 0)
                {
                    x = coll[0]*Globals.TileSize-w;
                }
                else if(xDir < 0)
                {
                    x = (1+coll[0]) * Globals.TileSize;
                }
            }
            y += (float)(delta * speed * yDir);
            coll = CheckCollision();
            if (coll != null)
            {
                if (yDir > 0)
                {
                    y = coll[1] * Globals.TileSize - h;
                }
                else if (yDir < 0)
                {
                    y = (1 + coll[1]) * Globals.TileSize;
                }
            }
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
