using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Room
    {
        public Tile[,] tileGrid;
        public int width, height, tileSize;
        public string tileStyle;
        public List<Enemy> enemies;

        public Room(int x, int y, int tileSize = Globals.TileSize, string tileStyle="Basic")
        {
            width = x;
            height = y;
            tileGrid = new Tile[x, y];
            this.tileStyle = tileStyle;
            this.tileSize = tileSize;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    tileGrid[i, j] = new Tile(new Sprite(tileSize, tileSize, 0, Window.texs[1]));
                }
            }
        }

        public void Update(double delta)
        {
            foreach (var enemy in enemies)
            {
                enemy.Update(delta);
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
