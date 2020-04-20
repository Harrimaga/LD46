using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class MazeBossRoom : Room
    {

        public MazeBossRoom(Theme theme) : base(25, 25, theme, Globals.TileSize, false)
        {
            isBossRoom = true;
            enemies.Clear();
            Globals.Boss = theme.GetBoss();
            enemies.Add(Globals.Boss);
            Globals.Boss.x = 10 * Globals.TileSize;
            Globals.Boss.y = 9 * Globals.TileSize;
            Random rng = new Random();
            if (rng.Next(2) == 0)
            {
                Structures.Add(new Wall(3, 2, tileGrid, theme, true));
                Structures.Add(new Wall(6, 2, tileGrid, theme, true));
                Structures.Add(new Wall(9, 2, tileGrid, theme, true));
                Structures.Add(new Wall(13, 2, tileGrid, theme, true));
                Structures.Add(new Wall(16, 2, tileGrid, theme, true));
                Structures.Add(new Wall(19, 2, tileGrid, theme, true));
                Structures.Add(new Wall(3, 22, tileGrid, theme, true));
                Structures.Add(new Wall(6, 22, tileGrid, theme, true));
                Structures.Add(new Wall(9, 22, tileGrid, theme, true));
                Structures.Add(new Wall(13, 22, tileGrid, theme, true));
                Structures.Add(new Wall(16, 22, tileGrid, theme, true));
                Structures.Add(new Wall(19, 22, tileGrid, theme, true));
                Structures.Add(new Wall(4, 3, tileGrid, theme, true));
                Structures.Add(new Wall(9, 4, tileGrid, theme, true));
                Structures.Add(new Wall(14, 4, tileGrid, theme, true));
                Structures.Add(new Wall(4, 5, tileGrid, theme, true));
                Structures.Add(new Wall(8, 6, tileGrid, theme, true));
                Structures.Add(new Wall(11, 6, tileGrid, theme, true));
                Structures.Add(new Wall(14, 6, tileGrid, theme, true));
                Structures.Add(new Wall(19, 7, tileGrid, theme, true));
                Structures.Add(new Wall(5, 8, tileGrid, theme, true));
                Structures.Add(new Wall(10, 8, tileGrid, theme, true));
                Structures.Add(new Wall(16, 8, tileGrid, theme, true));
                Structures.Add(new Wall(4, 10, tileGrid, theme, true));
                Structures.Add(new Wall(5, 11, tileGrid, theme, true));
                Structures.Add(new Wall(18, 11, tileGrid, theme, true));
                Structures.Add(new Wall(6, 12, tileGrid, theme, true));
                Structures.Add(new Wall(11, 13, tileGrid, theme, true));
                Structures.Add(new Wall(15, 13, tileGrid, theme, true));
                Structures.Add(new Wall(3, 14, tileGrid, theme, true));
                Structures.Add(new Wall(17, 14, tileGrid, theme, true));
                Structures.Add(new Wall(6, 15, tileGrid, theme, true));
                Structures.Add(new Wall(5, 17, tileGrid, theme, true));
                Structures.Add(new Wall(13, 18, tileGrid, theme, true));
                Structures.Add(new Wall(4, 20, tileGrid, theme, true));
                Structures.Add(new Wall(11, 20, tileGrid, theme, true));
                Structures.Add(new Wall(15, 20, tileGrid, theme, true));

                Structures.Add(new Wall(2, 2, tileGrid, theme, false));
                Structures.Add(new Wall(2, 5, tileGrid, theme, false));
                Structures.Add(new Wall(2, 8, tileGrid, theme, false));
                Structures.Add(new Wall(2, 11, tileGrid, theme, false));
                Structures.Add(new Wall(2, 14, tileGrid, theme, false));
                Structures.Add(new Wall(2, 17, tileGrid, theme, false));
                Structures.Add(new Wall(2, 20, tileGrid, theme, false));
                Structures.Add(new Wall(22, 2, tileGrid, theme, false));
                Structures.Add(new Wall(22, 5, tileGrid, theme, false));
                Structures.Add(new Wall(22, 8, tileGrid, theme, false));
                Structures.Add(new Wall(22, 11, tileGrid, theme, false));
                Structures.Add(new Wall(22, 14, tileGrid, theme, false));
                Structures.Add(new Wall(22, 17, tileGrid, theme, false));
                Structures.Add(new Wall(22, 20, tileGrid, theme, false));
                Structures.Add(new Wall(4, 6, tileGrid, theme, false));
                Structures.Add(new Wall(4, 17, tileGrid, theme, false));
                Structures.Add(new Wall(8, 18, tileGrid, theme, false));
                Structures.Add(new Wall(8, 7, tileGrid, theme, false));
                Structures.Add(new Wall(9, 11, tileGrid, theme, false));
                Structures.Add(new Wall(9, 15, tileGrid, theme, false));
                Structures.Add(new Wall(9, 8, tileGrid, theme, false));
                Structures.Add(new Wall(10, 18, tileGrid, theme, false));
                Structures.Add(new Wall(11, 14, tileGrid, theme, false));
                Structures.Add(new Wall(12, 16, tileGrid, theme, false));
                Structures.Add(new Wall(13, 3, tileGrid, theme, false));
                Structures.Add(new Wall(14, 8, tileGrid, theme, false));
                Structures.Add(new Wall(14, 11, tileGrid, theme, false));
                Structures.Add(new Wall(14, 14, tileGrid, theme, false));
                Structures.Add(new Wall(16, 9, tileGrid, theme, false));
                Structures.Add(new Wall(16, 16, tileGrid, theme, false));
                Structures.Add(new Wall(18, 4, tileGrid, theme, false));
                Structures.Add(new Wall(18, 15, tileGrid, theme, false));
                Structures.Add(new Wall(18, 19, tileGrid, theme, false));
                Structures.Add(new Wall(20, 4, tileGrid, theme, false));
                Structures.Add(new Wall(20, 8, tileGrid, theme, false));
                Structures.Add(new Wall(20, 14, tileGrid, theme, false));
                Structures.Add(new Wall(20, 18, tileGrid, theme, false));

                switch(rng.Next(6)) {
                    case 0:
                        tileGrid[11, 10] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 1:
                        tileGrid[8, 11] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 2:
                        tileGrid[8, 17] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 3:
                        tileGrid[14, 5] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 4:
                        tileGrid[21, 6] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 5:
                        tileGrid[13, 15] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                }
                
            }
            else
            {
                Structures.Add(new Wall(3, 2, tileGrid, theme, true));
                Structures.Add(new Wall(6, 2, tileGrid, theme, true));
                Structures.Add(new Wall(9, 2, tileGrid, theme, true));
                Structures.Add(new Wall(13, 2, tileGrid, theme, true));
                Structures.Add(new Wall(16, 2, tileGrid, theme, true));
                Structures.Add(new Wall(19, 2, tileGrid, theme, true));
                Structures.Add(new Wall(3, 22, tileGrid, theme, true));
                Structures.Add(new Wall(6, 22, tileGrid, theme, true));
                Structures.Add(new Wall(9, 22, tileGrid, theme, true));
                Structures.Add(new Wall(13, 22, tileGrid, theme, true));
                Structures.Add(new Wall(16, 22, tileGrid, theme, true));
                Structures.Add(new Wall(19, 22, tileGrid, theme, true));
                Structures.Add(new Wall(19, 3, tileGrid, theme, true));
                Structures.Add(new Wall(4, 4, tileGrid, theme, true));
                Structures.Add(new Wall(10, 4, tileGrid, theme, true));
                Structures.Add(new Wall(14, 4, tileGrid, theme, true));
                Structures.Add(new Wall(16, 5, tileGrid, theme, true));
                Structures.Add(new Wall(4, 8, tileGrid, theme, true));
                Structures.Add(new Wall(8, 8, tileGrid, theme, true));
                Structures.Add(new Wall(12, 8, tileGrid, theme, true));
                Structures.Add(new Wall(14, 9, tileGrid, theme, true));
                Structures.Add(new Wall(19, 9, tileGrid, theme, true));
                Structures.Add(new Wall(16, 11, tileGrid, theme, true));
                Structures.Add(new Wall(19, 11, tileGrid, theme, true));
                Structures.Add(new Wall(8, 13, tileGrid, theme, true));
                Structures.Add(new Wall(12, 13, tileGrid, theme, true));
                Structures.Add(new Wall(6, 14, tileGrid, theme, true));
                Structures.Add(new Wall(12, 15, tileGrid, theme, true));
                Structures.Add(new Wall(15, 15, tileGrid, theme, true));
                Structures.Add(new Wall(18, 16, tileGrid, theme, true));
                Structures.Add(new Wall(12, 17, tileGrid, theme, true));
                Structures.Add(new Wall(3, 18, tileGrid, theme, true));
                Structures.Add(new Wall(3, 19, tileGrid, theme, true));
                Structures.Add(new Wall(8, 20, tileGrid, theme, true));
                Structures.Add(new Wall(18, 20, tileGrid, theme, true));
                Structures.Add(new Wall(14, 21, tileGrid, theme, true));

                Structures.Add(new Wall(2, 2, tileGrid, theme, false));
                Structures.Add(new Wall(2, 5, tileGrid, theme, false));
                Structures.Add(new Wall(2, 8, tileGrid, theme, false));
                Structures.Add(new Wall(2, 11, tileGrid, theme, false));
                Structures.Add(new Wall(2, 14, tileGrid, theme, false));
                Structures.Add(new Wall(2, 17, tileGrid, theme, false));
                Structures.Add(new Wall(2, 20, tileGrid, theme, false));
                Structures.Add(new Wall(22, 2, tileGrid, theme, false));
                Structures.Add(new Wall(22, 5, tileGrid, theme, false));
                Structures.Add(new Wall(22, 8, tileGrid, theme, false));
                Structures.Add(new Wall(22, 11, tileGrid, theme, false));
                Structures.Add(new Wall(22, 14, tileGrid, theme, false));
                Structures.Add(new Wall(22, 17, tileGrid, theme, false));
                Structures.Add(new Wall(22, 20, tileGrid, theme, false));
                Structures.Add(new Wall(3, 10, tileGrid, theme, false));
                Structures.Add(new Wall(3, 13, tileGrid, theme, false));
                Structures.Add(new Wall(4, 11, tileGrid, theme, false));
                Structures.Add(new Wall(5, 5, tileGrid, theme, false));
                Structures.Add(new Wall(5, 14, tileGrid, theme, false));
                Structures.Add(new Wall(6, 9, tileGrid, theme, false));
                Structures.Add(new Wall(6, 18, tileGrid, theme, false));
                Structures.Add(new Wall(7, 6, tileGrid, theme, false));
                Structures.Add(new Wall(7, 16, tileGrid, theme, false));
                Structures.Add(new Wall(8, 4, tileGrid, theme, false));
                Structures.Add(new Wall(9, 9, tileGrid, theme, false));
                Structures.Add(new Wall(9, 16, tileGrid, theme, false));
                Structures.Add(new Wall(10, 5, tileGrid, theme, false));
                Structures.Add(new Wall(10, 14, tileGrid, theme, false));
                Structures.Add(new Wall(11, 17, tileGrid, theme, false));
                Structures.Add(new Wall(13, 4, tileGrid, theme, false));
                Structures.Add(new Wall(13, 19, tileGrid, theme, false));
                Structures.Add(new Wall(14, 10, tileGrid, theme, false));
                Structures.Add(new Wall(15, 5, tileGrid, theme, false));
                Structures.Add(new Wall(16, 12, tileGrid, theme, false));
                Structures.Add(new Wall(16, 17, tileGrid, theme, false));
                Structures.Add(new Wall(17, 7, tileGrid, theme, false));
                Structures.Add(new Wall(18, 13, tileGrid, theme, false));
                Structures.Add(new Wall(19, 5, tileGrid, theme, false));
                Structures.Add(new Wall(19, 17, tileGrid, theme, false));
                Structures.Add(new Wall(20, 12, tileGrid, theme, false));
                Structures.Add(new Wall(21, 5, tileGrid, theme, false));

                switch (rng.Next(6))
                {
                    case 0:
                        tileGrid[11, 11] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 1:
                        tileGrid[4, 7] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 2:
                        tileGrid[4, 14] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 3:
                        tileGrid[14, 5] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 4:
                        tileGrid[15, 8] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                    case 5:
                        tileGrid[17, 14] = new Tile(new Sprite(tileSize, tileSize, 0, Theme.GetTextureByType(TileType.STAIRS)), Walkable.WALKABLE, TileType.STAIRS, 0);
                        break;
                }
            }
        }
    }
}
