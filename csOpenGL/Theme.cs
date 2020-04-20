using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Theme
    {
        public string tileStyle;
        public List<Texture> textures;
        public List<EnemySpawn> enemies;
        public int chanceTotal = 0;

        public Theme(string tileStyle = "Basic")
        {
            this.tileStyle = tileStyle;
            Globals.PossibleBosses = new List<Enemy>();
            textures = new List<Texture>();
            textures.Add(new Texture("Textures/" + tileStyle + "Tile.png", tileStyle == "Space" || tileStyle == "SpaceDark" ? 64 : 32, 32, 32, 32));
            textures.Add(new Texture("Textures/" + tileStyle + "Wall.png", tileStyle == "Space" || tileStyle == "SpaceDark" ? 128 : 32, 32, 32, 32));
            textures.Add(new Texture("Textures/" + tileStyle + "Stairs.png", 32, 32, 32, 32));
            textures.Add(new Texture("Textures/" + tileStyle + "Button.png", 64, 32, 32, 32));

            foreach (var item in textures)
            {
                Window.texs.Add(item);
            }

            enemies = new List<EnemySpawn>();
            switch (tileStyle)
            {
                case("Space"):
                    enemies.Add(new EnemySpawn(4, (int x, int y) => { return new SuicideCharger(x, y); }));
                    enemies.Add(new EnemySpawn(30, (int x, int y) => { return new TestEnemy(x, y); }));
                    enemies.Add(new EnemySpawn(10, (int x, int y) => { return new RangedEnemy(x, y); }));
                    break;
                case ("SpaceDark"):
                    enemies.Add(new EnemySpawn(4, (int x, int y) => { return new SuicideCharger(x, y); }));
                    enemies.Add(new EnemySpawn(30, (int x, int y) => { return new DarkEnemy(x, y); }));
                    enemies.Add(new EnemySpawn(10, (int x, int y) => { return new RappidFireRanged(x, y); }));
                    break;
            }
            foreach (EnemySpawn e in enemies)
            {
                chanceTotal += e.chance;
                e.chance = chanceTotal;
            }
        }

        public Texture GetTextureByType(TileType type)
        {
            switch (type)
            {
                case TileType.TILE:
                    return textures[0];
                case TileType.WALL:
                    return textures[1];
                case TileType.STAIRS:
                    return textures[2];
                case TileType.BUTTON:
                    return textures[3];

                default:
                    return null;
            }
        }

        public Enemy GetBoss()
        {
            if(Globals.PossibleBosses.Count == 0)
            {
                //Return a fallback
                return new TestEnemy(12*Globals.l.Current.tileSize, 12*Globals.l.Current.tileSize);
            }
            Enemy boss = Globals.PossibleBosses[new Random().Next(0, Globals.PossibleBosses.Count)];
            Globals.PossibleBosses.Remove(boss);
            return boss;
        }

        public Enemy GetEnemy(int x, int y)
        {
            int rn = Globals.l.Rng.Next(chanceTotal);
            foreach( EnemySpawn e in enemies)
            {
                if(rn < e.chance)
                {
                    return e.make(x, y);
                }
            }
            return null;
        }

    }

    public class EnemySpawn
    {

        public int chance;
        public delegate Enemy Spawn(int x, int y);
        public Spawn make;

        public EnemySpawn(int chance, Spawn make)
        {
            this.chance = chance;
            this.make = make;
        }
    }


}
