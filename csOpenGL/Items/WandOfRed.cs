using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class WandOfRed : Item
    {
        public WandOfRed() : base(
            "Wand of red",
            Balance.WAND_OF_RED_RARITY,
            Balance.WAND_OF_RED_DESCRIPTION,
            new Sprite(40, 40, 0, Window.texs[9]),
            new Effect[] {
                new Effect(EffectType.HP, Balance.WAND_OF_RED_HEALTH, -999),
                new Effect(EffectType.MANA, Balance.WAND_OF_RED_MANA, -999),
                new Effect(EffectType.MPREGEN, Balance.WAND_OF_RED_MANAREGEN, -999)
            }
            )
        {

        }
    }
}
