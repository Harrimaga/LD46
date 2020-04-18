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
            if(deepness > 7)
            {
                return true;
            }else if(deepness == 0)
            {
                Current = new Room(Rng.Next(1, 20), Rng.Next(1, 20), theme);
                FileHandler.WriteText("Created a room with size (" + Current.width + "," + Current.height + ")", "../../logs/log.txt", WriteModes.CREATE_OR_APPEND);
                List<bool> results = new List<bool>();
                for (int i = 0; i<Rng.Next(1,5); i++)
                {
                    results.Add(CreateRoom(++deepness, Current));
                }
                return results.All((singleResult) => { return singleResult; });
            }
            Room newRoom = new Room(Rng.Next(1, 20), Rng.Next(1, 20), theme);
            newRoom.Connections.Add(lastRoom);
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
            DrawMinimap();
        }

        public void DrawMinimap()
        {
            back.Draw(1920 - 200, 1080 - 200, 0, 0, 0, 0, 0.85f);
            Current.DrawOnMinimap(0, 0, 1, 1920 - 200, 1080 - 200, null);
        }

        public void Update(double delta)
        {
            Current.Update(delta);
            p.Update(delta);
        }

    }
}
