using System;
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
                new List<SpellEffect> { },
                Spells.PILLAR_OF_LIGHT_AOE,
                new Sprite(40, 40, 0, Window.texs[1]),
                new Animation(0, 3, 30))
        {
        }
    }
}
