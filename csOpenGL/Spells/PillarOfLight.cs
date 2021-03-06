﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class PillarOfLight : Spell
    {
        public PillarOfLight() :
            base(
                Spells.PILLAR_OF_LIGHT_MANA,
                Spells.PILLAR_OF_LIGHT_DAMAGE,
                Spells.PILLAR_OF_LIGHT_COOLDOWN,
                Spells.PILLAR_OF_LIGHT_NAME,
                Spells.PILLAR_OF_LIGHT_DESCRIPTION,
                new List<Effect> { },
                Spells.PILLAR_OF_LIGHT_AOE,
                new Sprite(40, 40, 0, Window.texs[16]),
                new Animation(0, 0, 30),
                SpellType.AOE,
                20,
                100,
                0,
                0,
                1000,
                3)
        {
        }
    }
}
