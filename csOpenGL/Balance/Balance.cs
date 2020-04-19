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
        public const double MAGE_BLOCK = 20;
        public const double MAGE_BLOCK_REGEN = 0.1;

        /// <summary>
        /// Items
        /// </summary>
        //Orb of health
        public const Rarity ORB_OF_HEALTH_RARITY = Rarity.UNCOMMON;
        public const string ORB_OF_HEALTH_DESCRIPTION = "+20HP";
        public const int ORB_OF_HEALTH_HEALTH = 20;
        //Sword
        public const Rarity SWORD_RARITY = Rarity.COMMON;
        public const string SWORD_DESCRIPTION = "+10% Physical amp";
        public const double SWORD_PHYSICAL_AMP = 0.1;
        //Wand Of green
        public const Rarity WAND_OF_GREEN_RARITY = Rarity.COMMON;
        public const string WAND_OF_GREEN_DESCRIPTION = "+10% Magic amp";
        public const double WAND_OF_GREEN_MAGICAL_AMP = 0.1;
        //Wand Of red
        public const Rarity WAND_OF_RED_RARITY = Rarity.RARE;
        public const string WAND_OF_RED_DESCRIPTION = "+20MP, +0.1MP Regen\n-10HP";
        public const double WAND_OF_RED_HEALTH = -10;
        public const double WAND_OF_RED_MANA = 20;
        public const double WAND_OF_RED_MANAREGEN = 0.1;
        //Wand Of Blue
        public const Rarity WAND_OF_BLUE_RARITY = Rarity.UNCOMMON;
        public const string WAND_OF_BLUE_DESCRIPTION = "+25% Magic amp\n-10MP";
        public const double WAND_OF_BLUE_MAGIC_AMP = 0.25;
        public const double WAND_OF_BLUE_MANA = -10;
        //Helm of Pylonius
        public const Rarity HEML_OF_PYLONIUS_RARITY = Rarity.SUPER_RARE;
        public const string HEML_OF_PYLONIUS_DESCRIPTION = "+5 Block, +0.1HP regen, +1 Speed";
        public const double HEML_OF_PYLONIUS_BLOCK = 5;
        public const double HEML_OF_PYLONIUS_HP_REGEN = 0.1;
        public const double HEML_OF_PYLONIUS_SPEED = 1;
    }
}
