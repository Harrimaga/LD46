using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Hammer : Item
    {

        public Hammer() : base(
            "Hammer",
            Balance.HAMMER_RARITY,
            Balance.HAMMER_DESCRIPTION,
            new Sprite(40, 40, 0, Window.texs[4]),
            new Effect[] {
                new Effect(EffectType.PHYSICAL_DAMAGE, Balance.HAMMER_PHYSICAL_AMP, -999),
                new Effect(EffectType.SPEED, Balance.HAMMER_SPEED, -999),
                new Effect(EffectType.KNOCKBACK, Balance.HAMMER_KNOCKBACK, -999)
            }
            )
        {

        }

    }
}
