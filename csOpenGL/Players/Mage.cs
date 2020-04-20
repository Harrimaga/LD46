using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Mage : Player
    {

        public Mage(float x, float y) : base(Balance.MAGE_BASE_HEALTH, Balance.MAGE_BASE_MANA, x, y, 6, 7, 0, (int)(Globals.TileSize * 0.85), (int)(Globals.TileSize * 0.85), Balance.MAGE_BASE_SPEED, Balance.MAGE_BASE_ATTACK_POINT, Balance.MAGE_BASE_ATTACK_SPEED, Balance.MAGE_NAME, Balance.MAGE_BASE_DAMAGE, Balance.MAGE_PHYSICAL_DAMAGE_AMP, Balance.MAGE_MAGICAL_DAMAGE_AMP, Balance.MAGE_BLOCK, Balance.MAGE_BLOCK_REGEN, "Staff", Balance.MAGE_HEALTH_REGEN, Balance.MAGE_MANA_REGEN)
        {
            AddSpell(new Fireball());

        }
    }
}
