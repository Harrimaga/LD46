using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class OrbOfMana : Item
    {

        public OrbOfMana() : base(
            "Orb Of Mana",
            Balance.ORB_OF_MANA_RARITY,
            Balance.ORB_OF_MANA_DESCRIPTION,
            new Sprite(40, 40, 0, Window.texs[32]),
            new Effect[] {
                new Effect(EffectType.MANA, Balance.ORB_OF_MANA_MANA, -999),
                new Effect(EffectType.MPREGEN, Balance.ORB_OF_MANA_MANA_REGEN, -999) }
            )
        {

        }

    }
}
