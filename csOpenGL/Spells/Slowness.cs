using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Slowness : Spell
    {
        public Slowness() :
            base(
                Spells.SLOWNESS_MANA,
                Spells.SLOWNESS_DAMAGE,
                Spells.SLOWNESS_COOLDOWN,
                Spells.SLOWNESS_NAME,
                Spells.SLOWNESS_DESCRIPTION,
                new List<Effect> { new Effect(EffectType.SPEED, Spells.SLOWNESS_MAGNITUDE, Spells.SLOWNESS_DURATION)},
                Spells.SLOWNESS_AOE,
                new Sprite(40, 40, 0, Window.texs[14]),
                new Animation(0, 0, 30))
        {
        }
    }
}
