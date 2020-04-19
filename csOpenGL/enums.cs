using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public enum Rarity
    {
        BASIC, //Starting items
        COMMON,
        UNCOMMON,
        RARE,
        SUPER_RARE
    }

    public enum EffectType
    {
        HP,
        PHYSICAL_DAMAGE,
        MAGICAL_DAMAGE,
        BLOCK,
        SPEED,
        MANA,
        MPREGEN,
        HPREGEN
    }

    public enum Walkable
    {
        SOLID,
        WALKABLE
    }

    public enum TileType
    {
        TILE,
        WALL,
        STAIRS,
        DOOR,
        BUTTON,
    }

    public enum Direction
    {
        NORTH = 0,
        WEST = 1,
        SOUTH = 2,
        EAST = 3,
    }

    public enum SpellEffect
    {
        I,
        HATE,
        LIFE,
    }

    public enum GameState
    {
        PLAYING,
        DEAD,
        WON,
        MAINMENU
    }

}
