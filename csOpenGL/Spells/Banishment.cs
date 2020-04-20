using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Banishment : Spell
    {

        public Banishment() : base(
            Spells.BANISHMENT_MANA,
            Spells.BANISHMENT_DAMAGE,
            Spells.BANISHMENT_COOLDOWN,
            Spells.BANISHMENT_NAME,
            Spells.BANISHMENT_DESCRIPTION,
            new List<Effect> {  },
            Spells.BANISHMENT_AOE,
            new Sprite(40, 40, 0, Window.texs[29]),
            new Animation(0, 0, 30),
            SpellType.SINGLE_TARGET,
                20,
                250,
                0,
                0,
                100,
                15,
                0.0f,
                0.0f,
                0.0f)
        {
        }

    }
}
