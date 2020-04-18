using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Tile
    {
        private Sprite sprite;
        private Walkable walkable;
        private TileType tileType;


        public Tile(Sprite sprite, Walkable walkable, TileType tileType)
        {
            this.sprite = sprite;
            this.walkable = walkable;
        }

        public void Draw(float x, float y)
        {
            sprite.Draw(x, y);
        }

        public Walkable GetWalkable()
        {
            return walkable;
        }

        public TileType GetTileType()
        {
            return tileType;
        }

    }
}
