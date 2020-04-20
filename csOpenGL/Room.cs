using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LD46.Itemchances;

namespace LD46
{
    public class Room
    {
        public Tile[,] tileGrid;
        public int width, height, tileSize, chanceTotal = 0;
        public string tileStyle;
        public Sprite s;
        public List<Enemy> enemies;
        public List<Enemy> removables, addables;
        public List<Projectile> removeProjectiles;
        public List<Projectile> projectiles;
        public List<Itemchances> itemDrops;
        public List<Particle> particles;
        public List<ItemPos> items;
        public List<SpellPos> spells;
        public bool visited;
        public List<Structure> Structures { get; set; }
        public List<Connection> Connections { get; set; }
        public Theme Theme { get; set; }

        public Room(int x, int y, Theme theme, int tileSize = Globals.TileSize, bool genWalls = true)
        {
            AddItems();
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
            addables = new List<Enemy>();
            projectiles = new List<Projectile>();
            particles = new List<Particle>();
            removeProjectiles = new List<Projectile>();
            Structures = new List<Structure>();
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
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                int odds = Globals.Rng.Next(0, 1000);
                if (odds < 50)
                {
                    if(width >= 7 && height >= 5 && genWalls)
                    {
                        int structW = 3;
                        int structH = 1;
                        Structures.Add(new Wall(Globals.Rng.Next(2, width - 1 - structW), Globals.Rng.Next(2, height - 1 - structH), tileGrid, theme, true));
                    }
                }
                else if (odds < 100)
                {
                    if (width >= 5 && height >= 7 && genWalls)
                    {
                        int structW = 1;
                        int structH = 3;
                        Structures.Add(new Wall(Globals.Rng.Next(2, width - 1 - structW), Globals.Rng.Next(2, height - 1 - structH), tileGrid, theme, false));
                    }
                }else if (odds < 140)
                {
                    if(width>=5 && height>=5)
                    {
                        int structH = 1;
                        Structures.Add(new DefenseLaser(1, Globals.Rng.Next(2, height - 1 - structH), tileGrid, theme, width - 2));
                    }
                }
            }

            for (int i = 1; i < x - 1; i++)
            {
                for (int j = 1; j < y - 1; j++)
                {
                    if (tileGrid[i, j].GetWalkable() == Walkable.WALKABLE)
                    {
                        if (Globals.l.Rng.Next(1000) < 12)
                        {
                            enemies.Add(theme.GetEnemy(i * Globals.TileSize, j * Globals.TileSize));
                        }
                        else if (Globals.l.Rng.Next(1000) < 3)
                        {
                            int rn = Globals.Rng.Next(chanceTotal);
                            Item it = null;
                            foreach (Itemchances item in itemDrops)
                            {
                                if (rn < item.chance)
                                {
                                    it = item.make();
                                    break;
                                }
                            }
                            if (it != null)
                            {
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
            foreach (var enemy in addables)
            {
                enemies.Add(enemy);
            }
            addables.Clear();
            foreach(Structure structure in Structures)
            {
                structure.Update(delta);
            }
            int num = 0;
            for(int i = particles.Count-1; i >= 0; i--)
            {
                if(particles[i].Update(delta))
                {
                    particles[i] = particles[particles.Count - 1 - num];
                    num++;
                }
            }
            if (num > 0)
            {
                particles.RemoveRange(particles.Count - num, num);
            }
        }

        public virtual void DrawOnMinimap(int x, int y, float cc)
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

        public virtual void DrawOnMinimap(int x, int y, float r, float g, float b)
        {
            s.Draw(x, y, false, 0, r, g, b, 1);
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
            foreach (Particle p in particles)
            {
                p.Draw();
            }
            foreach (var enemy in enemies)
            {
                enemy.Draw();
            }
            foreach (var p in projectiles)
            {
                p.Draw();
            }
            foreach (var structure in Structures)
            {
                structure.Draw();
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
            spells.Add(new SpellPos((int)(x + (1.5 * Globals.l.Rng.NextDouble() - 0.75) * Globals.TileSize), (int)(y + (1.5 * Globals.l.Rng.NextDouble() - 0.75) * Globals.TileSize), (Spell)Globals.Spells[Globals.Rng.Next(Globals.Spells.Count)].Clone()));
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

        private void AddItems()
        {
            itemDrops = new List<Itemchances>();
            AddItem(new Sword(), () => { return new Sword(); });
            AddItem(new OrbOfHealth(), () => { return new OrbOfHealth(); });
            AddItem(new OrbOfMana(), () => { return new OrbOfMana(); });
            AddItem(new WandOfBlue(), () => { return new WandOfBlue(); });
            AddItem(new WandOfGreen(), () => { return new WandOfGreen(); });
            AddItem(new WandOfRed(), () => { return new WandOfRed(); });
            AddItem(new HelmOfPylonius(), () => { return new HelmOfPylonius(); });
            AddItem(new HealthPotion(), () => { return new HealthPotion(); });
            AddItem(new ManaPotion(), () => { return new ManaPotion(); });
            AddItem(new Hammer(), () => { return new Hammer(); });

            int i = 0;
            foreach (Itemchances itc in itemDrops)
            {
                int j = i + itc.chance;
                itc.chance += i;
                i = j;
            }
        }

        private void AddItem(Item it, Create make)
        {
            int chance = 0;
            switch (it.Rarity)
            {
                case Rarity.BASIC:
                    chance = 0;
                    break;
                case Rarity.COMMON:
                    chance = 10;
                    break;
                case Rarity.UNCOMMON:
                    chance = 5;
                    break;
                case Rarity.RARE:
                    chance = 2;
                    break;
                case Rarity.SUPER_RARE:
                    chance = 1;
                    break;

            }
            chanceTotal += chance;
            itemDrops.Add(new Itemchances(it.Rarity, chance, make));
        }

    }

    public class Itemchances
    {

        public Rarity rarity;
        public int chance;
        public delegate Item Create();
        public Create make;

        public Itemchances(Rarity rarity, int chance, Create make)
        {
            this.rarity = rarity;
            this.chance = chance;
            this.make = make;
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
