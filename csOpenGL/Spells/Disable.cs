using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Disable : Spell
    {
        public Disable() : base(
            Spells.DISABLE_MANA,
            Spells.DISABLE_DAMAGE,
            Spells.DISABLE_COOLDOWN,
            Spells.DISABLE_NAME,
            Spells.DISABLE_DESCRIPTION,
            new List<Effect> { new Effect(EffectType.STUN, 0, Spells.DISABLE_DURATION) },
            Spells.DISABLE_AOE,
            new Sprite(40, 40, 0, Window.texs[1]),
            new Animation(0, 3, 30))
        {
        }
    }
}
