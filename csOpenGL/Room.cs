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
        public Sprite s;
        public List<Enemy> enemies;
        public List<Connection> Connections { get; set; }
        private Theme Theme { get; set; }

        public Room(int x, int y, Theme theme, int tileSize = Globals.TileSize)
        {
            Connections = new List<Connection>();
            Theme = theme;
            s = new Sprite(x, y, 0, Window.texs[2]);
            width = x;
            height = y;
            tileGrid = new Tile[x, y];
            this.tileSize = tileSize;
            enemies = new List<Enemy>();
            enemies.Add(new TestEnemy(4 * Globals.TileSize, 4 * Globals.TileSize));
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0 || i == x - 1 || j == 0 || j == y - 1)
                    {
                        tileGrid[i, j] = new Tile(new Sprite(tileSize, tileSize, 0, theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL);
                    }
                    else if (i == 6 && j == 7)
                    {
                        tileGrid[i, j] = new Tile(new Sprite(tileSize, tileSize, 0, theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS);
                    }
                    else
                    {
                        tileGrid[i, j] = new Tile(new Sprite(tileSize, tileSize, 0, theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.TILE);
                    }                 
                }
            }
        }

        public Tile getTile(int x, int y)
        {
            if(x<0 || x>=width ||y<0||y>=height)
            {
                return new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL);
            }
            return tileGrid[x, y];
        }

        public void Update(double delta)
        {
            foreach (var enemy in enemies)
            {
                enemy.Update(delta);
            }
        }

        public void DrawOnMinimap(int x, int y, float cc)
        {
            s.Draw(x, y, 0, cc, cc, cc, 1);
        }

        public int getLocation(Room r)
        {
            foreach(Connection c in Connections)
            {
                if(c.Room == r)
                {
                    return c.location;
                }
            }
            return 0;
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

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
            switch(connection.Direction)
            {
                case Direction.NORTH:
                    tileGrid[connection.location, 0] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.DOOR);
                    break;
                case Direction.EAST:
                    tileGrid[width - 1, connection.location] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.DOOR);
                    break;
                case Direction.SOUTH:
                    tileGrid[connection.location, height - 1] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.DOOR);
                    break;
                case Direction.WEST:
                    tileGrid[0, connection.location] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.DOOR);
                    break;
            }
        }
    }
}
