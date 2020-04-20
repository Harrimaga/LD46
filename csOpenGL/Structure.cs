using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public abstract class Structure
    {
        public Theme Theme { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        protected Structure(int width, int height, int x, int y, Tile[,] tileGrid, Theme theme)
        {
            Theme = theme;
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }

        public abstract void Place(Tile[,] tileGrid);
        public virtual void Update(double deltaTime)
        {
            //For general structures nothing needs to happen here
        }

        public virtual void Draw(double deltaTime)
        {

        }

        public virtual void OnTrigger()
        {
            //General structures can't be triggered anyway so this is empty
        }
    }
}
