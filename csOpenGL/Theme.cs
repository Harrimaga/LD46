using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Theme
    {
        public string tileStyle;
        public List<Texture> textures;
        public Theme(string tileStyle = "Basic")
        {
            this.tileStyle = tileStyle;
            textures = new List<Texture>();
            textures.Add(new Texture("Textures/" + tileStyle + "Tile.png", tileStyle == "Space" ? 64 : 32, 32, 32, 32));
            textures.Add(new Texture("Textures/" + tileStyle + "Wall.png", tileStyle == "Space" ? 128 : 32, 32, 32, 32));
            textures.Add(new Texture("Textures/" + tileStyle + "Stairs.png", 32, 32, 32, 32));

            foreach (var item in textures)
            {
                Window.texs.Add(item);
            }
        }

        public Texture GetTextureByType(TileType type)
        {
            switch (type)
            {
                case TileType.TILE:
                    return textures[0];
                case TileType.WALL:
                    return textures[1];
                case TileType.STAIRS:
                    return textures[2];
                default:
                    return null;
            }
        }
    }
}
