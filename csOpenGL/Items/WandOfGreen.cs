using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class WandOfGreen : Item
    {
        public WandOfGreen() : base(
            "Wand of green",
            Balance.WAND_OF_GREEN_RARITY,
            Balance.WAND_OF_GREEN_DESCRIPTION,
            new Sprite(40, 40, 0, Window.texs[11]),
            new Effect[] { new Effect(EffectType.MAGICAL_DAMAGE, Balance.WAND_OF_GREEN_MAGICAL_AMP, -999) }
            )
        {

        }
    }
}
