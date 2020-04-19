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
        public const double FIGHTER_BASE_ATTACK_POINT = 1;
        public const double FIGHTER_BASE_ATTACK_SPEED = 50;
        public const double FIGHTER_BASE_DAMAGE = 20;
        public const double FIGHTER_BASE_HEALTH = 100;
        public const double FIGHTER_BASE_MANA = 50;
        public const double FIGHTER_BASE_SPEED = 5;
        public const string FIGHTER_NAME = "Fighter";
        public const double FIGHTER_BLOCK = 20;
        public const double FIGHTER_BLOCK_REGEN = 0.1;

        //Mage
        public const double MAGE_PHYSICAL_DAMAGE_AMP = 0.5;
        public const double MAGE_MAGICAL_DAMAGE_AMP = 1.3;
        public const double MAGE_BASE_ATTACK_POINT = 1;
        public const double MAGE_BASE_ATTACK_SPEED = 80;
        public const double MAGE_BASE_DAMAGE = 30;
        public const double MAGE_BASE_HEALTH = 75;
        public const double MAGE_BASE_MANA = 100;
        public const double MAGE_BASE_SPEED = 5;
        public const string MAGE_NAME = "Mage";

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
