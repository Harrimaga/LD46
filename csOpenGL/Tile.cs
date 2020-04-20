using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Tile: ICloneable
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

        public object Clone()
        {
            Tile t = new Tile(new Sprite(sprite.w, sprite.h, sprite.num, new Texture(sprite.texture.file, sprite.texture.totW, sprite.texture.totH, sprite.texture.sW, sprite.texture.sH)),walkable, tileType, rotation);
            return t;
        }
    }
}
