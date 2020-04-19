using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class OrbOfHealth : Item
    {
        public OrbOfHealth() : base(
            "Orb Of Health",
            Balance.ORB_OF_HEALTH_RARITY,
            Balance.ORB_OF_HEALTH_DESCRIPTION,
            new Sprite(40, 40, 0, Window.texs[0]),
            new Effect[] { new Effect(EffectType.HP, Balance.ORB_OF_HEALTH_HEALTH, -999) }
            )
        {

        }
    }
}
