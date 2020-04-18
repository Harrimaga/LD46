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
        private Sprite back;

        public Level(Theme theme, Player p, int seed)
        {
            this.theme = theme;
            Room room = new Room(16, 16, theme);
            Current = room;
            this.p = p;
            Rng = new Random(seed);
            CreateRoom();
            back = new Sprite(200, 200, 0, Window.texs[2]);
        }

        private bool CreateRoom(int deepness = 0, Room lastRoom = null)
        {
            List<Direction> directions = new List<Direction> { Direction.SOUTH, Direction.NORTH, Direction.EAST, Direction.WEST };
            if(lastRoom != null)
            {
                foreach (Connection conn in lastRoom.Connections)
                {
                    Direction dir = conn.Direction;
                    directions.Remove(dir);
                }
            }
            if(deepness > 7)
            {
                return true;
            }else if(deepness == 0)
            {
                Current = new Room(Rng.Next(4, 20), Rng.Next(4, 20), theme);
                FileHandler.WriteText("Created a room with size (" + Current.width + "," + Current.height + ")", "../../logs/log.txt", WriteModes.CREATE_OR_APPEND);
                List<bool> results = new List<bool>();
                
                results.Add(CreateRoom(++deepness, Current));
                return results.All((singleResult) => { return singleResult; });
            }
            Room newRoom = new Room(Rng.Next(4, 20), Rng.Next(4, 20), theme);
            FileHandler.WriteText("Created a room with size (" + newRoom.width + "," + newRoom.height + ")", "../../logs/log.txt", WriteModes.CREATE_OR_APPEND);
            for (int i = 0; i < Rng.Next(0, 4) && directions.Count > 0; i++)
            {
                int index = Rng.Next(0, directions.Count);
                int locationInGrid  = 2;
                int locationInGrid2 = 2;
                if (directions[index] == Direction.NORTH || directions[index] == Direction.SOUTH)
                {
                    locationInGrid = Rng.Next(1, newRoom.width - 2);
                    locationInGrid2 = Rng.Next(1, lastRoom.width - 2);
                }
                else
                {
                    locationInGrid = Rng.Next(1, newRoom.height - 2);
                    locationInGrid2 = Rng.Next(1, lastRoom.height - 2);
                }
                newRoom.AddConnection(new Connection(lastRoom, directions[index], locationInGrid));
                lastRoom.AddConnection(new Connection(newRoom,(Direction)(((int)directions[index] + 2) % 4), locationInGrid2));
                directions.RemoveAt(index);
            }

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
            DrawMinimap();
        }

        public void DrawMinimap()
        {
            back.Draw(1920 - 200, 1080 - 200, 0, 0, 0, 0, 0.85f);
            Current.DrawOnMinimap(100 - Current.width/2, 100 - Current.height/2, 1, 1920 - 200, 1080 - 200, null);
        }

        public void Update(double delta)
        {
            Current.Update(delta);
            p.Update(delta);
        }

    }
}
