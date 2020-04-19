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
        public List<Enemy> PossibleBosses { get; set; }

        public Theme(string tileStyle = "Basic")
        {
            this.tileStyle = tileStyle;
            PossibleBosses = new List<Enemy>();
            textures = new List<Texture>();
            textures.Add(new Texture("Textures/" + tileStyle + "Tile.png", 32, 32, 32, 32));
            textures.Add(new Texture("Textures/" + tileStyle + "Wall.png", 32, 32, 32, 32));
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

        public Enemy GetBoss()
        {
            if(PossibleBosses.Count == 0)
            {
                //Return a fallback
                return new TestEnemy(12*Globals.l.Current.tileSize, 12*Globals.l.Current.tileSize);
            }
            Enemy boss = PossibleBosses[new Random().Next(0, PossibleBosses.Count)];
            PossibleBosses.Remove(boss);
            return boss;
        }
    }
}
