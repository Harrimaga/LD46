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
    }

    public enum Direction
    {
        NORTH,
        WEST,
        SOUTH,
        EAST,
    }
}
