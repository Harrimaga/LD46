using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Wall : Structure
    {
        public Wall(int x, int y, Tile[,] tileGrid, Theme theme) : base(3, 1, x, y, tileGrid, theme)
        {

        }

        public override void Place(Tile[,] tileGrid)
        {
            tileGrid[X+1, Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 0, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, 0.5f * (float)Math.PI);
            tileGrid[X+2, Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 2, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, X - 1 == 0 ? 0 : 0.5f * (float)Math.PI);
            tileGrid[X, Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 3, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, X + 1 == 0 ? 0 : 1.5f * (float)Math.PI);
        }
    }
}
