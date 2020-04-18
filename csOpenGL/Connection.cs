using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Connection
    {
        public Room Room { get; set; }
        public Direction Direction { get; set; }
        public int location { get; set; }

        public Connection(Room room, Direction direction, int location)
        {
            Room = room;
            Direction = direction;
            this.location = location;
        }
    }
}
