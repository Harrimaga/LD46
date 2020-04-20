using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class HealthPotion : Item
    {

        public HealthPotion() : base(
            "Health potion",
            Balance.HEALTH_POTION_RARITY,
            Balance.HEALTH_POTION_DESCRIPTION,
            new Sprite(40, 40, 0, Window.texs[27]),
            new Effect[] { }
            )
        {

        }

        public override bool UseItem(float x, float y, IEnumerable<Entity> possibleTargets, Entity caster)
        {
            caster.Heal(Balance.HEALTH_POTION_HEALTH);
            return true;
        }

    }
}
