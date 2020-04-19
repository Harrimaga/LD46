using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Wall : Structure
    {
        private bool Horizontal;
        public Wall(int x, int y, Tile[,] tileGrid, Theme theme, bool horizontal) : base(horizontal ? 3 : 1, horizontal ? 1 : 3, x, y, tileGrid, theme)
        {
            Horizontal = horizontal;
            Place(tileGrid);
        }

        public override void Place(Tile[,] tileGrid)
        {
            if (Horizontal)
            {
                tileGrid[X + 1, Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 0, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, 0.5f * (float)Math.PI);
                tileGrid[X + 2, Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 2, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, X - 1 == 0 ? 0 : 0.5f * (float)Math.PI);
                tileGrid[X, Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 3, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, X + 1 == 0 ? 0 : 1.5f * (float)Math.PI);
            }
            else
            {
                tileGrid[X, Y + 1] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 0, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, 1f * (float)Math.PI);
                tileGrid[X, Y + 2] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 2, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, Y - 1 == 0 ? 0 : 1f * (float)Math.PI);
                tileGrid[X, Y] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 3, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, Y + 1 == 0 ? 0 : 2f * (float)Math.PI);
            }
                
        }
    }
}
