using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class WandOfBlue : Item
    {
        public WandOfBlue() : base(
            "Wand of Blue",
            Balance.WAND_OF_BLUE_RARITY,
            Balance.WAND_OF_BLUE_DESCRIPTION,
            new Sprite(40, 40, 0, Window.texs[10]),
            new Effect[] {
                new Effect(EffectType.MAGICAL_DAMAGE, Balance.WAND_OF_BLUE_MAGIC_AMP, -999),
                new Effect(EffectType.MANA, Balance.WAND_OF_BLUE_MANA, -999),
            }
            )
        {

        }
    }
}
