using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Room
    {
        public Sprite[,] tileGrid;
        public int width, height, tileSize;
        public string tileStyle;

        public Room(int x, int y, int tileSize = 64, string tileStyle="Basic")
        {
            width = x;
            height = y;
            tileGrid = new Sprite[x, y];
            this.tileSize = tileSize;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    tileGrid[i, j] = new Sprite(64, 64, 0, Window.texs[1]);
                }
            }
        }

        public void Draw(float x, float y)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tileGrid[i, j].Draw(x + i * tileSize, y + j * tileSize);
                }
            }
        }
    }
}
