using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public static class Spells
    {

        //Fireball
        public const double FIREBALL_MANA = 15;
        public const double FIREBALL_DAMAGE = 20;
        public const double FIREBALL_COOLDOWN = 4 * 60;
        public const string FIREBALL_NAME = "Fireball";
        public const string FIREBALL_DESCRIPTION = "Fiery ball of death and destruction";
        public const double FIREBALL_AOE = 150;

        //Slowness
        public const double SLOWNESS_MANA = 30;
        public const double SLOWNESS_DAMAGE = 0;
        public const double SLOWNESS_COOLDOWN = 10 * 60;
        public const string SLOWNESS_NAME = "Slowness";
        public const string SLOWNESS_DESCRIPTION = "Slowing cloud";
        public const double SLOWNESS_AOE = 150;
        public const double SLOWNESS_DURATION = 1 * 60;
        public const double SLOWNESS_MAGNITUDE = 0.20;

        //Pillar Of Light
        public const double PILLAR_OF_LIGHT_MANA = 10;
        public const double PILLAR_OF_LIGHT_DAMAGE = 30;
        public const double PILLAR_OF_LIGHT_COOLDOWN = 3 * 60;
        public const string PILLAR_OF_LIGHT_NAME = "Pillar of light";
        public const string PILLAR_OF_LIGHT_DESCRIPTION = "The heavens opening the gate allowing light to flood out. Or someone just shines their lamp directly in your eye.";
        public const double PILLAR_OF_LIGHT_AOE = 75;

        //Disable
        public const double DISABLE_MANA = 50;
        public const double DISABLE_DAMAGE = 0;
        public const double DISABLE_COOLDOWN = 20 * 60;
        public const double DISABLE_DURATION = 3 * 60;
        public const string DISABLE_NAME = "Disable";
        public const string DISABLE_DESCRIPTION = "Disable entities";
        public const double DISABLE_AOE = 100;

        //Shield
        public const double SHIELD_MANA = 15;
        public const double SHIELD_DAMAGE = 0;
        public const double SHIELD_COOLDOWN = 10 * 60;
        public const double SHIELD_DURATION = 5 * 60;
        public const double SHIELD_BLOCK = 50;
        public const string SHIELD_NAME = "Shield";
        public const string SHIELD_DESCRIPTION = "Gives you a temporary shield";
        public const double SHIELD_AOE = 0;


        //Banishment
        public const double BANISHMENT_MANA = 30;
        public const double BANISHMENT_DAMAGE = 999999;
        public const double BANISHMENT_COOLDOWN = 15 * 60;
        public const string BANISHMENT_NAME = "Banishment";
        public const string BANISHMENT_DESCRIPTION = "Kills a target enemy";
        public const double BANISHMENT_AOE = 0;


        //Ray Of Frost
        public const double RAY_OF_FROST_MANA = 5;
        public const double RAY_OF_FROST_DAMAGE = 5;
        public const double RAY_OF_FROST_COOLDOWN = 2 * 60;
        public const string RAY_OF_FROST_NAME = "Ray of Frost";
        public const string RAY_OF_FROST_DESCRIPTION = "Ich benutze frost strahle";
        public const double RAY_OF_FROST_AOE = 0;
        public const double RAY_OF_FROST_SLOW = 0.7;
        public const double RAY_OF_FROST_DURATION = 3*60;
        public const float RAY_OF_FROST_PROJECTILE_SPEED = 10;
        public const int RAY_OF_FROST_PIERCE = 99999;
    }
}
