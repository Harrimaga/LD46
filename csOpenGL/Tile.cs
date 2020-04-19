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
        private float rotation;


        public Tile(Sprite sprite, Walkable walkable, TileType tileType, float rotation)
        {
            this.sprite = sprite;
            this.walkable = walkable;
            this.tileType = tileType;
            this.rotation = rotation;
        }

        public void Draw(float x, float y)
        {
            sprite.Draw(x, y, true, rotation);
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
