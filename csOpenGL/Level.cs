using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Level
    {

        public Room Current;
        public Theme theme;
        public Player p;

        public Level(Room c, Theme theme, Player p)
        {
            this.theme = theme;
            Current = c;
            this.p = p;
        }

        public void Draw()
        {
            Current.Draw(0, 0);
            p.Draw();
        }

        public void Update(double delta)
        {
            Current.Update(delta);
            p.Update(delta);
        }

    }
}
