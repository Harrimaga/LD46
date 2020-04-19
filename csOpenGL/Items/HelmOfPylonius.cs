using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class HelmOfPylonius : Item
    {

        public HelmOfPylonius() : base(
            "Helm of Pylonius",
            Balance.HEML_OF_PYLONIUS_RARITY,
            Balance.HEML_OF_PYLONIUS_DESCRIPTION,
            new Sprite(40, 40, 0, Window.texs[11]),
            new Effect[] {
                new Effect(EffectType.BLOCK, Balance.HEML_OF_PYLONIUS_BLOCK, -999),
                new Effect(EffectType.HPREGEN, Balance.HEML_OF_PYLONIUS_HP_REGEN, -999),
                new Effect(EffectType.SPEED, Balance.HEML_OF_PYLONIUS_SPEED, -999)
            }
            )
        {

        }

    }
}
