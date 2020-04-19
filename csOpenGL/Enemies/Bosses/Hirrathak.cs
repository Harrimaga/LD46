using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Hirrathak : Enemy
    {
        public Hirrathak() : base(Enemies.HIRRATHAK_HEALTH, Enemies.HIRRATHAK_MANA, 12* Globals.TileSize, 12 * Globals.TileSize, 0, 3, 3, Globals.TileSize, Globals.TileSize, Enemies.HIRRATHAK_SPEED, Enemies.HIRRATHAK_ATTACKPOINT, Enemies.HIRRATHAK_ATTACKSPEED, Enemies.HIRRATHAK_DAMAGE, "Hirrathak, the Purple", Enemies.HIRRATHAK_BLOCK, Enemies.HIRRATHAK_PHYSICAL_AMP, Enemies.HIRRATHAK_MAGICAL_AMP)
        {

        }
    }
}
