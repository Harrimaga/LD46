using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class ManaPotion : Item
    {

        public ManaPotion() : base(
            "Mana potion",
            Balance.MANA_POTION_RARITY,
            Balance.MANA_POTION_DESCRIPTION,
            new Sprite(40, 40, 0, Window.texs[0]),
            new Effect[] { }
            )
        {

        }

        public override bool UseItem(float x, float y, IEnumerable<Entity> possibleTargets, Entity caster)
        {
            caster.Healmana(Balance.MANA_POTION_MANA);
            return true;
        }

    }
}
