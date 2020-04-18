using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Tile
    {
        private Sprite sprite;


        public Tile(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public void Draw(float x, float y)
        {
            sprite.Draw(x, y);
        }
    }
}
