using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Room
    {
        public Tile[,] tileGrid;
        public int width, height, tileSize;
        public string tileStyle;
        public Sprite s;
        public List<Enemy> enemies;
        public List<Enemy> removables;
        public List<Projectile> removeProjectiles;
        public List<Projectile> projectiles;
        public List<ItemPos> items;
        public List<SpellPos> spells;
        public bool visited;
        public List<Connection> Connections { get; set; }
        public Theme Theme { get; set; }

        public Room(int x, int y, Theme theme, int tileSize = Globals.TileSize)
        {
            Connections = new List<Connection>();
            items = new List<ItemPos>();
            spells = new List<SpellPos>();
            Theme = theme;
            s = new Sprite(x, y, 0, Window.texs[2]);
            visited = false;
            width = x;
            height = y;
            tileGrid = new Tile[x, y];
            this.tileSize = tileSize;
            enemies = new List<Enemy>();
            removables = new List<Enemy>();
            projectiles = new List<Projectile>();
            removeProjectiles = new List<Projectile>();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0 || i == x - 1 || j == 0 || j == y - 1)
                    {
                        float rotation = 0;
                        bool corner = false;

                        if (i == 0 && j == 0)
                        {
                            corner = true;
                        }
                        else if (i == x - 1 && j == 0)
                        {
                            corner = true;
                            rotation = .5f * (float)Math.PI;
                        }
                        else if (i == x - 1 && j == y - 1)
                        {
                            corner = true;
                            rotation = (float)Math.PI;
                        }
                        else if (i == 0 && j == y - 1)
                        {
                            corner = true;
                            rotation = 1.5f * (float)Math.PI;
                        }
                        else if (j == 0)
                        {
                            rotation = .5f * (float)Math.PI;
                        }
                        else if (j == y - 1)
                        {
                            rotation = 1.5f * (float)Math.PI;
                        }
                        else if (i == x - 1)
                        {
                            rotation = (float)Math.PI;
                        }

                        tileGrid[i, j] = new Tile(new Sprite(tileSize, tileSize, corner ? 1 : 0, theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, rotation);
                    }
                    else
                    {
                        tileGrid[i, j] = new Tile(new Sprite(tileSize, tileSize, Globals.l.Rng.Next(2) > 0 ? 0 : 1, theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.TILE, 0);
                        if (Globals.l.Rng.Next(1000) < 12)
                        {
                            if (Globals.l.Rng.Next(100) < 75)
                            {
                                enemies.Add(new TestEnemy(i * Globals.TileSize, j * Globals.TileSize));
                            }
                            else
                            {
                                enemies.Add(new RangedEnemy(i * Globals.TileSize, j * Globals.TileSize));
                            }
                        }
                        else if (Globals.l.Rng.Next(1000) < 5)
                        {
                            int rn = Globals.Rng.Next(5);
                            Item it = null;
                            switch(rn)
                            {
                                case 0:
                                    it = new Sword();
                                    break;
                                case 1:
                                    it = new OrbOfHealth();
                                    break;
                                case 2:
                                    it = new WandOfRed();
                                    break;
                                case 3:
                                    it = new WandOfBlue();
                                    break;
                                case 4:
                                    it = new WandOfGreen();
                                    break;
                            }
                            if(it != null)
                                items.Add(new ItemPos(i * Globals.TileSize, j * Globals.TileSize, (float)(Globals.l.Rng.NextDouble() * 2 * Math.PI), it));
                            }
                        }
                    }
                }
            }
        }

        public Tile getTile(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, 0);
            }
            return tileGrid[x, y];
        }

        public void SetTile(int x, int y, Tile tile)
        {
            tileGrid[x, y] = tile;
        }

        public void Update(double delta)
        {
            foreach (var p in projectiles)
            {
                if (p.Update(delta))
                {
                    removeProjectiles.Add(p);
                }
            }
            foreach (var p in removeProjectiles)
            {
                projectiles.Remove(p);
            }
            removeProjectiles.Clear();
            foreach (var enemy in enemies)
            {
                enemy.Update(delta);
            }
            foreach (var enemy in removables)
            {
                if (Globals.Rng.Next(100) < 10)
                {
                    DropSpell(enemy.x, enemy.y);
                }
                enemies.Remove(enemy);
            }
            removables.Clear();

        }

        public void DrawOnMinimap(int x, int y, float cc)
        {
            if (visited)
            {
                s.Draw(x, y, false, 0, cc * 0.1f, cc * 0.8f, cc * 0.1f, 1);
            }
            else
            {
                s.Draw(x, y, false, 0, cc, cc, cc, 1);
            }

        }

        public int getLocation(Room r)
        {
            foreach (Connection c in Connections)
            {
                if (c.Room == r)
                {
                    return c.location;
                }
            }
            return 0;
        }

        public void Draw(float x, float y)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tileGrid[i, j].Draw(x + i * tileSize, y + j * tileSize);
                }
            }
            foreach (ItemPos it in items)
            {
                it.it.DrawOnGround(it.x, it.y, it.rot);
            }
            foreach (SpellPos spell in spells)
            {
                spell.spell.DrawOnGround(spell.x, spell.y);
            }
            foreach (var enemy in enemies)
            {
                enemy.Draw();
            }
            foreach (var p in projectiles)
            {
                p.Draw();
            }
        }

        public void TryPickup()
        {
            foreach (ItemPos it in items)
            {
                if (Globals.l.p.items.Count >= 6) break;
                if (Globals.checkCol(it.x, it.y, Globals.TileSize, Globals.TileSize, (int)Globals.l.p.x, (int)Globals.l.p.y, Globals.l.p.w, Globals.l.p.h))
                {
                    Globals.l.p.EquipItem(it.it);
                    items.Remove(it);
                    return;
                }
            }
            foreach (SpellPos spell in spells)
            {
                if (Globals.l.p.Spells.Count >= 6) break;
                if (Globals.checkCol(spell.x, spell.y, Globals.TileSize, Globals.TileSize, (int)Globals.l.p.x, (int)Globals.l.p.y, Globals.l.p.w, Globals.l.p.h))
                {
                    Globals.l.p.AddSpell(spell.spell);
                    spells.Remove(spell);
                    return;
                }
            }
        }

        public void DropItem(Item i, float x, float y)
        {
            items.Add(new ItemPos((int)(x + (1.5 * Globals.l.Rng.NextDouble() - 0.75) * Globals.TileSize), (int)(y + (1.5 * Globals.l.Rng.NextDouble() - 0.75) * Globals.TileSize), (float)(Globals.l.Rng.NextDouble() * 2 * Math.PI), i));
        }

        public void DropSpell(Spell s, float x, float y)
        {
            spells.Add(new SpellPos((int)(x + (1.5 * Globals.l.Rng.NextDouble() - 0.75) * Globals.TileSize), (int)(y + (1.5 * Globals.l.Rng.NextDouble() - 0.75) * Globals.TileSize), s));
        }

        public void DropSpell(float x, float y)
        {
            int rn = Globals.Rng.Next(1);
            Spell s = null;
            switch (rn)
            {
                case 0:
                    s = new Fireball();
                    break;
            }
            if (s != null)
            {
                spells.Add(new SpellPos((int)(x + (1.5 * Globals.l.Rng.NextDouble() - 0.75) * Globals.TileSize), (int)(y + (1.5 * Globals.l.Rng.NextDouble() - 0.75) * Globals.TileSize), s));
            }
        }

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
            switch (connection.Direction)
            {
                case Direction.NORTH:
                    tileGrid[connection.location, 0] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.DOOR, 0);
                    tileGrid[connection.location - 1, 0] = new Tile(new Sprite(tileSize, tileSize, 2, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, connection.location - 1 == 0 ? 0 : 0.5f * (float)Math.PI);
                    tileGrid[connection.location + 1, 0] = new Tile(new Sprite(tileSize, tileSize, 3, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, connection.location + 1 == 0 ? 0 : 1.5f * (float)Math.PI);
                    break;
                case Direction.EAST:
                    tileGrid[width - 1, connection.location] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.DOOR, 0);
                    tileGrid[width - 1, connection.location - 1] = new Tile(new Sprite(tileSize, tileSize, 2, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, connection.location - 1 == 0 ? 0.5f * (float)Math.PI : (float)Math.PI);
                    tileGrid[width - 1, connection.location + 1] = new Tile(new Sprite(tileSize, tileSize, 3, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, connection.location + 1 == 0 ? 0.5f * (float)Math.PI : 0);
                    break;
                case Direction.SOUTH:
                    tileGrid[connection.location, height - 1] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.DOOR, 0);
                    tileGrid[connection.location - 1, height - 1] = new Tile(new Sprite(tileSize, tileSize, 2, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, connection.location - 1 == 0 ? (float)Math.PI : 0.5f * (float)Math.PI);
                    tileGrid[connection.location + 1, height - 1] = new Tile(new Sprite(tileSize, tileSize, 3, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, connection.location + 1 == 0 ? (float)Math.PI : 1.5f * (float)Math.PI);
                    break;
                case Direction.WEST:
                    tileGrid[0, connection.location] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.TILE)), Walkable.WALKABLE, TileType.DOOR, 0);
                    tileGrid[0, connection.location - 1] = new Tile(new Sprite(tileSize, tileSize, 2, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, connection.location - 1 == 0 ? 1.5f * (float)Math.PI : (float)Math.PI);
                    tileGrid[0, connection.location + 1] = new Tile(new Sprite(tileSize, tileSize, 3, Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, connection.location + 1 == 0 ? 1.5f * (float)Math.PI : 0);
                    break;
            }
        }

        public virtual void PressButton(float px, float py)
        {

        }
    }

    public struct ItemPos
    {
        public int x, y;
        public float rot;
        public Item it;

        public ItemPos(int x, int y, float rot, Item it)
        {
            this.x = x;
            this.y = y;
            this.it = it;
            this.rot = rot;
        }
    }

    public struct SpellPos
    {
        public int x, y;
        public Spell spell;

        public SpellPos(int x, int y, Spell spell)
        {
            this.x = x;
            this.y = y;
            this.spell = spell;
        }
    }
}
