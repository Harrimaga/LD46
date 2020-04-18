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

        public Level(Room c, Theme theme)
        {
            this.theme = theme;
            Current = c;
        }

    }
}
