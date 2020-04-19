using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Wall : Structure
    {
        public Wall(): base(3,1)
        {

        }

        public override void Place(int[,] tileGrid)
        {
            for(int i = X; i<X+Width; i++)
            {
                for(int j = Y; j<Y+Height; j++)
                {
                     tileGrid[i, j] = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 0, theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, rotation);
                }
            }
        }
    }
}
