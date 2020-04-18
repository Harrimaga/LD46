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

        public Level(Theme theme, Player p)
        {
            this.theme = theme;
            Room room = new Room(16, 16, theme);
            Current = room;
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
