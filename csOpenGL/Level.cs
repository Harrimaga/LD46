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
        public Random Rng { get; set; }
        private bool HasBossRoom { get; set; }

        public Level(Theme theme, Player p, int seed)
        {
            HasBossRoom = false;
            Globals.l = this;
            this.theme = theme;
            this.p = p;
            Rng = new Random(seed);
            CreateRoom();
            Current.visited = true;
        }

        private bool CreateRoom(int deepness = 0, Room lastRoom = null)
        {
            List<Direction> directions = new List<Direction> { Direction.SOUTH, Direction.NORTH, Direction.EAST, Direction.WEST };
            if (lastRoom != null)
            {
                foreach (Connection conn in lastRoom.Connections)
                {
                    Direction dir = conn.Direction;
                    directions.Remove(dir);
                }
            }
            if (deepness > 7)
            {
                return true;
            }
            else if (deepness == 0)
            {
                Current = new Room(Rng.Next(4, 25), Rng.Next(4, 25), theme);
                FileHandler.WriteText("Created a room with size (" + Current.width + "," + Current.height + ")", "../../logs/log.txt", WriteModes.CREATE_OR_APPEND);
                List<bool> results = new List<bool>();

                results.Add(CreateRoom(++deepness, Current));
                if (!HasBossRoom) //No bossroom had been created
                {
                    Room roomToUpdate = Current;
                    for (int i = 0; i < 12; i++)
                    {
                        roomToUpdate = roomToUpdate.Connections[Rng.Next(roomToUpdate.Connections.Count)].Room;
                    }
                    Room bossRoom = new ButtonClickBoss(theme);
                    foreach (Connection conn in roomToUpdate.Connections)
                    {
                        bossRoom.AddConnection(conn);
                        Connection otherSide = conn.Room.Connections.Find((connection) => { return connection.Direction == (Direction)(((int)conn.Direction + 2) % 4); });
                        otherSide.Room = bossRoom;
                    }
                }
                return results.All((singleResult) => { return singleResult; });
            }
            Room newRoom = null;
            if (!HasBossRoom && (Rng.Next(75) < deepness))
            {
                newRoom = new ButtonClickBoss(theme);
                HasBossRoom = true;
            }
            else
            {
                newRoom = new Room(Rng.Next(4, 25), Rng.Next(4, 25), theme);
            }
            FileHandler.WriteText("Created a room with size (" + newRoom.width + "," + newRoom.height + ")", "../../logs/log.txt", WriteModes.CREATE_OR_APPEND);

            int index = Rng.Next(0, directions.Count);
            int locationInGrid = 2;
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
            newRoom.AddConnection(new Connection(lastRoom, (Direction)(((int)directions[index] + 2) % 4), locationInGrid));
            lastRoom.AddConnection(new Connection(newRoom, directions[index], locationInGrid2));
            directions.RemoveAt(index);

            List<bool> result = new List<bool>();
            for (int i = 0; i < Rng.Next(Math.Max(0, 2 - deepness / 2), 4); i++)
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
            p.DrawUI();
        }

        public void DrawMinimap()
        {
            Queue<MMData> conns = new Queue<MMData>();
            Stack<MMData> drawOrder = new Stack<MMData>();
            int x = 150 - Current.width / 2;
            int y = 150 - Current.height / 2;
            drawOrder.Push(new MMData(null, Current, 1, x, y));
            foreach (Connection c in Current.Connections)
            {
                Room r = c.Room;
                switch (c.Direction)
                {
                    case Direction.EAST:
                        conns.Enqueue(new MMData(Current, r, 0.8f, x + Current.width + 2, y + c.location - r.getLocation(Current)));
                        break;
                    case Direction.SOUTH:
                        conns.Enqueue(new MMData(Current, r, 0.8f, x + c.location - r.getLocation(Current), y + Current.height + 2));
                        break;
                    case Direction.NORTH:
                        conns.Enqueue(new MMData(Current, r, 0.8f, x + c.location - r.getLocation(Current), y - r.height - 2));
                        break;
                    case Direction.WEST:
                        conns.Enqueue(new MMData(Current, r, 0.8f, x - r.width - 2, y + c.location - r.getLocation(Current)));
                        break;
                }
            }
            while (conns.Count > 0)
            {
                MMData md = conns.Dequeue();
                x = md.x;
                y = md.y;
                foreach (Connection c in md.itself.Connections)
                {
                    if (c.Room != md.from)
                    {
                        Room r = c.Room;
                        switch (c.Direction)
                        {
                            case Direction.EAST:
                                conns.Enqueue(new MMData(md.itself, r, md.cc * 0.8f, x + md.itself.width + 2, y + c.location - r.getLocation(md.itself)));
                                break;
                            case Direction.SOUTH:
                                conns.Enqueue(new MMData(md.itself, r, md.cc * 0.8f, x + c.location - r.getLocation(md.itself), y + md.itself.height + 2));
                                break;
                            case Direction.NORTH:
                                conns.Enqueue(new MMData(md.itself, r, md.cc * 0.8f, x + c.location - r.getLocation(md.itself), y - r.height - 2));
                                break;
                            case Direction.WEST:
                                conns.Enqueue(new MMData(md.itself, r, md.cc * 0.8f, x - r.width - 2, y + c.location - r.getLocation(md.itself)));
                                break;
                        }
                    }
                }
                drawOrder.Push(md);
            }
            while (drawOrder.Count > 0)
            {
                MMData md = drawOrder.Pop();
                md.itself.DrawOnMinimap(md.x, md.y, md.cc);
            }
        }

        public void Update(double delta)
        {
            Current.Update(delta);
            p.Update(delta);
        }

    }

    public struct MMData
    {
        public int x, y;
        public Room from, itself;
        public float cc;

        public MMData(Room from, Room itself, float cc, int x, int y)
        {
            this.from = from;
            this.cc = cc;
            this.x = x;
            this.y = y;
            this.itself = itself;
        }
    }

}
