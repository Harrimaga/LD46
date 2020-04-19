using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Fighter : Player
    {

        public Fighter(float x, float y) : base(Balance.FIGHTER_BASE_HEALTH, Balance.FIGHTER_BASE_MANA, x, y, 0, 3, 1, (int)(Globals.TileSize*0.85), (int)(Globals.TileSize*0.85), Balance.FIGHTER_BASE_SPEED, Balance.FIGHTER_BASE_ATTACK_POINT, Balance.FIGHTER_BASE_ATTACK_SPEED, Balance.FIGHTER_NAME, Balance.FIGHTER_BASE_DAMAGE, Balance.FIGHTER_PHYSICAL_DAMAGE_AMP, Balance.FIGHTER_MAGICAL_DAMAGE_AMP, Balance.FIGHTER_BLOCK, Balance.FIGHTER_BLOCK_REGEN, "Blade")
        {

        }

    }
}
