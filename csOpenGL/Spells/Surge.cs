using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Surge : Spell
    {

        public Surge() : base(
            Spells.SURGE_MANA,
            Spells.SURGE_DAMAGE,
            Spells.SURGE_COOLDOWN,
            Spells.SURGE_NAME,
            Spells.SURGE_DESCRIPTION,
            new List<Effect> { new Effect(EffectType.SPEED, Spells.SURGE_SPEED, Spells.SURGE_DURATION) },
            Spells.SHIELD_AOE,
            new Sprite(40, 40, 0, Window.texs[28]),
            new Animation(0, 0, 30),
            SpellType.SELF_TARGET,
                21,
                20,
                0,
                0,
                100,
                10,
                1f,
                1f,
                0.75f)
        {
        }

    }
}
