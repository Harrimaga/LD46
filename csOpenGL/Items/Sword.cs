using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Sword : Item
    {
        public Sword() : base(
            "Sword", 
            Balance.SWORD_RARITY, 
            Balance.SWORD_DESCRIPTION, 
            new Sprite(40,40,0,Window.texs[4]),
            new Effect[] { new Effect(EffectType.PHYSICAL_DAMAGE, 0.1, -999) }
            )
    }
}
