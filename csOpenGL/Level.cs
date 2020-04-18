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

        public Level(Theme theme)
        {
            this.theme = theme;
            Room room = new Room(16, 16, theme);
            Current = room;
        }

    }
}
