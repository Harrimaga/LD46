using System;
using System.Collections.Generic;
using System.Linq;
using Secretary;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Level
    {

        public Room Current;
        public Theme theme;
        public Player p;
        private Random Rng { get; set; }

        public Level(Theme theme, Player p, int seed)
        {
            this.theme = theme;
            Room room = new Room(16, 16, theme);
            Current = room;
            this.p = p;
            Rng = new Random(seed);
            CreateRoom();
        }

        private bool CreateRoom(int deepness = 0, Room lastRoom = null)
        {
            List<Direction> directions = new List<Direction> { Direction.SOUTH, Direction.NORTH, Direction.EAST, Direction.WEST };
            if(deepness > 7)
            {
                return true;
            }else if(deepness == 0)
            {
                Current = new Room(Rng.Next(3, 20), Rng.Next(3, 20), theme);
                FileHandler.WriteText("Created a room with size (" + Current.width + "," + Current.height + ")", "../../logs/log.txt", WriteModes.CREATE_OR_APPEND);
                List<bool> results = new List<bool>();
                
                results.Add(CreateRoom(++deepness, Current));
                return results.All((singleResult) => { return singleResult; });
            }
            Room newRoom = new Room(Rng.Next(3, 20), Rng.Next(3, 20), theme);
            for (int i = 0; i < Rng.Next(1, 5); i++)
            {
                int index = Rng.Next(0, directions.Count);
                newRoom.AddConnection(new Connection(lastRoom, directions[index], 2));
                lastRoom.AddConnection(new Connection(newRoom,(Direction)(((int)directions[index] + 2) % 4), 2));
                directions.RemoveAt(index);
            }
            FileHandler.WriteText("Created a room with size (" + newRoom.width + "," + newRoom.height + ")", "../../logs/log.txt", WriteModes.CREATE_OR_APPEND);

            List<bool> result = new List<bool>();
            for (int i = 0; i < Rng.Next(1, 5); i++)
            {
                result.Add(CreateRoom(++deepness, newRoom));
            }
            return result.All((singleResult) => { return singleResult; });
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
