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
        public Theme(string tileStyle = "Basic")
        {
            this.tileStyle = tileStyle;
            Window.texs.Add(new Texture("Textures/" + tileStyle + "Tile.png", 32, 32, 32, 32));
            Window.texs.Add(new Texture("Textures/" + tileStyle + "Wall.png", 32, 32, 32, 32));
            Window.texs.Add(new Texture("Textures/" + tileStyle + "Stairs.png", 32, 32, 32, 32));
        }
    }
}
