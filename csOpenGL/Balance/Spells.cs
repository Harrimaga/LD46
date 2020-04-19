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
        public const double FIREBALL_AOE = 300;

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
        public const double PILLAR_OF_LIGHT_AOE = 200;
    }
}
