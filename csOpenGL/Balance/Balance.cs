using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    static class Balance
    {
        /// <summary>
        /// Characters
        /// </summary>
        //Fighter
        public const double FIGHTER_PHYSICAL_DAMAGE_AMP = 1.3;
        public const double FIGHTER_MAGICAL_DAMAGE_AMP = 0.7;

        /// <summary>
        /// Items
        /// </summary>
        //Orb of health
        public const Rarity ORB_OF_HEALTH_RARITY = Rarity.RARE;
        public const string ORB_OF_HEALTH_DESCRIPTION = "An orb granting extra vitality to its carrier.";
        //Sword
        public const Rarity SWORD_RARITY = Rarity.BASIC;
        public const string SWORD_DESCRIPTION = "A trusty sword, for all your slashing needs.";
    }
}
