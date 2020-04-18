using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Room
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
            enemies = new List<Enemy>();
            enemies.Add(new TestEnemy(4 * Globals.TileSize, 4 * Globals.TileSize));
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0 || i == x - 1 || j == 0 || j == y - 1)
                    {
                        tileGrid[i, j] = new Tile(new Sprite(tileSize, tileSize, 0, Window.texs[2]), Walkable.SOLID);
                    }
                    else if (i == 6 && j == 7)
                    {
                        tileGrid[i, j] = new Tile(new Sprite(tileSize, tileSize, 0, Window.texs[3]), Walkable.WALKABLE);
                    }
                    else
                    {
                        tileGrid[i, j] = new Tile(new Sprite(tileSize, tileSize, 0, Window.texs[1]), Walkable.WALKABLE);
                    }                 
                }
            }
        }

        public Tile getTile(int x, int y)
        {
            return tileGrid[x, y];
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
            foreach (var enemy in enemies)
            {
                enemy.Draw();
            }
        }
    }
}
