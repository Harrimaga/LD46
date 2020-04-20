using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Shield : Spell
    {

        public Shield() : base(
            Spells.SHIELD_MANA,
            Spells.SHIELD_DAMAGE,
            Spells.SHIELD_COOLDOWN,
            Spells.SHIELD_NAME,
            Spells.SHIELD_DESCRIPTION,
            new List<Effect> { new Effect(EffectType.BLOCK, Spells.SHIELD_BLOCK, Spells.SHIELD_DURATION) },
            Spells.SHIELD_AOE,
            new Sprite(40, 40, 0, Window.texs[29]),
            new Animation(0, 0, 30),
            SpellType.SELF_TARGET,
                21,
                20,
                0,
                0,
                100,
                10,
                0.7f,
                0.7f,
                0.75f)
        {
        }
    }
}
